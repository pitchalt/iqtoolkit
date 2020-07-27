using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using IQToolkit;

namespace PivotForm
{
    public class QueryAdapter<T> : IQueryable<T>, IQueryable, IEnumerable<T>, IEnumerable, IOrderedQueryable<T>, IOrderedQueryable
    {
        private readonly IQueryProvider provider;
        private readonly Expression expression;

        public QueryAdapter(IQueryProvider provider) : this(provider, (Type)null)
        {
        }

        public QueryAdapter(IQueryProvider provider, Type staticType)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("Provider");
            }

            this.provider = provider;
            this.expression = staticType != null ? Expression.Constant(this, staticType) : Expression.Constant(this);
        }

        public QueryAdapter(IQueryProvider provider, Expression expression)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("Provider");
            }

            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            if (!typeof(IQueryable<T>).GetTypeInfo().IsAssignableFrom(expression.Type.GetTypeInfo()))
            {
                throw new ArgumentOutOfRangeException("expression");
            }

            this.provider = provider;
            this.expression = expression;
        }

        public Type ElementType { get { return typeof(T); } }

        public Expression Expression { get { return this.expression; } }

        public IQueryProvider Provider { get { return this.provider; } }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)this.provider.Execute<T>(this.expression)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this.provider.Execute(this.expression)).GetEnumerator();
        }

    }
}
