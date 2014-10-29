using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.HasChild
{
    public class HasChildSerializer : JsonConverter
    {
        private const string _CHILD_TYPE = "type";
        private const string _SCORE_TYPE = "score_type";
        private const string _QUERY = "query";

        internal static ScoreTypeEnum _SCORE_TYPE_DEFAULT = ScoreTypeEnum.None;

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> queryDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if(queryDict.ContainsKey(QueryTypeEnum.HasChild.ToString()))
                queryDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(queryDict.First().Value.ToString());

            string childType = queryDict.GetString(_CHILD_TYPE);
            IQuery query = JsonConvert.DeserializeObject<IQuery>(queryDict.GetString(_QUERY));

            HasChildQuery childQuery = new HasChildQuery(childType, query);
            ScoreTypeEnum scoreType = ScoreTypeEnum.None;
            childQuery.ScoreType = ScoreTypeEnum.Find(queryDict.GetStringOrDefault(_SCORE_TYPE));
            if (scoreType == null)
                throw new Exception(queryDict[_SCORE_TYPE].ToString() + " is not an allowed score type for the has child query.");

            childQuery.QueryName = queryDict.GetStringOrDefault(QuerySerializer._QUERY_NAME);

            return childQuery;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if(!(value is HasChildQuery))
                throw new SerializeTypeException<HasChildQuery>();

            HasChildQuery query = value as HasChildQuery;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();

            fieldDict.Add(_CHILD_TYPE, query.ChildType);
            fieldDict.AddObject(_SCORE_TYPE, query.ScoreType.ToString(), _SCORE_TYPE_DEFAULT.ToString());
            fieldDict.Add(_QUERY, query.Query);
            fieldDict.AddObject(QuerySerializer._QUERY_NAME, query.QueryName);
            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add(QueryTypeEnum.HasChild.ToString(), fieldDict);

            serializer.Serialize(writer, queryDict);
        }
    }
}
