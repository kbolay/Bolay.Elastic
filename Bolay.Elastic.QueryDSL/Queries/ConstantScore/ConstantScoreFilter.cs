using Bolay.Elastic.QueryDSL.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.ConstantScore
{
    public class ConstantScoreFilter : ConstantScoreQueryBase
    {
        public ConstantScoreFilter(IFilter filter) : base(filter) { }
    }
}
