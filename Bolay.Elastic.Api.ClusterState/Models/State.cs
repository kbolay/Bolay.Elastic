using Bolay.Elastic.Api.ClusterState.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.ClusterState.Models
{
    public class State
    {
        [JsonProperty(PropertyName = "cluster_name")]
        public string ClusterName { get; set; }

        [JsonProperty(PropertyName = "master_node")]
        public string MasterNode { get; set; }

        [JsonProperty(PropertyName = "blocks")]
        public Dictionary<string, object> Blocks { get; set; }

        [JsonProperty(PropertyName = "nodes")]
        [JsonConverter(typeof(NodeCollectionConverter))]
        public List<Node> Nodes { get; set; }

        [JsonProperty(PropertyName = "metadata")]
        public MetaData MetaData { get; set; }

        [JsonProperty(PropertyName = "routing_table")]
        [JsonConverter(typeof(RoutingIndexCollectionConverter))]
        public List<RoutingIndex> Indices { get; set; }

        [JsonProperty(PropertyName = "routing_nodes")]
        public RoutingNodes RoutingNodes { get; set; }

        [JsonProperty(PropertyName = "allocations")]
        public List<ShardInstance> Allocations { get; set; }
    }
}
