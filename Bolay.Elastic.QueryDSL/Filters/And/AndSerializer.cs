using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.And
{
    public class AndSerializer : JsonConverter
    {
        private const string _FILTERS = "filters";

        internal const bool _CACHE_DEFAULT = false;

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            IEnumerable<IFilter> filters = null;
            
            if (fieldDict.ContainsKey(FilterTypeEnum.And.ToString()))
            {
                try
                {
                    fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());       
                }
                catch
                { 
                    return new AndFilter(JsonConvert.DeserializeObject<IEnumerable<IFilter>>(fieldDict.First().Value.ToString()));
                }
            }

            AndFilter filter = new AndFilter(JsonConvert.DeserializeObject<IEnumerable<IFilter>>(fieldDict.GetString(_FILTERS)));
            FilterSerializer.DeserializeBaseValues(filter, _CACHE_DEFAULT, fieldDict);  
          
            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is AndFilter))
                throw new SerializeTypeException<AndFilter>();

            AndFilter filter = value as AndFilter;

            Dictionary<string, object> filterDict = new Dictionary<string, object>();

            if (filter.Cache != _CACHE_DEFAULT)
            {
                Dictionary<string, object> fieldDict = new Dictionary<string, object>();
                fieldDict.Add(_FILTERS, filter.Filters);
                FilterSerializer.SerializeBaseValues(filter, _CACHE_DEFAULT, fieldDict);

                filterDict.Add(FilterTypeEnum.And.ToString(), fieldDict);
            }
            else
            {
                filterDict.Add(FilterTypeEnum.And.ToString(), filter.Filters);
            }

            serializer.Serialize(writer, filterDict);
        }
    }
}
