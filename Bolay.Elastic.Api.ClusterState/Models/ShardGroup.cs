using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.ClusterState.Models
{
    public class ShardGroup
    {
        [JsonIgnore]
        public string Id { get; set; }

        [JsonIgnore]
        public List<ShardInstance> Instances { get; set; }
    }
}
