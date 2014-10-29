using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.Snowball
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-snowball-tokenfilter.html
    /// </summary>
    [JsonConverter(typeof(SnowballTokenFilterSerializer))]
    public class SnowballTokenFilter : TokenFilterBase
    {
        /// <summary>
        /// Gets the language of the snowball token filter.
        /// </summary>
        public SnowballLanguageEnum Language { get; private set; }

        /// <summary>
        /// Create a snowball token filter.
        /// </summary>
        /// <param name="name">Sets the name of the token filter.</param>
        /// <param name="language">Sets the language of the token filter.</param>
        public SnowballTokenFilter(string name, SnowballLanguageEnum language) 
            : base(name, TokenFilterTypeEnum.Snowball) 
        {
            if (language == null)
                throw new ArgumentNullException("language", "SnowballTokenFilter requires a language.");

            Language = language;
        }
    }
}
