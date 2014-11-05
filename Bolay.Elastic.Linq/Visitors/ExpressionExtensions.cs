using Bolay.Elastic.Linq.Interfaces;
using Bolay.Elastic.Linq.RequestBuilder;
using Bolay.Elastic.Mapping.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Linq.Visitors
{
    public static class ExpressionExtensions
    {
        private const char _METHOD_NAME_DELIMITER = '.';
        private static char[] _METHOD_NAME_DELIMITERS = new char[] { _METHOD_NAME_DELIMITER };

        public static DocumentPropertyBase GetMappingProperty(this Expression expression, MethodCallExpression method, IPropertyMappingResolver propertyMappingResolver, string rootPath)
        {
            DocumentPropertyBase result = null;

            string methodName = method.Arguments[0].ToString().Trim(_METHOD_NAME_DELIMITERS);
            if (method != null && method.Method.Name == WhereVisitor._ANY)
            {
                string propertyPath = methodName + _METHOD_NAME_DELIMITER + expression.ToString().Split(_METHOD_NAME_DELIMITERS)[1];
                result = propertyMappingResolver.Resolve(propertyPath);
            }
            else if (string.IsNullOrEmpty(methodName))
            {
                //result = expression.GetConstantName(propertyMappingResolver);
            }
            else
            {
                string propertyPath = methodName.Insert(0, rootPath + _METHOD_NAME_DELIMITER);
                propertyPath = propertyPath + _METHOD_NAME_DELIMITER + expression.ToString().Split(_METHOD_NAME_DELIMITERS)[1];
                result = propertyMappingResolver.Resolve(propertyPath);
            }

            return result;
        }

        public static DocumentPropertyBase GetMappingProperty(this Expression expression, IPropertyMappingResolver propertyMappingResolver)
        {
            string propertyPath = expression.ToString().Trim(_METHOD_NAME_DELIMITERS);
            return propertyMappingResolver.Resolve(propertyPath);
        }

        public static object GetMemberValue(this MemberExpression memberExpression)
        {
            object result = null;

            if (memberExpression.Expression is System.Linq.Expressions.ConstantExpression && memberExpression.Member is System.Reflection.FieldInfo)
            {
                ConstantExpression constantExpression = memberExpression.Expression as System.Linq.Expressions.ConstantExpression;
                FieldInfo fieldInfo = memberExpression.Member as System.Reflection.FieldInfo;

                if(constantExpression != null && fieldInfo != null)
                {
                    result = fieldInfo.GetValue(constantExpression.Value); 
                }                 
            }

            return result;
        }
    }
}
