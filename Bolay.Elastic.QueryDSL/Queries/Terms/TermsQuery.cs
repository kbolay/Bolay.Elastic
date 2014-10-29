using Bolay.Elastic.QueryDSL.MinimumShouldMatch;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Terms
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-terms-query.html
    /// </summary>
    [JsonConverter(typeof(TermsSerializer))]
    public class TermsQuery : QueryBase
    {
        /// <summary>
        /// The field to search against.
        /// </summary>
        public string Field { get; private set; }

        /// <summary>
        /// The values to match against.
        /// </summary>
        public IEnumerable<object> Values { get; private set; }

        /// <summary>
        /// The minimum number of the provided values that should be found in the field.
        /// </summary>
        public MinimumShouldMatchBase MinimumShouldMatch { get; set; }

        /// <summary>
        /// Create a terms query.
        /// </summary>
        /// <param name="field">The field to search in.</param>
        /// <param name="values">The values to find.</param>
        public TermsQuery(string field, IEnumerable<object> values)
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "TermsQuery requires a field.");
            if (values == null || values.All(x => x == null))
                throw new ArgumentNullException("values", "TermsQuery requires at least one value.");

            Field = field;
            Values = values.Where(x => x != null);
            MinimumShouldMatch = TermsSerializer._MINIMUM_SHOULD_MATCH_DEFAULT;
        }
    }
}
