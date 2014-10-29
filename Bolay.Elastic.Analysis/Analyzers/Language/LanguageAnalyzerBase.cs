using Bolay.Elastic.Analysis.Analyzers.Stop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Analyzers.Language
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-lang-analyzer.html
    /// </summary>
    [JsonConverter(typeof(LanguageAnalyzerSerializer))]
    public abstract class LanguageAnalyzerBase : StopAnalyzer
    {
        /// <summary>
        /// Creates a language analyzer.
        /// </summary>
        /// <param name="name">Sets the name of the analyzer.</param>
        /// <param name="type">Sets the language analyzer type.</param>
        public LanguageAnalyzerBase(string name, AnalyzerTypeEnum type)
            : base(name, type)
        { }
    }
}
