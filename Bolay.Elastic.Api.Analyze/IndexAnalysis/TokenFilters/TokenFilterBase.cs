using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.TokenFilters
{
    public abstract class TokenFilterBase
    {
        [JsonIgnore]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        public TokenFilterBase(TokenFilterEnum type)
        {
            Type = type.ToString();
        }
    }
}
