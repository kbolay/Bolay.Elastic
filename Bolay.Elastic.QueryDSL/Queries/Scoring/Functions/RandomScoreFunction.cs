using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Scoring.Functions
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/query-dsl-function-score-query.html#_random
    /// </summary>
    [JsonConverter(typeof(RandomScoreSerializer))]
    public class RandomScoreFunction : ScoreFunctionBase
    {
        public Int64 Seed { get; set; }

        public RandomScoreFunction(Int64 seed)
        {
            Seed = seed;
        }
    }
}
