using Bolay.Elastic.QueryDSL.Regex;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.Regex
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-regexp-filter.html
    /// </summary>
    [JsonConverter(typeof(RegexSerializer))]
    public class RegexFilter : FilterBase
    {
        /// <summary>
        /// The field to search against.
        /// </summary>
        public string Field { get; private set; }

        /// <summary>
        /// The regex pattern to search for.
        /// </summary>
        public string Pattern { get; private set; }

        /// <summary>
        /// Gets or sets the cache name. Only used if cache is true.
        /// </summary>
        public string CacheName { get; set; }

        /// <summary>
        /// The optional operator features to activate with the regex pattern.
        /// </summary>
        public IEnumerable<RegexOperatorEnum> RegexOperatorFlags { get; set; }

        /// <summary>
        /// Create a regexp filter.
        /// </summary>
        /// <param name="field">The field to search against.</param>
        /// <param name="pattern">The regex pattern to search with.</param>
        public RegexFilter(string field, string pattern)
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "RegexFilter requires a field.");
            if(string.IsNullOrWhiteSpace(pattern))
                throw new ArgumentNullException("pattern", "RegexFilter requires a pattern.");

            Field = field;
            Pattern = pattern;
            Cache = RegexSerializer._CACHE_DEFAULT;
        }
    }
}
