using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.Results
{
    public class BucketAggregation : AggregationResultBase
    {
        public object Key { get; set; }
        public string KeyAsString { get; set; }
        public Int64 DocumentCount { get; set; }
        public IEnumerable<IAggregationResult> AggregationResults { get; set; } 
    }
}
