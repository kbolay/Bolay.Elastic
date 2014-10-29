using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Linq
{
    public class Queryable<T> : IOrderedQueryable<T>
    {
        public Type ElementType { get { return typeof(T); } }
        public Expression Expression { get; set; }
        public IQueryProvider Provider { get; set; }

        public Queryable(IQueryProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");

            Provider = provider;
            Expression = Expression.Constant(this);
        }
        public Queryable(IQueryProvider queryProvider, Expression expression)
        {
            if (queryProvider == null)
                throw new ArgumentNullException("queryProvider");
            if (expression == null)
                throw new ArgumentNullException("expression");
            if (!typeof(IQueryable<T>).IsAssignableFrom(expression.Type))
                throw new ArgumentOutOfRangeException("expression");

            Provider = queryProvider;
            Expression = expression;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)Provider.Execute(Expression)).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}