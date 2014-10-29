using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Analyzers.Keyword
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-keyword-analyzer.html
    /// </summary>
    [JsonConverter(typeof(KeywordAnalyzerSerializer))]
    public class KeywordAnalyzer : AnalyzerBase
    {
        /// <summary>
        /// Creates the keyword analyzer.
        /// </summary>
        /// <param name="name">Sets the name.</param>
        public KeywordAnalyzer(string name) : base(name, AnalyzerTypeEnum.Keyword) { }
    }
}
