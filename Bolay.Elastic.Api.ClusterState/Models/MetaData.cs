using Bolay.Elastic.Api.Mapping.Models;
using Bolay.Elastic.Api.ClusterState.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.ClusterState.Models
{
    public class MetaData
    {
        //[JsonProperty(PropertyName="templates")]
        //[JsonConverter(typeof(TemplateCollectionConverter))]
        //public List<DynamicTemplate> Templates { get; set; }

        [JsonProperty(PropertyName="indices")]
        [JsonConverter(typeof(MetaDataIndexCollectionConverter))]
        public List<MetaDataIndex> Indices { get; set; }
    }
}
