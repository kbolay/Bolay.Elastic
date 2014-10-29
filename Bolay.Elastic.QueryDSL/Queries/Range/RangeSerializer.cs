using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Range
{
    public class RangeSerializer : JsonConverter
    {
        private const string _GREATER_THAN = "gt";
        private const string _GREATER_THAN_OR_EQUAL_TO = "gte";
        private const string _LESS_THAN = "lt";
        private const string _LESS_THAN_OR_EQUAL_TO = "lte";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(QueryTypeEnum.Range.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            return BuildRangeQuery(fieldDict.First());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is RangeQueryBase))
                throw new SerializeTypeException<RangeQueryBase>();

            RangeQueryBase query = value as RangeQueryBase;
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();

            fieldDict.AddObject(_GREATER_THAN, query.GreaterThan);
            fieldDict.AddObject(_LESS_THAN, query.LessThan);
            fieldDict.AddObject(_GREATER_THAN_OR_EQUAL_TO, query.GreaterThanOrEqualTo);
            fieldDict.AddObject(_LESS_THAN_OR_EQUAL_TO, query.LessThanOrEqualTo);
            fieldDict.AddObject(QuerySerializer._QUERY_NAME, query.QueryName);
            Dictionary<string, object> internalDict = new Dictionary<string, object>();
            internalDict.Add(query.Field, fieldDict);

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add(QueryTypeEnum.Range.ToString(), internalDict);

            serializer.Serialize(writer, queryDict);
        }

        internal static RangeQueryBase BuildRangeQuery(KeyValuePair<string, object> rangeKvp)
        {
            string field = rangeKvp.Key;

            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(rangeKvp.Value.ToString());

            return BuildRangeQuery(field, fieldDict);
        }

        private static RangeQueryBase BuildRangeQuery(string fieldName, Dictionary<string, object> fieldDict)
        {
            object value = fieldDict.First(x => x.Key != QuerySerializer._BOOST).Value;

            RangeQueryBase query = null;

            if (value is Int32 || value is Int64)
            {
                query = new IntegerRangeQuery(fieldName,
                    fieldDict.GetInt64OrNull(_GREATER_THAN),
                    fieldDict.GetInt64OrNull(_LESS_THAN),
                    fieldDict.GetInt64OrNull(_GREATER_THAN_OR_EQUAL_TO),
                    fieldDict.GetInt64OrNull(_LESS_THAN_OR_EQUAL_TO));
            }
            else if (value is Double || value is float)
            {
                query = new DoubleRangeQuery(fieldName,
                    fieldDict.GetDoubleOrNull(_GREATER_THAN),
                    fieldDict.GetDoubleOrNull(_LESS_THAN),
                    fieldDict.GetDoubleOrNull(_GREATER_THAN_OR_EQUAL_TO),
                    fieldDict.GetDoubleOrNull(_LESS_THAN_OR_EQUAL_TO));
            }
            else
            {
                try
                {
                    DateTime.Parse(value.ToString());
                    query = new DateTimeRangeQuery(fieldName,
                        fieldDict.GetDateTimeOrNull(_GREATER_THAN),
                        fieldDict.GetDateTimeOrNull(_LESS_THAN),
                        fieldDict.GetDateTimeOrNull(_GREATER_THAN_OR_EQUAL_TO),
                        fieldDict.GetDateTimeOrNull(_LESS_THAN_OR_EQUAL_TO));
                }
                catch
                {
                    query = new StringRangeQuery(fieldName,
                        fieldDict.GetStringOrDefault(_GREATER_THAN),
                        fieldDict.GetStringOrDefault(_LESS_THAN),
                        fieldDict.GetStringOrDefault(_GREATER_THAN_OR_EQUAL_TO),
                        fieldDict.GetStringOrDefault(_LESS_THAN_OR_EQUAL_TO));
                }
            }

            query.QueryName = fieldDict.GetStringOrDefault(QuerySerializer._QUERY_NAME);

            return query;
        }
    }
}
