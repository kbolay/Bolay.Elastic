using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.Phonetic
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-phonetic-tokenfilter.html
    /// </summary>
    [JsonConverter(typeof(PhoneticTokenFilterSerializer))]
    public class PhoneticTokenFilter : TokenFilterBase
    {
        /// <summary>
        /// Creates a phonetic token filter.
        /// </summary>
        /// <param name="name">Sets the name of the token filter.</param>
        public PhoneticTokenFilter(string name) : base(name, TokenFilterTypeEnum.Phonetic) { }
    }
}
