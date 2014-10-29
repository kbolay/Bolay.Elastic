using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.Trim
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-trim-tokenfilter.html
    /// </summary>
    [JsonConverter(typeof(TrimTokenFilterSerializer))]
    public class TrimTokenFilter : TokenFilterBase
    {
        /// <summary>
        /// Creates a trim token filter.
        /// </summary>
        /// <param name="name">Sets the name of the token filter.</param>
        public TrimTokenFilter(string name) : base(name, TokenFilterTypeEnum.Trim) { }
    }
}
