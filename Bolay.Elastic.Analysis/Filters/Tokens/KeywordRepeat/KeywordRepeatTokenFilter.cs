using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.KeywordRepeat
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-keyword-repeat-tokenfilter.html
    /// </summary>
    [JsonConverter(typeof(KeywordRepeatTokenFilterSerializer))]
    public class KeywordRepeatTokenFilter : TokenFilterBase
    {
        /// <summary>
        /// Create a keyword repeat token filter.
        /// </summary>
        /// <param name="name">Sets the name of the token filter.</param>
        public KeywordRepeatTokenFilter(string name) : base(name, TokenFilterTypeEnum.KeywordRepeat) { }
    }
}
