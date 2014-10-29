using Bolay.Elastic.QueryDSL.Sorting.Field;
using Bolay.Elastic.QueryDSL.Sorting.GeoDistance;
using Bolay.Elastic.QueryDSL.Sorting.Script;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Sorting
{
    public class SortClauseSerializer : JsonConverter
    {
        internal const string _ORDER = "order";
        internal const string _REVERSE = "reverse";

        internal static SortOrderEnum _ORDER_DEFAULT = SortOrderEnum.Ascending;
        internal const bool _REVERSE_DEFAULT = false;

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.ValueType == typeof(string))
                return new FieldSort(reader.Value.ToString());
            
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            
            // { "field": "asc" } - this is a field clause with the sort order
            // { "field": { "order": "asc", "mode": "sum", "ignore_unmapped": true } } - this is a field clause
            // { "field": { "missing": "_first" } - this is a missing clause
            // { "field": { "nested_filter": { IFILTER }, ... }
            // { "field": { "nested_path": "nested.field", ... }
            // { "_geo_distance": { "geo_point": {}, "unit": "km", "order": "asc" } }
            // { "_script": { "script": "asdf", "params": { ... }, "type": "number", "order": "asc" } }
            // {

            string firstKey = fieldDict.First().Key;

            if(SortTypeEnum.GeoDistance.ToString().Equals(firstKey, StringComparison.OrdinalIgnoreCase))
            { 
                // geo distance sort
                return JsonConvert.DeserializeObject<GeoDistanceSort>(reader.Value.ToString());
            }
            else if (SortTypeEnum.Script.ToString().Equals(firstKey, StringComparison.OrdinalIgnoreCase))
            {
                // script sort
                return JsonConvert.DeserializeObject<ScriptSort>(reader.Value.ToString());
            }
            else
            {
                // field sort
                return JsonConvert.DeserializeObject<FieldSort>(reader.Value.ToString());
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
