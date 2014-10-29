using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.TokenFilters
{
    public class StemmerOverride : TokenFilterBase
    {
        [JsonProperty("rules")]
        [DefaultValue(default(List<string>))]
        public List<string> Rules { get; set; }

        [JsonProperty("rules_path")]
        [DefaultValue(default(string))]
        public string RulesPath { get; set; }

        public StemmerOverride() : base(TokenFilterEnum.StemmerOverride) { }
    }
}
