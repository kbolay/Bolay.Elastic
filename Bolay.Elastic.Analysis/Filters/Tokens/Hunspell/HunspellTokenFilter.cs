using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.Hunspell
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-hunspell-tokenfilter.html
    /// </summary>
    [JsonConverter(typeof(HunspellTokenFilterSerializer))]
    public class HunspellTokenFilter : TokenFilterBase
    {
        internal const bool _DEDUPLICATE_DEFAULT = true;
        internal const int _RECURSION_LEVEL_DEFAULT = 2;
        internal const bool _IGNORE_CASE_DEFAULT = false;
        internal const bool _STRICT_AFFIX_PARSING_DEFAULT = true;

        private int _RecursionLevel { get; set; }

        /// <summary>
        /// Gets or sets whether to ignore case.
        /// Defaults to false.
        /// </summary>
        public bool IgnoreCase { get; set; }

        /// <summary>
        /// Gets or sets whether errors will cause exceptions to be thrown.
        /// Defaults to true.
        /// </summary>
        public bool StrictAffixParsing { get; set; }

        // TODO: turn locale and language into an object.
        /// <summary>
        /// Gets the locale for the filter.
        /// </summary>
        public string Locale { get; private set; }

        /// <summary>
        /// Gets the language for the filter.
        /// </summary>
        public string Language { get; private set; }

        /// <summary>
        /// Gets or sets the path to the hunspell dictionary configuration file.
        /// </summary>
        public string Dictionary { get; set; }

        /// <summary>
        /// Gets or sets whether to deduplicate the results.
        /// Defaults to true.
        /// </summary>
        public bool Deduplicate { get; set; }

        /// <summary>
        /// Gets or sets the recursion level.
        /// Defaults to 2.
        /// </summary>
        public int RecursionLevel 
        {
            get
            {
                return _RecursionLevel;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("RecursionLevel", "Must be greater than or equal to zero.");
                _RecursionLevel = value;
            }
        }

        /// <summary>
        /// Create a hunspell token filter.
        /// </summary>
        /// <param name="name">Sets the name of the token filter.</param>
        /// <param name="locale">Sets the locale or language value.</param>
        /// <param name="isLocale">Sets whether the locale value is for a locale or language.</param>
        public HunspellTokenFilter(string name, string locale, bool isLocale = true) 
            : base(name, TokenFilterTypeEnum.Hunspell) 
        {
            if (string.IsNullOrWhiteSpace(locale))
            {
                throw new ArgumentNullException("locale", "HunspellTokenFilter requires a locale value.");
            }

            if (isLocale)
                Locale = locale;
            else
                Language = locale;

            RecursionLevel = _RECURSION_LEVEL_DEFAULT;
            Deduplicate = _DEDUPLICATE_DEFAULT;
            IgnoreCase = _IGNORE_CASE_DEFAULT;
            StrictAffixParsing = _STRICT_AFFIX_PARSING_DEFAULT;
        }
    }
}
