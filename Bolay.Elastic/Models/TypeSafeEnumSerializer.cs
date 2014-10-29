using Bolay.Elastic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Serialization
{
    public class TypeSafeEnumSerializer : JsonConverter
    {
        private static bool _TypeSafeEnumsInitialized { get; set; }

        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(TypeSafeEnumBase<>))
                return true;

            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            //if (!_TypeSafeEnumsInitialized)
            //    InitializeTypeSafeEnums();

            string enumValue = serializer.Deserialize<string>(reader);
            if (string.IsNullOrWhiteSpace(enumValue))
                return null;

            Type typeSafeEnum = typeof(TypeSafeEnumBase<>).MakeGenericType(objectType);
            MethodInfo[] methods = typeSafeEnum.GetMethods();
            foreach (MethodInfo method in methods)
            { 
                if(method.Name.Contains("Find") && method.IsStatic)
                    return method.Invoke(null, new object[] { enumValue });
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //if (!_TypeSafeEnumsInitialized)
            //    InitializeTypeSafeEnums();

            //Type type = value.GetType();
            //if (!type.IsGenericType())
            //    return null;

            //Type genericType = type.GetGenericArguments().First();

            //var typedEnumItem = (value as TypeSafeEnumBase<object>);
            //if (typedEnumItem == null)
            //    return;

            serializer.Serialize(writer, value.ToString());
        }

        //private void InitializeTypeSafeEnums()
        //{
        //    var distance = DistanceUnit.Centimeter;
        //    var dynamic = DynamicSetting.Strict;
        //    var indexoption = IndexOptionSetting.DocumentId;
        //    var index = IndexSetting.Analyzed;
        //    var path = PathSetting.Full;
        //    var posting = PostingSetting.BloomDefault;
        //    var prefix = PrefixTree.GeoHash;
        //    var property = PropertyType.Object;
        //    var similarity = SimilarityAlgorithm.BM25;
        //    var size = SizeUnit.Byte;
        //    var store = StoreSetting.No;
        //    var term = TermVectorSetting.No;
        //    var time = TimeUnit.Days;
        //    _TypeSafeEnumsInitialized = true;
        //}
    }
}
