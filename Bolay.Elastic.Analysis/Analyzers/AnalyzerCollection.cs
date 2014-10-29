using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Analyzers
{
    [JsonConverter(typeof(AnalyzerCollectionSerializer))]
    internal class AnalyzerCollection : List<IAnalyzer>
    {
    }
}
