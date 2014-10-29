using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Exceptions
{
    public class CharacterFilterNotDefinedException : AnalysisNotDefinedException
    {
        public CharacterFilterNotDefinedException(string name) : base(name) { }
    }
}
