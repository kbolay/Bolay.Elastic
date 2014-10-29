using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Types.Binary
{
    internal class BinaryPropertySerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> propDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            BinaryProperty prop = new BinaryProperty(propDict.First().Key);
            FieldProperty.Deserialize(prop, JsonConvert.DeserializeObject<Dictionary<string, object>>(propDict.First().Value.ToString()));

            return prop;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is BinaryProperty))
                throw new SerializeTypeException<BinaryProperty>();

            BinaryProperty prop = value as BinaryProperty;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            FieldProperty.Serialize(prop, fieldDict);

            Dictionary<string, object> propDict = new Dictionary<string, object>();
            propDict.Add(prop.Name, fieldDict);

            serializer.Serialize(writer, propDict);
        }
    }
}
