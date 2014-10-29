using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.Analyzers
{
    public class Simple : AnalyzerBase
    {
        public Simple() : base(AnalyzerEnum.Simple) { }
    }
}
