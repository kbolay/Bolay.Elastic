using Bolay.Elastic.QueryDSL.Queries.Scoring.Functions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Scoring
{
    public class FunctionScoreQuery : FunctionScoreQueryBase
    {
        public IQuery Query { get { return SearchPiece as IQuery; } }

        public FunctionScoreQuery(IQuery query, IEnumerable<ScoreFunctionBase> scoreFunctions)
            : base(query, scoreFunctions)
        { }
    }
}
