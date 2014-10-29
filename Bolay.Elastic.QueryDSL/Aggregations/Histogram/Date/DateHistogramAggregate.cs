using Bolay.Elastic.Scripts;
using Bolay.Elastic.Time;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.Histogram.Date
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-aggregations-bucket-datehistogram-aggregation.html
    /// </summary>
    [JsonConverter(typeof(DateHistogramSerializer))]
    public class DateHistogramAggregate : BucketAggregationBase
    {
        private static List<TimeUnit> _AcceptedTimeUnits = new List<TimeUnit>()
        {
            TimeUnit.Days,
            TimeUnit.Hours,
            TimeUnit.Millisecond,
            TimeUnit.Minute,
            TimeUnit.Weeks
        };

        /// <summary>
        /// Gets the constant interval value.
        /// </summary>
        public DateIntervalEnum ConstantInterval { get; private set; }

        /// <summary>
        /// Gets the time interval value.
        /// </summary>
        public TimeValue TimeInterval { get; private set; }

        /// <summary>
        /// Gets or sets the pre-rounding to be performed.
        /// Defaults to 00:00.
        /// </summary>
        public TimeSpan PreZone { get; set; }

        /// <summary>
        /// Gets or sets the post-rounding to be performed.
        /// Defaults to 00:00.
        /// </summary>
        public TimeSpan PostZone { get; set; }

        /// <summary>
        /// Gets or sets the timezone.
        /// Defaults to 00:00.
        /// </summary>
        public TimeSpan TimeZone { get; set; }

        /// <summary>
        /// Gets or sets the pre_zone_adjust_large_interval value.
        /// </summary>
        public bool PreZoneAdjustLargeInterval { get; set; }

        /// <summary>
        /// Gets or set the factor value.
        /// </summary>
        public Int64? Factor { get; set; }

        /// <summary>
        /// Gets or sets the pre_offset value.
        /// </summary>
        public TimeValue PreOffset { get; set; }

        /// <summary>
        /// Gets or sets the post_offset value.
        /// </summary>
        public TimeValue PostOffset { get; set; }

        /// <summary>
        /// Get the field name.
        /// </summary>
        public string Field { get; private set; }

        /// <summary>
        /// Gets the script.
        /// </summary>
        public Script Script { get; private set; }

        /// <summary>
        /// Gets or sets the value to sort on.
        /// </summary>
        public string SortValue { get; set; }

        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        public SortOrderEnum SortOrder { get; set; }

        /// <summary>
        /// Gets or sets the min_doc_count value.
        /// </summary>
        public int MinimumDocumentCount { get; set; }

        /// <summary>
        /// Creates a date histogram facet using a constant interval that expects key/value field or script to be populated.
        /// </summary>
        /// <param name="facetName">Sets the name of the facet.</param>
        /// <param name="constantInterval">Sets the constant interval.</param>
        private DateHistogramAggregate(string facetName, DateIntervalEnum constantInterval)
            : base(facetName)
        {
            if (constantInterval == null)
                throw new ArgumentNullException("constantInterval", "DateHistogramAggregate requires a constantInterval.");

            ConstantInterval = constantInterval;
            PreZoneAdjustLargeInterval = DateHistogramSerializer._PRE_ZONE_ADJUST_LARGE_INTERVAL_DEFAULT;
            PreZone = DateHistogramSerializer._TIMESPAN_DEFAULT;
            PostZone = DateHistogramSerializer._TIMESPAN_DEFAULT;
            TimeZone = DateHistogramSerializer._TIMESPAN_DEFAULT;
            MinimumDocumentCount = DateHistogramSerializer._MINIMUM_DOCUMENT_COUNT_DEFAULT;
        }

        /// <summary>
        /// Creates a data histogram facet that expects a time value interval that expects key/value field or script to be populated.
        /// </summary>
        /// <param name="facetName">Sets the name of the facet.</param>
        /// <param name="timeInterval">Sets the time interval.</param>
        private DateHistogramAggregate(string facetName, TimeValue timeInterval)
            : base(facetName)
        {
            if (timeInterval == null)
                throw new ArgumentNullException("timeInterval", "DateHistogramAggregate requires a time interval.");
            if (!_AcceptedTimeUnits.Contains(timeInterval.Unit))
                throw new ArgumentOutOfRangeException("timeInterval", "DateHistogramAggregate requires a time interval on a unit of weeks or less.");

            TimeInterval = timeInterval;
            PreZoneAdjustLargeInterval = DateHistogramSerializer._PRE_ZONE_ADJUST_LARGE_INTERVAL_DEFAULT;
        }

        /// <summary>
        /// Creates a date histogram facet based on a field and constant interval.
        /// </summary>
        /// <param name="facetName">Sets the name for the facet.</param>
        /// <param name="field">Sets the field value.</param>
        /// <param name="constantInterval">Sets the constant interval to use.</param>
        public DateHistogramAggregate(string facetName, string field, DateIntervalEnum constantInterval)
            : this(facetName, constantInterval)
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "DateHistogramAggregate requires a field value.");

            Field = field;
        }

        /// <summary>
        /// Create a date histogram facet based on a field and time interval.
        /// </summary>
        /// <param name="facetName">Sets the name for the facet.</param>
        /// <param name="field">Sets the field.</param>
        /// <param name="timeInterval">Sets the time interval.</param>
        public DateHistogramAggregate(string facetName, string field, TimeValue timeInterval)
            : this(facetName, timeInterval)
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "DateHistogramAggregate requires a field value.");

            Field = field;
        }

        /// <summary>
        /// Creates a date histogram facet based on a field and constant interval.
        /// </summary>
        /// <param name="facetName">Sets the name for the facet.</param>
        /// <param name="field">Sets the field value.</param>
        /// <param name="script">Sets the script value.</param>
        /// <param name="constantInterval">Sets the constant interval to use.</param>
        public DateHistogramAggregate(string facetName, string field, Script script, DateIntervalEnum constantInterval)
            : this(facetName, constantInterval)
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "DateHistogramAggregate requires a field value.");
            if (script == null)
                throw new ArgumentNullException("script", "DateHistogramAggregate requires a script value.");
            Field = field;
            Script = script;
        }

        /// <summary>
        /// Create a date histogram facet based on a field and time interval.
        /// </summary>
        /// <param name="facetName">Sets the name for the facet.</param>
        /// <param name="field">Sets the field.</param>
        /// <param name="script">Sets the script value.</param>
        /// <param name="timeInterval">Sets the time interval.</param>
        public DateHistogramAggregate(string facetName, string field, Script script,TimeValue timeInterval)
            : this(facetName, timeInterval)
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "DateHistogramAggregate requires a field value.");
            if (script == null)
                throw new ArgumentNullException("script", "DateHistogramAggregate requires a script value.");
            Field = field;
            Script = script;
        }

        /// <summary>
        /// Creates a date histogram facet based on a field and constant interval.
        /// </summary>
        /// <param name="facetName">Sets the name for the facet.</param>
        /// <param name="script">Sets the script value.</param>
        /// <param name="constantInterval">Sets the constant interval to use.</param>
        public DateHistogramAggregate(string facetName, Script script, DateIntervalEnum constantInterval)
            : this(facetName, constantInterval)
        {
            if (script == null)
                throw new ArgumentNullException("script", "DateHistogramAggregate requires a script value.");
            Script = script;
        }

        /// <summary>
        /// Create a date histogram facet based on a field and time interval.
        /// </summary>
        /// <param name="facetName">Sets the name for the facet.</param>
        /// <param name="script">Sets the script value.</param>
        /// <param name="timeInterval">Sets the time interval.</param>
        public DateHistogramAggregate(string facetName, Script script, TimeValue timeInterval)
            : this(facetName, timeInterval)
        {
            if (script == null)
                throw new ArgumentNullException("script", "DateHistogramAggregate requires a script value.");
            Script = script;
        }
    }
}
