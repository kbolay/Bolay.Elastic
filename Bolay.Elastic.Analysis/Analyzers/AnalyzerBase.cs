using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Analyzers
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-analyzers.html
    /// </summary>
    public class AnalyzerBase : IAnalyzer
    {
        private const string _TYPE = "type";
        private const string _ALIASES = "aliases";
        private const string _VERSION = "version";

        /// <summary>
        /// Gets the name of the analyzer.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the type of the analyzer.
        /// </summary>
        public AnalyzerTypeEnum Type { get; private set; }

        /// <summary>
        /// Gets or sets the aliases for the analyzer.
        /// </summary>
        public IEnumerable<string> Aliases { get; set; }

        /// <summary>
        /// Gets or sets the lucene version to specify the desired functionality.
        /// </summary>
        public Double? Version { get; set; }

        /// <summary>
        /// Creates an analzyer.
        /// </summary>
        /// <param name="name">Sets the name of the analyzer.</param>
        /// <param name="type">Sets the type of the analyzer.</param>
        public AnalyzerBase(string name, AnalyzerTypeEnum type)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name", "All analyzers require a name.");
            if (type == null)
                throw new ArgumentNullException("type", "All analzyers require a type.");

            Name = name;
            Type = type;
        }

        internal static void Serialize(AnalyzerBase analyzer, Dictionary<string, object> fieldDict)
        {
            if (analyzer == null)
                return;

            if (fieldDict == null)
                fieldDict = new Dictionary<string, object>();

            fieldDict.Add(_TYPE, analyzer.Type.ToString());
            fieldDict.AddObject(_VERSION, analyzer.Version);
            if (analyzer.Aliases != null && analyzer.Aliases.Any())
            {
                fieldDict.Add(_ALIASES, analyzer.Aliases);
            }
        }

        internal static void Deserialize(AnalyzerBase analyzer, Dictionary<string, object> fieldDict)
        {
            if (fieldDict == null || !fieldDict.Any())
                return;

            analyzer.Version = fieldDict.GetDoubleOrNull(_VERSION);
            if (fieldDict.ContainsKey(_ALIASES))
            {
                analyzer.Aliases = JsonConvert.DeserializeObject<IEnumerable<string>>(fieldDict.GetString(_ALIASES));
            }
        }
    }
}
