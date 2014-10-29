using Bolay.Elastic.Exceptions;
using Bolay.Elastic.QueryDSL.Queries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Rescoring
{
    public class RescoreSerializer : JsonConverter
    {
        private const string _WINDOW_SIZE = "window_size";
        private const string _QUERY = "query";
        private const string _RESCORE_QUERY = "rescore_query";
        private const string _QUERY_WEIGHT = "query_weight";
        private const string _RESCORE_QUERY_WEIGHT = "rescore_query_weight";
        private const string _SCORE_MODE = "score_mode";

        internal static ScoreModeEnum _SCORE_MODE_DEFAULT = ScoreModeEnum.Multiply;
        internal const Double _QUERY_WEIGHT_DEFAULT = 1.0;
        internal const Double _RESCORE_QUERY_WEIGHT_DEFAULT = 1.0;

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            IEnumerable<Dictionary<string, object>> queries = null;
            if (reader.TokenType == JsonToken.StartArray)
            {
                queries = serializer.Deserialize<IEnumerable<Dictionary<string, object>>>(reader);
            }
            else
            {
                queries = new List<Dictionary<string, object>>()
                {
                    serializer.Deserialize<Dictionary<string, object>>(reader)
                };
            }

            List<RescoreQuery> rescoreQueries = new List<RescoreQuery>();
            foreach (Dictionary<string, object> rescoreDict in queries)
            {
                Dictionary<string, object> queryDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(rescoreDict.GetString(_QUERY));
                RescoreQuery rescoreQuery = new RescoreQuery(JsonConvert.DeserializeObject<IQuery>(queryDict.GetString(_RESCORE_QUERY)));

                rescoreQuery.QueryWeight = queryDict.GetDouble(_QUERY_WEIGHT, _QUERY_WEIGHT_DEFAULT);
                rescoreQuery.RescoreQueryWeight = queryDict.GetDouble(_RESCORE_QUERY_WEIGHT, _RESCORE_QUERY_WEIGHT_DEFAULT);
                rescoreQuery.ScoreMode = ScoreModeEnum.Find(queryDict.GetString(_SCORE_MODE, _SCORE_MODE_DEFAULT.ToString()));
                rescoreQuery.WindowSize = rescoreDict.GetInt32OrNull(_WINDOW_SIZE);

                rescoreQueries.Add(rescoreQuery);
            }

            return new Rescore(rescoreQueries);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is Rescore))
                throw new SerializeTypeException<Rescore>();

            Rescore rescore = value as Rescore;

            if (rescore.RescoreQueries.Count() > 1)
            {
                List<Dictionary<string, object>> rescoreQueryDicts = new List<Dictionary<string, object>>();
                foreach (RescoreQuery rescoreQuery in rescore.RescoreQueries)
                {
                    rescoreQueryDicts.Add(SerializeRescore(rescoreQuery));
                }

                serializer.Serialize(writer, rescoreQueryDicts);
            }
            else
            {
                serializer.Serialize(writer, SerializeRescore(rescore.RescoreQueries.First()));
            }
        }

        private Dictionary<string, object> SerializeRescore(RescoreQuery rescoreQuery)
        {
            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.AddObject(_SCORE_MODE, rescoreQuery.ScoreMode.ToString(), _SCORE_MODE_DEFAULT.ToString());
            queryDict.Add(_RESCORE_QUERY, rescoreQuery.Query);
            queryDict.AddObject(_QUERY_WEIGHT, rescoreQuery.QueryWeight, _QUERY_WEIGHT_DEFAULT);
            queryDict.AddObject(_RESCORE_QUERY_WEIGHT, rescoreQuery.RescoreQueryWeight, _RESCORE_QUERY_WEIGHT_DEFAULT);

            Dictionary<string, object> rescoreDict = new Dictionary<string, object>();
            rescoreDict.AddObject(_WINDOW_SIZE, rescoreQuery.WindowSize);
            rescoreDict.Add(_QUERY, queryDict);

            return rescoreDict;
        }
    }
}
