using Linq2ElasticSearch.Mapping;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Linq2ElasticSearch
{
    public class QueryableCollection<T> : IQueryable<T>, IQueryable, IEnumerable<T>, IEnumerable, IOrderedQueryable<T>, IOrderedQueryable 
    {
        public IQueryProviderAdapter Provider { get; private set; }
        public Expression Expression { get; private set; }
        private readonly ModelMapping<T> ModelMapping;

        #region weird stuff
        Expression IQueryable.Expression { get { return Expression; } }

        Type IQueryable.ElementType { get { return typeof(T); } }

        IQueryProvider IQueryable.Provider { get { return Provider; } }
        #endregion

        public QueryableCollection(IQueryProviderAdapter provider) 
        {
            if (provider == null) 
            {
                throw new ArgumentNullException("provider");
            }

            Provider = provider;
            Expression = Expression.Constant(this);
        }

        public QueryableCollection(IQueryProviderAdapter provider, Expression expression)
            : this(provider)
        {
            if (expression == null) 
            {
                throw new ArgumentNullException("expression");
            }

            if (!typeof(IQueryable<T>).IsAssignableFrom(expression.Type)) 
            {
                throw new ArgumentOutOfRangeException("expression");
            }
            Expression = expression;
        }        
 
        public IEnumerator<T> GetEnumerator() 
        {
            return ((IEnumerable<T>)Provider.ExecuteQuery(Expression)).GetEnumerator();
        }
 
        IEnumerator IEnumerable.GetEnumerator() 
        {
            return ((IEnumerable)Provider.ExecuteQuery(Expression)).GetEnumerator();
        }
 
        public override string ToString() 
        {
            return Provider.GetQueryText(Expression);
        }
    }
}
