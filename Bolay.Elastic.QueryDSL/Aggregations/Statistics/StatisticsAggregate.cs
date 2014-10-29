using Bolay.Elastic.Scripts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.Statistics
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-aggregations-metrics-stats-aggregation.html
    /// </summary>
    [JsonConverter(typeof(StatisticsSerializer))]
    public class StatisticsAggregate : MetricAggregationBase
    {
        /// <summary>
        /// Create a stats aggregation base on a field.
        /// </summary>
        /// <param name="name">Sets the name of the aggregation.</param>
        /// <param name="field">Sets the field.</param>
        public StatisticsAggregate(string name, string field) : base(name, field) { }

        /// <summary>
        /// Creates a stats aggregation using field and script.
        /// </summary>
        /// <param name="name">The name of the aggregation.</param>
        /// <param name="field">The field to retrieve values from.</param>
        /// <param name="script">The script that acts on the values.</param>
        public StatisticsAggregate(string name, string field, Script script) : base(name, field, script) { }

        /// <summary>
        /// Creates a stats aggregation based on a script.
        /// </summary>
        /// <param name="name">Sets the name of the aggregation.</param>
        /// <param name="script">Sets the script.</param>
        public StatisticsAggregate(string name, Script script) : base(name, script) { }
    }
}
