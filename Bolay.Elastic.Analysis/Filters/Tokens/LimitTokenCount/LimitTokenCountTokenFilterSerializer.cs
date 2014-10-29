using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.LimitTokenCount
{
    internal class LimitTokenCountTokenFilterSerializer : JsonConverter
    {
        private const string _MAXIMUM_TOKEN_COUNT = "max_token_count";
        private const string _CONSUME_ALL_TOKENS = "consume_all_tokens";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> filterDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(filterDict.First().Value.ToString());

            LimitTokenCountTokenFilter filter = new LimitTokenCountTokenFilter(filterDict.First().Key);
            TokenFilterBase.Deserialize(filter, fieldDict);

            filter.MaximumTokenCount = fieldDict.GetInt32(_MAXIMUM_TOKEN_COUNT, LimitTokenCountTokenFilter._MAXIMUM_TOKEN_COUNT_DEFAULT);
            filter.ConsumeAllTokens = fieldDict.GetBool(_CONSUME_ALL_TOKENS, LimitTokenCountTokenFilter._CONSUME_ALL_TOKENS_DEFAULT);

            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is LimitTokenCountTokenFilter))
                throw new SerializeTypeException<LimitTokenCountTokenFilter>();

            LimitTokenCountTokenFilter filter = value as LimitTokenCountTokenFilter;
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            TokenFilterBase.Serialize(filter, fieldDict);

            fieldDict.AddObject(_MAXIMUM_TOKEN_COUNT, filter.MaximumTokenCount, LimitTokenCountTokenFilter._MAXIMUM_TOKEN_COUNT_DEFAULT);
            fieldDict.AddObject(_CONSUME_ALL_TOKENS, filter.ConsumeAllTokens, LimitTokenCountTokenFilter._CONSUME_ALL_TOKENS_DEFAULT);

            Dictionary<string, object> filterDict = new Dictionary<string, object>();
            filterDict.Add(filter.Name, fieldDict);

            serializer.Serialize(writer, filterDict);
        }
    }
}
