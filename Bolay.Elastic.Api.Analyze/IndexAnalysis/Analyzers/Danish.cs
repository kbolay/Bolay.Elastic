using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.Analyzers
{
    public class Danish : StemExclusionLanguageBase
    {
        public Danish() : base(AnalyzerEnum.Danish) { }
    }
}
