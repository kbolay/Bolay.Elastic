using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.CompoundWord
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-compound-word-tokenfilter.html
    /// </summary>
    [JsonConverter(typeof(CompoundWordTokenFilterSerializer))]
    public abstract class CompoundWordTokenFilter : TokenFilterBase
    {
        internal const int _MINIMUM_WORD_SIZE_DEFAULT = 5;
        internal const int _MINIMUM_SUBWORD_SIZE_DEFAULT = 2;
        internal const int _MAXIMUM_SUBWORD_SIZE_DEFAULT = 15;
        internal const bool _ONLY_LONGEST_MATCH_DEFAULT = false;

        private int _MinimumWordSize { get; set; }
        private int _MinimumSubWordSize { get; set; }
        private int _MaximumSubWordSize { get; set; }

        /// <summary>
        /// Gets the words.
        /// </summary>
        public IEnumerable<string> WordList { get; private set; }

        /// <summary>
        /// Gets or sets the word list path.
        /// </summary>
        public string WordListPath { get; private set; }

        /// <summary>
        /// Gets or sets the minimum word size.
        /// Defaults to 5.
        /// </summary>
        public int MinimumWordSize 
        {
            get
            {
                return _MINIMUM_WORD_SIZE_DEFAULT;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("MinimumWordSize", "Must be greater than zero.");

                _MinimumWordSize = value;
            }
        }

        /// <summary>
        /// Gets or sets the minimum sub word size.
        /// Defaults to 2.
        /// </summary>
        public int MinimumSubWordSize
        {
            get
            {
                return _MINIMUM_SUBWORD_SIZE_DEFAULT;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("MinimumSubWordSize", "Must be greater than zero.");

                _MinimumSubWordSize = value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum sub word size.
        /// Defaults to 15.
        /// </summary>
        public int MaximumSubWordSize
        {
            get
            {
                return _MAXIMUM_SUBWORD_SIZE_DEFAULT;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("MaximumSubWordSize", "Must be greater than zero.");

                _MaximumSubWordSize = value;
            }
        }

        /// <summary>
        /// Gets or sets whether only the longest match.
        /// Defaults to false.
        /// </summary>
        public bool OnlyLongestMatch { get; set; }

        private CompoundWordTokenFilter(string name, TokenFilterTypeEnum type) : base(name, type) 
        {
            MinimumWordSize = _MINIMUM_WORD_SIZE_DEFAULT;
            MinimumSubWordSize = _MINIMUM_SUBWORD_SIZE_DEFAULT;
            MaximumSubWordSize = _MAXIMUM_SUBWORD_SIZE_DEFAULT;
            OnlyLongestMatch = _ONLY_LONGEST_MATCH_DEFAULT;
        }

        /// <summary>
        /// Create a compound word token filter based on a list of words.
        /// </summary>
        /// <param name="name">Sets the name of the token filter.</param>
        /// <param name="type">Sets the type of the token filter.</param>
        /// <param name="wordList">Sets the word list.</param>
        public CompoundWordTokenFilter(string name, TokenFilterTypeEnum type, IEnumerable<string> wordList)
            : this(name, type)
        {
            if (wordList == null || wordList.All(x => string.IsNullOrWhiteSpace(x)))
                throw new ArgumentNullException("wordList", "CompoundWordTokenFilter requires a word list in this constructor.");

            WordList = wordList;
        }

        /// <summary>
        /// Creates a compound word token filter based on a path to the configuration word list file.
        /// </summary>
        /// <param name="name">Sets the name of the token filter.</param>
        /// <param name="type">Sets the type of the token filter.</param>
        /// <param name="wordListPath">Sets the path to the word list configuration file.</param>
        public CompoundWordTokenFilter(string name, TokenFilterTypeEnum type, string wordListPath)
            : this(name, type)
        {
            if (string.IsNullOrWhiteSpace(wordListPath))
                throw new ArgumentNullException("wordListPath", "CompoundWordTokenFilter requires a path to the word list file in this constructor.");

            WordListPath = wordListPath;
        }
    }
}
