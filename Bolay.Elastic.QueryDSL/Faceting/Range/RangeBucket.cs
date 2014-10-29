using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting.Range
{
    public class RangeBucket
    {
        public object GreaterThan { get; set; }
        public object LessThan { get; set; }
        public object GreaterThanOrEqualTo { get; set; }
        public object LessThanOrEqualTo { get; set; }
    }
}
