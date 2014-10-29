using Bolay.Elastic.Exceptions;
using Bolay.Elastic.QueryDSL.Queries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.Query
{
    public class QuerySerializer : JsonConverter
    {
        internal const bool _CACHE_DEFAULT = false;

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> queryDict = fieldDict;
            if (fieldDict.ContainsKey(FilterTypeEnum.FQuery.ToString()))
                queryDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            QueryFilter filter = new QueryFilter(JsonConvert.DeserializeObject<IQuery>(queryDict.First().Value.ToString()));
            FilterSerializer.DeserializeBaseValues(filter, _CACHE_DEFAULT, queryDict);

            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is QueryFilter))
                throw new SerializeTypeException<QueryFilter>();

            QueryFilter filter = value as QueryFilter;
            Dictionary<string, object> filterDict = new Dictionary<string, object>();
            if (filter.Cache == _CACHE_DEFAULT)
            {
                filterDict.Add(FilterTypeEnum.Query.ToString(), filter.Query);
            }
            else
            {
                Dictionary<string, object> fieldDict = new Dictionary<string, object>();
                fieldDict.Add(FilterTypeEnum.Query.ToString(), filter.Query);
                FilterSerializer.SerializeBaseValues(filter, _CACHE_DEFAULT, fieldDict);

                filterDict.Add(FilterTypeEnum.FQuery.ToString(), fieldDict);
            }

            serializer.Serialize(writer, filterDict);
        }
    }
}
