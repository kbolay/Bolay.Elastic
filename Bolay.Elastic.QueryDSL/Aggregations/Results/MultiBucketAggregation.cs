using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.Results
{
    public class MultiBucketAggregation : BucketAggregation
    {
        [JsonProperty("buckets")]
        public IEnumerable<BucketAggregation> Buckets { get; set; }
    }
}
