using Bolay.Elastic.Api.Mapping.Models;
using Bolay.Elastic.Api.Serialization;
using Bolay.Elastic.Api.ClusterState.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using MappingIndex = Bolay.Elastic.Api.Mapping.Models.Index;

namespace Bolay.Elastic.Api.ClusterState.Models
{
    public class MetaDataIndex
    {
        [JsonIgnore]
        public string Name { get; set; }

        [JsonProperty(PropertyName="state")]
        [JsonConverter(typeof(TypeSafeEnumSerializer))]
        public IndexState State { get; set; }

        [JsonProperty(PropertyName="settings")]
        public Dictionary<string, string> Settings { get; set; }

        [JsonProperty(PropertyName="mappings")]
        [JsonConverter(typeof(MetaDataIndexConverter))]
        public List<IndexMapping> Mappings { get; set; }

        [JsonProperty(PropertyName = "aliases")]
        public List<string> Aliases { get; set; }
    }
}
