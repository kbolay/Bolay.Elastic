using Bolay.Elastic.Scripts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.Histogram
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-aggregations-bucket-histogram-aggregation.html
    /// </summary>
    [JsonConverter(typeof(HistogramSerializer))]
    public class HistogramAggregate : BucketAggregationBase
    {
        private int _MinimumDocumentCount { get; set; }

        /// <summary>
        /// Gets the field to perform aggregation on.
        /// </summary>
        public string Field { get; private set; }

        /// <summary>
        /// Gets the script value.
        /// </summary>
        public Script Script { get; private set; }

        /// <summary>
        /// Gets the interval to form the buckets.
        /// </summary>
        public Double Interval { get; private set; }

        /// <summary>
        /// Gets or sets the min_doc_count value.
        /// Defaults to 1.
        /// </summary>
        public int MinimumDocumentCount 
        {
            get { return _MinimumDocumentCount; }
            set 
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("MinimumDocumentCount", "MinimumDocumentCount must be greater than or equal to zero.");

                _MinimumDocumentCount = value;
            }
        }

        /// <summary>
        /// Gets or sets the value to sort on.
        /// </summary>
        public string SortValue { get; set; }

        /// <summary>
        /// Gets or sets the order to sort.
        /// Defaults to asc.
        /// </summary>
        public SortOrderEnum SortOrder { get; set; }

        /// <summary>
        /// Gets or sets if the returned buckets are keyed.
        /// Defaults to false.
        /// </summary>
        public bool AreBucketsKeyed { get; set; }

        private HistogramAggregate(string name, Double? interval)
            : base(name)
        {
            if (!interval.HasValue)
                throw new ArgumentNullException("interval", "HistogramAggregate requires an interval value.");
            if (interval.Value <= 0)
                throw new ArgumentOutOfRangeException("interval", "HistogramAggregate expects an interval greater than 0.");

            Interval = interval.Value;

            SortOrder = SortOrderEnum.Ascending;
            MinimumDocumentCount = HistogramSerializer._MINIMUM_DOCUMENT_COUNT_DEFAULT;
            AreBucketsKeyed = HistogramSerializer._KEYED_DEFAULT;
        }

        /// <summary>
        /// Creates a histogram aggregation.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="field"></param>
        /// <param name="interval"></param>
        public HistogramAggregate(string name, string field, Double? interval)
            : this(name, interval)
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "HistogramAggregate requires a field.");

            Field = field;
        }

        /// <summary>
        /// Creates a histogram aggregation.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="field"></param>
        /// <param name="script"></param>
        /// <param name="interval"></param>
        public HistogramAggregate(string name, string field, Script script, Double? interval)
            : this(name, field, interval)
        {
            if (script == null)
                throw new ArgumentNullException("script", "HistogramAggregate requires a script in this constructor.");

            Script = script;
        }

        /// <summary>
        /// Creates a histogram aggregation.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="script"></param>
        /// <param name="interval"></param>
        public HistogramAggregate(string name, Script script, Double? interval)
            : this(name, interval)
        {
            if (script == null)
                throw new ArgumentNullException("script", "HistogramAggregate requires a script in this constructor.");

            Script = script;
        }
    }
}
