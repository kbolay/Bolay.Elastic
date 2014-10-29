using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters
{
    public class FilterBase : IFilter
    {
        public bool Cache { get; set; }
        public string CacheKey { get; set; }
        public string FilterName { get; set; }
    }
}
