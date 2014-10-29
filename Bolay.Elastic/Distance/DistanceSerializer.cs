using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Distance
{
    public class DistanceSerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(DistanceValue))
                return true;

            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return new DistanceValue(reader.Value.ToString());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            DistanceValue precision = value as DistanceValue;
            serializer.Serialize(writer, precision.ToString());
        }
    }
}
