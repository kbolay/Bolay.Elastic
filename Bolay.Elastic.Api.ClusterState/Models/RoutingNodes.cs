using Bolay.Elastic.Api.ClusterState.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.ClusterState.Models
{
    public class RoutingNodes
    {
        [JsonProperty(PropertyName = "unassigned")]
        public List<ShardInstance> Unassigned { get; set; }

        [JsonProperty(PropertyName = "nodes")]
        [JsonConverter(typeof(ShardGroupCollectionConverter))]
        public List<ShardGroup> Nodes { get; set; }
    }
}
