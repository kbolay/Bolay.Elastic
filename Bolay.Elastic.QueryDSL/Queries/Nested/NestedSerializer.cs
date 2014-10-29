using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Nested
{
    public class NestedSerializer : JsonConverter
    {
        private const string _PATH = "path";        
        private const string _SCORE_MODE = "score_mode";
        private const string _QUERY = "query";
        private const string _JOIN = "join";

        internal static ScoreTypeEnum _SCORE_MODE_DEFAULT = ScoreTypeEnum.Average;
        internal const bool _JOIN_DEFAULT = true;

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(QueryTypeEnum.Nested.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            string path = fieldDict.GetString(_PATH);
            IQuery query = JsonConvert.DeserializeObject<IQuery>(fieldDict.GetString(_QUERY));

            NestedQuery nestedQuery = new NestedQuery(path, query);
            nestedQuery.ScoreMode = ScoreTypeEnum.Find(fieldDict.GetString(_SCORE_MODE, _SCORE_MODE_DEFAULT.ToString()));
            nestedQuery.Join = fieldDict.GetBool(_JOIN, _JOIN_DEFAULT);
            nestedQuery.QueryName = fieldDict.GetStringOrDefault(QuerySerializer._QUERY_NAME);
            return nestedQuery;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is NestedQuery))
                throw new SerializeTypeException<NestedQuery>();

            NestedQuery query = value as NestedQuery;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.Add(_PATH, query.Path);
            fieldDict.AddObject(_SCORE_MODE, query.ScoreMode.ToString(), _SCORE_MODE_DEFAULT.ToString());
            fieldDict.Add(_QUERY, query.Query);
            fieldDict.AddObject(_JOIN, query.Join, _JOIN_DEFAULT);
            fieldDict.AddObject(QuerySerializer._QUERY_NAME, query.QueryName);

            Dictionary<string, object> queryDict = new Dictionary<string,object>();
            queryDict.Add(QueryTypeEnum.Nested.ToString(), fieldDict);

            serializer.Serialize(writer, queryDict);
        }
    }
}
