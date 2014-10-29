using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting.Terms
{
    /// <summary>
    /// The response of a terms facet request.
    /// </summary>
    [JsonConverter(typeof(TermsResponseSerializer))]
    public class TermsResponse : IFacetResponse
    {
        /// <summary>
        /// Sets the name of the facet this is a response for.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the number of documents that don't have a value for the faceted field.
        /// </summary>
        public Int64 Missing { get; private set; }

        /// <summary>
        /// Gets the number of terms in this facet.
        /// </summary>
        public Int64 Total { get; private set; }

        /// <summary>
        /// Gets the number of terms not included in this response.
        /// </summary>
        public Int64 Other { get; private set; }

        /// <summary>
        /// Gets the top n terms for the faceted field.
        /// </summary>
        public IEnumerable<TermBucket> Terms { get; private set; }

        /// <summary>
        /// Creates the response of a terms facet request.
        /// </summary>
        /// <param name="name">Sets the name of the facet request.</param>
        /// <param name="missing">Sets the number of documents that have no value in the faceted field.</param>
        /// <param name="total">Sets the total number of terms in the facet.</param>
        /// <param name="other">Sets the number of terms not included in the returned terms.</param>
        /// <param name="terms"></param>
        internal TermsResponse(string name, Int64 missing, Int64 total, Int64 other, IEnumerable<TermBucket> terms)
        {
            Name = name;
            Missing = missing;
            Total = total;
            Other = other;
            Terms = terms;
        }
    }
}
