using Bolay.Elastic.Analysis.Analyzers;
using Bolay.Elastic.Analysis.Filters.Characters;
using Bolay.Elastic.Analysis.Filters.Tokens;
using Bolay.Elastic.Analysis.Tokenizers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis
{
    [JsonConverter(typeof(AnalysisSettingsSerializer))]
    public class AnalysisSettings
    {
        public IEnumerable<IAnalyzer> Analyzers { get; set; }
        public IEnumerable<ITokenizer> Tokenizers { get; set; }
        public IEnumerable<ITokenFilter> TokenFilters { get; set; }
        public IEnumerable<ICharacterFilter> CharacterFilters { get; set; } 
    }
}
