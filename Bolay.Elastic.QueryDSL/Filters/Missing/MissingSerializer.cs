using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.Missing
{
    public class MissingSerializer : JsonConverter
    {
        private const string _FIELD = "field";
        private const string _EXISTENCE = "existence";
        private const string _NULL_VALUE = "null_value";

        internal const bool _CACHE_DEFAULT = true;
        internal const bool _EXISTENCE_DEFAULT = false;
        internal const bool _NULL_VALUE_DEFAULT = false;

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(FilterTypeEnum.Missing.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            MissingFilter filter = new MissingFilter(fieldDict.GetString(_FIELD));
            filter.Existence = fieldDict.GetBool(_EXISTENCE, _EXISTENCE_DEFAULT);
            filter.NullValue = fieldDict.GetBool(_NULL_VALUE, _NULL_VALUE_DEFAULT);
            filter.CacheKey = fieldDict.GetStringOrDefault(FilterSerializer._CACHE_KEY);
            filter.FilterName = fieldDict.GetStringOrDefault(FilterSerializer._FILTER_NAME);

            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is MissingFilter))
                throw new SerializeTypeException<MissingFilter>();

            MissingFilter filter = value as MissingFilter;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.Add(_FIELD, filter.Field);
            fieldDict.AddObject(_EXISTENCE, filter.Existence, _EXISTENCE_DEFAULT);
            fieldDict.AddObject(_NULL_VALUE, filter.NullValue, _NULL_VALUE_DEFAULT);
            FilterSerializer.SerializeBaseValues(filter, _CACHE_DEFAULT, fieldDict);

            Dictionary<string, object> filterDict = new Dictionary<string,object>();
            filterDict.Add(FilterTypeEnum.Missing.ToString(), fieldDict);

            serializer.Serialize(writer, filterDict);
        }
    }
}
