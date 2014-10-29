using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Linq2ElasticSearch
{
    internal static class TypeExtensions
    {
        private readonly static IEnumerable<Type> PrimitiveTypes = new List<Type>
        { 
            typeof(Int16),
            typeof(Int32),
            typeof(Int64),
            typeof(double),
            typeof(float),
            typeof(char),
            typeof(Guid),
            typeof(string),
            typeof(byte),
            typeof(UInt16),
            typeof(UInt32),
            typeof(UInt64),
            typeof(DateTime),
            typeof(bool),
            typeof(decimal)
        };

        private readonly static IEnumerable<Type> IgnoreAttributeTypes = new List<Type>
        {
            typeof(JsonIgnoreAttribute)
        };

        /// <summary>
        /// Get the type of value that can be assigned to a property.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static Type GetValueType(this PropertyInfo property)
        {
            Type result = null;
            if (IsCollection(property.PropertyType))
            {
                Type[] genericArguments = property.PropertyType.GetGenericArguments();
                if (genericArguments != null && genericArguments.Count() == 1)
                {
                    result = genericArguments.First();
                }
                else if (genericArguments == null || !genericArguments.Any())
                {
                    throw new Exception("Collection did not have any generic arguments.");
                }
                else
                {
                    throw new Exception("Collection had more than one generic argument.");
                }
            }
            else
            {
                result = property.PropertyType;
            }

            return result;
        }

        /// <summary>
        /// Find if the type passed in is a "primitive type".
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>bool</returns>
        public static bool IsPrimitiveType(this Type type)
        {
            if (type.IsNullableType())
            {
                type = type.GetNonNullableType();
            }

            if (PrimitiveTypes.Contains(type))
                return true;

            return false;
        }

        public static bool IsPropertyIgnored(this PropertyInfo property)
        {
            bool result = false;

            IEnumerable<Attribute> attributes = property.GetCustomAttributes();
            if (attributes != null && attributes.Any(x => IgnoreAttributeTypes.Contains(x.GetType())))
            {
                result = true;
            }

            return result;
        }

        public static string GetJsonName(this PropertyInfo property)
        {
            string name = property.Name;

            IEnumerable<Attribute> attributes = property.GetCustomAttributes();
            if (attributes != null && attributes.Any(x => x.GetType() == typeof(JsonPropertyAttribute)))
            {
                JsonPropertyAttribute propertyAttr = attributes.First(x => x.GetType() == typeof(JsonPropertyAttribute)) as JsonPropertyAttribute;

                if (!string.IsNullOrWhiteSpace(propertyAttr.PropertyName))
                {
                    name = propertyAttr.PropertyName;
                }
            }

            return name;
        }

        /// <summary>
        /// Get the type of
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        internal static Type GetCollectionGenericType(this Type type)
        {
            Type collectionType = type.FindCollectionType();
            if (collectionType == null)
            {
                return type;
            }
            return collectionType.GetGenericArguments()[0];
        }

        internal static bool IsCollection(this Type type)
        {
            bool result = false;

            string[] stringArray = new string[15];
            if ((type != null || type != typeof(string)) && typeof(IEnumerable<>).IsAssignableFrom(type))
            {
                result = true;
            }

            return result;
        }

        internal static Type GetCollectionType(this Type elementType)
        {
            return typeof(IEnumerable<>).MakeGenericType(elementType);
        }

        internal static bool IsNullableType(this Type type)
        {
            return type != null && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        internal static bool IsNullAssignable(this Type type)
        {
            return !type.IsValueType || type.IsNullableType();
        }

        internal static Type GetNonNullableType(this Type type)
        {
            if (type.IsNullableType())
                return type.GetGenericArguments()[0];
            return type;
        }

        internal static Type GetMemberType(this MemberInfo memberInfo)
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
            return null;
        }

        private static Type FindCollectionType(this Type type)
        {
            if (type == null || type == typeof(string))
                return null;
            if (type.IsArray)
                return typeof(IEnumerable<>).MakeGenericType(type.GetElementType());
            if (type.IsGenericType)
            {
                foreach (Type arg in type.GetGenericArguments())
                {
                    Type ienum = typeof(IEnumerable<>).MakeGenericType(arg);
                    if (ienum.IsAssignableFrom(type))
                    {
                        return ienum;
                    }
                }
            }
            Type[] interfaces = type.GetInterfaces();
            if (interfaces != null && interfaces.Length > 0)
            {
                foreach (Type interfaceType in interfaces)
                {
                    Type collectionType = FindCollectionType(interfaceType);
                    if (collectionType != null) return collectionType;
                }
            }
            if (type.BaseType != null && type.BaseType != typeof(object))
            {
                return FindCollectionType(type.BaseType);
            }
            return null;
        }
    }
}
