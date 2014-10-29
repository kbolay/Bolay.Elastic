using Bolay.Elastic.Scripts;
using Bolay.Elastic.Time;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting.Histogram
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-facets-histogram-facet.html
    /// </summary>
    [JsonConverter(typeof(HistogramSerializer))]
    public class HistogramFacet : FacetBase
    {
        /// <summary>
        /// Gets the field.
        /// </summary>
        public string Field { get; private set; }

        /// <summary>
        /// Gets the key field.
        /// </summary>
        public string KeyField { get; set; }

        /// <summary>
        /// Gets the value field.
        /// </summary>
        public string ValueField { get; set; }

        /// <summary>
        /// Gets the key script.
        /// </summary>
        public string KeyScript { get; set; }

        /// <summary>
        /// Gets the value script.
        /// </summary>
        public string ValueScript { get; set; }

        /// <summary>
        /// Gets or sets the script parameters value.
        /// </summary>
        public IEnumerable<ScriptParameter> ScriptParameters { get; set; }

        /// <summary>
        /// Gets or sets the scripting language used in any script property values.
        /// </summary>
        public string ScriptLanguage { get; set; }

        /// <summary>
        /// Gets or sets the numeric interval separating the buckets.
        /// </summary>
        public Int64 Interval { get; private set; }

        /// <summary>
        /// Gets or sets the time interval separating the buckets.
        /// </summary>
        public TimeValue TimeInterval { get; private set; } 

        /// <summary>
        /// Create a histogram facet based on a numeric interval that will use a combination of key/value field/script
        /// and does not require an interval or time interval value.
        /// </summary>
        /// <param name="facetName"></param>
        public HistogramFacet(string facetName) : base(facetName) { }

        /// <summary>
        /// Creates a histogram facet using an interval and a combination of key/value field/script values.
        /// </summary>
        /// <param name="facetName">Sets the facet name.</param>
        /// <param name="interval">Sets teh interval value.</param>
        public HistogramFacet(string facetName, Int64 interval)
            : base(facetName)
        {
            if (interval <= 0)
                throw new ArgumentOutOfRangeException("interval", "HistogramFacet requires an interval.");

            Interval = interval;
        }

        /// <summary>
        /// Creates a histogram facet using a time interval and an expected combination of key/value field/script values.
        /// </summary>
        /// <param name="facetName">Sets the name for the facet.</param>
        /// <param name="timeInterval">Sets the time interval for the term buckets.</param>
        public HistogramFacet(string facetName, TimeValue timeInterval)
            : base(facetName)
        {
            if (timeInterval == null)
                throw new ArgumentNullException("timeInterval", "HistogramFacet requires a time interval.");

            TimeInterval = timeInterval;
        }

        /// <summary>
        /// Creates a histogram facet using a specific field and interval value.
        /// </summary>
        /// <param name="facetName">Sets the name for the facet.</param>
        /// <param name="field">Sets the field for the facet.</param>
        /// <param name="interval">Sets the interval for the term buckets.</param>
        public HistogramFacet(string facetName, string field, Int64 interval)
            : this(facetName, interval)
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "HistogramFacet requires a field in this constructor.");

            Field = field;
        }

        /// <summary>
        /// Creates a histogram facet using a specific field and time interval.
        /// </summary>
        /// <param name="facetName">Sets the name for the facet.</param>
        /// <param name="field">Sets the field for the facet.</param>
        /// <param name="timeInterval">Sets the time interval for the term buckets.</param>
        public HistogramFacet(string facetName, string field, TimeValue timeInterval)
            : this(facetName, timeInterval)
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "HistogramFacet requires a field in this constructor.");

            Field = field;
        }
    }
}
