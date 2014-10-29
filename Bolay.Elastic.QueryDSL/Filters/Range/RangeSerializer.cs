using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.Range
{
    public class RangeSerializer : JsonConverter
    {
        private const string _GREATER_THAN = "gt";
        private const string _GREATER_THAN_OR_EQUAL_TO = "gte";
        private const string _LESS_THAN = "lt";
        private const string _LESS_THAN_OR_EQUAL_TO = "lte";
        private const string _EXECUTION = "execution";

        internal const bool _CACHE_DEFAULT = false;
        internal const bool _INDEX_EXECUTION_CACHE_DEFAULT = true;

        private static List<string> _KnownFields = new List<string>()
        {
            FilterSerializer._CACHE,
            FilterSerializer._CACHE_KEY,
            _EXECUTION
        };

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(FilterTypeEnum.Range.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            KeyValuePair<string, object> rangeKvp = fieldDict.FirstOrDefault(x => !_KnownFields.Contains(x.Key, StringComparer.OrdinalIgnoreCase));

            if (string.IsNullOrWhiteSpace(rangeKvp.Key))
                throw new RequiredPropertyMissingException("field");

            RangeFilterBase filter = BuildRangeFilter(rangeKvp);

            if (fieldDict.ContainsKey(_EXECUTION))
            { 
                // warm the enum
                ExecutionTypeEnum type = ExecutionTypeEnum.FieldData;
                type = ExecutionTypeEnum.Find(fieldDict.GetString(_EXECUTION));
                if(type == null)
                    throw new ParsingException<ExecutionTypeEnum>(_EXECUTION);

                filter.ExecutionType = type;
            }

            if (filter.ExecutionType == ExecutionTypeEnum.Index)
                FilterSerializer.DeserializeBaseValues(filter, _INDEX_EXECUTION_CACHE_DEFAULT, fieldDict);
            else
                FilterSerializer.DeserializeBaseValues(filter, _CACHE_DEFAULT, fieldDict);

            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is RangeFilterBase))
                throw new SerializeTypeException<RangeFilterBase>();

            RangeFilterBase filter = value as RangeFilterBase;
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();

            fieldDict.AddObject(_GREATER_THAN, filter.GreaterThan);
            fieldDict.AddObject(_LESS_THAN, filter.LessThan);
            fieldDict.AddObject(_GREATER_THAN_OR_EQUAL_TO, filter.GreaterThanOrEqualTo);
            fieldDict.AddObject(_LESS_THAN_OR_EQUAL_TO, filter.LessThanOrEqualTo);

            Dictionary<string, object> internalDict = new Dictionary<string, object>();
            internalDict.Add(filter.Field, fieldDict);

            if(filter.ExecutionType != null)
                internalDict.AddObject(_EXECUTION, filter.ExecutionType.ToString());

            if (filter.ExecutionType != null && filter.ExecutionType == ExecutionTypeEnum.Index)
                FilterSerializer.SerializeBaseValues(filter, _INDEX_EXECUTION_CACHE_DEFAULT, internalDict);
            else
                FilterSerializer.SerializeBaseValues(filter, _CACHE_DEFAULT, fieldDict);

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add(FilterTypeEnum.Range.ToString(), internalDict);

            serializer.Serialize(writer, queryDict);
        }

        internal static RangeFilterBase BuildRangeFilter(KeyValuePair<string, object> rangeKvp)
        {
            string field = rangeKvp.Key;

            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(rangeKvp.Value.ToString());

            return BuildRangeFilter(field, fieldDict);
        }

        private static RangeFilterBase BuildRangeFilter(string fieldName, Dictionary<string, object> fieldDict)
        {
            object value = fieldDict.First().Value;

            if (value is Int32 || value is Int64)
            {
                return new IntegerRangeFilter(fieldName,
                    fieldDict.GetInt64OrNull(_GREATER_THAN),
                    fieldDict.GetInt64OrNull(_LESS_THAN),
                    fieldDict.GetInt64OrNull(_GREATER_THAN_OR_EQUAL_TO),
                    fieldDict.GetInt64OrNull(_LESS_THAN_OR_EQUAL_TO));
            }

            if (value is Double || value is float)
            {
                return new DoubleRangeFilter(fieldName,
                    fieldDict.GetDoubleOrNull(_GREATER_THAN),
                    fieldDict.GetDoubleOrNull(_LESS_THAN),
                    fieldDict.GetDoubleOrNull(_GREATER_THAN_OR_EQUAL_TO),
                    fieldDict.GetDoubleOrNull(_LESS_THAN_OR_EQUAL_TO));
            }

            try
            {
                DateTime.Parse(value.ToString());
                return new DateTimeRangeFilter(fieldName,
                    fieldDict.GetDateTimeOrNull(_GREATER_THAN),
                    fieldDict.GetDateTimeOrNull(_LESS_THAN),
                    fieldDict.GetDateTimeOrNull(_GREATER_THAN_OR_EQUAL_TO),
                    fieldDict.GetDateTimeOrNull(_LESS_THAN_OR_EQUAL_TO));
            }
            catch
            {
                return new StringRangeFilter(fieldName,
                    fieldDict.GetStringOrDefault(_GREATER_THAN),
                    fieldDict.GetStringOrDefault(_LESS_THAN),
                    fieldDict.GetStringOrDefault(_GREATER_THAN_OR_EQUAL_TO),
                    fieldDict.GetStringOrDefault(_LESS_THAN_OR_EQUAL_TO));
            }
        }
    }
}
