using Bolay.Elastic.Analysis;
using Bolay.Elastic.Analysis.Analyzers;
using Bolay.Elastic.Analysis.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping
{
    public class PropertyAnalyzer
    {
        private const string _ANALYZER = "analyzer";
        private const string _INDEX_ANALYZER = "index_analyzer";
        private const string _SEARCH_ANALYZER = "search_analyzer";

        internal static IEnumerable<IAnalyzer> _KnownAnalyzers { get; set; }

        /// <summary>
        /// Gets the analyzer for searching and indexing a property.
        /// </summary>
        public IAnalyzer Analyzer { get; private set; }

        /// <summary>
        /// Gets the analyzer used when indexing the property.
        /// </summary>
        public IAnalyzer IndexAnalyzer { get; private set; }

        /// <summary>
        /// Gets the analyzer used against the search value before comparing it to the tokens.
        /// </summary>
        public IAnalyzer SearchAnalyzer { get; private set; }

        /// <summary>
        /// Creates a property analyzer.
        /// </summary>
        /// <param name="analyzer">Sets the analyzer for indexing and searching.</param>
        public PropertyAnalyzer(IAnalyzer analyzer)
        {
            if (analyzer == null)
                throw new ArgumentNullException("analyzer", "PropertyAnalyzer expects an analyzer in this constructor.");

            Analyzer = analyzer;
        }

        /// <summary>
        /// Create a property analyzer that uses different analysis for indexing and searching.
        /// </summary>
        /// <param name="indexAnalyzer">Sets the analyzer to use for indexing a property.</param>
        /// <param name="searchAnalyzer">Sets the analyzer to use for searching a property. Optional.</param>
        public PropertyAnalyzer(IAnalyzer indexAnalyzer, IAnalyzer searchAnalyzer)
        {
            if (indexAnalyzer == null)
                throw new ArgumentNullException("indexAnalyzer", "PropertyAnalyzer expects an indexAnalyzer in this constructor.");

            IndexAnalyzer = indexAnalyzer;

            if (searchAnalyzer != null)
                SearchAnalyzer = searchAnalyzer;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is PropertyAnalyzer))
                return false;

            if (obj == null)
                return false;

            PropertyAnalyzer analyzer = obj as PropertyAnalyzer;

            if (this.Analyzer == analyzer.Analyzer && this.IndexAnalyzer == analyzer.IndexAnalyzer && this.SearchAnalyzer == analyzer.SearchAnalyzer)
            {
                return true;
            }

            return false;
        }

        public static void PopulateIndexAnalyzers(AnalysisSettings analysisSettings)
        {
            if(analysisSettings != null)
                _KnownAnalyzers = analysisSettings.Analyzers;
        }

        internal static void Serialize(PropertyAnalyzer analyzer, Dictionary<string, object> fieldDict)
        {
            if (analyzer == null)
                return;

            if (fieldDict == null)
                fieldDict = new Dictionary<string, object>();

            if (analyzer.Analyzer != null)
            {
                fieldDict.Add(_ANALYZER, analyzer.Analyzer.Name);
            }
            else
            {
                fieldDict.AddObject(_INDEX_ANALYZER, analyzer.IndexAnalyzer.Name);
                if(analyzer.SearchAnalyzer != null)
                {
                    fieldDict.AddObject(_SEARCH_ANALYZER, analyzer.SearchAnalyzer.Name);
                }                
            }
        }

        internal static PropertyAnalyzer Deserialize(Dictionary<string, object> fieldDict)
        {
            if (fieldDict == null || !fieldDict.Any())
                return null;

            PropertyAnalyzer propertyAnalyzer = null;
            if (fieldDict.ContainsKey(_ANALYZER))
            {
                propertyAnalyzer = new PropertyAnalyzer(GetAnalyzer(fieldDict.GetString(_ANALYZER)));
            }
            else if (fieldDict.ContainsKey(_INDEX_ANALYZER))
            {
                IAnalyzer searchAnalyzer = null;
                try 
                { 
                    searchAnalyzer = GetAnalyzer(fieldDict.GetStringOrDefault(_SEARCH_ANALYZER));
                }
                catch{}

                propertyAnalyzer = new PropertyAnalyzer(
                    GetAnalyzer(fieldDict.GetString(_INDEX_ANALYZER)),
                    searchAnalyzer);
            }

            return propertyAnalyzer;
        }

        private static IAnalyzer GetAnalyzer(string analyzerName)
        {
            AnalyzerTypeEnum analyzerType = AnalyzerTypeEnum.Standard;
            IAnalyzer analyzer = null;

            if (_KnownAnalyzers != null)
            {
                analyzer = _KnownAnalyzers.FirstOrDefault(x => x.Name.Equals(analyzerName));
            }

            if (analyzer == null)
            {
                analyzerType = AnalyzerTypeEnum.Find(analyzerName);
                try
                {
                    analyzer = Activator.CreateInstance(analyzerType.ImplementationType, new object[] { analyzerName }) as IAnalyzer;
                }
                catch
                {
                    throw new AnalyzerNotDefinedException(analyzerName);
                }
            }

            return analyzer;
        }
    }
}
