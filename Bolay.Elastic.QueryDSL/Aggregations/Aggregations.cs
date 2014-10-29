using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-aggregations.html
    /// </summary>
    [JsonConverter(typeof(AggregationsSerializer))]
    public class Aggregations
    {
        /// <summary>
        /// Gets the aggregations.
        /// </summary>
        public IEnumerable<IAggregation> Aggregators { get; private set; }

        /// <summary>
        /// Create an aggregations object.
        /// </summary>
        /// <param name="aggregators">Sets the aggregations.</param>
        public Aggregations(IEnumerable<IAggregation> aggregators)
        {
            if (aggregators == null || aggregators.All(x => x == null))
                throw new ArgumentNullException("aggregators", "Aggregations requires at least one aggregator.");

            Aggregators = aggregators.Where(x => x != null);
        }
    }
}
