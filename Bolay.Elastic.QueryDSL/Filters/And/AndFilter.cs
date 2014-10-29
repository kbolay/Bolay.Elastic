using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.And
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-and-filter.html
    /// </summary>
    [JsonConverter(typeof(AndSerializer))]
    public class AndFilter : FilterBase
    {
        /// <summary>
        /// All filters must be met to return the document.
        /// </summary>
        public IEnumerable<IFilter> Filters { get; private set; }

        /// <summary>
        /// Create an and filter.
        /// </summary>
        /// <param name="filters">All filters must be met to return the document.</param>
        public AndFilter(IEnumerable<IFilter> filters)
        { 
            if(filters == null || filters.All(x => x == null))
                throw new ArgumentNullException("filters", "AndFilter requires at least one filter.");

            Filters = filters;
            Cache = AndSerializer._CACHE_DEFAULT;
        }
    }
}
