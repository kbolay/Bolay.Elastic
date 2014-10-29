using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.DisMax
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-dis-max-query.html
    /// </summary>
    [JsonConverter(typeof(DisMaxSerializer))]
    public class DisjunctionMaxQuery : QueryBase
    {
        public IEnumerable<IQuery> Queries { get; set; }
        public Double TieBreaker { get; set; }
        public Double Boost { get; set; }


        internal DisjunctionMaxQuery()
        {
            Boost = QuerySerializer._BOOST_DEFAULT;
            TieBreaker = DisMaxSerializer._TIE_BREAKER_DEFAULT;   
        }

        /// <summary>
        /// Create a dis_max query.
        /// </summary>
        /// <param name="queries">The subqueries to create a union of documents.</param>
        public DisjunctionMaxQuery(IEnumerable<IQuery> queries) : this()
        {
            if (queries == null || !queries.Any())
                throw new ArgumentNullException("queries", "DisjunctionmaxQuery requires queries.");

            Queries = queries;
        }
    }
}
