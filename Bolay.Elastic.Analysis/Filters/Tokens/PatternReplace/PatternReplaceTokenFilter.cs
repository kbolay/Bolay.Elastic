using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.PatternReplace
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-pattern_replace-tokenfilter.html
    /// </summary>
    [JsonConverter(typeof(PatternReplaceTokenFilterSerializer))]
    public class PatternReplaceTokenFilter : TokenFilterBase
    {
        /// <summary>
        /// Gets the pattern.
        /// </summary>
        public string Pattern { get; set; }

        /// <summary>
        /// Gets the replacement value.
        /// </summary>
        public string Replacement { get; set; }

        /// <summary>
        /// Create a pattern replace token filter.
        /// </summary>
        /// <param name="name">Sets the name of the token filter.</param>
        /// <param name="pattern">Sets the pattern to replace.</param>
        /// <param name="replacement">Sets the replacement value.</param>
        public PatternReplaceTokenFilter(string name, string pattern, string replacement) 
            : base(name, TokenFilterTypeEnum.PatternReplace) 
        {
            //if (string.IsNullOrWhiteSpace(pattern))
            //    throw new ArgumentNullException("pattern", "PatternReplaceTokenFilter requires a pattern.");
            //if (string.IsNullOrWhiteSpace(replacement))
            //    throw new ArgumentNullException("replacement", "PatternReplaceTokenFilter requires a replacement.");

            Pattern = pattern;
            Replacement = replacement;
        }
    }
}
