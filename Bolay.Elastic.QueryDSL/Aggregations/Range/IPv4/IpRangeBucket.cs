using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.Range.IPv4
{
    public class IpRangeBucket
    {
        public string To { get; set; }
        public string From { get; set; }
    }
}
