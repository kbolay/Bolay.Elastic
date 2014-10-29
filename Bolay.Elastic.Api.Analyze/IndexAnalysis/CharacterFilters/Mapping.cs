using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.CharacterFilters
{
    public class Mapping : CharacterFilterBase
    {
        [JsonProperty("mappings")]
        [DefaultValue(default(List<string>))]
        public List<string> Mappings { get; set; }

        [JsonProperty("mappings_path")]
        [DefaultValue(default(string))]
        public string MappingsPath { get; set; }

        public Mapping() : base(CharacterFilterEnum.Mapping) { }
    }
}
