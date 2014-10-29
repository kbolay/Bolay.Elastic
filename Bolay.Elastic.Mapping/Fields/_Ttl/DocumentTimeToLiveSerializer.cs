using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Fields._Ttl
{
    public class DocumentTimeToLiveSerializer : JsonConverter
    {
        private const string _IS_ENABLED = "enabled";
        private const string _DEFAULT = "default";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            DocumentTimeToLive ttl = new DocumentTimeToLive(new Time.TimeValue(fieldDict.GetString(_DEFAULT)));
            MappingBase.Deserialize(ttl, fieldDict);
            ttl.IsEnabled = fieldDict.GetBool(_IS_ENABLED, DocumentTimeToLive._IS_ENABLED_DEFAULT);

            return ttl;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is DocumentTimeToLive))
                throw new SerializeTypeException<DocumentTimeToLive>();

            DocumentTimeToLive ttl = value as DocumentTimeToLive;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.AddObject(_IS_ENABLED, ttl.IsEnabled, DocumentTimeToLive._IS_ENABLED_DEFAULT);
            MappingBase.Serialize(ttl, fieldDict);
            fieldDict.AddObject(_DEFAULT, ttl.DefaultTimeToLive);

            serializer.Serialize(writer, fieldDict);
        }
    }
}
