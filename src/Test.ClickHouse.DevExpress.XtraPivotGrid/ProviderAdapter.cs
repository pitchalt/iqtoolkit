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
    public class ProviderAdapter : IQueryProvider, IQueryText, IEntityProvider, IQueryExecutorFactory
    {
        IQueryProvider provider;
        private readonly QueryLanguage language;
        private readonly QueryMapping mapping;
        private readonly QueryPolicy policy;
        private readonly Dictionary<MappingEntity, IEntityTable> tables;
        private QueryCache cache;
        private TextWriter log;
        Expression _expression;

        public ProviderAdapter(IQueryable source)
        {
            provider = source.Provider;
        }
        public IQueryable GetQueryableSource
        {
            get 
            {
                return new QueryAdapter<lineorder_flat>(this);
               // return CreateQuery<lineorder_flat>()
              //  return CheckRecordNumber();
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
            if(_expression == null)
            _expression = expression;
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
          //  var orig_query = provider.CreateQuery<S>(expression);
         //   var new_query = orig_query.Count();
           // var count_result = provider.Execute<S>(new_query.)
           var res = provider.Execute<S>(expression);
            return res;
        }

        public object Execute(Expression expression)
        {
            Type elementType = TypeHelper.GetElementType(expression.Type);
            var inst = (IQueryable)Activator.CreateInstance(typeof(QueryAdapter<>).MakeGenericType(elementType), new object[] { this, expression });
            var orig_query = this.CreateQuery<object>(expression);
            // var new_query = orig_query.Count();
            var new_query = TestExpr(orig_query.Expression);
            return provider.Execute(expression);
        }

        static Expression TestExpr(Expression exp)
        {
            //var methods = from m in typeof(Queryable).GetMethods()
            //              where m.Name == "Count"
            //              select m.GetParameters().Select(p => p.ParameterType);

            // .Select(t => t.IsGenericType ? t.GetGenericTypeDefinition() : t).SequenceEqual(paramTypes)
            ///   select m;
            //var genericArguments = exp.Getg

            //    var methods = typeof(Queryable).GetMethods()
            //                  .Where(m => m.IsGenericMethod && m.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(IEnumerable<>));

            //    var meme = methods.ElementAt(0);
            ////    Type tyyyy = meme.ElementAt(0);
            //    var typeCount = typeof(IQueryable<>);

            //var countMethod = typeof(Queryable).GetMethods().Single(m => m.Name == "Count"
            //    && m.GetGenericArguments().Length == 1
            //    && m.MakeGenericMethod(typeof(object)).GetParameters()[0].ParameterType == typeof(Expression<Func<object>>));

            //var par = Expression.Parameter()




            var method = GetMethoDInfo();
             // var orig_query = from 
             //  var new_query = orig_query.Count();

            //     MethodInfo countmethod = typeof(Queryable).GetMethod("Count", BindingFlags.Static | BindingFlags.Public, null, new[] { tyyyy }, null);
            //var ttype = 
            return Expression.Call(exp, method);
        }

        public string GetQueryText(Expression expression)
        {
            throw new NotImplementedException();
        }

        public IEntityTable<TEntity> GetTable<TEntity>(string entityId = null)
        {
            throw new NotImplementedException();
            //IEntityTable table;

            //if (!this.tables.TryGetValue(entity, out table))
            //{
            //    table = this.CreateTable(entity);
            //    this.tables.Add(entity, table);
            //}

            //return table;
        }

        public static MethodInfo GetMethoDInfo()
        {
        var repositoryInterfaceType = typeof(Queryable);
            foreach (var m in repositoryInterfaceType.GetMethods())
            {
                if (m.IsGenericMethodDefinition && m.Name == "Count")
                {
                    var parametres = m.GetParameters();
                    if (parametres.Length == 2)
                    {
                        var firstParamType = parametres[1].ParameterType;
                        var typepepe = parametres[0].GetType();
                        if(firstParamType == typeof(Expression<Func<object, bool>>))
                       // var firstName = parametres[0].Name;
                      //  if (firstName == "source")
                        {
                            return m;
                        }      

                    }
                }

            }
            throw new Exception("lol kek cheburek");
        }

        public static void LoadGeneric<S>(IQueryable<S> queryable)
        {
            var actualType = typeof(S);
            IQueryable actual = queryable;
            
        }

      //  public static IQueryable Load(IQueryable queryable)
        //{ 
       // }

        public IEntityTable GetTable(Type entityType, string entityId = null)
        {
            throw new NotImplementedException();
        }

        public bool CanBeEvaluatedLocally(Expression expression)
        {
            throw new NotImplementedException();
        }

        public bool CanBeParameter(Expression expression)
        {
            Type type = TypeHelper.GetNonNullableType(expression.Type);
            switch (TypeHelper.GetTypeCode(type))
            {
                case TypeCode.Object:
                    if (expression.Type == typeof(Byte[]) ||
                        expression.Type == typeof(Char[]))
                        return true;
                    return false;
                default:
                    return true;
            }
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
    }






}