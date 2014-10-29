using Bolay.Elastic.Scripts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.Percentiles
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-aggregations-metrics-percentile-aggregation.html
    /// </summary>
    [JsonConverter(typeof(PercentilesSerializer))]
    public class PercentilesAggregate : BucketAggregationBase
    {
        internal static List<Double> _PERCENT_BUCKETS_DEFAULT = new List<double>()
        {
            1, 5, 25, 50, 75, 95, 99
        };
        internal const int _COMPRESSION_DEFAULT = 100;

        /// <summary>
        /// Gets the field to create percentiles from.
        /// </summary>
        public string Field { get; private set; }

        /// <summary>
        /// Gets the script to create the percentiles from.
        /// </summary>
        public Script Script { get; private set; }

        /// <summary>
        /// Gets or sets the percentile buckets.
        /// Defaults to [ 1, 5, 25, 50, 75, 95, 99 ]
        /// </summary>
        public IEnumerable<Double> PercentBuckets { get; set; }

        /// <summary>
        /// Gets or sets the compression value that controls how much memory to allow for creating the percentiles and the affects the accuracy of the results.
        /// Defaults to 100.
        /// </summary>
        public int Compression { get; set; }

        internal PercentilesAggregate(string name)
            : base(name)
        {
            PercentBuckets = _PERCENT_BUCKETS_DEFAULT;
            Compression = _COMPRESSION_DEFAULT;
        }

        /// <summary>
        /// Creates a percentiles aggregation based on a document field.
        /// </summary>
        /// <param name="name">Sets the name of the aggregation.</param>
        /// <param name="field">Sets the field of the document to gather percentiles from.</param>
        public PercentilesAggregate(string name, string field)
            : this(name)
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "PercentilesAggregate requires a field in this constructor.");

            Field = field;
        }

        /// <summary>
        /// Creates a percentiles aggregation based on a script.
        /// </summary>
        /// <param name="name">Sets the name of the aggregation.</param>
        /// <param name="script">Sets the script for the aggregation.</param>
        public PercentilesAggregate(string name, Script script)
            : base(name)
        {
            if (script == null)
                throw new ArgumentNullException("script", "PercentilesAggregate requires a script in this constructor.");

            Script = script;
        }
    }
}
