using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting.GeoDistance
{
    /// <summary>
    /// The response of a geo distance facet request.
    /// </summary>
    [JsonConverter(typeof(GeoDistanceResponseSerializer))]
    public class GeoDistanceResponse : IFacetResponse
    {
        /// <summary>
        /// Gets the name of the facet.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the range buckets of the geo distance facet response.
        /// </summary>
        public IEnumerable<GeoDistanceRange> Ranges { get; private set; }

        internal GeoDistanceResponse(string name, IEnumerable<GeoDistanceRange> ranges)
        {
            Name = name;
            Ranges = ranges;
        }
    }
}
