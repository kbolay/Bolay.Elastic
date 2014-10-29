using Bolay.Elastic.QueryDSL.Queries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.Not
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-not-filter.html
    /// </summary>
    [JsonConverter(typeof(NotSerializer))]
    public class NotFilter : FilterBase
    {
        /// <summary>
        /// Gets the query the filter is based on.
        /// </summary>
        public IQuery Query { get; private set; }

        /// <summary>
        /// Gets the filter the filter surrounds.
        /// </summary>
        public IFilter Filter { get; private set; }

        public NotFilter(IQuery query)
        {
            if (query == null)
                throw new ArgumentNullException("query", "NotFilter requires a query for this constructor.");

            Query = query;
        }

        public NotFilter(IFilter filter)
        {
            if (filter == null)
                throw new ArgumentNullException("filter", "NotFilter requires a filter for this constructor.");

            Filter = filter;
        }
    }
}
