using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting.Filter
{
    [JsonConverter(typeof(FilterResponseSerializer))]
    public class FilterResponse : IFacetResponse
    {
        /// <summary>
        /// Gets the name of the facet response.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the number of documents matching the filter.
        /// </summary>
        public Int64 Count { get; private set; }

        /// <summary>
        /// Creates the filter facet response.
        /// </summary>
        /// <param name="name">Sets the name of the facet response.</param>
        /// <param name="count">Sets the number of documents matching the filter facet.</param>
        internal FilterResponse(string name, Int64 count)
        {
            Name = name;
            Count = count;
        }
    }
}
