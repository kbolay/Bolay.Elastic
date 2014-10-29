using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Indices
{
    public class IndicesSerializer : JsonConverter
    {
        private const string _INDICES = "indices";
        private const string _INDEX = "index";
        private const string _QUERY = "query";
        private const string _NON_MATCHING_QUERY = "no_match_query";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(QueryTypeEnum.Indices.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            List<string> indices = new List<string>();
            if (fieldDict.ContainsKey(_INDEX))
                indices.Add(fieldDict.GetString(_INDEX));
            else if (fieldDict.ContainsKey(_INDICES))
                indices = JsonConvert.DeserializeObject<List<string>>(fieldDict.GetString(_INDICES));
            else
                throw new RequiredPropertyMissingException(_INDEX + "/" + _INDICES);

            IQuery matchingQuery = JsonConvert.DeserializeObject<IQuery>(fieldDict[_QUERY].ToString());

            IndicesQuery query = null;

            NonMatchingTypeEnum nonMatching = NonMatchingTypeEnum.None;
            nonMatching = NonMatchingTypeEnum.Find(fieldDict.GetString(_NON_MATCHING_QUERY));
            if (nonMatching != null)
                query = new IndicesQuery(indices, matchingQuery, nonMatching);
            else
                query = new IndicesQuery(indices, matchingQuery, JsonConvert.DeserializeObject<IQuery>(fieldDict.GetString(_NON_MATCHING_QUERY)));

            query.QueryName = fieldDict.GetStringOrDefault(QuerySerializer._QUERY_NAME);

            return query;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is IndicesQuery))
                throw new SerializeTypeException<IndicesQuery>();

            IndicesQuery query = value as IndicesQuery;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            if (query.Indices.Count() > 1)
                fieldDict.Add(_INDICES, query.Indices);
            else
                fieldDict.Add(_INDEX, query.Indices.First());

            fieldDict.Add(_QUERY, query.MatchingQuery);
            if (query.NonMatchingQueryType != null)
                fieldDict.Add(_NON_MATCHING_QUERY, query.NonMatchingQueryType.ToString());
            else
                fieldDict.Add(_NON_MATCHING_QUERY, query.NonMatchingQuery);

            fieldDict.AddObject(QuerySerializer._QUERY_NAME, query.QueryName);

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add(QueryTypeEnum.Indices.ToString(), fieldDict);

            serializer.Serialize(writer, queryDict);
        }
    }
}
