using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.TokenFilters
{
    public class StopWord : TokenFilterBase
    {
        private const bool _ENABLE_POSITION_INCREMENTS_DEFAULT = true;
        private const bool _REMOVE_TRAILING_DEFAULT = true;

        private bool? _EnablePositionIncrements { get; set; }
        private bool? _RemoveTrailing { get; set; }

        [JsonProperty("stopwords")]
        [DefaultValue(default(List<string>))]
        public List<string> StopWords { get; set; }

        [JsonProperty("stopwords_path")]
        [DefaultValue(default(string))]
        public List<string> StopWordsPath { get; set; }

        [JsonProperty("enable_position_increments")]
        [DefaultValue(_ENABLE_POSITION_INCREMENTS_DEFAULT)]
        public bool EnablePositionIncrements 
        {
            get
            {
                if (_EnablePositionIncrements.HasValue)
                    return _EnablePositionIncrements.Value;
                return _ENABLE_POSITION_INCREMENTS_DEFAULT;
            }
            set
            {
                _EnablePositionIncrements = value;
            }
        }

        [JsonProperty("ignore_case")]
        [DefaultValue(default(bool))]
        public bool IgnoreCase { get; set; }

        [JsonProperty("remove_trailing")]
        [DefaultValue(_REMOVE_TRAILING_DEFAULT)]
        public bool RemoveTrailing 
        {
            get
            {
                if (_RemoveTrailing.HasValue)
                    return _RemoveTrailing.Value;
                return _REMOVE_TRAILING_DEFAULT;
            }
            set
            {
                _RemoveTrailing = value;
            }
        }

        public StopWord() : base(TokenFilterEnum.StopWord) { }
    }
}
