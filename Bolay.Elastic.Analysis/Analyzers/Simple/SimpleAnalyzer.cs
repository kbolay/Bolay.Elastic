using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Analyzers.Simple
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-simple-analyzer.html
    /// </summary>
    [JsonConverter(typeof(SimpleAnalyzerSerializer))]
    public class SimpleAnalyzer : AnalyzerBase
    {
        /// <summary>
        /// Creates a simple analyzer.
        /// </summary>
        /// <param name="name">Sets the name of the analyzer.</param>
        public SimpleAnalyzer(string name) : base(name, AnalyzerTypeEnum.Simple) { }
    }
}
