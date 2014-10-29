using Bolay.Elastic.QueryDSL.Filters;
using Bolay.Elastic.QueryDSL.Queries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Filtered
{
    [JsonConverter(typeof(FilteredQuerySerializer))]
    public class FilteredQuery : QueryBase
    {
        public IQuery Query { get; private set; }
        public IFilter Filter { get; private set; }

        /// <summary>
        /// Create a filtered query.
        /// </summary>
        /// <param name="query">The query for finding and scoring documents.</param>
        /// <param name="filter">The filter to apply to the documents found by the query.</param>
        public FilteredQuery(IQuery query, IFilter filter)
        {
            if (query == null)
                throw new ArgumentNullException("query", "FilteredQuery requires a query.");

            if (filter == null)
                throw new ArgumentNullException("filter", "FilteredQuery requires a filter.");

            Query = query;
            Filter = filter;
        }
    }
}
