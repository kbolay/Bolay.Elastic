using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Fields._Source
{
    public class DocumentSourceSerializer : JsonConverter
    {
        private const string _IS_ENABLED = "enabled";
        private const string _IS_COMPRESSED = "compress";
        private const string _COMPRESSION_THRESHOLD = "compress_threshold";
        private const string _INCLUDES = "includes";
        private const string _EXCLUDES = "excludes";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            DocumentSource source = new DocumentSource();

            source.IsCompressed = fieldDict.GetBool(_IS_COMPRESSED, DocumentSource._IS_COMPRESSED_DEFAULT);
            if (fieldDict.ContainsKey(_COMPRESSION_THRESHOLD))
            {
                source.CompressionThreshold = new CompressionThreshold(fieldDict.GetString(_COMPRESSION_THRESHOLD));
            }
                
            if(fieldDict.ContainsKey(_EXCLUDES))
            {
                source.Excludes = JsonConvert.DeserializeObject<IEnumerable<string>>(fieldDict.GetString(_EXCLUDES));
            }

            if (fieldDict.ContainsKey(_INCLUDES))
            {
                source.Includes = JsonConvert.DeserializeObject<IEnumerable<string>>(fieldDict.GetString(_INCLUDES));
            }

            source.IsEnabled = fieldDict.GetBool(_IS_ENABLED, DocumentSource._IS_ENABLED_DEFAULT);

            return source;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is DocumentSource))
                throw new SerializeTypeException<DocumentSource>();

            DocumentSource source = value as DocumentSource;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.AddObject(_IS_ENABLED, source.IsEnabled, DocumentSource._IS_ENABLED_DEFAULT);
            fieldDict.AddObject(_IS_COMPRESSED, source.IsCompressed, DocumentSource._IS_COMPRESSED_DEFAULT);
            fieldDict.AddObject(_COMPRESSION_THRESHOLD, source.CompressionThreshold);
            fieldDict.AddObject(_INCLUDES, source.Includes);
            fieldDict.AddObject(_EXCLUDES, source.Excludes);

            serializer.Serialize(writer, fieldDict);
        }
    }
}
