using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.DisMax
{
    public class DisMaxSerializer : JsonConverter
    {
        private const string _DIS_MAX = "dis_max";
        private const string _TIE_BREAKER = "tie_breaker";
        private const string _QUERIES = "queries";

        internal const Double _TIE_BREAKER_DEFAULT = default(Double);

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(_DIS_MAX))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            DisjunctionMaxQuery query = new DisjunctionMaxQuery();
            query.TieBreaker = fieldDict.GetDouble(_TIE_BREAKER, _TIE_BREAKER_DEFAULT);
            query.Boost = fieldDict.GetDouble(QuerySerializer._BOOST, QuerySerializer._BOOST_DEFAULT);
            query.Queries = JsonConvert.DeserializeObject<IEnumerable<IQuery>>(fieldDict.GetString(_QUERIES));
            query.QueryName = fieldDict.GetStringOrDefault(QuerySerializer._QUERY_NAME);
            return query;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is DisjunctionMaxQuery))
                throw new SerializeTypeException<DisjunctionMaxQuery>();

            DisjunctionMaxQuery query = value as DisjunctionMaxQuery;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.AddObject(QuerySerializer._BOOST, query.Boost, QuerySerializer._BOOST_DEFAULT);
            fieldDict.AddObject(_TIE_BREAKER, query.TieBreaker, _TIE_BREAKER_DEFAULT);
            fieldDict.Add(_QUERIES, query.Queries);
            fieldDict.AddObject(QuerySerializer._QUERY_NAME, query.QueryName);

            Dictionary<string, object> disMaxDict = new Dictionary<string,object>();
            disMaxDict.Add(_DIS_MAX, fieldDict);

            serializer.Serialize(writer, disMaxDict);
        }
    }
}
