using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.TokenFilters
{
    public class KStem : TokenFilterBase
    {
        public KStem() : base(TokenFilterEnum.KStem) { }
    }
}
