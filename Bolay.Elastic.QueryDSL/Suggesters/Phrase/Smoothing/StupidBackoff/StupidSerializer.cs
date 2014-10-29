using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Suggesters.Phrase.Smoothing.StupidBackoff
{
    public class StupidSerializer : JsonConverter
    {
        private const string _DISCOUNT = "discount";

        internal const Double _DISCOUNT_DEFAULT = 0.4;

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(SmoothingTypeEnum.StupidBackoff.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            StupidBackoffSmoothing smoothing = new StupidBackoffSmoothing();
            smoothing.Discount = fieldDict.GetDouble(_DISCOUNT, _DISCOUNT_DEFAULT);

            return smoothing;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is StupidBackoffSmoothing))
                throw new SerializeTypeException<StupidBackoffSmoothing>();

            StupidBackoffSmoothing smoothing = value as StupidBackoffSmoothing;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.AddObject(_DISCOUNT, smoothing.Discount, _DISCOUNT_DEFAULT);

            Dictionary<string, object> smoothDict = new Dictionary<string, object>();
            smoothDict.Add(SmoothingTypeEnum.StupidBackoff.ToString(), fieldDict);

            serializer.Serialize(writer, smoothDict);
        }
    }
}
