using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Analyzers.Standard
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-standard-analyzer.html
    /// </summary>
    [JsonConverter(typeof(StandardAnalyzerSerializer))]
    public class StandardAnalyzer : AnalyzerBase
    {
        internal const int _MAXIMUM_TOKEN_LENGTH_DEFAULT = 255;

        /// <summary>
        /// Gets or sets the stopword list.
        /// Defaults to null.
        /// </summary>
        public IEnumerable<string> Stopwords { get; set; }

        /// <summary>
        /// Gets or sets the max_token_length.
        /// Defaults to 255.
        /// </summary>
        public int MaximumTokenLength { get; set; }

        /// <summary>
        /// Create a standard analyzer.
        /// </summary>
        /// <param name="name">Sets the name of the analyzer</param>
        public StandardAnalyzer(string name)
            : base(name, AnalyzerTypeEnum.Standard)
        {
            MaximumTokenLength = _MAXIMUM_TOKEN_LENGTH_DEFAULT;
        }
    }
}
