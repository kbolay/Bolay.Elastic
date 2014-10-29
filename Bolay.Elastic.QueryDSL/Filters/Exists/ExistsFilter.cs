using Bolay.Elastic.QueryDSL.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.Exists
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/query-dsl-exists-filter.html
    /// </summary>
    [JsonConverter(typeof(ExistsSerializer))]
    public class ExistsFilter : IFilter
    {
        public bool Cache
        {
            get
            {
                return true;
            }
            set
            {
                if (!value)
                    throw new ArgumentException("ExistsFilter always caches.");
            }
        }
        public string CacheKey { get; set; }

        /// <summary>
        /// The field to look in.
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// The value to look for.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the filter name.
        /// </summary>
        public string FilterName { get; set; }

        /// <summary>
        /// Create an exists filter.
        /// </summary>
        /// <param name="field">The field to search in.</param>
        /// <param name="value">The value to search for.</param>
        public ExistsFilter(string field, string value)
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "ExistsFilter requires a field.");
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException("value", "ExistsFilter requires a value.");

            Field = field;
            Value = value;
        }
    }
}
