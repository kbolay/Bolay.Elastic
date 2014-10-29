using Bolay.Elastic.QueryDSL.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Scoring.Functions
{
    public abstract class ScoreFunctionBase
    {
        public IFilter Filter { get; set; }
    }
}
