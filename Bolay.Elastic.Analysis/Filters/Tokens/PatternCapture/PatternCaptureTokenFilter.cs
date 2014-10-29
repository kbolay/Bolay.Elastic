using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.PatternCapture
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-pattern-capture-tokenfilter.html
    /// </summary>
    [JsonConverter(typeof(PatternCaptureTokenFilterSerializer))]
    public class PatternCaptureTokenFilter : TokenFilterBase
    {
        internal const bool _PRESERVE_ORIGINAL_DEFAULT = true;

        /// <summary>
        /// Gets or sets the regular expression match group to preserve.
        /// </summary>
        public bool PreserveOriginal { get; set; }

        /// <summary>
        /// Gets or sets the patterns to capture.
        /// </summary>
        public IEnumerable<string> Patterns { get; set; }

        /// <summary>
        /// Creates a pattern capture token filter.
        /// </summary>
        /// <param name="name">Sets the name of the token filter.</param>
        public PatternCaptureTokenFilter(string name, IEnumerable<string> patterns) 
            : base(name, TokenFilterTypeEnum.PatternCapture) 
        {
            if (patterns == null || patterns.All(x => string.IsNullOrWhiteSpace(x)))
                throw new ArgumentNullException("patterns", "PatternCaptureTokenFilter requires at least one pattern.");

            Patterns = patterns.Where(x => !string.IsNullOrWhiteSpace(x));
            PreserveOriginal = _PRESERVE_ORIGINAL_DEFAULT;
        }
    }
}
