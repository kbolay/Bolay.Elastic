using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.Range.IPv4
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-aggregations-bucket-iprange-aggregation.html
    /// </summary>
    [JsonConverter(typeof(IPv4RangeSerializer))]
    public class IPv4RangeAggregate : BucketAggregationBase
    {
        /// <summary>
        /// Gets the field that holds ip addresses.
        /// </summary>
        public string Field { get; private set; }

        /// <summary>
        /// Gets the ranges that define the buckets.
        /// </summary>
        public IEnumerable<IpRangeBucket> Ranges { get; private set; }

        /// <summary>
        /// Creates an ip_range aggregation.
        /// </summary>
        /// <param name="name">Sets the name of the aggregation.</param>
        /// <param name="field">Sets the ip address field of the document.</param>
        /// <param name="ranges">Sets the ip range buckets.</param>
        public IPv4RangeAggregate(string name, string field, IEnumerable<IpRangeBucket> ranges)
            : base(name)
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "IPv4RangeAggregate requires a field.");

            if (ranges == null || ranges.All(x => x == null))
                throw new ArgumentNullException("ranges", "IPv4RangeAggregate requires at least one range bucket.");

            Field = field;
            Ranges = ranges.Where(x => x != null);
        }
    }
}