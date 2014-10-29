using Bolay.Elastic.Exceptions;
using Bolay.Elastic.QueryDSL.MinimumShouldMatch;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.Bool
{    
    public class BoolSerializer : JsonConverter
    {
        private const string _MUST = "must";
        private const string _MUST_NOT = "must_not";
        private const string _SHOULD = "should";
        private const string _MINIMUM_SHOULD_MATCH = "minimum_should_match";
        private const string _DISABLE_COORDS = "disable_coords";

        internal const bool _CACHE_DEFAULT = false;
        internal static MinimumShouldMatchBase _MINIMUM_SHOULD_MATCH_DEFAULT = new IntegerMatch(1);
        internal const bool _DISABLE_COORDS_DEFAULT = false;

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> boolDict = serializer.Deserialize<Dictionary<string, object>>(reader);

            BoolFilter filter = new BoolFilter();
            if (boolDict.First().Key.Equals(FilterTypeEnum.Bool.ToString(), StringComparison.OrdinalIgnoreCase))
                boolDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(boolDict.First().Value.ToString());

            if (boolDict.ContainsKey(_MUST))
                filter.MustFilters = JsonConvert.DeserializeObject<IEnumerable<IFilter>>(boolDict[_MUST].ToString());
            if (boolDict.ContainsKey(_MUST_NOT))
                filter.MustNotFilters = JsonConvert.DeserializeObject<IEnumerable<IFilter>>(boolDict[_MUST_NOT].ToString());
            if (boolDict.ContainsKey(_SHOULD))
                filter.ShouldFilters = JsonConvert.DeserializeObject<IEnumerable<IFilter>>(boolDict[_SHOULD].ToString());

            filter.DisableCoords = boolDict.GetBool(_DISABLE_COORDS, _DISABLE_COORDS_DEFAULT);

            if (boolDict.ContainsKey(_MINIMUM_SHOULD_MATCH))
                filter.MinimumShouldMatch = MinimumShouldMatchBase.BuildMinimumShouldMatch(boolDict[_MINIMUM_SHOULD_MATCH].ToString());
            else
                filter.MinimumShouldMatch = _MINIMUM_SHOULD_MATCH_DEFAULT;

            FilterSerializer.DeserializeBaseValues(filter, _CACHE_DEFAULT, boolDict);

            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is BoolFilter))
                throw new SerializeTypeException<BoolFilter>();

            BoolFilter filter = value as BoolFilter;
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();

            if (filter.MustFilters != null && filter.MustFilters.Any())
                fieldDict.Add(_MUST, filter.MustFilters);
            if (filter.MustNotFilters != null && filter.MustNotFilters.Any())
                fieldDict.Add(_MUST_NOT, filter.MustNotFilters);
            if (filter.ShouldFilters != null && filter.ShouldFilters.Any())
                fieldDict.Add(_SHOULD, filter.ShouldFilters);

            fieldDict.AddObject(_MINIMUM_SHOULD_MATCH, filter.MinimumShouldMatch.GetValue(), _MINIMUM_SHOULD_MATCH_DEFAULT.GetValue());
            fieldDict.AddObject(_DISABLE_COORDS, filter.DisableCoords);

            FilterSerializer.SerializeBaseValues(filter, _CACHE_DEFAULT, fieldDict);

            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add(FilterTypeEnum.Bool.ToString(), fieldDict);

            serializer.Serialize(writer, dict);
        }
    }
}
