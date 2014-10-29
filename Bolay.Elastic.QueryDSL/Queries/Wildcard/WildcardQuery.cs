using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Wildcard
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-wildcard-query.html
    /// </summary>
    [JsonConverter(typeof(WildcardSerializer))]
    public class WildcardQuery : QueryBase
    {
        /// <summary>
        /// The field to search in.
        /// </summary>
        public string Field { get; private set; }

        /// <summary>
        /// The value to search for.
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// The boost of the query.
        /// </summary>
        public Double Boost { get; set; }

        /// <summary>
        /// Create a wildcard query.
        /// </summary>
        /// <param name="field">The field to search against.</param>
        /// <param name="value">The value to search for. May contain wildcards as defined in ES documentation.</param>
        public WildcardQuery(string field, string value)
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "WildcardQuery requires a field.");
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException("value", "WildcardQuery requires a value.");

            Field = field;
            Value = value;
            Boost = QuerySerializer._BOOST_DEFAULT;
        }
    }
}
