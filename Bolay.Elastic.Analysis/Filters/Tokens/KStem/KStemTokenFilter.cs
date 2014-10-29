using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.KStem
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-kstem-tokenfilter.html
    /// </summary>
    [JsonConverter(typeof(KStemTokenFilterSerializer))]
    public class KStemTokenFilter : TokenFilterBase
    {
        /// <summary>
        /// Create a kstem token filter.
        /// </summary>
        /// <param name="name">Sets the name of the token filter.</param>
        public KStemTokenFilter(string name) : base(name, TokenFilterTypeEnum.KStem) { }
    }
}
