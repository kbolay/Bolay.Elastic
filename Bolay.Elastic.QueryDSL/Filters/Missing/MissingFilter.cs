using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.Missing
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-missing-filter.html
    /// </summary>
    [JsonConverter(typeof(MissingSerializer))]
    public class MissingFilter : IFilter
    {
        /// <summary>
        /// Gets the field that may be missing.
        /// </summary>
        public string Field { get; private set; }

        /// <summary>
        /// Gets or sets whether to find documents in which the field does not exist, instead of just not having a value.
        /// </summary>
        public bool Existence { get; set; }

        /// <summary>
        /// Gets or sets whether to find documents in which the field has the null value specified in the mapping.
        /// </summary>
        public bool NullValue { get; set; }

        /// <summary>
        /// The missing filter always caches.
        /// </summary>
        public bool Cache
        {
	        get 
	        {
                return MissingSerializer._CACHE_DEFAULT;
	        }
	        set 
	        {
                throw new ArgumentException("Cache is not a property that can be edited for the missing filter."); 
	        }
        }

        /// <summary>
        /// Gets or sets a specific key to cache the result of this filter in.
        /// </summary>
        public string CacheKey { get; set; }

        /// <summary>
        /// Gets or sets the filter name.
        /// </summary>
        public string FilterName { get; set; }

        public MissingFilter(string field)
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "MissingFilter requires a field.");

            Field = field;
            Existence = MissingSerializer._EXISTENCE_DEFAULT;
            NullValue = MissingSerializer._NULL_VALUE_DEFAULT;
        }
    }
}
