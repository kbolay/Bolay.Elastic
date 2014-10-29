using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.TokenFilters
{
    public class Synonym : TokenFilterBase
    {
        private const bool _EXPAND_DEFAULT = true;

        private bool? _Expand { get; set; }

        [JsonProperty("ignore_case")]
        [DefaultValue(default(bool))]
        public bool IgnoreCase { get; set; }

        [JsonProperty("expand")]
        [DefaultValue(_EXPAND_DEFAULT)]
        public bool Expand
        {
            get
            {
                if (_Expand.HasValue)
                    return _Expand.Value;
                return _EXPAND_DEFAULT;
            }
            set
            {
                _Expand = value;
            }
        }

        [JsonProperty("synonyms")]
        [DefaultValue(default(List<string>))]
        public List<string> Synonyms { get; set; }

        [JsonProperty("synonyms_path")]
        [DefaultValue(default(string))]
        public string SynonymsPath { get; set; }

        public Synonym() : base(TokenFilterEnum.Synonym) { }
    }
}