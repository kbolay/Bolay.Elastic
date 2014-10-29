using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.Missing
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-aggregations-bucket-missing-aggregation.html
    /// </summary>
    [JsonConverter(typeof(MissingSerializer))]
    public class MissingAggregate : BucketAggregationBase
    {
        /// <summary>
        /// Gets the field to find missing/null configured values in.
        /// </summary>
        public string Field { get; private set; }

        public MissingAggregate(string name, string field)
            : base(name)
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "MissingAggregate requires a field.");

            Field = field;
        }
    }
}
