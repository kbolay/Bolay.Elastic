using Bolay.Elastic.Exceptions;
using Bolay.Elastic.QueryDSL.Regex;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.Regex
{
    public class RegexSerializer : JsonConverter
    {
        private const string _FLAG_DELIMITER = "|";
        private static List<string> _FLAG_DELIMITERS = new List<string>() { " | ", " |", "| ", "|" };

        private const string _PATTERN = "value";
        private const string _FLAGS = "flags";
        private const string _CACHE_NAME = "_name";

        internal const bool _CACHE_DEFAULT = false;

        private static List<string> _KnownFields = new List<string>()
        {
            _PATTERN,
            _FLAGS,
            _CACHE_NAME,
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
            if (fieldDict.ContainsKey(FilterTypeEnum.Regex.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            // find the field name
            KeyValuePair<string, object> fieldKvp = fieldDict.FirstOrDefault(x => !_KnownFields.Contains(x.Key, StringComparer.OrdinalIgnoreCase));

            if (string.IsNullOrWhiteSpace(fieldKvp.Key))
                throw new RequiredPropertyMissingException("field");

            Dictionary<string, object> internalDict = null;
            string field = null;
            string pattern = null;
            try
            {
                internalDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldKvp.Value.ToString());
            }
            catch
            {
                internalDict = fieldDict;
            }

            RegexFilter filter = null;

            if (internalDict.ContainsKey(_PATTERN))
            {
                field = fieldDict.First().Key;
                pattern = internalDict.GetString(_PATTERN);
                filter = new RegexFilter(field, pattern);

                if (internalDict.ContainsKey(_FLAGS))
                {
                    string flagsValue = internalDict.GetString(_FLAGS);
                    if (!string.IsNullOrWhiteSpace(flagsValue))
                    {
                        RegexOperatorEnum feature = RegexOperatorEnum.All;
                        List<RegexOperatorEnum> features = new List<RegexOperatorEnum>();
                        foreach (string flagValue in flagsValue.Split(_FLAG_DELIMITERS.ToArray(), StringSplitOptions.RemoveEmptyEntries))
                        {
                            feature = RegexOperatorEnum.Find(flagValue);
                            if (feature == null)
                                throw new Exception("Unable to match " + flagValue + " to a regex operator.");

                            features.Add(feature);
                        }

                        if (features.Any())
                            filter.RegexOperatorFlags = features;
                    }
                }
            }
            else
            {
                field = fieldKvp.Key;
                pattern = fieldKvp.Value.ToString();
                filter = new RegexFilter(field, pattern);
            }

            FilterSerializer.DeserializeBaseValues(filter, _CACHE_DEFAULT, internalDict);
            filter.CacheName = internalDict.GetStringOrDefault(_CACHE_NAME);

            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is RegexFilter))
                throw new SerializeTypeException<RegexFilter>();

            RegexFilter filter = value as RegexFilter;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();

            if (filter.RegexOperatorFlags != null && filter.RegexOperatorFlags.Any(x => x != null))
            {
                Dictionary<string, object> internalDict = new Dictionary<string, object>();
                internalDict.Add(_PATTERN, filter.Pattern);

                if (filter.RegexOperatorFlags != null && filter.RegexOperatorFlags.Any(x => x != null))
                {
                    string flagsValue = string.Join(_FLAG_DELIMITER, filter.RegexOperatorFlags.Where(x => x != null).Select(x => x.ToString()));

                    internalDict.Add(_FLAGS, flagsValue);
                }

                fieldDict.Add(filter.Field, internalDict);
            }
            else
            {
                fieldDict.Add(filter.Field, filter.Pattern);
            }

            if (filter.Cache && !string.IsNullOrWhiteSpace(filter.CacheName))
                fieldDict.Add(_CACHE_NAME, filter.CacheName);
            FilterSerializer.SerializeBaseValues(filter, _CACHE_DEFAULT, fieldDict);

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add(FilterTypeEnum.Regex.ToString(), fieldDict);

            serializer.Serialize(writer, queryDict);
        }
    }
}
