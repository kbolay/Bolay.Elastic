using Bolay.Elastic.Exceptions;
using Bolay.Elastic.QueryDSL.Suggesters.Phrase.Smoothing;
using Bolay.Elastic.QueryDSL.Suggesters.Phrase.Smoothing.StupidBackoff;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Suggesters.Phrase
{
    public class PhraseSerializer : JsonConverter
    {
        private const string _DIRECT_GENERATOR = "direct_generator";
        private const string _GRAM_SIZE = "gram_size";
        private const string _REAL_WORD_ERROR_LIKELYHOOD = "real_word_error_likelihood";
        private const string _CONFIDENCE = "confidence";
        private const string _MAXIMUM_ERRORS = "max_errors";
        private const string _SEPARATOR = "separator";
        private const string _SHARD_SIZE = "shard_size";
        private const string _ANALYZER = "analyzer";

        internal const Double _REAL_WORD_ERROR_LIKELYHOOD_DEFAULT = 0.95;
        internal const Double _CONFIDENCE_DEFAULT = 1.0;
        internal const Double _MAXIMUM_ERRORS_DEFAULT = 1.0;
        internal const string _SEPARATOR_DEFAULT = " ";
        internal const int _SHARD_SIZE_DEFAULT = 5;
        internal static ISmoothing _SMOOTHING_DEFAULT = new StupidBackoffSmoothing();

        private static List<string> _KnownFields = new List<string>()
        {
            _DIRECT_GENERATOR,
            _GRAM_SIZE,
            _REAL_WORD_ERROR_LIKELYHOOD,
            _CONFIDENCE,
            _MAXIMUM_ERRORS,
            _SEPARATOR,
            _SHARD_SIZE,
            _ANALYZER,
            SuggestSerializer._FIELD,
            SuggestSerializer._SIZE,
            SuggestSerializer._TEXT
        };

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> wholeDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> suggestDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(wholeDict.First().Value.ToString());
            Dictionary<string, object> phraseDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(suggestDict.GetString(SuggestTypeEnum.Phrase.ToString()));

            PhraseSuggester suggestor = new PhraseSuggester(wholeDict.First().Key, phraseDict.GetString(SuggestSerializer._FIELD));
            suggestor.Text = suggestDict.GetStringOrDefault(SuggestSerializer._TEXT);
            suggestor.Analyzer = phraseDict.GetStringOrDefault(_ANALYZER);
            suggestor.Confidence = phraseDict.GetDouble(_CONFIDENCE, _CONFIDENCE_DEFAULT);
            if (phraseDict.ContainsKey(_DIRECT_GENERATOR))
                suggestor.DirectGenerators = JsonConvert.DeserializeObject<IEnumerable<DirectGenerator>>(phraseDict.GetString(_DIRECT_GENERATOR));
            suggestor.Field = phraseDict.GetStringOrDefault(SuggestSerializer._FIELD);
            suggestor.GramSize = phraseDict.GetInt32OrNull(_GRAM_SIZE);
            suggestor.MaximumErrors = phraseDict.GetDouble(_MAXIMUM_ERRORS, _MAXIMUM_ERRORS_DEFAULT);
            suggestor.RealWordErrorLikelyhood = phraseDict.GetDouble(_REAL_WORD_ERROR_LIKELYHOOD, _REAL_WORD_ERROR_LIKELYHOOD_DEFAULT);
            suggestor.Separator = phraseDict.GetString(_SEPARATOR, _SEPARATOR_DEFAULT);
            suggestor.ShardSize = phraseDict.GetInt32(_SHARD_SIZE, _SHARD_SIZE_DEFAULT);
            suggestor.Size = phraseDict.GetInt32(SuggestSerializer._SIZE, SuggestSerializer._SIZE_DEFAULT);
            
            SmoothingTypeEnum type = SmoothingTypeEnum.Laplace;
            KeyValuePair<string, object> smoothingKvp = phraseDict.FirstOrDefault(x => !_KnownFields.Contains(x.Key, StringComparer.OrdinalIgnoreCase) && SmoothingTypeEnum.Find(x.Key) != null);
            if(!string.IsNullOrWhiteSpace(smoothingKvp.Key))
            {
                type = SmoothingTypeEnum.Find(smoothingKvp.Key);
                suggestor.Smoothing = JsonConvert.DeserializeObject(smoothingKvp.Value.ToString(), type.ImplementationType) as ISmoothing;
            }
            suggestor.Text = suggestDict.GetStringOrDefault(SuggestSerializer._TEXT);

            return suggestor;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is PhraseSuggester))
                throw new SerializeTypeException<PhraseSuggester>();

            PhraseSuggester suggestor = value as PhraseSuggester;

            Dictionary<string, object> phraseDict = new Dictionary<string, object>();
            
            phraseDict.Add(SuggestSerializer._FIELD, suggestor.Field);
            phraseDict.AddObject(SuggestSerializer._SIZE, suggestor.Size, SuggestSerializer._SIZE_DEFAULT);
            phraseDict.AddObject(_ANALYZER, suggestor.Analyzer);
            phraseDict.AddObject(_CONFIDENCE, suggestor.Confidence, _CONFIDENCE_DEFAULT);
            phraseDict.AddObject(_DIRECT_GENERATOR, suggestor.DirectGenerators);
            phraseDict.AddObject(_GRAM_SIZE, suggestor.GramSize);
            phraseDict.AddObject(_MAXIMUM_ERRORS, suggestor.MaximumErrors, _MAXIMUM_ERRORS_DEFAULT);
            phraseDict.AddObject(_REAL_WORD_ERROR_LIKELYHOOD, suggestor.RealWordErrorLikelyhood, _REAL_WORD_ERROR_LIKELYHOOD_DEFAULT);
            phraseDict.AddObject(_SEPARATOR, suggestor.Separator, _SEPARATOR_DEFAULT);
            phraseDict.AddObject(_SHARD_SIZE, suggestor.ShardSize, suggestor.Size);

            if(suggestor.Smoothing != null && suggestor.Smoothing != _SMOOTHING_DEFAULT)
            {
                Dictionary<string, object> smoothingDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(suggestor.Smoothing));

                phraseDict.Add(smoothingDict.First().Key, smoothingDict.First().Value);
            }

            Dictionary<string, object> internalDict = new Dictionary<string, object>();
            internalDict.AddObject(SuggestSerializer._TEXT, suggestor.Text);
            internalDict.Add(SuggestTypeEnum.Phrase.ToString(), phraseDict);

            Dictionary<string, object> suggestDict = new Dictionary<string, object>();
            suggestDict.Add(suggestor.SuggestName, internalDict);

            serializer.Serialize(writer, suggestDict);
        }
    }
}
