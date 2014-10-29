using Bolay.Elastic.Scripts;
using Bolay.Elastic.Time;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting.DateHistogram
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-facets-date-histogram-facet.html
    /// </summary>
    [JsonConverter(typeof(DateHistogramSerializer))]
    public class DateHistogramFacet : FacetBase
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
        /// Gets or sets the key_field value.
        /// </summary>
        public string KeyField { get; set; }

        /// <summary>
        /// Gets or sets the value_field value.
        /// </summary>
        public string ValueField { get; set; }

        /// <summary>
        /// Gets or sets the value_script value.
        /// </summary>
        public string ValueScript { get; set; }

        /// <summary>
        /// Gets or sets the script parameters.
        /// </summary>
        public IEnumerable<ScriptParameter> ScriptParameters { get; set; }

        /// <summary>
        /// Gets or sets the scripting language used in any script property values.
        /// </summary>
        public string ScriptLanguage { get; set; }

        /// <summary>
        /// Creates a date histogram facet using a constant interval that expects key/value field or script to be populated.
        /// </summary>
        /// <param name="facetName">Sets the name of the facet.</param>
        /// <param name="constantInterval">Sets the constant interval.</param>
        public DateHistogramFacet(string facetName, DateIntervalEnum constantInterval)
            : base(facetName)
        {
            if (constantInterval == null)
                throw new ArgumentNullException("constantInterval", "DateHistogramFacet requires a constantInterval.");

            ConstantInterval = constantInterval;
            PreZoneAdjustLargeInterval = DateHistogramSerializer._PRE_ZONE_ADJUST_LARGE_INTERVAL_DEFAULT;
        }

        /// <summary>
        /// Creates a data histogram facet that expects a time value interval that expects key/value field or script to be populated.
        /// </summary>
        /// <param name="facetName">Sets the name of the facet.</param>
        /// <param name="timeInterval">Sets the time interval.</param>
        public DateHistogramFacet(string facetName, TimeValue timeInterval)
            : base(facetName)
        {
            if (timeInterval == null)
                throw new ArgumentNullException("timeInterval", "DateHistogramFacet requires a time interval.");
            if (!_AcceptedTimeUnits.Contains(timeInterval.Unit))
                throw new ArgumentOutOfRangeException("timeInterval", "DateHistogramFacet requires a time interval on a unit of weeks or less.");

            TimeInterval = timeInterval;
            PreZoneAdjustLargeInterval = DateHistogramSerializer._PRE_ZONE_ADJUST_LARGE_INTERVAL_DEFAULT;
        }

        /// <summary>
        /// Creates a date histogram facet based on a field and constant interval.
        /// </summary>
        /// <param name="facetName">Sets the name for the facet.</param>
        /// <param name="field">Sets the field value.</param>
        /// <param name="constantInterval">Sets the constant interval to use.</param>
        public DateHistogramFacet(string facetName, string field, DateIntervalEnum constantInterval)
            : this(facetName, constantInterval)
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "DateHistogramFacet requires a field value.");

            Field = field;
        }

        /// <summary>
        /// Create a date histogram facet based on a field and time interval.
        /// </summary>
        /// <param name="facetName">Sets the name for the facet.</param>
        /// <param name="field">Sets the field.</param>
        /// <param name="timeInterval">Sets the time interval.</param>
        public DateHistogramFacet(string facetName, string field, TimeValue timeInterval)
            : this(facetName, timeInterval)
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "DateHistogramFacet requires a field value.");

            Field = field;
        }
    }
}
