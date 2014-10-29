using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Analyzers.Language
{
    public class ArabicAnalyzer : StemExclusionLanguageAnalyzerBase
    {
        public ArabicAnalyzer(string name) : base(name, AnalyzerTypeEnum.Arabic) { }
    }
}
