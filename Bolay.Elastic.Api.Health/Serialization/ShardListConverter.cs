using Bolay.Elastic.Api.Health.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Health.Serialization
{
    public class ShardListConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            List<Shard> shards = new List<Shard>();
            Dictionary<string, object> shardKvps = serializer.Deserialize<Dictionary<string, object>>(reader);
            foreach (KeyValuePair<string, object> shardObj in shardKvps)
            {
                Shard shard = JsonConvert.DeserializeObject<Shard>(shardObj.Value.ToString());
                if (shard != null)
                {
                    shard.Id = shardObj.Key;
                    shards.Add(shard);
                }
            }

            return shards;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
