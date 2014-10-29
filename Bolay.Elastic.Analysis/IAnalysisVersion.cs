using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis
{
    public interface IAnalysisVersion
    {
        /// <summary>
        /// Gets the lucene version the desired functionality comes from.
        /// Defaults to the latest version available.
        /// </summary>
        Double? Version { get; }
    }
}
