using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.Terms
{
    public class TermsSerializer : JsonConverter
    {
        private const string _EXECUTION = "execution";
        private const string _INDEX = "index";
        private const string _TYPE = "type";
        private const string _PATH = "path";
        private const string _ID = "id";
        private const string _ROUTING = "routing";

        internal static ExecutionTypeEnum _EXECUTION_DEFAULT = ExecutionTypeEnum.Plain;
        internal static bool _INDEXED_TERMS_CACHE_DEFAULT = true;

        private static List<string> _KnownFields = new List<string>()
        {
            _EXECUTION,
            FilterSerializer._CACHE,
            FilterSerializer._CACHE_KEY
        };

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(FilterTypeEnum.Terms.ToString()) || fieldDict.ContainsKey(FilterTypeEnum.In.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            KeyValuePair<string, object> fieldKvp = fieldDict.FirstOrDefault(x => !_KnownFields.Contains(x.Key, StringComparer.OrdinalIgnoreCase));

            if (string.IsNullOrWhiteSpace(fieldKvp.Key))
                throw new RequiredPropertyMissingException("field");

            TermsFilter filter = null;
            Dictionary<string, object> indexTermsDict = null;
            try
            {
                indexTermsDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldKvp.Value.ToString());
            }
            catch
            {
                filter = new TermsFilter(fieldKvp.Key, JsonConvert.DeserializeObject<IEnumerable<object>>(fieldKvp.Value.ToString()));
                filter.ExecutionType = ExecutionTypeEnum.Find(fieldDict.GetString(_EXECUTION, _EXECUTION_DEFAULT.ToString()));
                FilterSerializer.DeserializeBaseValues(filter, filter.ExecutionType.CacheDefault, fieldDict);
                return filter;
            }

            filter = new TermsFilter(fieldKvp.Key,
                                indexTermsDict.GetString(_INDEX),
                                indexTermsDict.GetString(_TYPE),
                                indexTermsDict.GetString(_ID),
                                indexTermsDict.GetString(_PATH));

            filter.Routing = indexTermsDict.GetStringOrDefault(_ROUTING);
            FilterSerializer.DeserializeBaseValues(filter, _INDEXED_TERMS_CACHE_DEFAULT, fieldDict);

            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is TermsFilter))
                throw new SerializeTypeException<TermsFilter>();

            TermsFilter filter = value as TermsFilter;

            bool currentCacheDefault = _INDEXED_TERMS_CACHE_DEFAULT;
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            if (!string.IsNullOrWhiteSpace(filter.Index))
            {
                Dictionary<string, object> indexedTermsDict = new Dictionary<string, object>();
                indexedTermsDict.Add(_INDEX, filter.Index);
                indexedTermsDict.Add(_TYPE, filter.DocumentType);
                indexedTermsDict.Add(_ID, filter.DocumentId);
                indexedTermsDict.Add(_PATH, filter.Path);
                indexedTermsDict.AddObject(_ROUTING, filter.Routing);

                fieldDict.Add(filter.Field, indexedTermsDict);
            }
            else
            {
                fieldDict.Add(filter.Field, filter.Terms);
                fieldDict.AddObject(_EXECUTION, filter.ExecutionType.ToString(), _EXECUTION_DEFAULT.ToString());
                currentCacheDefault = filter.ExecutionType.CacheDefault;
            }

            FilterSerializer.SerializeBaseValues(filter, currentCacheDefault, fieldDict);

            Dictionary<string, object> filterDict = new Dictionary<string,object>();
            filterDict.Add(FilterTypeEnum.Terms.ToString(), fieldDict);

            serializer.Serialize(writer, filterDict);
        }
    }
}
