using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Analyzers
{
    public interface IAnalyzer : IAnalysisVersion
    {
        string Name { get; }
        IEnumerable<string> Aliases { get; }
        AnalyzerTypeEnum Type { get; }
    }
}
