using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Suggesters.Phrase
{
    public class DirectGeneratorSerializer : JsonConverter
    {
        private const string _DIRECT_GENERATOR = "direct_generator";
        private const string _SUGGEST_MODE = "suggest_mode";
        private const string _MAXIMUM_EDIT_DISTANCE = "max_edits";
        private const string _PREFIX_LENGTH = "prefix_length";
        private const string _MINIMUM_WORD_LENGTH = "min_word_length";
        private const string _MAXIMUM_INSPECTIONS = "max_inspections";
        private const string _MINIMUM_DOCUMENT_FREQUENCY = "min_doc_freq";
        private const string _MAXIMUM_TERM_FREQUENCY = "max_term_freq";
        private const string _PRE_FILTER = "pre_filter";
        private const string _POST_FILTER = "post_filter";

        internal static SuggestModeEnum _SUGGEST_MODE_DEFAULT = SuggestModeEnum.Missing;
        internal const Double _MAXIMUM_EDIT_DISTANCE_DEFAULT = 2.0;
        internal const int _PREFIX_LENGTH_DEFAULT = 1;
        internal const int _MINIMUM_WORD_LENGTH_DEFAULT = 4;
        internal const int _MAXIMUM_INSPECTIONS_DEFAULT = 5;
        internal const Double _MINIMUM_DOCUMENT_FREQUENCY_DEFAULT = 0.0;
        internal const Double _MAXIMUM_TERM_FREQUENCY_DEFAULT = 0.01;

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(_DIRECT_GENERATOR))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            DirectGenerator generator = new DirectGenerator();
            generator.Field = fieldDict.GetStringOrDefault(SuggestSerializer._FIELD);
            generator.MaximumEditDistance = fieldDict.GetDouble(_MAXIMUM_EDIT_DISTANCE, _MAXIMUM_EDIT_DISTANCE_DEFAULT);
            generator.MaximumInspections = fieldDict.GetInt32(_MAXIMUM_INSPECTIONS, _MAXIMUM_INSPECTIONS_DEFAULT);
            generator.MaximumTermFrequency = fieldDict.GetDouble(_MAXIMUM_TERM_FREQUENCY, _MAXIMUM_TERM_FREQUENCY_DEFAULT);
            generator.MinimumDocumentFrequency = fieldDict.GetDouble(_MINIMUM_DOCUMENT_FREQUENCY, _MINIMUM_DOCUMENT_FREQUENCY_DEFAULT);
            generator.MinimumWordLength = fieldDict.GetInt32(_MINIMUM_WORD_LENGTH, _MINIMUM_WORD_LENGTH_DEFAULT);
            generator.PostFilter = fieldDict.GetStringOrDefault(_POST_FILTER);
            generator.PreFilter = fieldDict.GetStringOrDefault(_PRE_FILTER);
            generator.PrefixLength = fieldDict.GetInt32(_PREFIX_LENGTH, _PREFIX_LENGTH_DEFAULT);
            generator.Size = fieldDict.GetInt32(SuggestSerializer._SIZE, SuggestSerializer._SIZE_DEFAULT);
            generator.SuggestMode = SuggestModeEnum.Find(fieldDict.GetString(_SUGGEST_MODE, _SUGGEST_MODE_DEFAULT.ToString()));

            return generator;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is DirectGenerator))
                throw new SerializeTypeException<DirectGenerator>();

            DirectGenerator generator = value as DirectGenerator;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.AddObject(SuggestSerializer._FIELD, generator.Field);
            fieldDict.AddObject(SuggestSerializer._SIZE, generator.Size, SuggestSerializer._SIZE_DEFAULT);
            fieldDict.AddObject(_MAXIMUM_EDIT_DISTANCE, generator.MaximumEditDistance, _MAXIMUM_EDIT_DISTANCE_DEFAULT);
            fieldDict.AddObject(_MAXIMUM_INSPECTIONS, generator.MaximumInspections, _MAXIMUM_INSPECTIONS_DEFAULT);
            fieldDict.AddObject(_MAXIMUM_TERM_FREQUENCY, generator.MaximumTermFrequency, _MAXIMUM_TERM_FREQUENCY_DEFAULT);
            fieldDict.AddObject(_MINIMUM_DOCUMENT_FREQUENCY, generator.MinimumDocumentFrequency, _MINIMUM_DOCUMENT_FREQUENCY_DEFAULT);
            fieldDict.AddObject(_MINIMUM_WORD_LENGTH, generator.MinimumWordLength, _MINIMUM_WORD_LENGTH_DEFAULT);
            fieldDict.AddObject(_PRE_FILTER, generator.PreFilter);
            fieldDict.AddObject(_POST_FILTER, generator.PostFilter);
            fieldDict.AddObject(_PREFIX_LENGTH, generator.PrefixLength, _PREFIX_LENGTH_DEFAULT);
            fieldDict.AddObject(_SUGGEST_MODE, generator.SuggestMode.ToString(), _SUGGEST_MODE_DEFAULT.ToString());

            serializer.Serialize(writer, fieldDict);
        }
    }
}
