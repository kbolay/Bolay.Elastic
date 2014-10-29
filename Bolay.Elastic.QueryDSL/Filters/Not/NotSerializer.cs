using Bolay.Elastic.Exceptions;
using Bolay.Elastic.QueryDSL.Queries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.Not
{
    public class NotSerializer : JsonConverter
    {
        private const string _FILTER = "filter";

        internal const bool _CACHE_DEFAULT = false;

        private static List<string> _KnownFields = new List<string>()
        {
            FilterSerializer._CACHE,
            FilterSerializer._CACHE_KEY,
            _FILTER
        };

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(FilterTypeEnum.Not.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            NotFilter filter = null;
            if (fieldDict.ContainsKey(_FILTER))
            {
                filter = new NotFilter(JsonConvert.DeserializeObject<IFilter>(fieldDict.GetString(_FILTER)));
            }
            else
            {
                KeyValuePair<string, object> queryKvp = fieldDict.FirstOrDefault(x => !_KnownFields.Contains(x.Key));
                if (string.IsNullOrWhiteSpace(queryKvp.Key))
                    throw new RequiredPropertyMissingException("filter");

                QueryTypeEnum queryType = QueryTypeEnum.Term;
                queryType = QueryTypeEnum.Find(queryKvp.Key);
                if (queryType == null)
                    throw new Exception("Unable to find query type.");
                IQuery query = JsonConvert.DeserializeObject(fieldDict.First().Value.ToString(), queryType.ImplementationType) as IQuery;

                filter = new NotFilter(query);
            }

            FilterSerializer.DeserializeBaseValues(filter, _CACHE_DEFAULT, fieldDict);
            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is NotFilter))
                throw new SerializeTypeException<NotFilter>();

            NotFilter filter = value as NotFilter;            
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();

            if (filter.Filter != null)
            {
                fieldDict.Add(_FILTER, filter.Filter);
            }
            else if(filter.Query != null)
            {
                string queryJson = JsonConvert.SerializeObject(filter.Query);
                Dictionary<string, object> queryDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(queryJson);
                KeyValuePair<string, object> queryKvp = queryDict.First();
                fieldDict.Add(queryKvp.Key, JsonConvert.DeserializeObject<Dictionary<string, object>>(queryKvp.Value.ToString()));
            }

            FilterSerializer.SerializeBaseValues(filter, _CACHE_DEFAULT, fieldDict);

            Dictionary<string, object> filterDict = new Dictionary<string, object>();
            filterDict.Add(FilterTypeEnum.Not.ToString(), fieldDict);

            serializer.Serialize(writer, filterDict);
        }
    }
}
