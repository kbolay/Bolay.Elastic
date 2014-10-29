using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.Limit
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-limit-filter.html
    /// </summary>
    [JsonConverter(typeof(LimitSerializer))]
    public class LimitFilter : FilterBase
    {
        /// <summary>
        /// Get the number of documents to execute the query on per shard.
        /// </summary>
        public Int64 Size { get; private set; }

        /// <summary>
        /// Create a limit filter.
        /// </summary>
        /// <param name="size">Set the number of documents to execute the query on per shard.</param>
        public LimitFilter(Int64 size)
        {
            if (size <= 0)
                throw new ArgumentOutOfRangeException("size", "LimitFilter requires a size.");

            Size = size;
        }
    }
}
