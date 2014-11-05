using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Fields._Analyzer
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/mapping-analyzer-field.html
    /// </summary>
    [JsonConverter(typeof(DocumentAnalyzerSerializer))]
    public class DocumentAnalyzer
    {
        internal const string PATH = "path";
        internal const string PATH_DEFAULT = "_analyzer";

        /// <summary>
        /// Gets the path of the document default analyzer.
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// Creates the path for the default analyzer for a document.
        /// </summary>
        /// <param name="path">Sets the path of the default analyzer for this document.</param>
        public DocumentAnalyzer(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException("path", "DocumentAnalyzer requires a path.");

            Path = path;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is DocumentAnalyzer))
                return false;

            if (obj == null)
                return false;

            DocumentAnalyzer analyzer = obj as DocumentAnalyzer;

            if (this.Path.Equals(analyzer.Path))
                return true;

            return false;
        }
    }
}
