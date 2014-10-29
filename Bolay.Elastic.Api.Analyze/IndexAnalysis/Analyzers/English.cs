using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.Analyzers
{
    public class English : StemExclusionLanguageBase
    {
        public English() : base(AnalyzerEnum.English) { }
    }
}
