using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.KeywordMarker
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-keyword-marker-tokenfilter.html
    /// </summary>
    [JsonConverter(typeof(KeywordMarkerTokenFilterSerializer))]
    public class KeywordMarkerTokenFilter : TokenFilterBase
    {
        internal const bool _IGNORE_CASE_DEFAULT = false;

        /// <summary>
        /// Gets or sets the keywords.
        /// </summary>
        public IEnumerable<string> Keywords { get; set; }

        /// <summary>
        /// Gets or sets the path to the keywords configuration file.
        /// </summary>
        public string KeywordsPath { get; set; }

        /// <summary>
        /// Gets or sets whether to ignore case.
        /// </summary>
        public bool IgnoreCase { get; set; }

        /// <summary>
        /// Create a keyword market token filter.
        /// </summary>
        /// <param name="name">Sets the name of the token filter.</param>
        public KeywordMarkerTokenFilter(string name) 
            : base(name, TokenFilterTypeEnum.KeywordMarker) 
        {
            IgnoreCase = _IGNORE_CASE_DEFAULT;
        }
    }
}
