using Bolay.Elastic.Api.Health.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Health.Serialization
{
    public class IndexListConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            List<Index> indexes = new List<Index>();
            Dictionary<string, object> indexKvps = serializer.Deserialize<Dictionary<string, object>>(reader);
            foreach (KeyValuePair<string, object> indexObj in indexKvps)
            {
                Index index = JsonConvert.DeserializeObject<Index>(indexObj.Value.ToString());
                if (index != null)
                {
                    index.Name = indexObj.Key;
                    indexes.Add(index);
                }
            }

            return indexes;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
