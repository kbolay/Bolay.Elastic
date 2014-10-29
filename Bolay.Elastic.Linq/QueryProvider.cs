using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Linq
{
    public class QueryProvider : IQueryProvider
    {
        public IQueryable<T> CreateQuery<T>(Expression expression)
        {
            var query = new Queryable<T>(this, expression);

            return query;            
        }

        public IQueryable CreateQuery(Expression expression)
        {
            Type elementType = TypeUtility.GetElementType(expression.Type);
            try
            {
                return (IQueryable)Activator.CreateInstance(
                        typeof(IQueryable<>).MakeGenericType(elementType),
                        new object[] { this, expression });
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        public TResult Execute<TResult>(Expression expression)
        {
            var value = Execute(expression);
            if (expression is MethodCallExpression)
            {
                MethodCallExpression methodCallExpression = expression as MethodCallExpression;

                return (TResult)methodCallExpression.Method.Invoke(value, new object[] { value });
            }
            // if (expression is Expression.
            return (TResult)value;
        }

        public object Execute(Expression expression)
        {
            throw new NotImplementedException();
        }
    }
}
