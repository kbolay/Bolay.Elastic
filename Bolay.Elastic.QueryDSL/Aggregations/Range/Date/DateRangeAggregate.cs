using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.Range.Date
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-aggregations-bucket-daterange-aggregation.html
    /// </summary>
    [JsonConverter(typeof(DateRangeSerializer))]
    public class DateRangeAggregate : BucketAggregationBase
    {
        /// <summary>
        /// Gets the field.
        /// </summary>
        public string Field { get; private set; }

        /// <summary>
        /// Gets or sets the date format.
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// Gets the ranges.
        /// </summary>
        public IEnumerable<DateRangeBucket> Ranges { get; private set; }

        public DateRangeAggregate(string name, string field, IEnumerable<DateRangeBucket> ranges)
            : base(name)
        {
            if(string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "DateRangeBucket requires a field.");
            if(ranges == null || ranges.All(x => x == null))
                throw new ArgumentNullException("ranges", "DateRangeBucket requires at least one date range bucket.");

            Field = field;
            Ranges = ranges.Where(x => x != null);
        }
    }
}
