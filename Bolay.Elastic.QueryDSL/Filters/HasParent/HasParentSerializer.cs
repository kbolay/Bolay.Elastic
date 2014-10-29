using Bolay.Elastic.Exceptions;
using Bolay.Elastic.QueryDSL.Queries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.HasParent
{
    public class HasParentSerializer : JsonConverter
    {
        private const string _PARENT_TYPE = "parent_type";
        private const string _FILTER = "filter";
        private const string _QUERY = "query";
        
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(FilterTypeEnum.HasParent.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            HasParentFilter filter = null;
            if (fieldDict.ContainsKey(_FILTER))
            {
                filter = new HasParentFilter(
                                fieldDict.GetString(_PARENT_TYPE),
                                JsonConvert.DeserializeObject<IFilter>(fieldDict.GetString(_FILTER)));
            }
            else
            {
                filter = new HasParentFilter(
                                fieldDict.GetString(_PARENT_TYPE),
                                JsonConvert.DeserializeObject<IQuery>(fieldDict.GetString(_QUERY)));
            }

            filter.FilterName = fieldDict.GetStringOrDefault(FilterSerializer._FILTER_NAME);

            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is HasParentFilter))
                throw new SerializeTypeException<HasParentFilter>();

            HasParentFilter filter = value as HasParentFilter;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.Add(_PARENT_TYPE, filter.ParentType);
            fieldDict.AddObject(_FILTER, filter.Filter);
            fieldDict.AddObject(_QUERY, filter.Query);

            Dictionary<string, object> filterDict = new Dictionary<string, object>();
            filterDict.Add(FilterTypeEnum.HasParent.ToString(), fieldDict);

            serializer.Serialize(writer, filterDict);
        }
    }
}
