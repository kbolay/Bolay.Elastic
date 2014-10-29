using Bolay.Elastic.Distance;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.Results
{
    public class DistanceAggregation : RangeAggregation
    {
        [JsonProperty("unit")]
        public DistanceUnitEnum Unit { get; set; }
    }
}
