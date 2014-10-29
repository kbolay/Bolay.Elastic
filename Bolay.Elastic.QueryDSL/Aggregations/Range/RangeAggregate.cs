using Bolay.Elastic.Scripts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.Range
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-aggregations-bucket-range-aggregation.html
    /// </summary>
    [JsonConverter(typeof(RangeSerializer))]
    public class RangeAggregate : BucketAggregationBase
    {
        /// <summary>
        /// Gets the field.
        /// </summary>
        public string Field { get; private set; }

        /// <summary>
        /// Gets the script.
        /// </summary>
        public Script Script { get; private set; }

        /// <summary>
        /// Gets or sets whether the range buckets will be returned with keys.
        /// Defaults to false.
        /// </summary>
        public bool AreRangesKeyed { get; set; }

        /// <summary>
        /// Gets the range buckets.
        /// </summary>
        public IEnumerable<RangeBucket> Ranges { get; private set; }

        private RangeAggregate(string name, IEnumerable<RangeBucket> ranges)
            : base(name)
        {
            if(ranges == null || ranges.All(x => x == null))
                throw new ArgumentNullException("ranges", "RangeAggregate requires at least one range bucket.");

            Ranges = ranges.Where(x => x != null);
            AreRangesKeyed = RangeSerializer._KEYED_DEFAULT;
        }

        /// <summary>
        /// Create a range aggregation based on a field.
        /// </summary>
        /// <param name="name">Sets the name of the aggregation.</param>
        /// <param name="field">Sets the field for the aggregation.</param>
        /// <param name="ranges">Sets the range buckets for this aggregation.</param>
        public RangeAggregate(string name, string field, IEnumerable<RangeBucket> ranges)
            : this(name, ranges)
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "RangeAggregate requires a field in this constructor.");

            Field = field;
        }

        /// <summary>
        /// Create a range aggregation based on a field and value script.
        /// </summary>
        /// <param name="name">Sets the name of the aggregation.</param>
        /// <param name="field">Sets the field value for the aggregation.</param>
        /// <param name="script">Sets the value script for the aggregation.</param>
        /// <param name="ranges">Sets the range buckets for the aggregation.</param>
        public RangeAggregate(string name, string field, Script script, IEnumerable<RangeBucket> ranges)
            : this(name, field, ranges)
        {
            if (script == null)
                throw new ArgumentNullException("script", "RangeAggregate requires a script in this constructor.");

            Script = script;
        }

        /// <summary>
        /// Create a range aggregation based on a script.
        /// </summary>
        /// <param name="name">Sets the name of the aggregation.</param>
        /// <param name="script">Sets the script for the aggregation.</param>
        /// <param name="ranges">Sets the range buckets for the aggregation.</param>
        public RangeAggregate(string name, Script script, IEnumerable<RangeBucket> ranges)
            : this(name, ranges)
        {
            if (script == null)
                throw new ArgumentNullException("script", "RangeAggregate requires a script in this constructor.");

            Script = script;
        }
    }
}
