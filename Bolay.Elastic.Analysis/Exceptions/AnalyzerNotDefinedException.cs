using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Exceptions
{
    public class AnalyzerNotDefinedException : AnalysisNotDefinedException
    {
        public AnalyzerNotDefinedException(string name) : base(name) { }
    }
}
