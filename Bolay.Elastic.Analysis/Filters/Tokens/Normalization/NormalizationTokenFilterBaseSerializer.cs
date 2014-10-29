using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.Normalization
{
    internal class NormalizationTokenFilterBaseSerializer : JsonConverter
    {
        private const string _TYPE = "type";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> filterDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(filterDict.First().Value.ToString());

            string filterName = filterDict.First().Key;

            NormalizationTokenFilterBase filter = null;
            TokenFilterTypeEnum tokenFilterType = TokenFilterTypeEnum.Ngram;
            string tokenFilterTypeStr = fieldDict.GetString(_TYPE);
            tokenFilterType = TokenFilterTypeEnum.Find(tokenFilterTypeStr);
            if (tokenFilterType == null)
            {
                throw new Exception(tokenFilterTypeStr + " is not a valid token filter.");
            }
            else if (tokenFilterType == TokenFilterTypeEnum.ArabicNormalization)
            {
                filter = new ArabicNormalizationTokenFilter(filterName);
            }
            else if (tokenFilterType == TokenFilterTypeEnum.PersianNormalization)
            {
                filter = new PersianNormalizationTokenFilter(filterName);
            }
            else
            {
                throw new Exception(tokenFilterTypeStr + " is not a valid normalization token filter.");
            }

            TokenFilterBase.Deserialize(filter, fieldDict);

            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is NormalizationTokenFilterBase))
                throw new SerializeTypeException<NormalizationTokenFilterBase>();

            NormalizationTokenFilterBase filter = value as NormalizationTokenFilterBase;
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            TokenFilterBase.Serialize(filter, fieldDict);

            Dictionary<string, object> filterDict = new Dictionary<string, object>();
            filterDict.Add(filter.Name, fieldDict);

            serializer.Serialize(writer, filterDict);
        }
    }
}
