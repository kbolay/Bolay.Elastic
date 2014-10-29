using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.Reverse
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-tokenfilters.html
    /// </summary>
    [JsonConverter(typeof(ReverseTokenFilterSerializer))]
    public class ReverseTokenFilter : TokenFilterBase
    {
        /// <summary>
        /// Create a reverse token filter.
        /// </summary>
        /// <param name="name">Sets the name of the filter token.</param>
        public ReverseTokenFilter(string name) : base(name, TokenFilterTypeEnum.Reverse) { }
    }
}
