using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Suggesters.Phrase.Smoothing.LinearInterpolation
{
    public class LinearInterpolationSerializer : JsonConverter
    {
        private const string _UNIGRAM_LAMBDA = "unigram_lambda";
        private const string _BIGRAM_LAMBDA = "bigram_lambda";
        private const string _TRIGRAM_LAMBDA = "trigram_lambda";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(SmoothingTypeEnum.LinearInterpolation.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.GetString(SmoothingTypeEnum.LinearInterpolation.ToString()));

            LinearInterpolationSmoothing smoothing = new LinearInterpolationSmoothing(
                                                        fieldDict.GetDouble(_UNIGRAM_LAMBDA),
                                                        fieldDict.GetDouble(_BIGRAM_LAMBDA),
                                                        fieldDict.GetDouble(_TRIGRAM_LAMBDA));

            return smoothing;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is LinearInterpolationSmoothing))
                throw new SerializeTypeException<LinearInterpolationSmoothing>();

            LinearInterpolationSmoothing smoothing = value as LinearInterpolationSmoothing;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.Add(_UNIGRAM_LAMBDA, smoothing.UnigramWeight);
            fieldDict.Add(_BIGRAM_LAMBDA, smoothing.BigramWeight);
            fieldDict.Add(_TRIGRAM_LAMBDA, smoothing.TrigramWeight);

            Dictionary<string, object> smoothDict = new Dictionary<string, object>();
            smoothDict.Add(SmoothingTypeEnum.LinearInterpolation.ToString(), fieldDict);

            serializer.Serialize(writer, smoothDict);
        }
    }
}
