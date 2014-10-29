using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Fields._Source
{
    public class CompressionThresholdSerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(CompressionThreshold))
                return true;

            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return new CompressionThreshold(reader.Value.ToString());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            CompressionThreshold threshold = value as CompressionThreshold;
            serializer.Serialize(writer, threshold.ToString());
        }
    }
}
