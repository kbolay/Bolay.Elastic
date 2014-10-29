using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.Indices
{
    public class IndicesSerializer : JsonConverter
    {
        private const string _INDICES = "indices";
        private const string _INDEX = "index";
        private const string _FILTER = "filter";
        private const string _NON_MATCHING_FILTER = "no_match_filter";

        internal const bool _CACHE_DEFAULT = false;

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(FilterTypeEnum.Indices.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            List<string> indices = new List<string>();
            if (fieldDict.ContainsKey(_INDEX))
                indices.Add(fieldDict.GetString(_INDEX));
            else if (fieldDict.ContainsKey(_INDICES))
                indices = JsonConvert.DeserializeObject<List<string>>(fieldDict.GetString(_INDICES));
            else
                throw new RequiredPropertyMissingException(_INDEX + "/" + _INDICES);

            IFilter matchingFilter = JsonConvert.DeserializeObject<IFilter>(fieldDict[_FILTER].ToString());

            NonMatchingTypeEnum nonMatching = NonMatchingTypeEnum.None;
            nonMatching = NonMatchingTypeEnum.Find(fieldDict.GetString(_NON_MATCHING_FILTER));

            IndicesFilter filter = null;
            if (nonMatching != null)
            {
                filter = new IndicesFilter(indices, matchingFilter, nonMatching);
            }                
            else
            {
                filter = new IndicesFilter(indices, matchingFilter, JsonConvert.DeserializeObject<IFilter>(fieldDict.GetString(_NON_MATCHING_FILTER)));
            }

            FilterSerializer.DeserializeBaseValues(filter, _CACHE_DEFAULT, fieldDict);

            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is IndicesFilter))
                throw new SerializeTypeException<IndicesFilter>();

            IndicesFilter filter = value as IndicesFilter;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            if (filter.Indices.Count() > 1)
                fieldDict.Add(_INDICES, filter.Indices);
            else
                fieldDict.Add(_INDEX, filter.Indices.First());

            fieldDict.Add(_FILTER, filter.MatchingFilter);
            if (filter.NonMatchingFilterType != null)
                fieldDict.Add(_NON_MATCHING_FILTER, filter.NonMatchingFilterType.ToString());
            else
                fieldDict.Add(_NON_MATCHING_FILTER, filter.NonMatchingFilter);

            FilterSerializer.SerializeBaseValues(filter, _CACHE_DEFAULT, fieldDict);

            Dictionary<string, object> filterDict = new Dictionary<string, object>();
            filterDict.Add(FilterTypeEnum.Indices.ToString(), fieldDict);            

            serializer.Serialize(writer, filterDict);
        }
    }
}
