using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.ClusterState.Models
{
    public class ShardInstance
    {
        //TODO: Create a TypeSafeEnum for this
        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "primary")]
        public bool IsPrimary { get; set; }

        [JsonProperty(PropertyName = "node")]
        public string Node { get; set; }

        [JsonProperty(PropertyName = "relocating_node")]
        public string RelocatingNode { get; set; }

        [JsonProperty(PropertyName = "shard")]
        public Int64 Shard { get; set; }

        [JsonProperty(PropertyName = "index")]
        public string Index { get; set; }
    }
}
