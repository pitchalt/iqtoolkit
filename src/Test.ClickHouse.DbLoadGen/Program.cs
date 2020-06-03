﻿using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Test.ClickHouse.DbLoadGen {

    public class Property {

        public String CName { get; protected set; }
        public String CType { get; protected set; }

        public String DbName { get; protected set; }
        public String DbType { get; protected set; }
        public Int16 DbSize { get; protected set; }
        public Boolean IsCanNull { get; protected set; }

        public String GetReaderValueExp(Int32 index)
        {
            return (IsCanNull ? ($"reader.IsDBNull({index}) ? " + 
                (CType != "String" ? $"({CType}?)" : String.Empty) + 
                " null : ") : String.Empty) + 
                $"reader.Get{CType}({index})";
        }

        public Property(String cname, String ctype, String dbname, String dbtype, Int16 dbsize, Boolean iscannull) {
            CName = cname;
            CType = ctype;
            DbName = dbname;
            DbType = dbtype;
            DbSize = dbsize;
            IsCanNull = iscannull;
        }

    }

    public class Table {

        public String CName { get; set; }
        public String DbName { get; set; }

        private readonly List<Property> _Properties;
        public IReadOnlyList<Property> Properties {
            get { return _Properties; }
        }

        public Property PropertiesCreate(String cname, String ctype, String dbname, String dbtype, Int16 dbsize, Boolean iscannull) {
            var property = new Property(cname, ctype, dbname, dbtype, dbsize, iscannull);
            _Properties.Add(property);
            return property;
        }

        public Table(String cname, String dbname) {
            CName = cname;
            DbName = dbname;
            _Properties = new List<Property>(16);
        }

    }

    class Program
    {

        static IReadOnlyList<Table> Introspect(SQLiteConnection connection)
        {
            var tables = new List<Table>(32);
            var cmd = connection.CreateCommand();
            cmd.CommandText = "select type, name, sql from sqlite_master";
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (reader.GetString(0) == "table")
                    {
                        var tablename = reader.GetString(1);
                        if (tablename.Contains("sqlite", StringComparison.InvariantCultureIgnoreCase))
                            continue;
                        if (tablename.Contains("XP_PROC", StringComparison.InvariantCultureIgnoreCase))
                            continue;
                        tables.Add(IntrospectTable(tablename, reader.GetString(2)));
                    }
                }

                reader.Close();
            }

            return tables;
        }

        static Regex PropExpr = new Regex(@"^\s*""(.+)""\s+([^\s,]+)(\s+(PRIMARY KEY|NOT NULL|[^\s,]+))*",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        static Table IntrospectTable(String tablename, String sql)
        {
            var cname = GetCName(tablename);
            var table = new Table(cname, tablename);
            foreach (var field in sql.Split('\n'))
            {
                var match = PropExpr.Match(field);
                if (match.Success)
                {
                    var pname = match.Groups["1"].Value;
                    var ptype = match.Groups["2"].Value;
                    var options = match.Groups["4"].Captures;
//                    Console.WriteLine($"{pname} {ptype}");
                    Boolean cannull = true;
                    foreach (Capture option in options)
                    {
                        if (option.Value.Equals("not null", StringComparison.InvariantCultureIgnoreCase))
                            cannull = false;
                    }

                    var dbtype = GetCType(ptype);
                    if (dbtype != null)
                        table.PropertiesCreate(GetCName(pname), dbtype, pname, ptype, 0, cannull);
                }
            }

            return table;
        }

        static String GetCName(String dbname)
        {
            return dbname.Replace(" ", String.Empty).Replace("_", String.Empty);
        }

        static String GetCType(String dbtype)
        {
            dbtype = dbtype.ToLower().Trim();
            switch (dbtype)
            {
                case "real":
                    return "Double";
//                case "blob":
//                    return "Byte[]";
                case "bit":
                    return "Boolean";
                case "datetime":
                    return "DateTime";
//                case "text":
                case "nvarchar":
                case "varchar":
                case "char":
                    return "String";
//                    return "Char";
                case "guid":
                    return "Guid";
                case "numeric":
                    return "Decimal";
                case "integer":
                case "int":
                    return "Int32";
                case "smallint":
                    return "Int16";
                default:
                    Console.WriteLine("Fail type " + dbtype);
                    return null;
            }
        }

        static void GenFile(IndentedTextWriter writer, IEnumerable<Table> tables)
        {
            writer.WriteLine("using System;");
            writer.WriteLine("using System.Collections;");            
            writer.WriteLine("using System.Collections.Generic;");            
            writer.WriteLine("using System.Data;");
            writer.WriteLine();
            writer.WriteLine("namespace Test.ClickHouse.DbLoad {");
            writer.Indent++;
            GenLoader(writer, tables);
            foreach (var table in tables)
            {
                GenTable(writer, table);
                GenTableLoader(writer, table);
            }
            writer.Indent--;
            writer.WriteLine();
            writer.WriteLine("}");

        }
        
        static void GenLoader(IndentedTextWriter writer, IEnumerable<Table> tables)
        {
            writer.WriteLine();
            writer.WriteLine($"public class Northwind {{");
            writer.Indent++;
            writer.WriteLine();
            foreach (var table in tables)
            {
                writer.WriteLine();
                writer.WriteLine($"public {table.CName}List {table.CName}List {{");
                writer.Indent++;
                writer.WriteLine($" get {{ return new {table.CName}List(Connection); }}");
                writer.Indent--;
                writer.WriteLine("}");
            }
            writer.WriteLine();
            writer.WriteLine($"public IDbConnection Connection {{ get; protected set;}}");
            writer.WriteLine();
            writer.WriteLine($"public Northwind(IDbConnection connection) {{");
            writer.Indent++;
            writer.WriteLine("Connection = connection;");
            writer.Indent--;
            writer.WriteLine("}");
            writer.WriteLine();
            writer.WriteLine();
            writer.WriteLine($"public void DoReload() {{");
            foreach (var table in tables)
            {
                writer.Indent++;
                writer.WriteLine($"{table.CName}List.Reload();");
                writer.Indent--;
            }
            writer.WriteLine("}");
            writer.WriteLine();
            writer.Indent--;
            writer.WriteLine("}");
        }

        static void GenTableLoader(IndentedTextWriter writer, Table table)
        {
            writer.WriteLine();
            writer.WriteLine($"public class {table.CName}List: List<{table.CName}> {{");
            writer.Indent++;
            writer.WriteLine();
            writer.WriteLine($"public {table.CName}List() {{ }}");
            writer.WriteLine();
            writer.WriteLine($"public {table.CName}List(IDbConnection connection) {{");
            writer.Indent++;
            writer.WriteLine("var cmd = connection.CreateCommand();");
            writer.WriteLine($"cmd.CommandText = \"select " + 
                             String.Join(",",table.Properties.Select(x => $"[{x.DbName}]"))
                             + $" from [{table.DbName}]\";");
            writer.WriteLine("var reader = cmd.ExecuteReader();");
            writer.WriteLine("while(reader.Read()) {");
            writer.Indent++;
            writer.WriteLine($"Add(new {table.CName}(reader));");
            writer.Indent--;
            writer.WriteLine("}");
            writer.Indent--;
            writer.WriteLine("}");            
            writer.WriteLine();
            writer.WriteLine("public void Reload() {");
            writer.WriteLine( "}");

            writer.WriteLine();
            writer.Indent--;
            writer.WriteLine("}");
        }

        static void GenTable(IndentedTextWriter writer, Table table)
        {
            writer.WriteLine();
            writer.WriteLine($"public class {table.CName}: IEnumerable {{");
            writer.Indent++;
            writer.WriteLine();
            writer.WriteLine($"public const String TableName = \"{table.DbName}\";");
            writer.WriteLine();
            foreach (var property in table.Properties) {
                GenTableProperty(writer, property);
            }
            writer.WriteLine();
            writer.WriteLine($"public {table.CName}() {{ }}");
            writer.WriteLine();
            writer.WriteLine($"public {table.CName}(IDataReader reader) {{");
            writer.Indent++;
            for (int i = 0; i < table.Properties.Count; i++)
            {
                var property = table.Properties[i];
                writer.WriteLine($"{property.CName} = { property.GetReaderValueExp(i) };");
            }
            writer.Indent--;
            writer.WriteLine("}");
            writer.WriteLine();
            writer.WriteLine("public IEnumerator GetEnumerator() {");
            writer.Indent++;
            foreach (var property in table.Properties)
            {
                writer.WriteLine($"yield return {property.CName};");
            }
            writer.Indent--;
            writer.WriteLine("}");
            writer.Indent--;
            writer.WriteLine("}");
        }

        static void GenTableProperty(IndentedTextWriter writer, Property property) {
            writer.WriteLine($"public {property.CType}{(property.CType != "String" && property.IsCanNull? "?": String.Empty)} {property.CName} {{ get; set; }}");
        }

        static void Main(string[] args) {
            Console.WriteLine("Hello World!");
            var dbfilename = Path.GetFullPath("Northwind.db3");
            var path = Path.Combine(
                        Path.GetDirectoryName(
                            Path.GetDirectoryName(
                                        Path.GetDirectoryName(
                                            Path.GetDirectoryName(
                                                Path.GetDirectoryName(typeof(Program).Assembly.Location)))
                                    )) ?? throw new NullReferenceException(), "Test.ClickHouse.DbLoad", "Nortwind.cs");
            using (var connection = new SQLiteConnection($"Data Source={dbfilename};Pooling=True"))
            using(var stream = File.Open(path, FileMode.Create))
            using (var writer = new IndentedTextWriter(new StreamWriter(stream, Encoding.UTF8))){
                connection.Open();
                var objects = Introspect(connection);
                GenFile(writer, objects);
                connection.Close();
                writer.Close();
                stream.Close();
            }

        }

    }

}