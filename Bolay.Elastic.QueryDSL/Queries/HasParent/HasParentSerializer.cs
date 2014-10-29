using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.HasParent
{
    public class HasParentSerializer : JsonConverter
    {
        private const string _PARENT_TYPE = "parent_type";
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
            if (queryDict.ContainsKey(QueryTypeEnum.HasParent.ToString()))
                queryDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(queryDict.First().Value.ToString());

            if (!queryDict.ContainsKey(_PARENT_TYPE))
                throw new Exception("HasParent expects a type property.");
            if (!queryDict.ContainsKey(_QUERY))
                throw new Exception("HasParent expects a query property.");

            string parentType = queryDict.GetString(_PARENT_TYPE);
            IQuery query = JsonConvert.DeserializeObject<IQuery>(queryDict.GetString(_QUERY));

            HasParentQuery parentQuery = new HasParentQuery(parentType, query);
            ScoreTypeEnum scoreType = ScoreTypeEnum.None;
            parentQuery.ScoreType = ScoreTypeEnum.Find(queryDict.GetStringOrDefault(_SCORE_TYPE));
            if (scoreType == null)
                throw new Exception(queryDict[_SCORE_TYPE].ToString() + " is not an allowed score type for the has parent query.");

            parentQuery.QueryName = queryDict.GetStringOrDefault(QuerySerializer._QUERY_NAME);

            return parentQuery;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is HasParentQuery))
                throw new SerializeTypeException<HasParentQuery>();

            HasParentQuery query = value as HasParentQuery;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();

            fieldDict.Add(_PARENT_TYPE, query.ParentType);
            fieldDict.AddObject(_SCORE_TYPE, query.ScoreType.ToString(), _SCORE_TYPE_DEFAULT.ToString());
            fieldDict.Add(_QUERY, query.Query);

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add(QueryTypeEnum.HasParent.ToString(), fieldDict);

            serializer.Serialize(writer, queryDict);
        }
    }
}
