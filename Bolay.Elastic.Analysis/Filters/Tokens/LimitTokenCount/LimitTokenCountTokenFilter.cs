using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.LimitTokenCount
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-limit-token-count-tokenfilter.html
    /// </summary>
    [JsonConverter(typeof(LimitTokenCountTokenFilterSerializer))]
    public class LimitTokenCountTokenFilter : TokenFilterBase
    {
        internal const int _MAXIMUM_TOKEN_COUNT_DEFAULT = 1;
        internal const bool _CONSUME_ALL_TOKENS_DEFAULT = false;

        private int _MaximumTokenCount { get; set; }

        /// <summary>
        /// Gets or sets the maximum token count.
        /// Defaults to 1.
        /// </summary>
        public int MaximumTokenCount
        {
            get { return _MaximumTokenCount; }
            set 
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("MaximumTokenCount", "MaximumTokenCount must be greater than zero.");
                }

                _MaximumTokenCount = value;
            }
        }

        /// <summary>
        /// Gets or set whether to consume all tokens, even after passing the maximum.
        /// Defaults to false.
        /// </summary>
        public bool ConsumeAllTokens { get; set; }

        /// <summary>
        /// Create a limit token count token filter.
        /// </summary>
        /// <param name="name">Sets the name of the token filter.</param>
        public LimitTokenCountTokenFilter(string name) 
            : base(name, TokenFilterTypeEnum.LimitTokenCount) 
        {
            MaximumTokenCount = _MAXIMUM_TOKEN_COUNT_DEFAULT;
            ConsumeAllTokens = _CONSUME_ALL_TOKENS_DEFAULT;
        }
    }
}
