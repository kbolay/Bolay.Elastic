using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.AsciiFolding
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-asciifolding-tokenfilter.html
    /// </summary>
    [JsonConverter(typeof(AsciiFoldingTokenFilterSerializer))]
    public class AsciiFoldingTokenFilter : TokenFilterBase
    {
        internal const bool _PRESERVE_ORIGINAL_DEFAULT = false;

        /// <summary>
        /// Gets or sets whether to keep the original token after the filter.
        /// </summary>
        public bool PreserveOriginal { get; set; }

        /// <summary>
        /// Creates an ascii folding token filter.
        /// </summary>
        /// <param name="name">Sets the name of the token filter.</param>
        public AsciiFoldingTokenFilter(string name) : base(name, TokenFilterTypeEnum.AsciiFolding) 
        {
            PreserveOriginal = _PRESERVE_ORIGINAL_DEFAULT;
        }
    }
}
