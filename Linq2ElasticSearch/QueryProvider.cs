using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Linq2ElasticSearch
{
    public class QueryProvider : IQueryProviderAdapter
    {
        public string LastQuery { get; set; }
        private readonly string _ConnectionInfo;
        private readonly IExpressionTranslator _ExpressionTranslator;

        public QueryProvider(string connectionInfo, IExpressionTranslator expressionTranslator)
        {
            _ConnectionInfo = connectionInfo;
            _ExpressionTranslator = expressionTranslator;
        }
 
        IQueryable<T> IQueryProvider.CreateQuery<T>(Expression expression) 
        {
            return new QueryableCollection<T>(this, expression);
        }
 
        IQueryable IQueryProvider.CreateQuery(Expression expression) 
        {
            Type elementType = TypeExtensions.GetCollectionGenericType(expression.Type);
            try {
                return (IQueryable)Activator.CreateInstance(typeof(QueryableCollection<>).MakeGenericType(elementType), new object[] { this, expression });
            }
            catch (TargetInvocationException tie) {
                throw tie.InnerException;
            }
        }
 
        T IQueryProvider.Execute<T>(Expression expression) 
        {
            return (T)this.ExecuteQuery(expression);
        }
 
        object IQueryProvider.Execute(Expression expression) 
        {
            return this.ExecuteQuery(expression);
        }

        public string GetQueryText(Expression expression)
        {
            return _ExpressionTranslator.Translate(expression);
        }

        public object ExecuteQuery(Expression expression)
        {
            // create connection from connection info

            // translate the expression into a string
            string query = _ExpressionTranslator.Translate(expression);

            // execute command


            // read from result.


            // capture last query
            LastQuery = query;

            // return result
            return null;
        }
    }
}
