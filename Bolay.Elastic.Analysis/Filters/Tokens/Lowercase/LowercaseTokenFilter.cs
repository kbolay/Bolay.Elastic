using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.Lowercase
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-lowercase-tokenfilter.html
    /// </summary>
    [JsonConverter(typeof(LowercaseTokenFilterSerializer))]
    public class LowercaseTokenFilter : TokenFilterBase
    {
        /// <summary>
        /// Gets or sets the language value.
        /// </summary>
        public LowercaseSupportedLanguageEnum Language { get; set; }

        /// <summary>
        /// Creats a lowercase token filter.
        /// </summary>
        /// <param name="name">Sets the name of the token filter.</param>
        public LowercaseTokenFilter(string name) 
            : base(name, TokenFilterTypeEnum.Lowercase) 
        { }
    }
}
