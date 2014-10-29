using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Scoring.Functions
{
    public class DecaySerializer : JsonConverter
    {
        private const string _ORIGIN = "origin";
        private const string _SCALE = "scale";
        private const string _OFFSET = "offset";
        private const string _DECAY = "decay";

        internal const string _OFFSET_DEFAULT = "0";
        internal const Double _DECAY_DEFAULT = 0.5;

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            DecayFunction decayFunction = new DecayFunction();
            Dictionary<string, object> decayDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (decayDict.Keys.Count == 1)
            { 
                DecayFunctionsEnum decayType = DecayFunctionsEnum.Gauss;
                decayType = DecayFunctionsEnum.Find(decayDict.First().Key);
                if (decayType != null)
                {
                    decayFunction.Function = decayType;
                    decayDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(decayDict.First().Value.ToString());
                }

                if (decayDict.Keys.Count == 1)
                {
                    decayFunction.Field = decayDict.First().Key;
                    decayDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(decayDict.First().Value.ToString());
                }
            }

            decayDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(decayDict.First().Value.ToString());
            decayFunction.Origin = decayDict.GetStringOrDefault(_ORIGIN);
            decayFunction.Scale = decayDict.GetStringOrDefault(_SCALE);
            decayFunction.Offset = decayDict.GetString(_OFFSET, _OFFSET_DEFAULT);
            decayFunction.Decay = decayDict.GetDouble(_DECAY, _DECAY_DEFAULT);

            return decayFunction;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is DecayFunction))
                throw new SerializeTypeException<DecayFunction>();

            DecayFunction decayFunction = value as DecayFunction;

            Dictionary<string, object> decayDict = new Dictionary<string, object>();
            decayDict.Add(_ORIGIN, decayFunction.Origin);
            decayDict.Add(_SCALE, decayFunction.Scale);
            decayDict.AddObject(_OFFSET, decayFunction.Offset, _OFFSET_DEFAULT);
            decayDict.AddObject(_DECAY, decayFunction.Decay, _DECAY_DEFAULT);

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.Add(decayFunction.Field, decayDict);

            serializer.Serialize(writer, fieldDict);
        }
    }
}
