using Bolay.Elastic.Api.Analyze.IndexAnalysis.Analyzers;
using Bolay.Elastic.Api.Analyze.IndexAnalysis.CharacterFilters;
using Bolay.Elastic.Api.Analyze.IndexAnalysis.TokenFilters;
using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.Serialization
{
    public class AnalysisSerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is IEnumerable<AnalyzerBase>))
                throw new SerializeTypeException<IEnumerable<AnalyzerBase>>();

            IEnumerable<AnalyzerBase> analyzerList = value as IEnumerable<AnalyzerBase>;

            Dictionary<string, object> analyzersDict = new Dictionary<string, object>();
            Dictionary<string, object> tokenizersDict = new Dictionary<string, object>();
            Dictionary<string, object> tokenFiltersDict = new Dictionary<string, object>();
            Dictionary<string, object> charFiltersDict = new Dictionary<string, object>();

            foreach (AnalyzerBase analyzer in analyzerList)
            {
                if (analyzer is Custom)
                {
                    Custom customAnalyzer = analyzer as Custom;
                    analyzersDict.Add(customAnalyzer.Name, analyzer);

                    if (customAnalyzer.Tokenizer != null)
                        tokenizersDict.Add(customAnalyzer.Tokenizer.Name, customAnalyzer.Tokenizer);
                    if (customAnalyzer.TokenFilters != null && customAnalyzer.TokenFilters.Any())
                    {
                        foreach (TokenFilterBase tokenFilter in customAnalyzer.TokenFilters)
                            tokenFiltersDict.Add(tokenFilter.Name, tokenFilter);
                    }
                    if (customAnalyzer.CharacterFilters != null && customAnalyzer.CharacterFilters.Any())
                    {
                        foreach (CharacterFilterBase characterFilter in customAnalyzer.CharacterFilters)
                            charFiltersDict.Add(characterFilter.Name, characterFilter);
                    }
                }
                else
                    analyzersDict.Add(analyzer.Name, analyzer);
            }

            Dictionary<string, object> rootDict = new Dictionary<string,object>();
            Dictionary<string, object> analysisDict = new Dictionary<string, object>();

            if (analyzersDict.Any())
                analysisDict.Add("analyzer", analyzersDict);
            if (tokenizersDict.Any())
                analysisDict.Add("tokenizer", tokenizersDict);
            if (tokenFiltersDict.Any())
                analysisDict.Add("filter", tokenFiltersDict);
            if (charFiltersDict.Any())
                analysisDict.Add("char_filter", charFiltersDict);

            if (analysisDict.Any())
                rootDict.Add("analysis", analysisDict);
            
            if(rootDict.Any())
                serializer.Serialize(writer, rootDict);
        }
    }
}
