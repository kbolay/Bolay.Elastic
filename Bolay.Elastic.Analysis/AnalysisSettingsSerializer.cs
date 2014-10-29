using Bolay.Elastic.Analysis.Analyzers;
using Bolay.Elastic.Analysis.Analyzers.Custom;
using Bolay.Elastic.Analysis.Filters.Characters;
using Bolay.Elastic.Analysis.Filters.Tokens;
using Bolay.Elastic.Analysis.Tokenizers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis
{
    internal class AnalysisSettingsSerializer : JsonConverter
    {
        private const string _TOKENIZER = "tokenizer";
        private const string _TOKEN_FILTERS = "filter";
        private const string _CHARACTER_FILTERS = "char_filter";
        private const string _ANALYZER = "analyzer";
        private const string _TYPE = "type";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> analysisDict = serializer.Deserialize<Dictionary<string, object>>(reader);

            AnalysisSettings settings = new AnalysisSettings();

            if (analysisDict.ContainsKey(_CHARACTER_FILTERS))
            {
                settings.CharacterFilters = JsonConvert.DeserializeObject<CharacterFilterCollection>(analysisDict.GetString(_CHARACTER_FILTERS));
            }

            if (analysisDict.ContainsKey(_TOKEN_FILTERS))
            {
                settings.TokenFilters = JsonConvert.DeserializeObject<TokenFilterCollection>(analysisDict.GetString(_TOKEN_FILTERS));
            }

            if (analysisDict.ContainsKey(_TOKENIZER))
            {
                settings.Tokenizers = JsonConvert.DeserializeObject<TokenizerCollection>(analysisDict.GetString(_TOKENIZER));
            }

            if (analysisDict.ContainsKey(_ANALYZER))
            {
                CustomAnalyzerSerializer._AnalysisSettings = settings;
                settings.Analyzers = JsonConvert.DeserializeObject<AnalyzerCollection>(analysisDict.GetString(_ANALYZER));
            }

            return settings;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
