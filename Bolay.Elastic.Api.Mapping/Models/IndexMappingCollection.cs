using Bolay.Elastic.Analysis;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Mapping.Models
{
    [JsonConverter(typeof(IndexMappingCollectionSerializer))]
    internal class IndexMappingCollection : List<IndexMapping>
    {
        internal static Dictionary<string, AnalysisSettings> IndicesAnalysis;

        internal static void PopulateIndicesAnalysis(Settings.Models.Settings indexSettings)
        {
            if (indexSettings == null)
                return;

            if (IndicesAnalysis == null)
                IndicesAnalysis = new Dictionary<string, AnalysisSettings>();

            IndicesAnalysis[indexSettings.IndexName] = indexSettings.Analysis;
        }
        internal static void PopulateIndicesAnalysis(IEnumerable<Settings.Models.Settings> settings)
        {
            if (settings == null)
                return;

            if (IndicesAnalysis == null)
                IndicesAnalysis = new Dictionary<string, AnalysisSettings>();

            foreach (Settings.Models.Settings indexSettings in settings)
            {
                IndicesAnalysis[indexSettings.IndexName] = indexSettings.Analysis;
            }
        }
        internal static AnalysisSettings GetIndexAnalysis(string indexName)
        {
            AnalysisSettings analysis = null;
            if (IndicesAnalysis != null && IndicesAnalysis.ContainsKey(indexName))
            {
                analysis = IndicesAnalysis[indexName];
            }

            return analysis;
        }
    }
}
