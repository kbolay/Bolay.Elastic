using Bolay.Elastic.QueryDSL.Regex;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Regex
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-regexp-query.html
    /// </summary>
    [JsonConverter(typeof(RegexSerializer))]
    public class RegexQuery : QueryBase
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
        /// Boost value for the query.
        /// </summary>
        public Double Boost { get; set; }

        /// <summary>
        /// The optional operator features to activate with the regex pattern.
        /// </summary>
        public IEnumerable<RegexOperatorEnum> RegexOperatorFlags { get; set; }

        /// <summary>
        /// Create a regexp query.
        /// </summary>
        /// <param name="field">The field to search against.</param>
        /// <param name="pattern">The regex pattern to search with.</param>
        public RegexQuery(string field, string pattern)
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "RegexQuery requires a field.");
            if(string.IsNullOrWhiteSpace(pattern))
                throw new ArgumentNullException("pattern", "RegexQuery requires a pattern.");

            Field = field;
            Pattern = pattern;
            Boost = QuerySerializer._BOOST_DEFAULT;
        }
    }
}
