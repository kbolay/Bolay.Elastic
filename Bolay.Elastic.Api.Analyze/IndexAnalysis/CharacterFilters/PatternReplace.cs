using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.CharacterFilters
{
    public class PatternReplace : CharacterFilterBase
    {
        [JsonProperty("pattern")]
        [DefaultValue(default(string))]
        public string RegexPattern { get; set; }

        [JsonProperty("replacement")]
        [DefaultValue(default(string))]
        public string Replacement { get; set; }

        public PatternReplace() : base(CharacterFilterEnum.PatternReplace) { }
    }
}
