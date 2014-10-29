using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.TopChildren
{
    public class TopChildrenSerializer : JsonConverter
    {
        private const string _SCORE = "score";
        private const string _TYPE = "type";
        private const string _QUERY = "query";
        private const string _FACTOR = "factor";
        private const string _SCOPE = "_scope";
        private const string _INCREMENTAL_FACTOR = "incremental_factor";

        internal static ScoreTypeEnum _SCORE_DEFAULT = ScoreTypeEnum.Maximum;
        internal const int _FACTOR_DEFAULT = 5;
        internal const int _INCREMENTAL_FACTOR_DEFAULT = 2;

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(QueryTypeEnum.TopChildren.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            TopChildrenQuery query = new TopChildrenQuery(
                fieldDict.GetString(_TYPE),
                JsonConvert.DeserializeObject<IQuery>(fieldDict.GetString(_QUERY)));

            query.Factor = fieldDict.GetInt32(_FACTOR, _FACTOR_DEFAULT);
            query.IncrementalFactor = fieldDict.GetInt32(_INCREMENTAL_FACTOR, _INCREMENTAL_FACTOR_DEFAULT);
            query.Scope = fieldDict.GetStringOrDefault(_SCOPE);
            query.Score = ScoreTypeEnum.Find(fieldDict.GetString(_SCORE, _SCORE_DEFAULT.ToString()));
            query.QueryName = fieldDict.GetStringOrDefault(QuerySerializer._QUERY_NAME);

            return query;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is TopChildrenQuery))
                throw new SerializeTypeException<TopChildrenQuery>();

            TopChildrenQuery query = value as TopChildrenQuery;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.Add(_TYPE, query.DocumentType);
            fieldDict.Add(_QUERY, query.Query);
            fieldDict.AddObject(_FACTOR, query.Factor, _FACTOR_DEFAULT);
            fieldDict.AddObject(_INCREMENTAL_FACTOR, query.IncrementalFactor, _INCREMENTAL_FACTOR_DEFAULT);
            fieldDict.AddObject(_SCOPE, query.Scope);
            fieldDict.AddObject(_SCORE, query.Score.ToString(), _SCORE_DEFAULT.ToString());
            fieldDict.AddObject(QuerySerializer._QUERY_NAME, query.QueryName);

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add(QueryTypeEnum.TopChildren.ToString(), fieldDict);

            serializer.Serialize(writer, queryDict);
        }
    }
}
