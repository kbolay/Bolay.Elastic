using Bolay.Elastic;
using Bolay.Elastic.Exceptions;
using Bolay.Elastic.QueryDSL.MinimumShouldMatch;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Bool
{
    public class BoolQuerySerializer : JsonConverter
    {
        private const string _MUST = "must";
        private const string _MUST_NOT = "must_not";
        private const string _SHOULD = "should";
        private const string _MINIMUM_SHOULD_MATCH = "minimum_should_match";
        private const string _DISABLE_COORDS = "disable_coords";

        internal static MinimumShouldMatchBase _MINIMUM_SHOULD_MATCH_DEFAULT = new IntegerMatch(1);
        internal const bool _DISABLE_COORDS_DEFAULT = false;

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> boolDict = serializer.Deserialize<Dictionary<string, object>>(reader);

            BoolQuery query = new BoolQuery();
            if(boolDict.First().Key.Equals(QueryTypeEnum.Bool.ToString(), StringComparison.OrdinalIgnoreCase))
                boolDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(boolDict.First().Value.ToString());

            if (boolDict.ContainsKey(_MUST))
                query.MustQueries = JsonConvert.DeserializeObject<IEnumerable<IQuery>>(boolDict[_MUST].ToString());
            if (boolDict.ContainsKey(_MUST_NOT))
                query.MustNotQueries = JsonConvert.DeserializeObject<IEnumerable<IQuery>>(boolDict[_MUST_NOT].ToString());
            if (boolDict.ContainsKey(_SHOULD))
                query.ShouldQueries = JsonConvert.DeserializeObject<IEnumerable<IQuery>>(boolDict[_SHOULD].ToString());

            query.Boost = boolDict.GetDouble(QuerySerializer._BOOST, QuerySerializer._BOOST_DEFAULT);
            query.DisableCoords = boolDict.GetBool(_DISABLE_COORDS, _DISABLE_COORDS_DEFAULT);
            
            if (boolDict.ContainsKey(_MINIMUM_SHOULD_MATCH))
                query.MinimumShouldMatch = MinimumShouldMatchBase.BuildMinimumShouldMatch(boolDict[_MINIMUM_SHOULD_MATCH].ToString());
            else
                query.MinimumShouldMatch = _MINIMUM_SHOULD_MATCH_DEFAULT;

            query.QueryName = boolDict.GetStringOrDefault(QuerySerializer._QUERY_NAME);

            return query;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is BoolQuery))
                throw new SerializeTypeException<BoolQuery>();

            BoolQuery query = value as BoolQuery;
            Dictionary<string, object> fieldsDict = new Dictionary<string, object>();

            if(query.MustQueries != null && query.MustQueries.Any())
                fieldsDict.Add(_MUST, query.MustQueries);
            if(query.MustNotQueries != null && query.MustNotQueries.Any())
                fieldsDict.Add(_MUST_NOT, query.MustNotQueries);
            if (query.ShouldQueries != null && query.ShouldQueries.Any())
                fieldsDict.Add(_SHOULD, query.ShouldQueries);

            fieldsDict.AddObject(QuerySerializer._BOOST, query.Boost, QuerySerializer._BOOST_DEFAULT);
            fieldsDict.AddObject(_MINIMUM_SHOULD_MATCH, query.MinimumShouldMatch.GetValue(), _MINIMUM_SHOULD_MATCH_DEFAULT.GetValue());
            fieldsDict.AddObject(_DISABLE_COORDS, query.DisableCoords);
            fieldsDict.AddObject(QuerySerializer._QUERY_NAME, query.QueryName);

            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add(QueryTypeEnum.Bool.ToString(), fieldsDict);

            serializer.Serialize(writer, dict);
        }
    }
}
