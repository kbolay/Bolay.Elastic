using Bolay.Elastic.Analysis.Filters.Tokens.Stemmer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.Stemmer
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-stemmer-tokenfilter.html
    /// </summary>
    [JsonConverter(typeof(StemmerTokenFilterSerializer))]
    public class StemmerTokenFilter : TokenFilterBase
    {
        /// <summary>
        /// Gets the language for the stemmer token filter.
        /// </summary>
        public StemmerLanguageEnum Language { get; private set; }

        /// <summary>
        /// Create the stemmer token filter.
        /// </summary>
        /// <param name="name">Sets the name of the token filter.</param>
        /// <param name="language">Sets the language.</param>
        public StemmerTokenFilter(string name, StemmerLanguageEnum language) 
            : base(name, TokenFilterTypeEnum.Stemmer) 
        {
            if (language == null)
                throw new ArgumentNullException("language", "StemmerTokenFilter requires a language.");

            Language = language;
        }
    }
}
