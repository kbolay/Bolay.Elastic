using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.ClusterState.Models
{
    public class NodeAttributes
    {
        public bool Data { get; set; }
        public bool Master { get; set; }
        public Dictionary<string, object> Attributes { get; set; }
    }
}
