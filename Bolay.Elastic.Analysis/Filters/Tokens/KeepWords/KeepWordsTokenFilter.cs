using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.KeepWords
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-keep-words-tokenfilter.html
    /// </summary>
    [JsonConverter(typeof(KeepWordsTokenFilterSerializer))]
    public class KeepWordsTokenFilter : TokenFilterBase
    {
        internal const bool _LOWERCASE_DEFAULT = false;

        /// <summary>
        /// Gets the list of keep words.
        /// </summary>
        public IEnumerable<string> KeepWords { get; set; }

        /// <summary>
        /// Gets the path of the keep words configuration file.
        /// </summary>
        public string KeepWordsPath { get; set; }

        /// <summary>
        /// Gets or sets whether to lowercase the words.
        /// Defaults to false.
        /// </summary>
        public bool Lowercase { get; set; }

        /// <summary>
        /// Create the keep words token filter.
        /// </summary>
        /// <param name="name">Sets the name of the token filter.</param>
        private KeepWordsTokenFilter(string name) 
            : base(name, TokenFilterTypeEnum.KeepWords) 
        {
            Lowercase = _LOWERCASE_DEFAULT;
        }

        /// <summary>
        /// Create a keep words token filter based on a list of keep words.
        /// </summary>
        /// <param name="name">Sets the name of the token filter.</param>
        /// <param name="keepWords">Sets the list of keep words.</param>
        public KeepWordsTokenFilter(string name, IEnumerable<string> keepWords)
            : this(name)
        { 
            if(keepWords == null || keepWords.All(x => string.IsNullOrWhiteSpace(x)))
            {
                throw new ArgumentNullException("keepWords", "KeepWordsTokenFilter requires at least one keep word in this constructor.");
            }

            KeepWords = keepWords.Where(x => !string.IsNullOrWhiteSpace(x));
        }

        /// <summary>
        /// Creates a keep words token filter based on a path for the keep words configuration file.
        /// </summary>
        /// <param name="name">Sets the name of the token filter.</param>
        /// <param name="keepWordsPath">Sets the path of the keep words configuration file.</param>
        public KeepWordsTokenFilter(string name, string keepWordsPath)
            : this(name)
        { 
            if(string.IsNullOrWhiteSpace(keepWordsPath))
            {
                throw new ArgumentNullException("keepWordsPath", "KeepWordsTokenFilter requires a path value in this constructor.");
            }

            KeepWordsPath = keepWordsPath;
        }
    }
}
