using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Suggesters.Completion
{
    public class CompletionSerializer : JsonConverter
    {
        private const string _FUZZY = "fuzzy";
        private const string _FUZZINESS = "fuzziness";
        private const string _TRANSPOSITIONS = "transpositions";
        private const string _MINIMUM_LENGTH = "min_length";
        private const string _PREFIX_LENGTH = "prefix_length";
        private const string _IS_UNICODE_AWARE = "unicode_aware";

        internal const object _FUZZINESS_DEFAULT = null;
        internal const bool _TRANSPOSITIONS_DEFAULT = true;
        internal const int _MINIMUM_LENGTH_DEFAULT = 3;
        internal const int _PREFIX_LENGTH_DEFAULT = 1;
        internal const bool _IS_UNICODE_AWARE_DEFAULT = false;

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> wholeDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> suggestDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(wholeDict.First().Value.ToString());
            Dictionary<string, object> compDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(suggestDict.GetString(SuggestTypeEnum.Completion.ToString()));

            FuzzyCompletion fuzzy = null;
            if (compDict.ContainsKey(_FUZZY))
            {
                Dictionary<string, object> fuzzyDict = null;
                try { fuzzyDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(compDict.GetString(_FUZZY)); }
                catch { }

                if (fuzzyDict != null)
                {
                    fuzzy = new FuzzyCompletion();
                    if (fuzzyDict.ContainsKey(_FUZZINESS))
                        fuzzy.Fuzziness = fuzzyDict[_FUZZINESS];
                    else
                        fuzzy.Fuzziness = _FUZZINESS_DEFAULT;

                    fuzzy.IsUnicodeAware = fuzzyDict.GetBool(_IS_UNICODE_AWARE, _IS_UNICODE_AWARE_DEFAULT);
                    fuzzy.MinimumLength = fuzzyDict.GetInt32(_MINIMUM_LENGTH, _MINIMUM_LENGTH_DEFAULT);
                    fuzzy.PrefixLength = fuzzyDict.GetInt32(_PREFIX_LENGTH, _PREFIX_LENGTH_DEFAULT);
                    fuzzy.Transpositions = fuzzyDict.GetBool(_TRANSPOSITIONS, _TRANSPOSITIONS_DEFAULT);
                }
                else if(compDict.GetBool(_FUZZY))
                {
                    fuzzy = new FuzzyCompletion();
                }
            }

            CompletionSuggester suggestor = null;
            if(fuzzy == null)
                suggestor = new CompletionSuggester(wholeDict.First().Key, compDict.GetString(SuggestSerializer._FIELD));
            else
                suggestor = new CompletionSuggester(wholeDict.First().Key, compDict.GetString(SuggestSerializer._FIELD), fuzzy);
            suggestor.Size = compDict.GetInt32(SuggestSerializer._SIZE, SuggestSerializer._SIZE_DEFAULT);
            suggestor.Text = suggestDict.GetStringOrDefault(SuggestSerializer._TEXT);

            return suggestor;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is CompletionSuggester))
                throw new SerializeTypeException<CompletionSuggester>();

            CompletionSuggester suggestor = value as CompletionSuggester;

            Dictionary<string, object> compDict = new Dictionary<string, object>();
            compDict.AddObject(SuggestSerializer._FIELD, suggestor.Field);
            compDict.AddObject(SuggestSerializer._SIZE, suggestor.Size, SuggestSerializer._SIZE_DEFAULT);
            if (suggestor.Fuzzy != null)
            {
                Dictionary<string, object> fuzzyDict = new Dictionary<string, object>();
                fuzzyDict.AddObject(_FUZZINESS, suggestor.Fuzzy.Fuzziness, _FUZZINESS_DEFAULT);
                fuzzyDict.AddObject(_IS_UNICODE_AWARE, suggestor.Fuzzy.IsUnicodeAware, _IS_UNICODE_AWARE_DEFAULT);
                fuzzyDict.AddObject(_MINIMUM_LENGTH, suggestor.Fuzzy.MinimumLength, _MINIMUM_LENGTH_DEFAULT);
                fuzzyDict.AddObject(_PREFIX_LENGTH, suggestor.Fuzzy.PrefixLength, _PREFIX_LENGTH_DEFAULT);
                fuzzyDict.AddObject(_TRANSPOSITIONS, suggestor.Fuzzy.Transpositions, _TRANSPOSITIONS_DEFAULT);

                compDict.Add(_FUZZY, fuzzyDict);
            }

            Dictionary<string, object> internalDict = new Dictionary<string, object>();
            internalDict.AddObject(SuggestSerializer._TEXT, suggestor.Text);
            internalDict.Add(SuggestTypeEnum.Completion.ToString(), compDict);

            Dictionary<string, object> suggestDict = new Dictionary<string, object>();
            suggestDict.Add(suggestor.SuggestName, internalDict);

            serializer.Serialize(writer, suggestDict);
        }
    }
}
