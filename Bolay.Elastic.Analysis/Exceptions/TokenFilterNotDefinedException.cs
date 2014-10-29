using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Exceptions
{
    public class TokenFilterNotDefinedException : AnalysisNotDefinedException
    {
        public TokenFilterNotDefinedException(string name) : base(name) { }
    }
}
