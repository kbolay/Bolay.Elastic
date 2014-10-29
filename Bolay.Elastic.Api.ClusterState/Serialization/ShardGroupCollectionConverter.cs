using Bolay.Elastic.Api.ClusterState.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.ClusterState.Serialization
{
    public class ShardGroupCollectionConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            List<ShardGroup> shardGroups = new List<ShardGroup>();
            Dictionary<string, object> groups = serializer.Deserialize<Dictionary<string, object>>(reader);
            foreach (KeyValuePair<string, object> group in groups)
            {
                ShardGroup shardGroup = JsonConvert.DeserializeObject<ShardGroup>(group.Value.ToString());
                if (shardGroup != null)
                {
                    shardGroup.Id = group.Key;
                    shardGroups.Add(shardGroup);
                }
            }

            if (!shardGroups.Any())
                return null;

            return shardGroups;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
