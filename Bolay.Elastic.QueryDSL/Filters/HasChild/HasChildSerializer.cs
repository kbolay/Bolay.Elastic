using Bolay.Elastic.Exceptions;
using Bolay.Elastic.QueryDSL.Queries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.HasChild
{
    public class HasChildSerializer : JsonConverter
    {
        private const string _TYPE = "type";
        private const string _FILTER = "filter";
        private const string _QUERY = "query";

        internal const bool _CACHE_DEFAULT = false;
        
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(FilterTypeEnum.HasChild.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            HasChildFilter filter = null;
            if(fieldDict.ContainsKey(_FILTER))
            {
                filter = new HasChildFilter(
                                fieldDict.GetString(_TYPE),
                                JsonConvert.DeserializeObject<IFilter>(fieldDict.GetString(_FILTER)));
            }
            else
            {
                filter = new HasChildFilter(
                                fieldDict.GetString(_TYPE),
                                JsonConvert.DeserializeObject<IQuery>(fieldDict.GetString(_QUERY)));
            }

            filter.FilterName = fieldDict.GetStringOrDefault(FilterSerializer._FILTER_NAME);

            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is HasChildFilter))
                throw new SerializeTypeException<HasChildFilter>();

            HasChildFilter filter = value as HasChildFilter;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.Add(_TYPE, filter.ChildType);
            fieldDict.AddObject(_FILTER, filter.Filter);
            fieldDict.AddObject(_QUERY, filter.Query);
            fieldDict.AddObject(FilterSerializer._FILTER_NAME, filter.FilterName);

            Dictionary<string, object> filterDict = new Dictionary<string,object>();
            filterDict.Add(FilterTypeEnum.HasChild.ToString(), fieldDict);

            serializer.Serialize(writer, filterDict);
        }
    }
}
