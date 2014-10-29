using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.ConstantScore
{
    public class ConstantScoreQuery : ConstantScoreQueryBase
    {
        public ConstantScoreQuery(IQuery query) : base(query) { }
    }
}
