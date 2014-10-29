using Bolay.Elastic.Coordinates;
using Bolay.Elastic.Distance;
using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Sorting.GeoDistance
{
    public class GeoDistanceSerializer : JsonConverter
    {
        private const string _UNIT = "unit";
        private const string _MODE = "mode";

        private static List<string> _KnownFields = new List<string>()
        {
            _UNIT,
            _MODE,
            SortClauseSerializer._ORDER,
            SortClauseSerializer._REVERSE
        };

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(SortTypeEnum.GeoDistance.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            KeyValuePair<string, object> fieldKvp = fieldDict.FirstOrDefault(x => !_KnownFields.Contains(x.Key, StringComparer.OrdinalIgnoreCase));

            if (string.IsNullOrWhiteSpace(fieldKvp.Key))
                throw new RequiredPropertyMissingException("GeoPointField");

            string fieldName = fieldKvp.Key;
            CoordinatePoint centerPoint = CoordinatePointSerializer.DeserializeCoordinatePoint(fieldKvp.Value.ToString());
            DistanceUnitEnum unit = DistanceUnitEnum.Kilometer;
            unit = DistanceUnitEnum.Find(fieldDict.GetString(_UNIT));
            GeoDistanceSort sort = new GeoDistanceSort(fieldName, centerPoint, unit);

            sort.Reverse = fieldDict.GetBool(SortClauseSerializer._REVERSE, SortClauseSerializer._REVERSE_DEFAULT);
            if (fieldDict.ContainsKey(_MODE))
                sort.SortMode = SortModeEnum.Find(fieldDict.GetString(_MODE));
            sort.SortOrder = SortOrderEnum.Find(fieldDict.GetString(SortClauseSerializer._ORDER, SortClauseSerializer._ORDER_DEFAULT.ToString()));

            return sort;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is GeoDistanceSort))
                throw new SerializeTypeException<GeoDistanceSort>();

            GeoDistanceSort sort = value as GeoDistanceSort;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.Add(sort.Field, sort.CenterPoint);
            fieldDict.Add(_UNIT, sort.Unit.ToString());
            fieldDict.AddObject(SortClauseSerializer._ORDER, sort.SortOrder, SortClauseSerializer._ORDER_DEFAULT);
            if (sort.SortMode != null)
                fieldDict.AddObject(_MODE, sort.SortMode);
            fieldDict.AddObject(SortClauseSerializer._REVERSE, sort.Reverse, SortClauseSerializer._REVERSE_DEFAULT);

            Dictionary<string, object> sortDict = new Dictionary<string, object>();
            sortDict.Add(SortTypeEnum.GeoDistance.ToString(), fieldDict);

            serializer.Serialize(writer, sortDict);
        }
    }
}