using Bolay.Elastic.QueryDSL.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.Filter
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-aggregations-bucket-filter-aggregation.html
    /// </summary>
    [JsonConverter(typeof(FilterSerializer))]
    public class FilterAggregate : BucketAggregationBase
    {
        /// <summary>
        /// Gets the filter for this aggregation.
        /// </summary>
        public IFilter Filter { get; private set; }
        
        /// <summary>
        /// Create a filter aggregation.
        /// </summary>
        /// <param name="name">Sets the name of the aggregation.</param>
        /// <param name="filter">Sets the filter to use for the aggregation.</param>
        public FilterAggregate(string name, IFilter filter)
            : base(name)
        {
            if (filter == null)
                throw new ArgumentNullException("filter", "FilterAggregate requires a filter.");

            Filter = filter;
        }
    }
}
