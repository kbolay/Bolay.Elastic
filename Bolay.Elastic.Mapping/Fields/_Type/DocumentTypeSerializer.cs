using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Fields._Type
{
    public class DocumentTypeSerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            DocumentType type = new DocumentType();
            MappingBase.Deserialize(type, fieldDict);

            return type;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is DocumentType))
                throw new SerializeTypeException<DocumentType>();

            DocumentType type = value as DocumentType;
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            MappingBase.Serialize(type, fieldDict);

            serializer.Serialize(writer, fieldDict);
        }
    }
}