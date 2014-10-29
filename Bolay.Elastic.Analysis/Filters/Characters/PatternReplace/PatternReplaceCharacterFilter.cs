using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Characters.PatternReplace
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-pattern-replace-charfilter.html
    /// </summary>
    [JsonConverter(typeof(PatternReplaceCharacterFilterSerializer))]
    public class PatternReplaceCharacterFilter : CharacterFilterBase
    {
        /// <summary>
        /// Gets the regular expression pattern.
        /// </summary>
        public string Pattern { get; set; }

        /// <summary>
        /// Gets the replacement value.
        /// </summary>
        public string Replacement { get; set; }

        /// <summary>
        /// Create a pattern replace character filter.
        /// </summary>
        /// <param name="name">Sets the name of the character filter.</param>
        public PatternReplaceCharacterFilter(string name, string pattern, string replacement)
            : base(name, CharacterFilterTypeEnum.PatternReplace)
        {
            if (string.IsNullOrWhiteSpace(pattern))
            {
                throw new ArgumentNullException("pattern", "PatternReplaceCharacterFilter requires a pattern.");
            }
            if (string.IsNullOrWhiteSpace(replacement))
            {
                throw new ArgumentNullException("replacement", "PatternReplaceCharacterFilter requires a replacement value.");
            }

            Pattern = pattern;
            Replacement = replacement;
        }
    }
}
