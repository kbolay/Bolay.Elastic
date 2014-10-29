using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.TokenFilters
{
    public class Reverse : TokenFilterBase
    {
        public Reverse() : base(TokenFilterEnum.Reverse) { }
    }
}
