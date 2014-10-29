using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Fields._Id
{
    public class DocumentIdentifierSerializer : JsonConverter
    {
        private const string _PATH = "path";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);

            DocumentIdentifier id = new DocumentIdentifier();
            MappingBase.Deserialize(id, fieldDict);
            id.Path = fieldDict.GetStringOrDefault(_PATH);

            return id;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is DocumentIdentifier))
                throw new SerializeTypeException<DocumentIdentifier>();

            DocumentIdentifier id = value as DocumentIdentifier;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            MappingBase.Serialize(id, fieldDict);
            fieldDict.AddObject(_PATH, id.Path);

            serializer.Serialize(writer, fieldDict);
        }
    }
}
