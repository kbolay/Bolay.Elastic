using Bolay.Elastic.Time;
using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Fields._Timestamp
{
    public class DocumentTimestampSerializer : JsonConverter
    {
        private const string _IS_ENABLED = "enabled";
        private const string _PATH = "path";
        private const string _FORMAT = "format";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            DocumentTimestamp timestamp = new DocumentTimestamp();

            timestamp.IsEnabled = fieldDict.GetBool(_IS_ENABLED, DocumentTimestamp._IS_ENABLED_DEFAULT);
            MappingBase.Deserialize(timestamp, fieldDict);
            timestamp.Path = fieldDict.GetStringOrDefault(_PATH);
            if (fieldDict.ContainsKey(_FORMAT))
            {
                timestamp.Format = new DateFormat(fieldDict.GetString(_FORMAT));
            }

            return timestamp;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is DocumentTimestamp))
                throw new SerializeTypeException<DocumentTimestamp>();

            DocumentTimestamp stamp = value as DocumentTimestamp;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.AddObject(_IS_ENABLED, stamp.IsEnabled, DocumentTimestamp._IS_ENABLED_DEFAULT);
            DocumentTimestamp.Deserialize(stamp, fieldDict);
            fieldDict.AddObject(_PATH, stamp.Path);

            if (stamp.Format != null)
                fieldDict.AddObject(_FORMAT, stamp.Format.Format);

            serializer.Serialize(writer, fieldDict);
        }
    }
}
