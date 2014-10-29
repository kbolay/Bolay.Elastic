using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.ClusterState.Models
{
    public class Node
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string TransportAddress { get; set; }
        public Dictionary<string, object> Attributes { get; set; }
    }
}
