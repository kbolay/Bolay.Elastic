using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Boosting
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-boosting-query.html
    /// TODO: Find more documentation on boosting query.
    /// </summary>
    [JsonConverter(typeof(BoostingSerializer))]
    public class BoostingQuery : QueryBase
    {
        public IQuery PositiveQuery { get; set; }
        public IQuery NegativeQuery { get; set; }
        public Double NegativeBoost { get; set; }

        internal BoostingQuery() { }

        public BoostingQuery(IQuery negativeQuery, IQuery positiveQuery, Double negativeBoost)
        {
            if (negativeQuery == null && positiveQuery == null)
                throw new ArgumentNullException("query", "BoostingQuery expects a negative or positive query.");
            if (negativeBoost == default(Double) || negativeBoost <= 0)
                throw new ArgumentOutOfRangeException("negativeBoost", "BoostingQuery requires the negative boost be greater than 0.0.");

            NegativeQuery = negativeQuery;
            PositiveQuery = positiveQuery;
            NegativeBoost = negativeBoost;
        }
    }
}
