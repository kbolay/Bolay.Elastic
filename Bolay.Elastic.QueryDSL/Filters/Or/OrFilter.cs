using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.Or
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-or-filter.html
    /// </summary>
    [JsonConverter(typeof(OrSerializer))]
    public class OrFilter : FilterBase
    {
        /// <summary>
        /// Gets a colleciton filters of which one must be met to return the document.
        /// </summary>
        public IEnumerable<IFilter> Filters { get; private set; }

        /// <summary>
        /// Create an and filter.
        /// </summary>
        /// <param name="filters">All filters must be met to return the document.</param>
        public OrFilter(IEnumerable<IFilter> filters)
        { 
            if(filters == null || filters.All(x => x == null))
                throw new ArgumentNullException("filters", "OrFilter requires at least one filter.");

            Filters = filters;
            Cache = OrSerializer._CACHE_DEFAULT;
        }
    }
}
