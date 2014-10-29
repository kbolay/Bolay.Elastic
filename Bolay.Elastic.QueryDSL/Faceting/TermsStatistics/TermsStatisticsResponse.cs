using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting.TermsStatistics
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-facets-terms-stats-facet.html
    /// </summary>
    [JsonConverter(typeof(TermsStatisticsResponseSerializer))]
    public class TermsStatisticsResponse : IFacetResponse
    {
        /// <summary>
        /// Gets the name of the facet response.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the terms of the facet response.
        /// </summary>
        public IEnumerable<TermStatisticBucket> Terms { get; private set; }

        internal TermsStatisticsResponse(string name, IEnumerable<TermStatisticBucket> terms)
        {
            Name = name;
            Terms = terms;
        }
    }
}
