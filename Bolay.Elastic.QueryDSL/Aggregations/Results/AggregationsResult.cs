using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.Results
{
    [JsonConverter(typeof(AggregationsResultSerializer))]
    public class AggregationsResult
    {
        public IEnumerable<IAggregationResult> Aggregations { get; set; }
    }
}
