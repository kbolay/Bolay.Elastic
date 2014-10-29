using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Fields._Routing
{
    public class DocumentRoutingSerializer : JsonConverter
    {
        private const string _IS_REQUIRED = "required";
        private const string _PATH = "path";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            DocumentRouting routing = new DocumentRouting();
            MappingBase.Deserialize(routing, fieldDict);
            routing.IsRequired = fieldDict.GetBool(_IS_REQUIRED, DocumentRouting._IS_REQUIRED_DEFAULT);
            routing.Path = fieldDict.GetStringOrDefault(_PATH);
            return routing;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is DocumentRouting))
                throw new SerializeTypeException<DocumentRouting>();

            DocumentRouting routing = value as DocumentRouting;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            MappingBase.Serialize(routing, fieldDict);
            fieldDict.AddObject(_IS_REQUIRED, routing.IsRequired, DocumentRouting._IS_REQUIRED_DEFAULT);
            fieldDict.AddObject(_PATH, routing.Path);

            serializer.Serialize(writer, fieldDict);
        }
    }
}
