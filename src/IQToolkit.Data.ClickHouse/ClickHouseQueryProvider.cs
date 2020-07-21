﻿// Copyright (c) Microsoft Corporation.  All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (MS-PL)

using System;
using System.Data;
using System.Data.Common;
using ClickHouse.Ado;

namespace IQToolkit.Data.ClickHouse
{
    using IQToolkit.Data.Common;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A <see cref="DbEntityProvider"/> for MySql databases
    /// </summary>
    public class ClickHouseQueryProvider : DbEntityProvider
    {
        /// <summary>
        /// Constructs a <see cref="MySqlQueryProvider"/>
        /// </summary>
        public ClickHouseQueryProvider(ClickHouseConnection connection, QueryMapping mapping = null, QueryPolicy policy = null)
            : base(connection, ClickHouseLanguage.Default, mapping, policy)
        {
        }
                

        /// <summary>
        /// Constructs a <see cref="MySqlQueryProvider"/>
        /// </summary>
        public ClickHouseQueryProvider(string connectionString, QueryMapping mapping = null, QueryPolicy policy = null)
            : this(new ClickHouseConnection(connectionString), mapping, policy)
        {
        }

        protected override DbEntityProvider New(IDbConnection connection, QueryMapping mapping, QueryPolicy policy)
        {
            return new ClickHouseQueryProvider((ClickHouseConnection)connection, mapping, policy);
        }

        protected override QueryExecutor CreateExecutor()
        {
            return new Executor(this);
        }

        public class ClickHouseFieldReader : DbFieldReader {

//            private static _BlockGet;
            private ClickHouseDataReader _Reader;

            protected override Type GetFieldType(Int32 ordinal) {
//                _Reader.GetFieldType()
                return base.GetFieldType(ordinal);
            }

            public ClickHouseFieldReader(QueryExecutor executor, IDataReader reader) : base(executor, reader) {
                _Reader = (ClickHouseDataReader)reader; 
                //typeof(ClickHouseDataReader).Pro
            }
        }

        new class Executor : DbEntityProvider.Executor
        {
            ClickHouseQueryProvider provider;

            public Executor(ClickHouseQueryProvider provider)
                : base(provider)
            {
                this.provider = provider;
            }

            protected override bool BufferResultRows
            {
                get { return true; }
            }

            protected override void AddParameter(IDbCommand command, QueryParameter parameter, object value)
            {
                SqlQueryType sqlType = (SqlQueryType)parameter.QueryType;
                IDbDataParameter p;
                if (sqlType == null)
                {
                    sqlType = (SqlQueryType)this.provider.Language.TypeSystem.GetColumnType(parameter.Type);
                }
                if (parameter.Type == typeof(DateTime?))
                {
                    sqlType = (SqlQueryType)this.provider.Language.TypeSystem.GetColumnType(parameter.Type);
                  //  value = ((DateTime)value).ToShortDateString();
                   // var meme = ((DateTime)value).Date;
                  //  var mememem = ((DateTime)value).ToShortDateString();
                    //value = value.ToString("dd/MM/yyy");
                    //value = ((Date)value)

                }
                if (parameter.Type == typeof(List<string>))
                {
                    p = (IDbDataParameter)((ClickHouseCommand)command).Parameters.Add(parameter.Name, value);
                }
                else
                {
                    p = (IDbDataParameter)((ClickHouseCommand)command).Parameters.Add(parameter.Name, ((SqlQueryType)sqlType).SqlType.ToDbType(), sqlType.Length);

                    if (sqlType.Precision != 0)
                    {
                        p.Precision = (byte)sqlType.Precision;
                    }

                    if (sqlType.Scale != 0)
                    {
                        p.Scale = (byte)sqlType.Scale;
                    }

                    p.Value = value ?? DBNull.Value;
                }
                    //                var p = ((ClickHouseCommand)command).Parameters.Add(parameter.Name, ToMySqlDbType(sqlType.SqlType), sqlType.Length);
                    // var p = (IDbDataParameter)((ClickHouseCommand)command).Parameters.Add(parameter.Name, ((SqlQueryType)sqlType).SqlType.ToDbType(), sqlType.Length);

              
            }

            public override IEnumerable<T> Execute<T>(QueryCommand command, Func<FieldReader, T> fnProjector, MappingEntity entity, object[] paramValues)
            {
                return base.Execute(command, fnProjector, entity, paramValues);
            }

            protected override IEnumerable<T> Project<T>(IDataReader reader, Func<FieldReader, T> fnProjector, MappingEntity entity, bool closeReader)
            {
                var freader = new ClickHouseFieldReader(this, reader);
                try
                {
                    do
                    {
                        while (reader.Read())
                        {
                            yield return fnProjector(freader);
                        }

                    }
                    while (reader.NextResult());
                    
                }
                finally
                {
                    if (closeReader)
                    {
                        ((IDataReader)reader).Close();
                    }
                }
            }

            public override IEnumerable<T> ExecuteDeferred<T>(QueryCommand query, Func<FieldReader, T> fnProjector, MappingEntity entity, object[] paramValues)
            {
                return base.ExecuteDeferred(query, fnProjector, entity, paramValues);
            }
                     
        }

        public override object Execute(Expression expression)
        {
            var obj = base.Execute(expression);
            return obj;

        }

        //        public static MySqlDbType ToMySqlDbType(SqlType dbType)
        //        {
        //            switch (dbType)
        //            {
        //                case SqlType.BigInt:
        //                    return MySqlDbType.Int64;
        //                case SqlType.Binary:
        //                    return MySqlDbType.Binary;
        //                case SqlType.Bit:
        //                    return MySqlDbType.Bit;
        //                case SqlType.NChar:
        //                case SqlType.Char:
        //                    return MySqlDbType.Text;
        //                case SqlType.Date:
        //                    return MySqlDbType.Date;
        //                case SqlType.DateTime:
        //                case SqlType.SmallDateTime:
        //                    return MySqlDbType.DateTime;
        //                case SqlType.Decimal:
        //                    return MySqlDbType.Decimal;
        //                case SqlType.Float:
        //                    return MySqlDbType.Float;
        //                case SqlType.Image:
        //                    return MySqlDbType.LongBlob;
        //                case SqlType.Int:
        //                    return MySqlDbType.Int32;
        //                case SqlType.Money:
        //                case SqlType.SmallMoney:
        //                    return MySqlDbType.Decimal;
        //                case SqlType.NVarChar:
        //                case SqlType.VarChar:
        //                    return MySqlDbType.VarChar;
        //                case SqlType.SmallInt:
        //                    return MySqlDbType.Int16;
        //                case SqlType.NText:
        //                case SqlType.Text:
        //                    return MySqlDbType.LongText;
        //                case SqlType.Time:
        //                    return MySqlDbType.Time;
        //                case SqlType.Timestamp:
        //                    return MySqlDbType.Timestamp;
        //                case SqlType.TinyInt:
        //                    return MySqlDbType.Byte;
        //                case SqlType.UniqueIdentifier:
        //                    return MySqlDbType.Guid;
        //                case SqlType.VarBinary:
        //                    return MySqlDbType.VarBinary;
        //                case SqlType.Xml:
        //                    return MySqlDbType.Text;
        //                default:
        //                    throw new NotSupportedException(string.Format("The SQL type '{0}' is not supported", dbType));
        //            }
        //        }
    }
}
