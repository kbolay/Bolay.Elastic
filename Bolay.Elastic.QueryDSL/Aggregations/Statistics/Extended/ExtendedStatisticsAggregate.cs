using Bolay.Elastic.Scripts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.Statistics.Extended
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-aggregations-metrics-extendedstats-aggregation.html
    /// </summary>
    [JsonConverter(typeof(ExtendedStatisticSerializer))]
    public class ExtendedStatisticsAggregate : MetricAggregationBase
    {
        /// <summary>
        /// Create an extended_stats aggregation base on a field.
        /// </summary>
        /// <param name="name">Sets the name of the aggregation.</param>
        /// <param name="field">Sets the field.</param>
        public ExtendedStatisticsAggregate(string name, string field) : base(name, field) { }

        /// <summary>
        /// Creates an extended_stats aggregation using field and script.
        /// </summary>
        /// <param name="name">The name of the aggregation.</param>
        /// <param name="field">The field to retrieve values from.</param>
        /// <param name="script">The script that acts on the values.</param>
        public ExtendedStatisticsAggregate(string name, string field, Script script) : base(name, field, script) { }

        /// <summary>
        /// Creates an extended_stats aggregation based on a script.
        /// </summary>
        /// <param name="name">Sets the name of the aggregation.</param>
        /// <param name="script">Sets the script.</param>
        public ExtendedStatisticsAggregate(string name, Script script) : base(name, script) { }
    }
}
