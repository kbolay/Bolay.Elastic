using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting.Query
{
    /// <summary>
    /// The response from a query facet.
    /// </summary>
    [JsonConverter(typeof(QueryResponseSerializer))]
    public class QueryResponse : IFacetResponse
    {
        /// <summary>
        /// Gets the name of the query facet.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the number of documents that match the query.
        /// </summary>
        public Int64 Count { get; private set; }

        /// <summary>
        /// Creates a query facet response.
        /// </summary>
        /// <param name="name">Sets the name of the query facet response.</param>
        /// <param name="count">Sets the number of documents that match the query from the query facet.</param>
        internal QueryResponse(string name, Int64 count)
        {
            Name = name;
            Count = count;
        }
    }
}
