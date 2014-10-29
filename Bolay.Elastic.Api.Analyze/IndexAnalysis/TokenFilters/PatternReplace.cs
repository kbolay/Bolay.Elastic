using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.TokenFilters
{
    public class PatternReplace : TokenFilterBase
    {
        [JsonProperty("pattern")]
        [DefaultValue(default(string))]
        public string RegexPattern { get; set; }

        [JsonProperty("replacement")]
        [DefaultValue(default(string))]
        public string Replacement { get; set; }

        public PatternReplace() : base(TokenFilterEnum.PatternReplace) { }
    }
}
