using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Explanations
{
    public class Explanation
    {
        [JsonProperty("value")]
        public Double Value { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("details")]
        public IEnumerable<Explanation> Details { get; set; }
    }
}
