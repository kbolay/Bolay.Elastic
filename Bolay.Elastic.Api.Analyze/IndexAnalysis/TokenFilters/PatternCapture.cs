using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.TokenFilters
{
    public class PatternCapture : TokenFilterBase
    {
        [JsonProperty("preserve_original")]
        [DefaultValue(default(bool))]
        public bool PreserveOriginal { get; set; }

        [JsonProperty("patterns")]
        [DefaultValue(default(List<string>))]
        public List<string> Patterns { get; set; }

        public PatternCapture() : base(TokenFilterEnum.PatternCapture) { }
    }
}
