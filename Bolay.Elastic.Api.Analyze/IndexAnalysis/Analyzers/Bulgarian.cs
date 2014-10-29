using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.Analyzers
{
    public class Bulgarian : StemExclusionLanguageBase
    {
        public Bulgarian() : base(AnalyzerEnum.Bulgarian) { }
    }
}
