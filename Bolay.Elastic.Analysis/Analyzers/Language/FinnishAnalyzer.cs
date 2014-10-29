using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Analyzers.Language
{
    public class FinnishAnalyzer : StemExclusionLanguageAnalyzerBase
    {
        public FinnishAnalyzer(string name) : base(name, AnalyzerTypeEnum.Finnish) { }
    }
}
