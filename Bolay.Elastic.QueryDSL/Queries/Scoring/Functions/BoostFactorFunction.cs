using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Scoring.Functions
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/query-dsl-function-score-query.html#_boost_factor
    /// </summary>
    [JsonConverter(typeof(BoostFactorSerializer))]
    public class BoostFactorFunction : ScoreFunctionBase
    {
        public Double Boost { get; private set; }

        public BoostFactorFunction(Double boost)
        {
            Boost = boost;
        }
    }
}
