using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Fields._Index
{
    public class DocumentIndexSerializer : JsonConverter
    {
        private const string _IS_ENABLED = "enabled";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            return new DocumentIndex() 
            { 
                IsEnabled = fieldDict.GetBool(_IS_ENABLED, DocumentIndex._IS_ENABLED_DEFAULT) 
            };
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is DocumentIndex))
                throw new SerializeTypeException<DocumentIndex>();

            DocumentIndex index = value as DocumentIndex;

            if (!index.IsEnabled)
                return;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.AddObject(_IS_ENABLED, index.IsEnabled, DocumentIndex._IS_ENABLED_DEFAULT);

            serializer.Serialize(writer, fieldDict);
        }
    }
}
