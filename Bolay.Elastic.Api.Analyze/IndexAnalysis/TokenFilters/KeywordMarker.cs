using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.TokenFilters
{
    public class KeywordMarker : TokenFilterBase
    {
        [JsonProperty("keywords")]
        [DefaultValue(default(List<string>))]
        public List<string> Keywords { get; set; }

        [JsonProperty("keywords_path")]
        [DefaultValue(default(string))]
        public string KeywordsPath { get; set; }

        [JsonProperty("ignore_case")]
        [DefaultValue(default(bool))]
        public bool IgnoreCase { get; set; }

        public KeywordMarker() : base(TokenFilterEnum.KeywordMarker) { }
    }
}
