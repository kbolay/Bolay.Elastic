using Bolay.Elastic.Api.Alias.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Alias.Models
{
    public class Index
    {
        [JsonIgnore]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "aliases")]
        public List<IndexAlias> Aliases { get; set; }
    }
}
