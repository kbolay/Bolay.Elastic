using Bolay.Elastic.QueryDSL.Queries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.Query
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-query-filter.html
    /// </summary>
    [JsonConverter(typeof(QuerySerializer))]
    public class QueryFilter : FilterBase
    {
        /// <summary>
        /// Gets the query this filter uses to search documents.
        /// </summary>
        public IQuery Query { get; private set; }

        /// <summary>
        /// Create a query filter.
        /// </summary>
        /// <param name="query">Set the query this filter uses to search documents.</param>
        public QueryFilter(IQuery query)
        {
            if (query == null)
                throw new ArgumentNullException("query", "QueryFilter requires a query.");

            Query = query;
        }
    }
}
