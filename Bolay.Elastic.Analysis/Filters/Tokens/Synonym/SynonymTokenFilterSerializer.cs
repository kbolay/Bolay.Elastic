using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.Synonym
{
    internal class SynonymTokenFilterSerializer : JsonConverter
    {
        private const string _EXPAND = "expand";
        private const string _IGNORE_CASE = "ignore_case";
        private const string _SYNONYMS = "synonyms";
        private const string _SYNONYMS_PATH = "synonyms_path";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> filterDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(filterDict.First().Value.ToString());

            string filterName = filterDict.First().Key;
            SynonymTokenFilter filter = null;
            if (fieldDict.ContainsKey(_SYNONYMS))
            {
                filter = new SynonymTokenFilter(filterName, JsonConvert.DeserializeObject<IEnumerable<string>>(fieldDict.GetString(_SYNONYMS)));
            }
            else if (fieldDict.ContainsKey(_SYNONYMS_PATH))
            {
                filter = new SynonymTokenFilter(filterName, fieldDict.GetString(_SYNONYMS_PATH));
            }
            else
            {
                throw new RequiredPropertyMissingException(_SYNONYMS + "/" + _SYNONYMS_PATH);
            }

            TokenFilterBase.Deserialize(filter, fieldDict);

            filter.Expand = fieldDict.GetBool(_EXPAND, SynonymTokenFilter._EXPAND_DEFAULT);
            filter.IgnoreCase = fieldDict.GetBool(_IGNORE_CASE, SynonymTokenFilter._INGORE_CASE_DEFAULT);

            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is SynonymTokenFilter))
                throw new SerializeTypeException<SynonymTokenFilter>();

            SynonymTokenFilter filter = value as SynonymTokenFilter;
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            TokenFilterBase.Serialize(filter, fieldDict);

            fieldDict.AddObject(_EXPAND, filter.Expand, SynonymTokenFilter._EXPAND_DEFAULT);
            fieldDict.AddObject(_IGNORE_CASE, filter.IgnoreCase, SynonymTokenFilter._INGORE_CASE_DEFAULT);
            if (filter.Synonyms != null && filter.Synonyms.Any(x => !string.IsNullOrWhiteSpace(x)))
            {
                fieldDict.AddObject(_SYNONYMS, filter.Synonyms.Where(x => !string.IsNullOrWhiteSpace(x)));
            }
            fieldDict.AddObject(_SYNONYMS_PATH, filter.SynonymsPath);

            Dictionary<string, object> filterDict = new Dictionary<string, object>();
            filterDict.Add(filter.Name, fieldDict);

            serializer.Serialize(writer, filterDict);
        }
    }
}
