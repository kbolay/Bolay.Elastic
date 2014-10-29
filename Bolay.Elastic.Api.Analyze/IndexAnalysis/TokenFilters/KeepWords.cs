using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.TokenFilters
{
    public class KeepWords : TokenFilterBase
    {
        [JsonProperty("keep_words")]
        [DefaultValue(default(List<string>))]
        public List<string> KeepWordsList { get; set; }

        [JsonProperty("keep_words_path")]
        [DefaultValue(default(string))]
        public string KeepWordsPath { get; set; }

        [JsonProperty("keep_words_case")]
        [DefaultValue(default(bool))]
        public bool LowerCaseWords { get; set; }

        public KeepWords() : base(TokenFilterEnum.KeepWords) { }
    }
}
