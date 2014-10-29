using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.Standard
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-standard-tokenfilter.html
    /// </summary>
    [JsonConverter(typeof(StandardTokenFilterSerializer))]
    public class StandardTokenFilter : TokenFilterBase
    {
        /// <summary>
        /// Create a standard token filter.
        /// </summary>
        /// <param name="name">Sets the name of the token filter.</param>
        public StandardTokenFilter(string name) : base(name, TokenFilterTypeEnum.Standard) { }
    }
}
