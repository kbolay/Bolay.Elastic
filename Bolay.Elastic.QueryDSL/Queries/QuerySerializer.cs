using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries
{
    public class QuerySerializer : JsonConverter
    {
        internal const string _QUERY_NAME = "_name";
        internal const string _BOOST = "boost";

        internal const Double _BOOST_DEFAULT = 1.0;

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> queryDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (queryDict == null || !queryDict.Any() || queryDict.Count != 1)
                return null;

            QueryTypeEnum queryType = QueryTypeEnum.Term;
            queryType = QueryTypeEnum.Find(queryDict.First().Key);
            if (queryType == null)
                throw new Exception("queryType not found.");

            object query = JsonConvert.DeserializeObject(queryDict.First().Value.ToString(), queryType.ImplementationType);
            return query;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
