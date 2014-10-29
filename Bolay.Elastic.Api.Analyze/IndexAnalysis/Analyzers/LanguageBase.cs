using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.Analyzers
{
    public abstract class LanguageBase : AnalyzerBase
    {
        [JsonProperty("stopwords")]
        [DefaultValue(default(List<string>))]
        public List<string> StopWords { get; set; }

        [JsonProperty("stopwords")]
        [DefaultValue(default(string))]
        public List<string> StopWordsPath { get; set; }

        public LanguageBase(AnalyzerEnum type)
            : base(type)
        { }
    }
}
