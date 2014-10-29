using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Types.Numbers.Longs
{
    internal class LongPropertySerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> propDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            LongProperty prop = new LongProperty(propDict.First().Key);
            NumberProperty.DeserializeNumber(prop, JsonConvert.DeserializeObject<Dictionary<string, object>>(propDict.First().Value.ToString()));

            return prop;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is LongProperty))
                throw new SerializeTypeException<LongProperty>();

            LongProperty prop = value as LongProperty;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            NumberProperty.SerializeNumber(prop, fieldDict);

            Dictionary<string, object> propDict = new Dictionary<string, object>();
            propDict.Add(prop.Name, fieldDict);

            serializer.Serialize(writer, propDict);
        }
    }
}
