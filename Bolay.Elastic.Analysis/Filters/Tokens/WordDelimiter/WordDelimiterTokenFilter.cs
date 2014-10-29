using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.WordDelimiter
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-word-delimiter-tokenfilter.html
    /// </summary>
    [JsonConverter(typeof(WordDelimiterTokenFilterSerializer))]
    public class WordDelimiterTokenFilter : TokenFilterBase
    {
        internal const bool _GENERATE_WORD_PARTS_DEFAULT = true;
        internal const bool _GENERATE_NUMBER_PARTS_DEFAULT = true;
        internal const bool _CATENATE_WORDS_DEFAULT = false;
        internal const bool _CATENATE_NUMBERS_DEFAULT = false;
        internal const bool _CATENATE_ALL_DEFAULT = false;
        internal const bool _SPLIT_ON_CASE_CHANGE_DEFAULT = true;
        internal const bool _PRESERVE_ORIGINAL_DEFAULT = false;
        internal const bool _SPLIT_ON_NUMERICS_DEFAULT = true;
        internal const bool _STEM_ENGLISH_POSSESSIVE_DEFAULT = true;

        /// <summary>
        /// Gets or sets whether to generate word parts.
        /// Defaults to true.
        /// </summary>
        public bool GenerateWordParts { get; set; }

        /// <summary>
        /// Gets or sets whether to generate number parts.
        /// Defaults to true.
        /// </summary>
        public bool GenerateNumberParts { get; set; }

        /// <summary>
        /// Gets or sets whether to catenate words.
        /// Defaults to false.
        /// </summary>
        public bool CatenateWords { get; set; }

        /// <summary>
        /// Gets or sets whether to catenate numbers.
        /// Defaults to false.
        /// </summary>
        public bool CatenateNumbers { get; set; }

        /// <summary>
        /// Gets or sets whether to catenate all.
        /// Defaults to false.
        /// </summary>
        public bool CatenateAll { get; set; }

        /// <summary>
        /// Gets or sets whether to split on case change.
        /// Defaults to true.
        /// </summary>
        public bool SplitOnCaseChange { get; set; }

        /// <summary>
        /// Gets or sets whether to preserve the original token.
        /// Defaults to false.
        /// </summary>
        public bool PreserveOriginal { get; set; }

        /// <summary>
        /// Gets or sets whether to split on numeric characters.
        /// Defaults to true.
        /// </summary>
        public bool SplitOnNumerics { get; set; }

        /// <summary>
        /// Gets or sets whether to stem on english possessive.
        /// Defaults to true.
        /// </summary>
        public bool StemEnglishPossessive { get; set; }

        /// <summary>
        /// Gets or sets a list of words protected from being delimited.
        /// </summary>
        public IEnumerable<string> ProtectedWords { get; set; }

        /// <summary>
        /// Gets or sets the path to the configuration file for protected words.
        /// </summary>
        public string ProtectedWordsPath { get; set; }

        // TODO: Turn TypeTable into an object.

        /// <summary>
        /// Gets or sets the value of a type table.
        /// </summary>
        public IEnumerable<string> TypeTable { get; set; }

        /// <summary>
        /// Gets or sets the path to a file containing the type table.
        /// </summary>
        public string TypeTablePath { get; set; }

        /// <summary>
        /// Creates a word delimiter token filter.
        /// </summary>
        /// <param name="name">Sets the name of the token filter.</param>
        public WordDelimiterTokenFilter(string name) 
            : base(name, TokenFilterTypeEnum.WordDelimeter) 
        { 
            GenerateWordParts = _GENERATE_WORD_PARTS_DEFAULT;
            GenerateNumberParts = _GENERATE_NUMBER_PARTS_DEFAULT;
            CatenateWords = _CATENATE_WORDS_DEFAULT;
            CatenateNumbers = _CATENATE_NUMBERS_DEFAULT;
            CatenateAll = _CATENATE_ALL_DEFAULT;
            SplitOnCaseChange = _SPLIT_ON_CASE_CHANGE_DEFAULT;
            PreserveOriginal = _PRESERVE_ORIGINAL_DEFAULT;
            SplitOnNumerics = _SPLIT_ON_NUMERICS_DEFAULT;
            StemEnglishPossessive = _STEM_ENGLISH_POSSESSIVE_DEFAULT;
        }
    }
}
