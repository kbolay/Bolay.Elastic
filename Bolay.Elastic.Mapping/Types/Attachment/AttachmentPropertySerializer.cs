using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Properties.Attachment
{
    internal class AttachmentPropertySerializer : JsonConverter
    {
        private const string _FIELDS = "fields";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> propDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            AttachmentProperty prop = new AttachmentProperty(propDict.First().Key);

            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(propDict.First().Value.ToString());

            DocumentPropertyBase.Deserialize(prop, fieldDict);

            if (fieldDict.ContainsKey(_FIELDS))
                prop.Fields = JsonConvert.DeserializeObject<DocumentPropertyCollection>(fieldDict.GetString(_FIELDS));

            return prop;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is AttachmentProperty))
                throw new SerializeTypeException<AttachmentProperty>();

            AttachmentProperty prop = value as AttachmentProperty;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            DocumentPropertyBase.Serialize(prop, fieldDict);
            if(prop.Fields != null && prop.Fields.Any(x => x != null))
                fieldDict.Add(_FIELDS, new DocumentPropertyCollection(prop.Fields));

            Dictionary<string, object> propDict = new Dictionary<string, object>();
            propDict.Add(prop.Name, fieldDict);

            serializer.Serialize(writer, propDict);
        }
    }
}
