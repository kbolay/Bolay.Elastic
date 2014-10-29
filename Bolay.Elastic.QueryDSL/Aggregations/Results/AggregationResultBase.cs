using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.Results
{
    public class AggregationResultBase : IAggregationResult
    {
        public string Name { get; set; }
    }
}
