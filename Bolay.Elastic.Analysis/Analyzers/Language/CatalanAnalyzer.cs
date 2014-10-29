using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Analyzers.Language
{
    public class CatalanAnalyzer : StemExclusionLanguageAnalyzerBase
    {
        public CatalanAnalyzer(string name) : base(name, AnalyzerTypeEnum.Catalan) { }
    }
}
