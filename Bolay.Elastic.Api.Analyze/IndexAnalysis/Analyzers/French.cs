using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.Analyzers
{
    public class French : StemExclusionLanguageBase
    {
        public French() : base(AnalyzerEnum.French) { }
    }
}
