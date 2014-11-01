using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Document.Models
{
    public class IndexShards
    {
        [JsonIgnore]
        public string Index { get; set; }
        
        [JsonProperty("_shards")]
        public Shards Shards { get; set; }
    }
}
