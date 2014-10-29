using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Exceptions
{
    public abstract class AnalysisNotDefinedException : Exception
    {
        public string Name { get; private set; }

        public AnalysisNotDefinedException(string name)
            : base(name + " was not defined.")
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name", "All AnalysisNotDefinedExceptions expects a name.");

            Name = name;
        }
    }
}
