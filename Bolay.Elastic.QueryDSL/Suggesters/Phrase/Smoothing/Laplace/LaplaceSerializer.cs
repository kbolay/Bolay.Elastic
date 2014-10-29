using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Suggesters.Phrase.Smoothing.Laplace
{
    public class LaplaceSerializer : JsonConverter
    {
        private const string _ALPHA = "alpha";

        internal const Double _ALPHA_DEFAULT = 0.5;

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(SmoothingTypeEnum.Laplace.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            LaplaceSmoothing smoothing = new LaplaceSmoothing();
            smoothing.Alpha = fieldDict.GetDouble(_ALPHA, _ALPHA_DEFAULT);

            return smoothing;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is LaplaceSmoothing))
                throw new SerializeTypeException<LaplaceSmoothing>();

            LaplaceSmoothing smoothing = value as LaplaceSmoothing;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.AddObject(_ALPHA, smoothing.Alpha, _ALPHA_DEFAULT);

            Dictionary<string, object> smoothDict = new Dictionary<string, object>();
            smoothDict.Add(SmoothingTypeEnum.StupidBackoff.ToString(), fieldDict);

            serializer.Serialize(writer, smoothDict);
        }
    }
}
