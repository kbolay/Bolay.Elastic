using Bolay.Elastic.Analysis.Exceptions;
using Bolay.Elastic.Analysis.Filters.Characters;
using Bolay.Elastic.Analysis.Filters.Tokens;
using Bolay.Elastic.Analysis.Tokenizers;
using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Analyzers.Custom
{
    internal class CustomAnalyzerSerializer : JsonConverter
    {
        private const string _TOKENIZER = "tokenizer";
        private const string _TOKEN_FILTERS = "filter";
        private const string _CHARACTER_FILTERS = "char_filter";
        private const string _ANALYZER = "analyzer";
        private const string _TYPE = "type";

        internal static AnalysisSettings _AnalysisSettings { get; set; }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> analysisDict = serializer.Deserialize<Dictionary<string, object>>(reader);

            if (_AnalysisSettings == null)
            {
                throw new Exception("AnalysisSettings must be present to deserialize a custom analyzer.");
            }

            Dictionary<string, object> analyzerDict = null;
            if (analysisDict.ContainsKey(_ANALYZER))
            {
                analyzerDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(analysisDict.GetString(_ANALYZER));
            }
            else
            { 
                analyzerDict = analysisDict;
            }
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(analyzerDict.First().Value.ToString());

            string tokenizerName = fieldDict.GetString(_TOKENIZER);
            ITokenizer tokenizer = _AnalysisSettings.Tokenizers.FirstOrDefault(x => x.Name.Equals(tokenizerName));
            if (tokenizer == null)
            {
                TokenizerTypeEnum tokenizerType = TokenizerTypeEnum.Standard;
                tokenizerType = TokenizerTypeEnum.Find(tokenizerName);
                try
                {
                    tokenizer = Activator.CreateInstance(tokenizerType.ImplementationType, new object[] { tokenizerName }) as ITokenizer;
                }
                catch
                {
                    throw new TokenizerNotDefinedException(tokenizerName);
                }                
            }                

            CustomAnalyzer analyzer = new CustomAnalyzer(analyzerDict.First().Key, tokenizer);
            AnalyzerBase.Deserialize(analyzer, fieldDict);

            if (fieldDict.ContainsKey(_TOKEN_FILTERS))
            {
                List<ITokenFilter> filters = new List<ITokenFilter>();
                foreach (string tokenFilterName in JsonConvert.DeserializeObject<IEnumerable<string>>(fieldDict.GetString(_TOKEN_FILTERS)))
                {
                    ITokenFilter tokenFilter = _AnalysisSettings.TokenFilters.FirstOrDefault(x => x.Name.Equals(tokenFilterName));
                    if (tokenFilter == null)
                    {
                        TokenFilterTypeEnum filterType = TokenFilterTypeEnum.Standard;
                        filterType = TokenFilterTypeEnum.Find(tokenFilterName);

                        try
                        {
                            tokenFilter = Activator.CreateInstance(filterType.ImplementationType, new object[] { tokenFilterName }) as ITokenFilter;
                        }
                        catch
                        {
                            throw new TokenFilterNotDefinedException(tokenFilterName);
                        } 
                    }                        

                    filters.Add(tokenFilter);
                }

                if (filters.Any())
                {
                    analyzer.TokenFilters = filters;
                }
            }

            if (fieldDict.ContainsKey(_CHARACTER_FILTERS))
            {
                List<ICharacterFilter> charFilters = new List<ICharacterFilter>();
                foreach (string charFilterName in JsonConvert.DeserializeObject<IEnumerable<string>>(fieldDict.GetString(_CHARACTER_FILTERS)))
                {
                    ICharacterFilter charFilter = _AnalysisSettings.CharacterFilters.FirstOrDefault(x => x.Name.Equals(charFilterName));
                    if (charFilter == null)
                    {
                        CharacterFilterTypeEnum filterType = CharacterFilterTypeEnum.Mapping;
                        filterType = CharacterFilterTypeEnum.Find(charFilterName);
                        
                        try
                        {
                            charFilter = Activator.CreateInstance(filterType.ImplementationType, new object[] { charFilterName }) as ICharacterFilter;
                        }
                        catch
                        {
                            throw new CharacterFilterNotDefinedException(charFilterName);   
                        }                        
                    }                       

                    charFilters.Add(charFilter);
                }

                if (charFilters.Any())
                {
                    analyzer.CharacterFilters = charFilters;
                }
            }

            return analyzer;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is CustomAnalyzer))
                throw new SerializeTypeException<CustomAnalyzer>();

            CustomAnalyzer analyzer = value as CustomAnalyzer;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            AnalyzerBase.Serialize(analyzer, fieldDict);

            fieldDict.AddObject(_TOKENIZER, analyzer.Tokenizer.Name);

            if (analyzer.TokenFilters != null && analyzer.TokenFilters.Any(x => x != null))
            {
                fieldDict.AddObject(_TOKEN_FILTERS, analyzer.TokenFilters.Where(x => x != null).Select(x => x.Name));
            }

            if (analyzer.CharacterFilters != null && analyzer.CharacterFilters.Any(x => x != null))
            {
                fieldDict.AddObject(_CHARACTER_FILTERS, analyzer.CharacterFilters.Where(x => x != null).Select(x => x.Name));
            }

            Dictionary<string, object> analyzerDict = new Dictionary<string, object>();
            analyzerDict.Add(analyzer.Name, fieldDict);

            serializer.Serialize(writer, analyzerDict);
        }
    }
}
