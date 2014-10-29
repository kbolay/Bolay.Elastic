using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Bolay.Elastic.Linq
{
    public class TypeUtility
    {
        public static Type CreateIEnumerableType(Type type)
        {
            // there are no enumerables in a null or string type
            if (type == null || type == typeof(string))
                return null;

            // if the type is an array return an IEnumerable<type> matching the array
            if (type.IsArray)
                return typeof(IEnumerable<>).MakeGenericType(type.GetElementType());

            //is this an object<T>
            if (type.IsGenericType)
            {
                foreach (Type arg in type.GetGenericArguments())
                {
                    Type collection = typeof(IEnumerable<>).MakeGenericType(arg);
                    if (collection.IsAssignableFrom(type))
                        return collection;
                }
            }

            foreach (Type interfaceType in type.GetInterfaces())
            {
                Type collection = CreateIEnumerableType(interfaceType);
                if (collection != null) 
                    return collection;
            }

            if (type.BaseType != null && type.BaseType != typeof(object))
                return CreateIEnumerableType(type.BaseType);

            return null;
        }

        public static Type CreateSequenceType(Type elementType)
        {
            return typeof(IEnumerable<>).MakeGenericType(elementType);
        }

        public static Type GetElementType(Type type)
        {
            Type collection = CreateIEnumerableType(type);

            if (collection == null)
                return type;

            return collection.GetGenericArguments().First();
        }

        public static bool InheritsNullable(Type type)
        {
            if (type != null && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                return true;

            return false;
        }

        public static bool IsNullAssignable(Type type)
        {
            if (!type.IsValueType || InheritsNullable(type))
                return true;

            return false;
        }

        public static Type GetNonNullableType(Type type)
        {
            if (InheritsNullable(type))
                return type.GetGenericArguments().First();

            return type;
        }

        public static Type GetNullAssignableType(Type type)
        {
            if (!IsNullAssignable(type))
                return typeof(Nullable<>).MakeGenericType(type);

            return type;
        }

        public static ConstantExpression GetNullConstant(Type type)
        {
            return Expression.Constant(null, GetNullAssignableType(type));
        }

        public static Type GetMemberType(MemberInfo memberInfo)
        {
            FieldInfo fieldInfo = memberInfo as FieldInfo;
            if (fieldInfo != null) 
                return fieldInfo.FieldType;

            PropertyInfo propertyInfo = memberInfo as PropertyInfo;
            if (propertyInfo != null) 
                return propertyInfo.PropertyType;

            EventInfo eventInfo = memberInfo as EventInfo;
            if (eventInfo != null) 
                return eventInfo.EventHandlerType;

            MethodInfo methodInfo = memberInfo as MethodInfo;
            if (methodInfo != null) 
                return methodInfo.ReturnType;

            return null;
        }

        public static object GetDefault(Type type)
        {
            if (type.IsValueType || !InheritsNullable(type))
                return Activator.CreateInstance(type);
            return null;
        }

        public static bool IsReadOnly(MemberInfo memberInfo)
        {
            switch (memberInfo.MemberType)
            {
                case MemberTypes.Field:
                    return (((FieldInfo)memberInfo).Attributes & FieldAttributes.InitOnly) != 0;
                case MemberTypes.Property:
                    PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
                    return !propertyInfo.CanWrite || propertyInfo.GetSetMethod() == null;
                default:
                    return true;
            }
        }

        public static bool IsInteger(Type type)
        {
            Type nonNullableType = GetNonNullableType(type);
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.SByte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Byte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return true;
                default:
                    return false;
            }
        }
    }
}
