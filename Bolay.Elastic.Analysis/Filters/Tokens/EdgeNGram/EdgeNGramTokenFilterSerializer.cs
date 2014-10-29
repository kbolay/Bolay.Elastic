using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.EdgeNGram
{
    internal class EdgeNGramTokenFilterSerializer : JsonConverter
    {
        private const string _MINIMUM_GRAM = "min_gram";
        private const string _MAXIMUM_GRAM = "max_gram";
        private const string _SIDE = "side";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> filterDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(filterDict.First().Value.ToString());

            EdgeNGramTokenFilter filter = new EdgeNGramTokenFilter(filterDict.First().Key);
            TokenFilterBase.Deserialize(filter, fieldDict);
            filter.MinimumSize = fieldDict.GetInt64(_MINIMUM_GRAM, EdgeNGramTokenFilter._MINIMUM_SIZE_DEFAULT);
            filter.MaximumSize = fieldDict.GetInt64(_MAXIMUM_GRAM, EdgeNGramTokenFilter._MAXIMUM_SIZE_DEFAULT);
            filter.Side = SideEnum.Find(fieldDict.GetString(_SIDE, EdgeNGramTokenFilter._SIDE_DEFAULT.ToString()));

            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is EdgeNGramTokenFilter))
                throw new SerializeTypeException<EdgeNGramTokenFilter>();

            EdgeNGramTokenFilter filter = value as EdgeNGramTokenFilter;
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            TokenFilterBase.Serialize(filter, fieldDict);
            fieldDict.AddObject(_MINIMUM_GRAM, filter.MinimumSize, EdgeNGramTokenFilter._MINIMUM_SIZE_DEFAULT);
            fieldDict.AddObject(_MAXIMUM_GRAM, filter.MaximumSize, EdgeNGramTokenFilter._MAXIMUM_SIZE_DEFAULT);
            fieldDict.AddObject(_SIDE, filter.Side.ToString(), EdgeNGramTokenFilter._SIDE_DEFAULT.ToString());

            Dictionary<string, object> filterDict = new Dictionary<string, object>();
            filterDict.Add(filter.Name, fieldDict);

            serializer.Serialize(writer, filterDict);
        }
    }
}
