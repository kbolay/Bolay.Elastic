using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Boosting
{
    public class BoostingSerializer : JsonConverter
    {
        private const string _POSITIVE = "positive";
        private const string _NEGATIVE = "negative";
        private const string _NEGATIVE_BOOST = "negative_boost";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> boostingDict = serializer.Deserialize<Dictionary<string, object>>(reader);

            BoostingQuery query = new BoostingQuery();

            if (boostingDict.First().Key.Equals(QueryTypeEnum.Boosting.ToString(), StringComparison.OrdinalIgnoreCase))
                boostingDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(boostingDict.First().Value.ToString());

            if (boostingDict.ContainsKey(_POSITIVE))
                query.PositiveQuery = JsonConvert.DeserializeObject<IQuery>(boostingDict[_POSITIVE].ToString());
            if (boostingDict.ContainsKey(_NEGATIVE))
                query.NegativeQuery = JsonConvert.DeserializeObject<IQuery>(boostingDict[_NEGATIVE].ToString());

            query.NegativeBoost = boostingDict.GetDouble(_NEGATIVE_BOOST, default(Double));
            query.QueryName = boostingDict.GetStringOrDefault(QuerySerializer._QUERY_NAME);

            return query;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is BoostingQuery))
                throw new SerializeTypeException<BoostingQuery>();

            BoostingQuery query = value as BoostingQuery;
            Dictionary<string, object> fieldsDict = new Dictionary<string, object>();

            if (query.PositiveQuery != null)
                fieldsDict.Add(_POSITIVE, query.PositiveQuery);
            if (query.NegativeQuery != null)
                fieldsDict.Add(_NEGATIVE, query.NegativeQuery);

            fieldsDict.AddObject(_NEGATIVE_BOOST, query.NegativeBoost, default(Double));
            fieldsDict.AddObject(QuerySerializer._QUERY_NAME, query.QueryName);

            Dictionary<string, object> boostingDict = new Dictionary<string, object>();
            boostingDict.Add(QueryTypeEnum.Boosting.ToString(), fieldsDict);

            serializer.Serialize(writer, boostingDict);
        }
    }
}
