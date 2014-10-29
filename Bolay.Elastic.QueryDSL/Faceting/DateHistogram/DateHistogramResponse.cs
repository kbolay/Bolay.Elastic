using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting.DateHistogram
{  
    /// <summary>
    /// The response of a date_histogram facet request.
    /// </summary>
    [JsonConverter(typeof(DateHistogramResponseSerializer))]
    public class DateHistogramResponse : IFacetResponse
    {
        /// <summary>
        /// Gets the name of the date histogram facet.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the entries for the date histogram facet.
        /// </summary>
        public IEnumerable<DateHistogramEntry> Entries { get; private set; }

        /// <summary>
        /// Create a date_histogram response object, this constructor is for deserializing.
        /// </summary>
        /// <param name="name">Sets the name of the date_histogram facet.</param>
        /// <param name="entries">Sets the entries of the date_histogram facet.</param>
        internal DateHistogramResponse(string name, IEnumerable<DateHistogramEntry> entries)
        {
            Name = name;
            Entries = entries;
        }
    }
}
