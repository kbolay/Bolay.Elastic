using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Fields._Parent
{
    public class ParentTypeSerializer : JsonConverter
    {
        private const string _TYPE = "type";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            return new ParentType(fieldDict.GetString(_TYPE));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is ParentType))
                throw new SerializeTypeException<ParentType>();

            ParentType parent = value as ParentType;
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.AddObject(_TYPE, parent.Type);

            serializer.Serialize(writer, fieldDict);
        }
    }
}
