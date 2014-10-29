using Bolay.Elastic.QueryDSL.Filters;
using Bolay.Elastic.QueryDSL.Queries.Scoring.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Scoring
{
    public class FunctionScoreFilter : FunctionScoreQueryBase
    {
        public IFilter Filter { get { return SearchPiece as IFilter; } }

        public FunctionScoreFilter(IFilter filter, IEnumerable<ScoreFunctionBase> scoreFunctions)
            : base(filter, scoreFunctions)
        { }
    }
}
