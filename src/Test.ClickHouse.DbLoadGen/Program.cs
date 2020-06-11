using System;
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

        public Boolean IsPrimaryKey { get; protected set; }

        public String GetReaderValueExp(Int32 index)
        {
            // проверка на булен, потому что в кх нет булена, надо переводить в инт
            // но это надо делать не здесь, а при запихнутии в кх
            return (IsCanNull ? ($"reader.IsDBNull({index}) ? " + 
                (CType != "String" ? $"({CType}?)" : String.Empty) + 
                " null : ") : String.Empty) + //(CType == "Boolean" ? $"reader.GetInt16({index})" :
                $"reader.Get{CType}({index})";
        }

        public Property(String cname, String ctype, String dbname, String dbtype, Int16 dbsize, Boolean iscannull, Boolean isPrimaryKey) {
            CName = cname;
            CType = ctype;
            DbName = dbname;
            DbType = dbtype;
            DbSize = dbsize;
            IsCanNull = iscannull;
            IsPrimaryKey = isPrimaryKey;
        }

    }

    public class Table {

        public String CName { get; set; }
        public String DbName { get; set; }

        private readonly List<Property> _Properties;
        public IReadOnlyList<Property> Properties {
            get { return _Properties; }
        }

        public Property PropertiesCreate(String cname, String ctype, String dbname, String dbtype, Int16 dbsize, Boolean iscannull, Boolean isPrimaryKey) {
            var property = new Property(cname, ctype, dbname, dbtype, dbsize, iscannull, isPrimaryKey);
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

        // Не обрабатывает несколько Primary key
        static Regex PropExpr = new Regex(@"^\s*""(.+)""\s+([^\s,]+)(\s+(PRIMARY KEY|NOT NULL|[^\s,]+))*",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);


        static Regex PrimaryKeyExpr = new Regex(@"^\s*PRIMARY KEY\s*\(\[([a-z]*)\],*\s*\[([a-z]+)\]\)",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        static Table IntrospectTable(String tablename, String sql)
        {
            var cname = GetCName(tablename);
            var table = new Table(cname, tablename);
              if (cname == "CustomerCustomerDemo" || cname == "Product_Category_Map" || cname == "EmployeeTerritories")
            //    {
   //        if (cname == "OrderDetails")
                Console.WriteLine(sql);
            //     }

             
            //  Неправильно, создается куча групп для значений, потом переделаю
            List<String> primaryKeys = new List<String>();
            foreach (var field in sql.Split('\n'))
            {

                var matchPrimaryKey = PrimaryKeyExpr.Match(field);
                if (matchPrimaryKey.Success)
                {

                    //var memes = matchPrimaryKey.Groups["1"].Value;

                    foreach (var el in matchPrimaryKey.Groups)
                    {                        
                        primaryKeys.Add(el.ToString());
                    }
                }

            }

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
                    Boolean isPrimaryKey = false;
                    foreach (Capture option in options)
                    {
                        if (option.Value.Equals("not null", StringComparison.InvariantCultureIgnoreCase))
                            cannull = false;
                        if (option.Value.Equals("primary key", StringComparison.InvariantCultureIgnoreCase))
                            isPrimaryKey = true;
                    }

                    if (primaryKeys.Contains(pname))
                        isPrimaryKey = true;

                    var dbtype = GetCType(ptype);
                    if (dbtype != null)
                        table.PropertiesCreate(GetCName(pname), dbtype, pname, ptype, 0, cannull, isPrimaryKey);

                }        

            }

            return table;
        }

   
        static String GetInsertString(Table  table)
        {
            var str = $"insert into Northwind.`{table.DbName}` ({ table.Properties[0].DbName}";

            for (int i = 1; i < table.Properties.Count; i++)
            {
                str += ", " + table.Properties[i].DbName;
            }

            str += ") values @bulk";

            return str;
        }

        static String GetCName(String dbname)
        {
            return dbname.Replace(" ", String.Empty).Replace("_", String.Empty);
        }

        static String GetClickHouseTypeExt(Property property)
        {
            var dbType = GetClickHouseType(property.DbType);
            if (property.IsCanNull)
                return $"Nullable({dbType})";
            else
                return dbType;
                    


        }
            static String GetClickHouseType(String dbtype)
        {
            dbtype = dbtype.ToLower().Trim();
            switch (dbtype)
            {
                case "real":
                    return "Float64";
                //                case "blob":
                //                    return "Byte[]";
                case "bit":
                    return "UInt8";
                case "boolean":
                    return "UInt8";
                case "datetime":
                    return "DateTime";
                //                case "text":
                case "nvarchar":
                    return "String";
                case "varchar":
                    return "String";
                case "char":
                    return "String";
                //                    return "Char";
                case "guid":
                    return "UUID";
                case "numeric":
                    return "Decimal32(4)";
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
               //     return
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
            writer.WriteLine("using ClickHouse.Ado;");
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
            writer.WriteLine($"public void DoReload(ClickHouseConnection clickHouseConnection) {{");

            writer.WriteLine("var cmd = clickHouseConnection.CreateCommand();");
            writer.WriteLine($"cmd.CommandText = \"drop database if exists Northwind\";");
            writer.WriteLine("cmd.ExecuteReader();");
            writer.WriteLine($"cmd.CommandText = \"create database Northwind\";");
            writer.WriteLine("cmd.ExecuteReader();");
            foreach (var table in tables)
            {
                writer.Indent++;
                writer.WriteLine($"var _{table.CName}List = {table.CName}List;");
                writer.WriteLine($"_{table.CName}List.CreateTable(clickHouseConnection);");
                writer.WriteLine($"_{table.CName}List.Reload(clickHouseConnection);");
                writer.Indent--;
            }
            writer.WriteLine("}");
            writer.WriteLine();
            writer.Indent--;
            writer.WriteLine("}");
        }

        static String GetPrimaryKey(Table table)
        {
            String str = "";
            foreach (var el in table.Properties)
            {
                if (el.IsPrimaryKey == true)
                     str += el.DbName + ",";
            }
            if (str.Count() == 0)
                return "ERROR PRIMARY KEY";
                //throw new Exception();
            String resStr = str.Substring(0, str.Count() - 1);
           
            return resStr;
        
        }

        static void GenTableLoader(IndentedTextWriter writer, Table table)
        {
            writer.WriteLine();
            writer.WriteLine($"public class {table.CName}List: List<{table.CName}> {{");
            writer.Indent++;
            writer.WriteLine();

            List<String> stringLens = new List<string>();
            foreach(var el in table.Properties)
                if (el.CType == "String")
                {
                    String str = el.CName ;
                    stringLens.Add(str);
                    writer.WriteLine($"int {str}Len;");
                }

            writer.WriteLine($"public {table.CName}List() {{ }}");
            writer.WriteLine();

            writer.WriteLine($"public {table.CName}List(IDbConnection connection) {{");
            writer.Indent++;

            writer.WriteLine("var cmd = connection.CreateCommand();");
            writer.WriteLine($"cmd.CommandText = \"select " +
                             String.Join(",", table.Properties.Select(x => $"[{x.DbName}]"))
                             + $" from [{table.DbName}]\";");
            writer.WriteLine("var reader = cmd.ExecuteReader();");
            writer.WriteLine("while(reader.Read()) {");
            writer.Indent++;
            writer.WriteLine($"var rec = new {table.CName}(reader);");
            if (stringLens.Count != 0)
            {
                foreach (var el in stringLens)
                {
                    writer.WriteLine($"if ( {el}Len < (rec.{el} ?? String.Empty).Length)");
                    writer.WriteLine($"{el}Len = rec.{el}.Length;");
                }
            }
            writer.WriteLine();
            writer.WriteLine("Add(rec);");

            writer.Indent--;
            writer.WriteLine("}");





            writer.Indent--;
            writer.WriteLine("}");            
            writer.WriteLine();
            writer.WriteLine("public void Reload(ClickHouseConnection clickHouseConnection) {");            
            writer.Indent++;
            writer.WriteLine("if (this.Count == 0) return;");
            writer.WriteLine("var command = clickHouseConnection.CreateCommand();");
            writer.WriteLine($"command.CommandText = \"{GetInsertString(table)}\";");
            writer.WriteLine("command.Parameters.Add(new ClickHouseParameter { ParameterName = \"bulk\", Value = this });");
            writer.WriteLine("command.ExecuteNonQuery();");
            writer.Indent--;

            writer.WriteLine( "}");
            writer.WriteLine();


            writer.WriteLine("public void CreateTable(ClickHouseConnection clickHouseConnection) {");
            writer.Indent++;

            writer.WriteLine("var command = clickHouseConnection.CreateCommand();");
            writer.WriteLine($"command.CommandText = \"create table Northwind.`{table.DbName}` (\"");
            writer.Indent++;

            
            for (int i = 0; i < table.Properties.Count; i++)
            {
                writer.Write($"+ \"{table.Properties[i].DbName} {GetClickHouseTypeExt(table.Properties[i])}");
                if (i != table.Properties.Count - 1)
                    writer.WriteLine(", \"");
                else
                    writer.WriteLine(")\"");
             
            }
            writer.Indent--;
            writer.WriteLine($"+ \"Engine = MergeTree \"");
            writer.WriteLine($"+ \"Order by ({GetPrimaryKey(table)})\";");


            writer.WriteLine("command.ExecuteNonQuery();");

          
            writer.Indent--;
            writer.WriteLine("}");


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

         //  List<Type> typesList = new List<Type>();
            for (int i = 0; i < table.Properties.Count; i++)
            {
                var property = table.Properties[i];
             
                writer.WriteLine($"{property.CName} = { property.GetReaderValueExp(i) };");
                //if (!typesList.Contains(property.GetType()))
                //{
                //    typesList.Add(property.GetType());
                //}
            }
            writer.Indent--;
            writer.WriteLine("}");
            writer.WriteLine();
            writer.WriteLine("public IEnumerator GetEnumerator() {");
            writer.Indent++;
            writer.WriteLine("int i = 0;");
            writer.WriteLine($"foreach (var property in GetItems()) {{");
            writer.Indent++;           
            writer.WriteLine($"if (property != null) yield return property; else yield return GetDefault(GetPropertyType(i));  ");
            writer.WriteLine("i++;");
            writer.Indent--;
            writer.WriteLine("}");
            writer.Indent--;
            writer.WriteLine("}");


            writer.WriteLine($"public IEnumerable GetItems() {{");           
            writer.Indent++;
            foreach (var el in table.Properties)
            {
                if(el.CType == "Boolean")
                {                   
                    writer.WriteLine($" yield return (byte)({el.CName} ? 1 : 0 );");
                }              
                else
                writer.WriteLine($" yield return {el.CName};");
            }
            writer.Indent--;
            writer.WriteLine("}");

           
            writer.Indent++;
            writer.WriteLine($"public Type GetPropertyType(int i) {{");
            writer.WriteLine($"switch (i) {{");
            writer.Indent++;
            for(int i = 0; i < table.Properties.Count; i++)
            {
                writer.WriteLine($"case {i} : return typeof({table.Properties.ElementAt(i).CType});");
            }

            writer.WriteLine($"default: return null;");

            writer.Indent--;
            writer.WriteLine("}");

            writer.Indent--;
            writer.WriteLine("}");


        //    foreach (var el in typesList)
         //   {
                writer.WriteLine($"public object GetDefault(Type t) {{");
                writer.Indent++;
                writer.WriteLine($"return this.GetType().GetMethod(\"GetDefaultGeneric\").MakeGenericMethod(t).Invoke(this,null);");
                writer.Indent--;
                writer.WriteLine("}");
        //    }

            writer.WriteLine($"public T GetDefaultGeneric<T>() {{");
            writer.Indent++;
            writer.WriteLine($"return default(T);");
            writer.Indent--;
            writer.WriteLine("}");





            //foreach (var property in table.Properties)
            //{
            //    writer.WriteLine($"if ({property.CName} != null) return GetItems({property.CName}); else return null; ");
            //    //   writer.WriteLine($" yield return {property.CName};");
            //}
            //writer.Indent--;
            //writer.WriteLine("}");


            //foreach (var el in typesList)
            //{
            //    writer.WriteLine($"public IEnumerator GetItems({el} value) {{");
            //    writer.Indent++;
            //    writer.WriteLine($" yield return value;");
            //    writer.Indent--;
            //    writer.WriteLine("}");
            //}


            writer.Indent--;
            writer.WriteLine("}");



        }

        static void GenTableProperty(IndentedTextWriter writer, Property property) {     ///////////// УБРАЛА CAN NULL а потом вернула и опять убарала и вернула
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
                                    )) ?? throw new NullReferenceException(), "Test.ClickHouse.DbLoad", "Northwind.cs");
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