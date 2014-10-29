using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.CompoundWord
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-compound-word-tokenfilter.html
    /// </summary>
    public class HyphenationDecompounderTokenFilter : CompoundWordTokenFilter
    {
        /// <summary>
        /// Create a hyphenation decompounder compound word token filter based on a list of words.
        /// </summary>
        /// <param name="name">Sets the name of the token filter.</param>
        /// <param name="type">Sets the type of the token filter.</param>
        /// <param name="wordList">Sets the word list.</param>
        public HyphenationDecompounderTokenFilter(string name, IEnumerable<string> wordList)
            : base(name, TokenFilterTypeEnum.HyphenationDecompounder, wordList)
        {
        }

        /// <summary>
        /// Creates a hyphenation decompounder compound word token filter based on a path to the configuration word list file.
        /// </summary>
        /// <param name="name">Sets the name of the token filter.</param>
        /// <param name="type">Sets the type of the token filter.</param>
        /// <param name="wordListPath">Sets the path to the word list configuration file.</param>
        public HyphenationDecompounderTokenFilter(string name, string wordListPath)
            : base(name, TokenFilterTypeEnum.HyphenationDecompounder, wordListPath)
        {
        }
    }
}
