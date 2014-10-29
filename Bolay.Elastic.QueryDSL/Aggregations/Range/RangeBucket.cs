using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.Range
{
    public class RangeBucket
    {
        public string Key { get; set; }
        public object GreaterThan { get; set; }
        public object LessThan { get; set; }
        public object GreaterThanOrEqualTo { get; set; }
        public object LessThanOrEqualTo { get; set; }
    }
}
