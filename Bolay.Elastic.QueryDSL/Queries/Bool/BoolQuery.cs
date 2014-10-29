using Bolay.Elastic.QueryDSL.MinimumShouldMatch;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Bool
{
    [JsonConverter(typeof(BoolQuerySerializer))]
    public class BoolQuery : QueryBase
    {       
        /// <summary>
        /// Queries that must match the document.
        /// </summary>
        public IEnumerable<IQuery> MustQueries { get; set; }

        /// <summary>
        /// Queries that must not match the document.
        /// </summary>
        public IEnumerable<IQuery> MustNotQueries { get; set; }

        /// <summary>
        /// Queries that are optional to match the the document.
        /// </summary>
        public IEnumerable<IQuery> ShouldQueries { get; set; }

        /// <summary>
        /// Look to ES and Lucene documentation for information on this property.
        /// </summary>
        public bool DisableCoords { get; set; }

        /// <summary>
        /// Describe the number or percentage of should queries that are required.
        /// </summary>
        public MinimumShouldMatchBase MinimumShouldMatch { get; set; }

        /// <summary>
        /// The boost value to apply to this entire query.
        /// </summary>
        public Double Boost { get; set; }

        internal BoolQuery()
        {
            Boost = QuerySerializer._BOOST_DEFAULT;
            MinimumShouldMatch = BoolQuerySerializer._MINIMUM_SHOULD_MATCH_DEFAULT;
            DisableCoords = BoolQuerySerializer._DISABLE_COORDS_DEFAULT;
        }

        /// <summary>
        /// Create a bool query.
        /// </summary>
        /// <param name="mustQueries">The resulting documents must match these queries.</param>
        /// <param name="mustNotQueries">The resulting documents cannot match these queries.</param>
        /// <param name="shouldQueries">The resulting document may or may not match these queries.</param>
        public BoolQuery(IEnumerable<IQuery> mustQueries, IEnumerable<IQuery> mustNotQueries, IEnumerable<IQuery> shouldQueries) : this()
        {
            if ((mustQueries == null || !mustQueries.Any()) &&
                (mustNotQueries == null || !mustNotQueries.Any()) &&
                (shouldQueries == null || !shouldQueries.Any()))
            {
                throw new ArgumentNullException("queries", "BoolQuery requires at least one query be provided.");
            }

            MustQueries = mustQueries;
            ShouldQueries = shouldQueries;
            MustNotQueries = mustNotQueries;
        }
    }
}
