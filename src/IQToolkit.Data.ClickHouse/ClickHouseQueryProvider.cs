﻿// Copyright (c) Microsoft Corporation.  All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (MS-PL)

using System;
using System.Data;
using System.Data.Common;
using ClickHouse.Ado;

namespace IQToolkit.Data.ClickHouse
{
    using IQToolkit.Data.Common;

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
                if (sqlType == null)
                {
                    sqlType = (SqlQueryType)this.provider.Language.TypeSystem.GetColumnType(parameter.Type);
                }

//                var p = ((ClickHouseCommand)command).Parameters.Add(parameter.Name, ToMySqlDbType(sqlType.SqlType), sqlType.Length);
                var p = (IDbDataParameter)((ClickHouseCommand)command).Parameters.Add(parameter.Name, ((SqlQueryType)sqlType).SqlType.ToDbType(), sqlType.Length);
                
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
