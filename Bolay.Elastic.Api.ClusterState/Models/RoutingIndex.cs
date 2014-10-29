using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.ClusterState.Models
{
    public class RoutingIndex
    {
        [JsonIgnore]
        public string Name { get; set; }

        [JsonProperty(PropertyName="shards")]
        public List<ShardGroup> Shards { get; set; }
    }
}
