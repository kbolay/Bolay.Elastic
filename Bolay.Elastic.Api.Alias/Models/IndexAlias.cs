using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Alias.Models
{
    public class IndexAlias
    {
        [JsonIgnore]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "filter")]
        public Dictionary<string, object> Filter { get; set; }

        [JsonProperty(PropertyName = "index_routing")]
        public string IndexRouting { get; set; }

        [JsonProperty(PropertyName = "search_routing")]
        public string SearchRouting { get; set; }
    }
}
