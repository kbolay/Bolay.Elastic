using Bolay.Elastic.Exceptions;
using Bolay.Elastic.QueryDSL.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Filtered
{
    public class FilteredQuerySerializer : JsonConverter
    {
        private const string _FILTERED = "filtered";
        private const string _QUERY = "query";
        private const string _FILTER = "filter";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(_FILTERED))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict[_FILTERED].ToString());

            IQuery query = JsonConvert.DeserializeObject<IQuery>(fieldDict.GetString(_QUERY));
            IFilter filter = JsonConvert.DeserializeObject<IFilter>(fieldDict.GetString(_FILTER));
            
            FilteredQuery filteredQuery = new FilteredQuery(query, filter);
            filteredQuery.QueryName = fieldDict.GetStringOrDefault(QuerySerializer._QUERY_NAME);
            return filteredQuery;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is FilteredQuery))
                throw new SerializeTypeException<FilteredQuery>();

            FilteredQuery query = value as FilteredQuery;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.Add(_QUERY, query.Query);
            fieldDict.Add(_FILTER, query.Filter);
            fieldDict.AddObject(QuerySerializer._QUERY_NAME, query.QueryName);

            Dictionary<string, object> filteredDict = new Dictionary<string, object>();
            filteredDict.Add(_FILTERED, fieldDict);
            
            serializer.Serialize(writer, filteredDict);
        }
    }
}
