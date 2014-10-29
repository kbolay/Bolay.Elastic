using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting.Histogram
{
    /// <summary>
    /// The response of a histogram facet request.
    /// </summary>
    [JsonConverter(typeof(HistogramResponseSerializer))]
    public class HistogramResponse : IFacetResponse
    {
        /// <summary>
        /// Gets the name of the histogram facet.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the entries of the histogram facet response.
        /// </summary>
        public IEnumerable<HistogramEntry> Entries { get; private set; }

        /// <summary>
        /// Creates a histogram response object.
        /// </summary>
        /// <param name="name">Sets the name of the histogram facet.</param>
        /// <param name="entries">Sets the entries of the histogram facet response.</param>
        internal HistogramResponse(string name, IEnumerable<HistogramEntry> entries)
        {
            Name = name;
            Entries = entries;
        }
    }
}
