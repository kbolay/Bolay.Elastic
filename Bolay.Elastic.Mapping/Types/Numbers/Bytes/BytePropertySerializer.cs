using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Types.Numbers.Bytes
{
    internal class BytePropertySerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> propDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            ByteProperty byteProperty = new ByteProperty(propDict.First().Key);
            NumberProperty.DeserializeNumber(byteProperty, JsonConvert.DeserializeObject<Dictionary<string, object>>(propDict.First().Value.ToString()));

            return byteProperty;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is ByteProperty))
                throw new SerializeTypeException<ByteProperty>();

            ByteProperty byteProperty = value as ByteProperty;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            NumberProperty.SerializeNumber(byteProperty, fieldDict);

            Dictionary<string, object> propDict = new Dictionary<string, object>();
            propDict.Add(byteProperty.Name, fieldDict);

            serializer.Serialize(writer, propDict);
        }
    }
}
