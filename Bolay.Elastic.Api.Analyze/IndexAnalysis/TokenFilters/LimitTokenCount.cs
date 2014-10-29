using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.TokenFilters
{
    public class LimitTokenCount : TokenFilterBase
    {
        private const int _MAXIMUM_DEFAULT = 1;

        private int? _Maximum { get; set; }

        [JsonProperty("max_token_count")]
        [DefaultValue(_MAXIMUM_DEFAULT)]
        public int Maximum
        {
            get
            {
                if (_Maximum.HasValue)
                    return _Maximum.Value;

                return _MAXIMUM_DEFAULT;
            }
            set
            {
                _Maximum = value;
            }
        }

        [JsonProperty("consume_all_tokens")]
        [DefaultValue(default(bool))]
        public bool ConsumeAllTokens { get; set; }

        public LimitTokenCount() : base(TokenFilterEnum.LimitTokenCount) { }
    }
}
