using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.Analyzers
{
    public abstract class StemExclusionLanguageBase : LanguageBase
    {
        [JsonProperty("stem_exclusion")]
        [DefaultValue(default(List<string>))]
        public List<string> StemExclusions { get; set; }

        public StemExclusionLanguageBase(AnalyzerEnum type)
            : base(type)
        { }
    }
}
