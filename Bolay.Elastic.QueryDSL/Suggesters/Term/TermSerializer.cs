using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Suggesters.Term
{
    public class TermSerializer : JsonConverter
    {
        private const string _ANALYZER = "analyzer";
        private const string _SORT = "sort";
        private const string _SUGGEST_MODE = "suggest_mode";
        private const string _LOWERCASE_TERMS = "lowercase_terms";
        private const string _MAXIMUM_EDIT_DISTANCE = "max_edits";
        private const string _PREFIX_LENGTH = "prefix_length";
        private const string _MINIMUM_WORD_LENGTH = "min_word_length";
        private const string _SHARD_SIZE = "shard_size";
        private const string _MAXIMUM_INSPECTIONS = "max_inspections";
        private const string _MINIMUM_DOCUMENT_FREQUENCY = "min_doc_freq";
        private const string _MAXIMUM_TERM_FREQUENCY = "max_term_freq";

        internal static SortModeEnum _SORT_DEFAULT = SortModeEnum.Score;
        internal static SuggestModeEnum _SUGGEST_MODE_DEFAULT = SuggestModeEnum.Missing;
        internal const bool _LOWERCASE_TERMS_DEFAULT = false;
        internal const int _PREFIX_LENGTH_DEFAULT = 1;
        internal const int _MINIMUM_WORD_LENGTH_DEFAULT = 4;
        internal const int _SHARD_SIZE_DEFAULT = 5;
        internal const int _MAXIMUM_INSPECTIONS_DEFAULT = 5;
        internal const Double _MINIMUM_DOCUMENT_FREQUENCY_DEFAULT = 0.0;
        internal const Double _MAXIMUM_TERM_FREQUENCY_DEFAULT = 0.01;
        internal const Double _MAXIMUM_EDIT_DISTANCE_DEFAULT = 2;

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> wholeDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> suggestDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(wholeDict.First().Value.ToString());
            Dictionary<string, object> termDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(suggestDict.GetString(SuggestTypeEnum.Term.ToString()));

            TermSuggester suggestor = new TermSuggester(wholeDict.First().Key, termDict.GetString(SuggestSerializer._FIELD));
            suggestor.Analyzer = termDict.GetStringOrDefault(_ANALYZER);
            suggestor.LowercaseTerms = termDict.GetBool(_LOWERCASE_TERMS, _LOWERCASE_TERMS_DEFAULT);
            suggestor.MaximumEditDistance = termDict.GetDouble(_MAXIMUM_EDIT_DISTANCE, _MAXIMUM_EDIT_DISTANCE_DEFAULT);
            suggestor.MaximumInspections = termDict.GetInt32(_MAXIMUM_INSPECTIONS, _MAXIMUM_INSPECTIONS_DEFAULT);
            suggestor.MaximumTermFrequency = termDict.GetDouble(_MAXIMUM_TERM_FREQUENCY, _MAXIMUM_TERM_FREQUENCY_DEFAULT);
            suggestor.MinimumDocumentFrequency = termDict.GetDouble(_MINIMUM_DOCUMENT_FREQUENCY, _MINIMUM_DOCUMENT_FREQUENCY_DEFAULT);
            suggestor.MinimumWordLength = termDict.GetInt32(_MINIMUM_WORD_LENGTH, _MINIMUM_WORD_LENGTH_DEFAULT);
            suggestor.PrefixLength = termDict.GetInt32(_PREFIX_LENGTH, _PREFIX_LENGTH_DEFAULT);
            suggestor.ShardSize = termDict.GetInt32(_SHARD_SIZE, _SHARD_SIZE_DEFAULT);
            suggestor.Size = termDict.GetInt32(SuggestSerializer._SIZE, SuggestSerializer._SIZE_DEFAULT);
            suggestor.Sort = SortModeEnum.Find(termDict.GetString(_SORT, _SORT_DEFAULT.ToString()));
            suggestor.SuggestMode = SuggestModeEnum.Find(termDict.GetString(_SUGGEST_MODE, _SUGGEST_MODE_DEFAULT.ToString()));
            suggestor.Text = suggestDict.GetStringOrDefault(SuggestSerializer._TEXT);

            return suggestor;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is TermSuggester))
                throw new SerializeTypeException<TermSuggester>();

            TermSuggester suggestor = value as TermSuggester;

            Dictionary<string, object> termDict = new Dictionary<string, object>();
            termDict.Add(SuggestSerializer._FIELD, suggestor.Field);
            termDict.AddObject(SuggestSerializer._SIZE, suggestor.Size, SuggestSerializer._SIZE_DEFAULT);
            termDict.AddObject(_ANALYZER, suggestor.Analyzer);
            termDict.AddObject(_LOWERCASE_TERMS, suggestor.LowercaseTerms, _LOWERCASE_TERMS_DEFAULT);
            termDict.AddObject(_MAXIMUM_EDIT_DISTANCE, suggestor.MaximumEditDistance, _MAXIMUM_EDIT_DISTANCE_DEFAULT);
            termDict.AddObject(_MAXIMUM_INSPECTIONS, suggestor.MaximumInspections, _MAXIMUM_INSPECTIONS_DEFAULT);
            termDict.AddObject(_MAXIMUM_TERM_FREQUENCY, suggestor.MaximumTermFrequency, _MAXIMUM_TERM_FREQUENCY_DEFAULT);
            termDict.AddObject(_MINIMUM_DOCUMENT_FREQUENCY, suggestor.MinimumDocumentFrequency, _MINIMUM_DOCUMENT_FREQUENCY_DEFAULT);
            termDict.AddObject(_MINIMUM_WORD_LENGTH, suggestor.MinimumWordLength, _MINIMUM_WORD_LENGTH_DEFAULT);
            termDict.AddObject(_PREFIX_LENGTH, suggestor.PrefixLength, _PREFIX_LENGTH_DEFAULT);
            termDict.AddObject(_SHARD_SIZE, suggestor.ShardSize, suggestor.Size);
            termDict.AddObject(_SORT, suggestor.Sort.ToString(), _SORT_DEFAULT.ToString());
            termDict.AddObject(_SUGGEST_MODE, suggestor.SuggestMode.ToString(), _SUGGEST_MODE_DEFAULT.ToString());

            Dictionary<string, object> internalDict = new Dictionary<string, object>();
            internalDict.AddObject(SuggestSerializer._TEXT, suggestor.Text);
            internalDict.Add(SuggestTypeEnum.Term.ToString(), termDict);

            Dictionary<string, object> suggestDict = new Dictionary<string, object>();
            suggestDict.Add(suggestor.SuggestName, internalDict);

            serializer.Serialize(writer, suggestDict);
        }
    }
}
