using Bolay.Elastic.Exceptions;
using Bolay.Elastic.Models;
using Bolay.Elastic.QueryDSL.Filters;
using Bolay.Elastic.Scripts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting.Terms
{
    public class TermsSerializer : JsonConverter
    {
        private const string _FIELDS_DELIMITER = ",";
        private static List<string> _FIELDS_DELIMITERS = new List<string>() { " , ", ", ", " ,", "," };
        private const string _FLAG_DELIMITER = "|";
        private static List<string> _FLAG_DELIMITERS = new List<string>(){ " | ", "| ", " |", "|" };

        private const string _FIELD = "field";
        private const string _FIELDS = "fields";
        private const string _SCRIPT_FIELD = "script_field";
        private const string _SCRIPT = "script";
        private const string _REGEX = "regex";
        private const string _REGEX_FLAGS = "regex_flags";
        private const string _EXCLUDE = "exclude";
        private const string _ALL_TERMS = "all_terms";
        private const string _ORDER = "order";
        private const string _SHARD_SIZE = "shard_size";
        private const string _FACET_FILTER = "facet_filter";
        private const string _PARAMETERS = "params";
        private const string _LANGUAGE = "lang";

        internal static List<RegexFlagEnum> _REGEX_FLAGS_DEFAULT = new List<RegexFlagEnum>() { RegexFlagEnum.DotAll };
        internal const bool _ALL_TERMS_DEFAULT = false;
        internal static OrderingEnum _ORDER_DEFAULT = OrderingEnum.Count;

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> wholeDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> facetDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(wholeDict.First().Value.ToString());
            Dictionary<string, object> termDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(facetDict.GetString(FacetTypeEnum.Terms.ToString()));

            string facetName = wholeDict.First().Key;
            TermsFacet facet = null;
            if (termDict.ContainsKey(_FIELD))
            {
                facet = new TermsFacet(facetName, new List<string>() { termDict.GetString(_FIELD) });
            }
            else if(termDict.ContainsKey(_FIELDS))
            {
                facet = new TermsFacet(facetName, JsonConvert.DeserializeObject<IEnumerable<string>>(termDict.GetString(_FIELDS)));
            }
            else if(termDict.ContainsKey(_SCRIPT_FIELD))
            {
                facet = new TermsFacet(facetName, termDict.GetString(_SCRIPT_FIELD));
            }
            else
                throw new RequiredPropertyMissingException(_FIELD + "/" + _FIELDS + "/" + _SCRIPT_FIELD);

            if (termDict.ContainsKey(_PARAMETERS))
                facet.ScriptParameters = JsonConvert.DeserializeObject<ScriptParameterCollection>(termDict.GetString(_PARAMETERS));
            facet.ScriptLanguage = termDict.GetStringOrDefault(_LANGUAGE);

            if(termDict.ContainsKey(_EXCLUDE))
                facet.ExcludeTerms = JsonConvert.DeserializeObject<IEnumerable<string>>(termDict.GetString(_EXCLUDE));

            facet.GetAllTerms = termDict.GetBool(_ALL_TERMS, _ALL_TERMS_DEFAULT);
            facet.Order = OrderingEnum.Find(termDict.GetString(_ORDER, _ORDER_DEFAULT.ToString()));
            
            if(termDict.ContainsKey(_REGEX_FLAGS))
            {
                List<RegexFlagEnum> flags = new List<RegexFlagEnum>();
                RegexFlagEnum flagEnum = RegexFlagEnum.DotAll;
                foreach(string flag in termDict.GetString(_REGEX_FLAGS).Split(_FLAG_DELIMITERS.ToArray(), StringSplitOptions.RemoveEmptyEntries))
                {
                    flagEnum = RegexFlagEnum.Find(flag);
                    if(flagEnum == null)
                        throw new Exception(flag + " is not a valid regex flag.");

                    flags.Add(flagEnum);
                }

                if(flags.Any())
                    facet.RegexFlags = flags;
            }
            
            facet.RegexPattern = termDict.GetStringOrDefault(_REGEX);
            facet.Script = termDict.GetStringOrDefault(_SCRIPT);           
            facet.ShardSize = termDict.GetInt32(_SHARD_SIZE, facet.Size);

            FacetSerializer.DeserializeFacetInfo(facet, termDict);

            return facet;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is TermsFacet))
                throw new SerializeTypeException<TermsFacet>();

            TermsFacet facet = value as TermsFacet;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            if(facet.Fields != null)
            {
                if(facet.Fields.Count() > 1)
                    fieldDict.Add(_FIELDS, string.Join(_FIELDS_DELIMITER, facet.Fields));
                else
                    fieldDict.Add(_FIELD, facet.Fields.First());
            }
            else
            {
                fieldDict.Add(_SCRIPT_FIELD, facet.ScriptField);
            }

            fieldDict.AddObject(_EXCLUDE, facet.ExcludeTerms);
            fieldDict.AddObject(_ALL_TERMS, facet.GetAllTerms, _ALL_TERMS_DEFAULT);
            fieldDict.AddObject(_ORDER, facet.Order.ToString(), _ORDER_DEFAULT.ToString());
            fieldDict.AddObject(_REGEX, facet.RegexPattern);

            string defaultFlags = string.Join(_FLAG_DELIMITER, _REGEX_FLAGS_DEFAULT);
            string flagsValue = string.Join(_FLAG_DELIMITER, facet.RegexFlags);
            fieldDict.AddObject(_REGEX_FLAGS, flagsValue, defaultFlags);

            fieldDict.AddObject(_SCRIPT, facet.Script);
            fieldDict.AddObject(_LANGUAGE, facet.ScriptLanguage);
            if (facet.ScriptParameters != null && facet.ScriptParameters.Any(x => x != null))
                fieldDict.AddObject(_PARAMETERS, new ScriptParameterCollection(facet.ScriptParameters));
            fieldDict.AddObject(_SHARD_SIZE, facet.ShardSize, facet.Size);
            fieldDict.AddObject(_FACET_FILTER, facet.FacetFilter);

            Dictionary<string, object> termDict = new Dictionary<string, object>();
            termDict.Add(FacetTypeEnum.Terms.ToString(), fieldDict);

            Dictionary<string, object> facetDict = new Dictionary<string, object>();
            facetDict.Add(facet.FacetName, termDict);

            serializer.Serialize(writer, facetDict);
        }
    }
}
