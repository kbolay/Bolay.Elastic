using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.Terms
{
    public class RegexPattern
    {
        /// <summary>
        /// Gets the pattern for the regular expression.
        /// </summary>
        public string Pattern { get; private set; }

        /// <summary>
        /// Gets the flags for the regular expression.
        /// </summary>
        public IEnumerable<RegexFlagEnum> Flags { get; private set; }

        /// <summary>
        /// Create a RegexPattern with only a pattern.
        /// </summary>
        /// <param name="pattern">Sets the pattern for the regular expression.</param>
        public RegexPattern(string pattern)
        {
            if (string.IsNullOrWhiteSpace(pattern))
                throw new ArgumentNullException("pattern", "RegexPattern requires a pattern.");

            Pattern = pattern;
        }

        /// <summary>
        /// Creates the RegexPattern using a pattern and flags.
        /// </summary>
        /// <param name="pattern">Sets the pattern.</param>
        /// <param name="flags">Sets the flags for the regex pattern.</param>
        public RegexPattern(string pattern, IEnumerable<RegexFlagEnum> flags)
            : this(pattern)
        {
            if (flags == null || flags.All(x => x == null))
                throw new ArgumentNullException("flags", "RegexPattern requires flags in this constructor.");

            Flags = flags.Where(x => x != null);
        }
    }
}
