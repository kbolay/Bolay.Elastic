using Bolay.Elastic.Exceptions;
using Bolay.Elastic.QueryDSL.Queries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Highlighting
{
    public class HighlighterSerializer : JsonConverter
    {
        private const string _HIGHLIGHTER_TYPE = "type";
        private const string _FRAGMENT_SIZE = "fragment_size";
        private const string _NUMBER_OF_FRAGMENTS = "number_of_fragments";
        private const string _TAGS_SCHEMA = "tags_schema";
        private const string _PRE_TAGS = "pre_tags";
        private const string _POST_TAGS = "post_tags";
        private const string _ENCODER = "encoder";
        private const string _NO_MATCH_SIZE = "no_match_size";
        private const string _REQUIRE_FIELD_MATCH = "required_field_match";
        private const string _BOUNDARY_CHARACTERS = "boundary_chars";
        private const string _BOUNDARY_MAXIMUM_SCAN = "boundary_max_scan";
        private const string _PHRASE_LIMIT = "phrase_limit";
        private const string _HIGHLIGHT_QUERY = "highlight_query";
        private const string _MATCHED_FIELDS = "matched_fields";

        internal static HighlighterTypeEnum _HIGHLIGHTER_TYPE_DEFAULT = HighlighterTypeEnum.Plain;
        internal const int _FRAGMENT_SIZE_DEFAULT = 100;
        internal const int _NUMBER_OF_FRAGMENTS_DEFAULT = 5;
        internal static EncoderTypeEnum _ENCODER_DEFAULT = EncoderTypeEnum.NoEncoding;
        internal const int _NO_MATCH_SIZE_DEFAULT = 0;
        internal const bool _REQUIRE_FIELD_MATCH_DEFAULT = false;
        internal const string _BOUNDARY_CHARACTERS_DEFAULT = ".,!? \t\n";
        internal const int _BOUNDARY_MAXIMUM_SCAN_DEFAULT = 20;
        internal const int _PHRASE_LIMIT_DEFAULT = 256;
        internal static TagsSchemaEnum _TAGS_SCHEMA_DEFAULT = TagsSchemaEnum.None;

        private static List<string> _KnownFields = new List<string>()
        {
            _BOUNDARY_CHARACTERS,
            _BOUNDARY_MAXIMUM_SCAN,
            _ENCODER,
            _FRAGMENT_SIZE,
            _HIGHLIGHTER_TYPE,
            _NO_MATCH_SIZE,
            _NUMBER_OF_FRAGMENTS,
            _PHRASE_LIMIT,
            _POST_TAGS,
            _PRE_TAGS,
            _REQUIRE_FIELD_MATCH,
            _TAGS_SCHEMA,
        };

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(SearchPieceTypeEnum.Highlighter.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            List<FieldHighlighter> fieldHighlighters = new List<FieldHighlighter>();
            foreach (KeyValuePair<string, object> fieldKvp in fieldDict.Where(x => !_KnownFields.Contains(x.Key, StringComparer.OrdinalIgnoreCase)))
            {
                Dictionary<string, object> internalDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldKvp.Value.ToString());
                FieldHighlighter fieldHighlighter = new FieldHighlighter(fieldKvp.Key);
                DeserializeHighlighterOptions(fieldHighlighter, internalDict);

                if (internalDict.ContainsKey(_HIGHLIGHT_QUERY))
                    fieldHighlighter.HighlightQuery = JsonConvert.DeserializeObject<IQuery>(internalDict.GetString(_HIGHLIGHT_QUERY));
                if (internalDict.ContainsKey(_MATCHED_FIELDS))
                    fieldHighlighter.MatchedFields = JsonConvert.DeserializeObject<IEnumerable<string>>(internalDict.GetString(_MATCHED_FIELDS));

                fieldHighlighters.Add(fieldHighlighter);
            }

            if (!fieldHighlighters.Any())
                throw new RequiredPropertyMissingException("highlight fields");

            Highlighter highlighter = new Highlighter(fieldHighlighters);
            DeserializeHighlighterOptions(highlighter, fieldDict);

            return highlighter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is Highlighter))
                throw new SerializeTypeException<Highlighter>();

            Highlighter highlighter = value as Highlighter;

            Dictionary<string, object> highlightDict = new Dictionary<string, object>();
            SerializerHighlighterOptions(highlighter, highlightDict);

            foreach (FieldHighlighter field in highlighter.FieldHighlighters)
            {
                Dictionary<string, object> internalDict = new Dictionary<string, object>();
                internalDict.AddObject(_MATCHED_FIELDS, field.MatchedFields);
                SerializerHighlighterOptions(field, internalDict);
                internalDict.AddObject(_HIGHLIGHT_QUERY, field.HighlightQuery);

                highlightDict.Add(field.Field, internalDict);
            }

            serializer.Serialize(writer, highlightDict);
        }

        internal static void DeserializeHighlighterOptions(HighlighterOptions options, Dictionary<string, object> fieldDict)
        {
            if(fieldDict == null || !fieldDict.Any())
                return;

            options.BoundaryCharacters = fieldDict.GetString(_BOUNDARY_CHARACTERS, _BOUNDARY_CHARACTERS_DEFAULT);
            options.BoundaryMaximumScan = fieldDict.GetInt32(_BOUNDARY_MAXIMUM_SCAN, _BOUNDARY_MAXIMUM_SCAN_DEFAULT);
            options.Encoder = EncoderTypeEnum.Find(fieldDict.GetString(_ENCODER, _ENCODER_DEFAULT.ToString()));
            options.FragmentSize = fieldDict.GetInt32(_FRAGMENT_SIZE, _FRAGMENT_SIZE_DEFAULT);
            options.NoMatchSize = fieldDict.GetInt32(_NO_MATCH_SIZE, _NO_MATCH_SIZE_DEFAULT);
            options.NumberOfFragments = fieldDict.GetInt32(_NUMBER_OF_FRAGMENTS, _NUMBER_OF_FRAGMENTS_DEFAULT);
            options.PhraseLimit = fieldDict.GetInt32(_PHRASE_LIMIT, _PHRASE_LIMIT_DEFAULT);
            if(fieldDict.ContainsKey(_POST_TAGS))
                options.PostTags = JsonConvert.DeserializeObject<IEnumerable<string>>(fieldDict.GetString(_POST_TAGS));
            if(fieldDict.ContainsKey(_PRE_TAGS))
                options.PreTags = JsonConvert.DeserializeObject<IEnumerable<string>>(fieldDict.GetString(_PRE_TAGS));
            options.RequireFieldMatch = fieldDict.GetBool(_REQUIRE_FIELD_MATCH, _REQUIRE_FIELD_MATCH_DEFAULT);
            options.TagsSchema = TagsSchemaEnum.Find(fieldDict.GetString(_TAGS_SCHEMA, _TAGS_SCHEMA_DEFAULT.ToString()));
            options.Type = HighlighterTypeEnum.Find(fieldDict.GetString(_HIGHLIGHTER_TYPE, _HIGHLIGHTER_TYPE_DEFAULT.ToString()));
        }

        internal static void SerializerHighlighterOptions(HighlighterOptions options, Dictionary<string, object> fieldDict)
        {
            if(fieldDict == null)
                fieldDict = new Dictionary<string,object>();

            fieldDict.AddObject(_BOUNDARY_CHARACTERS, options.BoundaryCharacters, _BOUNDARY_CHARACTERS_DEFAULT);
            fieldDict.AddObject(_BOUNDARY_MAXIMUM_SCAN, options.BoundaryMaximumScan, _BOUNDARY_MAXIMUM_SCAN_DEFAULT);
            fieldDict.AddObject(_ENCODER, options.Encoder.ToString(), _ENCODER_DEFAULT.ToString());
            fieldDict.AddObject(_FRAGMENT_SIZE, options.FragmentSize, _FRAGMENT_SIZE_DEFAULT);
            fieldDict.AddObject(_HIGHLIGHTER_TYPE, options.Type.ToString(), _HIGHLIGHTER_TYPE_DEFAULT.ToString());
            fieldDict.AddObject(_NO_MATCH_SIZE, options.NoMatchSize, _NO_MATCH_SIZE_DEFAULT);
            fieldDict.AddObject(_NUMBER_OF_FRAGMENTS, options.NumberOfFragments, _NUMBER_OF_FRAGMENTS_DEFAULT);
            fieldDict.AddObject(_PHRASE_LIMIT, options.PhraseLimit, _PHRASE_LIMIT_DEFAULT);
            fieldDict.AddObject(_POST_TAGS, options.PostTags);
            fieldDict.AddObject(_PRE_TAGS, options.PreTags);
            fieldDict.AddObject(_REQUIRE_FIELD_MATCH, options.RequireFieldMatch, _REQUIRE_FIELD_MATCH_DEFAULT);
            fieldDict.AddObject(_TAGS_SCHEMA, options.TagsSchema.ToString(), _TAGS_SCHEMA_DEFAULT.ToString());
        }
    }
}
