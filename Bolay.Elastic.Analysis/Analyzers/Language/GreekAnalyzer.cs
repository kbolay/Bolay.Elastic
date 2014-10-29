using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Analyzers.Language
{
    public class GreekAnalyzer : LanguageAnalyzerBase
    {
        public GreekAnalyzer(string name) : base(name, AnalyzerTypeEnum.Greek) { }
    }
}
