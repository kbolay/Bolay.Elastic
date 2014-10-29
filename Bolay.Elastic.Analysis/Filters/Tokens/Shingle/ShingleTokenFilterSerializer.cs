using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.Shingle
{
    internal class ShingleTokenFilterSerializer : JsonConverter
    {
        private const string _MINIMUM_SHINGLE_SIZE = "min_shingle_size";
        private const string _MAXIMUM_SHINGLE_SIZE = "max_shingle_size";
        private const string _OUTPUT_UNIGRAMS = "output_unigrams";
        private const string _OUTPUT_UNIGRAMS_IF_NO_SHINGLES = "output_unigrams_if_no_shingles";
        private const string _TOKEN_SEPARATOR = "token_separator";
        private const string _FILLER_TOKEN = "filler_token";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> filterDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(filterDict.First().Value.ToString());

            ShingleTokenFilter filter = new ShingleTokenFilter(filterDict.First().Key);
            TokenFilterBase.Deserialize(filter, fieldDict);
            filter.MinimumSize = fieldDict.GetInt64(_MINIMUM_SHINGLE_SIZE, ShingleTokenFilter._MINIMUM_SIZE_DEFAULT);
            filter.MaximumSize = fieldDict.GetInt64(_MAXIMUM_SHINGLE_SIZE, ShingleTokenFilter._MAXIMUM_SIZE_DEFAULT);
            filter.OutputUnigrams = fieldDict.GetBool(_OUTPUT_UNIGRAMS, ShingleTokenFilter._OUTPUT_UNIGRAMS_DEFAULT);
            filter.OutputUnigramsIfNoShingles = fieldDict.GetBool(_OUTPUT_UNIGRAMS_IF_NO_SHINGLES, ShingleTokenFilter._OUTPUT_UNIGRAMS_IF_NO_SHINGLES_DEFAULT);
            filter.TokenSeparator = fieldDict.GetString(_TOKEN_SEPARATOR, ShingleTokenFilter._TOKEN_SEPARATOR_DEFAULT);
            filter.FillerToken = fieldDict.GetString(_FILLER_TOKEN, ShingleTokenFilter._FILLER_TOKEN_DEFAULT);

            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is ShingleTokenFilter))
                throw new SerializeTypeException<ShingleTokenFilter>();

            ShingleTokenFilter filter = value as ShingleTokenFilter;
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            TokenFilterBase.Serialize(filter, fieldDict);
            fieldDict.AddObject(_MINIMUM_SHINGLE_SIZE, filter.MinimumSize, ShingleTokenFilter._MINIMUM_SIZE_DEFAULT);
            fieldDict.AddObject(_MAXIMUM_SHINGLE_SIZE, filter.MaximumSize, ShingleTokenFilter._MAXIMUM_SIZE_DEFAULT);
            fieldDict.AddObject(_OUTPUT_UNIGRAMS, filter.OutputUnigrams, ShingleTokenFilter._OUTPUT_UNIGRAMS_DEFAULT);
            fieldDict.AddObject(_OUTPUT_UNIGRAMS_IF_NO_SHINGLES, filter.OutputUnigramsIfNoShingles, ShingleTokenFilter._OUTPUT_UNIGRAMS_IF_NO_SHINGLES_DEFAULT);
            fieldDict.AddObject(_TOKEN_SEPARATOR, filter.TokenSeparator, ShingleTokenFilter._TOKEN_SEPARATOR_DEFAULT);
            fieldDict.AddObject(_FILLER_TOKEN, filter.FillerToken, ShingleTokenFilter._FILLER_TOKEN_DEFAULT);

            Dictionary<string, object> filterDict = new Dictionary<string, object>();
            filterDict.Add(filter.Name, fieldDict);

            serializer.Serialize(writer, filterDict);
        }
    }
}
