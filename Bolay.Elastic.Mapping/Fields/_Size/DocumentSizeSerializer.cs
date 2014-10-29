using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Fields._Size
{
    public class DocumentSizeSerializer : JsonConverter
    {
        private const string _IS_ENABLED = "enabled";
        private const string _STORE = "store";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            DocumentSize size = new DocumentSize()
            {
                IsEnabled = fieldDict.GetBool(_IS_ENABLED, DocumentSize._IS_ENABLED_DEFAULT),
                Store = fieldDict.GetBool(_STORE, DocumentSize._STORE_DEFAULT)
            };

            return size;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is DocumentSize))
                throw new SerializeTypeException<DocumentSize>();

            DocumentSize size = value as DocumentSize;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.AddObject(_IS_ENABLED, size.IsEnabled, DocumentSize._IS_ENABLED_DEFAULT);
            fieldDict.AddObject(_STORE, size.Store, DocumentSize._STORE_DEFAULT);

            serializer.Serialize(writer, fieldDict);
        }
    }
}
