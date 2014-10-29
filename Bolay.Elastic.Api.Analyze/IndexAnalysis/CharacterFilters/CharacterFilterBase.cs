using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.CharacterFilters
{
    public abstract class CharacterFilterBase
    {
        [JsonIgnore]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        public CharacterFilterBase(CharacterFilterEnum type)
        {
            Type = type.ToString();
        }
    }
}
