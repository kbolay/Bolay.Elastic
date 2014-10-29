using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Analyzers.Whitespace
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-whitespace-analyzer.html
    /// </summary>
    [JsonConverter(typeof(WhitespaceAnalyzerSerializer))]
    public class WhitespaceAnalyzer : AnalyzerBase
    {
        /// <summary>
        /// Creats a whitespace analyzer.
        /// </summary>
        /// <param name="name">Sets the name of the analyzer.</param>
        public WhitespaceAnalyzer(string name) : base(name, AnalyzerTypeEnum.Whitespace) { }
    }
}
