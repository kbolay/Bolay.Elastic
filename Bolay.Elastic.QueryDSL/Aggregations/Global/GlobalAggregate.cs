using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.Global
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-aggregations-bucket-global-aggregation.html
    /// </summary>
    [JsonConverter(typeof(GlobalSerializer))]
    public class GlobalAggregate : BucketAggregationBase
    {
        /// <summary>
        /// Create a global aggregate.
        /// </summary>
        /// <param name="name">Sets the name of the aggregation.</param>
        /// <param name="subAggregations">Sets the sub aggregations.</param>
        public GlobalAggregate(string name, IEnumerable<IAggregation> subAggregations)
            : base(name)
        {
            if (subAggregations == null || subAggregations.All(x => x == null))
                throw new ArgumentNullException("subAggregations", "GlobalAggregate requires at least one sub aggregation.");

            SubAggregations = subAggregations.Where(x => x != null);
        }
    }
}
