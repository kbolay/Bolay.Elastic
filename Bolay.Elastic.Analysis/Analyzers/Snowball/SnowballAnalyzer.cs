using Bolay.Elastic.Analysis.Analyzers.Stop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Analyzers.Snowball
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-snowball-analyzer.html
    /// </summary>
    [JsonConverter(typeof(SnowballAnalyzerSerializer))]
    public class SnowballAnalyzer : StopAnalyzer
    {
        internal static SnowballLanguageEnum _LANGUAGE_DEFAULT = SnowballLanguageEnum.English;

        /// <summary>
        /// Gets the language of the snowball analyzer.
        /// Defaults to English.
        /// </summary>
        public SnowballLanguageEnum Language { get; set; }

        /// <summary>
        /// Creates the snowball analyzer.
        /// </summary>
        /// <param name="name"></param>
        public SnowballAnalyzer(string name) 
            : base(name, AnalyzerTypeEnum.Snowball) 
        {
            Language = _LANGUAGE_DEFAULT;
        }
    }
}
