using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.TokenFilters
{
    public class CommonGrams : TokenFilterBase
    {
        [JsonProperty("common_words")]
        [DefaultValue(default(List<string>))]
        public List<string> CommonWords { get; set; }

        [JsonProperty("common_words_path")]
        [DefaultValue(default(string))]
        public string CommonWordsPath { get; set; }

        [JsonProperty("ignore_case")]
        [DefaultValue(default(bool))]
        public bool IgnoreCase { get; set; }

        [JsonProperty("query_mode")]
        [DefaultValue(default(bool))]
        public bool QueryMode { get; set; }

        public CommonGrams() : base(TokenFilterEnum.CommonGrams) { }
    }
}
