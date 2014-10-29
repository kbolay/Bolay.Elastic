using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.StemmerOverride
{
    internal class StemmerOverrideTokenFilterSerializer : JsonConverter
    {
        private const string _RULES = "rules";
        private const string _RULES_PATH = "rules_path";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> filterDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(filterDict.First().Value.ToString());            

            StemmerOverrideTokenFilter filter = new StemmerOverrideTokenFilter(filterDict.First().Key);
            TokenFilterBase.Deserialize(filter, fieldDict);

            if (fieldDict.ContainsKey(_RULES))
            {
                filter.Rules = JsonConvert.DeserializeObject<IEnumerable<string>>(fieldDict.GetString(_RULES));
            }
            filter.RulesPath = fieldDict.GetStringOrDefault(_RULES_PATH);

            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is StemmerOverrideTokenFilter))
                throw new SerializeTypeException<StemmerOverrideTokenFilter>();

            StemmerOverrideTokenFilter filter = value as StemmerOverrideTokenFilter;
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            TokenFilterBase.Serialize(filter, fieldDict);

            if(filter.Rules != null || filter.Rules.Any(x => !string.IsNullOrWhiteSpace(x)))
            {
                fieldDict.AddObject(_RULES, filter.Rules);
            }
            fieldDict.AddObject(_RULES_PATH, filter.RulesPath);

            Dictionary<string, object> filterDict = new Dictionary<string, object>();
            filterDict.Add(filter.Name, fieldDict);

            serializer.Serialize(writer, filterDict);
        }
    }
}
