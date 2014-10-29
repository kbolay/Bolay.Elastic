using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting.Range
{
    /// <summary>
    /// The response of a range facet.
    /// </summary>
    [JsonConverter(typeof(RangeResponseSerializer))]
    public class RangeResponse : IFacetResponse
    {
        /// <summary>
        /// Gets the name of the facet.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the range buckets of the geo range facet response.
        /// </summary>
        public IEnumerable<RangeResponseBucket> Ranges { get; private set; }

        internal RangeResponse(string name, IEnumerable<RangeResponseBucket> ranges)
        {
            Name = name;
            Ranges = ranges;
        }
    }
}
