using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.Nested
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-aggregations-bucket-nested-aggregation.html
    /// </summary>
    [JsonConverter(typeof(NestedSerializer))]
    public class NestedAggregate : BucketAggregationBase
    {
        /// <summary>
        /// Gets the path of the nested document.
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// Create a nested aggregation.
        /// </summary>
        /// <param name="name">Sets the name of the aggregation.</param>
        /// <param name="path">Sets the path of the nested document.</param>
        public NestedAggregate(string name, string path)
            : base(name)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException("path", "NestedAggregate requires a path.");

            Path = path;
        }
    }
}
