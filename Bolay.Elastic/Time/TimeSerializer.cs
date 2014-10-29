using Bolay.Elastic.Models;
using Bolay.Elastic.Time;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Time
{
    public class TimeSerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(TimeValue))
                return true;

            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string expiration = serializer.Deserialize<string>(reader);
            return new TimeValue(expiration);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            TimeValue expiration = value as TimeValue;
            serializer.Serialize(writer, expiration.ToString());
        }
    }
}
