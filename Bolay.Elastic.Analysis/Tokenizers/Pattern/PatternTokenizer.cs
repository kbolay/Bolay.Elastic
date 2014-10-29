using Bolay.Elastic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Tokenizers.Pattern
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-pattern-tokenizer.html
    /// </summary>
    [JsonConverter(typeof(PatternTokenizerSerializer))]
    public class PatternTokenizer : TokenizerBase
    {
        internal const string _REGEX_PATTERN_DEFAULT = @"\\W+";
        internal const Int64 _GROUP_DEFAULT = -1;

        /// <summary>
        /// Gets the pattern used for the tokenizer.
        /// Defaults to \\W+.
        /// </summary>
        public string Pattern { get; set; }

        /// <summary>
        /// Gets or sets the regular expression flags.
        /// </summary>
        public IEnumerable<RegexFlagEnum> Flags { get; set; }

        /// <summary>
        /// Gets or sets the group to extract into tokens.
        /// Defaults to -1.
        /// </summary>
        public Int64 Group { get; set; }

        /// <summary>
        /// Create a pattern tokenizer.
        /// </summary>
        /// <param name="name">Sets the name of the tokenizer.</param>
        public PatternTokenizer(string name) : base(name, TokenizerTypeEnum.Pattern) 
        {
            Pattern = _REGEX_PATTERN_DEFAULT;
            Group = _GROUP_DEFAULT;
        }
    }
}
