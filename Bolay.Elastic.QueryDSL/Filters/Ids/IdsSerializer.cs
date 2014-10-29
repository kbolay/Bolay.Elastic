using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.Ids
{
    public class IdsSerializer : JsonConverter
    {
        private const string _TYPE = "type";
        private const string _IDS = "values";

        internal const bool _CACHE_DEFAULT = true;

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(FilterTypeEnum.Ids.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            IdsFilter filter = new IdsFilter(JsonConvert.DeserializeObject<IEnumerable<string>>(fieldDict.GetString(_IDS)));

            if(fieldDict.ContainsKey(_TYPE))
            {
                string typeValue = fieldDict.GetString(_TYPE);
                try
                {
                    filter.Types = JsonConvert.DeserializeObject<IEnumerable<string>>(fieldDict.GetStringOrDefault(_TYPE));
                }
                catch
                {
                    filter.Types = new List<string>() { typeValue };
                }
            }

            FilterSerializer.DeserializeBaseValues(filter, _CACHE_DEFAULT, fieldDict);
            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is IdsFilter))
                throw new SerializeTypeException<IdsFilter>();

            IdsFilter filter = value as IdsFilter;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.AddObject(_TYPE, filter.Types);
            fieldDict.Add(_IDS, filter.DocumentIds);

            FilterSerializer.SerializeBaseValues(filter, _CACHE_DEFAULT, fieldDict);
            Dictionary<string, object> filterDict = new Dictionary<string, object>();
            filterDict.Add(FilterTypeEnum.Ids.ToString(), fieldDict);

            serializer.Serialize(writer, filterDict);
        }
    }
}
