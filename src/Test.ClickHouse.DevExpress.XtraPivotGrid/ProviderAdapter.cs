using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections;
using System.Linq.Expressions;
using System.Reflection;
using IQToolkit;
using Test;
using IQToolkit.Data.Common;
using IQToolkit.Data;
using IQToolkit.Data.ClickHouse;
using System.Data;
using ClickHouse.Ado;
using static IQToolkit.Data.ClickHouse.ClickHouseQueryProvider;

namespace PivotForm
{
    public class ProviderAdapter : IQueryProvider, IQueryExecutorFactory
    {
        IQueryProvider provider;    
        public ProviderAdapter(IQueryable source)
        {
            provider = source.Provider;
        }
        public IQueryable GetQueryableSource
        {
            get 
            {
                return new QueryAdapter<lineorder_flat>(this);
            }           
        }

        public IQueryable<S> CreateQuery<S>(IQueryable<S> query)
        {
            return new QueryAdapter<S>(this, query.Expression);
        }

        public IQueryable<S> CreateQuery<S>(Expression expression)
        {
            return new QueryAdapter<S>(this, expression);
        }


        public IQueryable CreateQuery(Expression expression)
        {
            Type elementType = TypeHelper.GetElementType(expression.Type);          
            try
            {
                var inst = (IQueryable)Activator.CreateInstance(typeof(QueryAdapter<>).MakeGenericType(elementType), new object[] { this, expression });
                return inst;
            }
            catch (TargetInvocationException tie)
            {
                throw tie.InnerException;
            }
        }

       
        IQueryable<S> IQueryProvider.CreateQuery<S>(Expression expression)
        {
            var query = provider.CreateQuery<S>(expression);
            return new QueryAdapter<S>(this, query.Expression);
        }

        public S Execute<S>(Expression expression)
        {
           var res = provider.Execute<S>(expression);
            return res;
        }

        public object Execute(Expression expression)
        {     
            var orig_query = this.CreateQuery<object>(expression);     
            
            var count = GetCount<object>(orig_query.Expression);
            if (count > 5000)
                throw new Exception("too much");

            return provider.Execute(expression);
        }        

        public static int GetCount<S>(Expression expression)
        {
            var origType = typeof(S);

            MethodCallExpression countExpression;

            var countMethod = typeof(Queryable).GetMethods().Single(m => m.Name == "Count"   
            && m.GetParameters().Count() == 1
                && m.MakeGenericMethod(typeof(object)).GetParameters()[0].ParameterType == typeof(IQueryable<object>));

            var resmeth = countMethod.MakeGenericMethod(new Type[] { origType });
            countExpression = Expression.Call(null, resmeth, new Expression[] { expression});

            var res = Expression.Lambda(countExpression).Compile().DynamicInvoke();      

            return (int)res;
        }

        public QueryExecutor CreateExecutor()
        {
            return new ExecutorAdapter((ClickHouseQueryProvider)provider);
        }


        new class ExecutorAdapter : DbEntityProvider.Executor
        {
            ClickHouseQueryProvider provider;

            public ExecutorAdapter(ClickHouseQueryProvider provider)
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
    }






}