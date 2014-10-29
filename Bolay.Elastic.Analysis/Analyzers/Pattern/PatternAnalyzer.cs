using Bolay.Elastic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Analyzers.Pattern
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-pattern-analyzer.html
    /// </summary>
    [JsonConverter(typeof(PatternAnalyzerSerializer))]
    public class PatternAnalyzer : AnalyzerBase
    {
        internal const bool _LOWERCASE_DEFAULT = true;
        internal const string _REGEX_PATTERN_DEFAULT = @"\W+";

        /// <summary>
        /// Gets or sets whether to lowercase the terms.
        /// Defaults to true.
        /// </summary>
        public bool Lowercase { get; set; }

        /// <summary>
        /// Gets or sets the regular expression pattern.
        /// Defaults to \W+.
        /// </summary>
        public string Pattern { get; private set; }

        /// <summary>
        /// Gets or sets the regular expression flags.
        /// </summary>
        public IEnumerable<RegexFlagEnum> Flags { get; set; }

        /// <summary>
        /// Gets or sets the stopwords.
        /// </summary>
        public IEnumerable<string> Stopwords { get; set; }

        /// <summary>
        /// Create a pattern analyzer.
        /// </summary>
        /// <param name="name">Sets the name of the analyzer.</param>
        public PatternAnalyzer(string name)
            : base(name, AnalyzerTypeEnum.Pattern)
        {
            Lowercase = _LOWERCASE_DEFAULT;
            Pattern = _REGEX_PATTERN_DEFAULT;
        }

        /// <summary>
        /// Create a pattern analyzer.
        /// </summary>
        /// <param name="name">Sets the name of the analyzer.</param>
        /// <param name="pattern">Sets the regular expression pattern to use.</param>
        public PatternAnalyzer(string name, string pattern)
            : this(name)
        {
            if (string.IsNullOrWhiteSpace(pattern))
                throw new ArgumentNullException("pattern", "PatternAnalyzer requires a pattern in this analyzer.");

            Pattern = pattern;
        }

        /// <summary>
        /// Create a pattern analyzer using a pre-defined pattern.
        /// </summary>
        /// <param name="name">Set the name of the analyzer.</param>
        /// <param name="pattern">Set the pre-defined pattern.</param>
        public PatternAnalyzer(string name, CommonTokenizerPatternEnum pattern)
            : this(name)
        {
            if (pattern == null)
                throw new ArgumentNullException("pattern", "PatternAnalyzer requires a CommonTokenizerPatternEnum in this constructor.");

            Pattern = pattern.ToString();
        }
    }
}
