using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.Prefix
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-prefix-filter.html
    /// </summary>
    [JsonConverter(typeof(PrefixSerializer))]
    public class PrefixFilter : FilterBase
    {
        /// <summary>
        /// Gets the field to search in.
        /// </summary>
        public string Field { get; private set; }

        /// <summary>
        /// Gets the prefix value to search for in the field.
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Create a prefix filter.
        /// </summary>
        /// <param name="field">Sets the field to search in.</param>
        /// <param name="value">Sets the prefix value to search for.</param>
        public PrefixFilter(string field, string value)
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "PrefixFilter requires a field to search against.");
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException("value", "PrefixFilter requires a value to search for in the field.");

            Field = field;
            Value = value;
            Cache = PrefixSerializer._CACHE_DEFAULT;
        }
    }
}
