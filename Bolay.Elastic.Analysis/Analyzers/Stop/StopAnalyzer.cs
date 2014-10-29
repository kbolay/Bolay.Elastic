using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Analyzers.Stop
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-stop-analyzer.html
    /// </summary>
    [JsonConverter(typeof(StopAnalyzerSerializer))]
    public class StopAnalyzer : AnalyzerBase
    {
        internal const string _EMPTY_STOPWORDS_DEFAULT = "_none_";

        /// <summary>
        /// Gets or sets the stopwords list. An empty list will use the _none_ value. A null value will use the default english stopwords.
        /// Defaults to null (english stopwords).
        /// </summary>
        public IEnumerable<string> Stopwords { get; set; }

        /// <summary>
        /// Gets or sets the relative or absolute path to the stowords configuration file.
        /// </summary>
        public string StopwordsPath { get; set; }

        /// <summary>
        /// Creates a stop analyzer.
        /// </summary>
        /// <param name="name">Sets the name of the analyzer.</param>
        public StopAnalyzer(string name) : base(name, AnalyzerTypeEnum.Stop) { }

        /// <summary>
        /// This constructor is for items that inherit the StopAnalyzer.
        /// </summary>
        /// <param name="name">Sets the name of the analyzer.</param>
        /// <param name="type">Sets the type of the analyzer.</param>
        protected StopAnalyzer(string name, AnalyzerTypeEnum type) : base(name, type) { }

    }
}
