using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Exceptions
{
    public class TokenizerNotDefinedException : AnalysisNotDefinedException
    {
        public TokenizerNotDefinedException(string name) : base(name) { }
    }
}
