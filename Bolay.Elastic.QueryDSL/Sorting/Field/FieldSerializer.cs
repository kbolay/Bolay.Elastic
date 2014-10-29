using Bolay.Elastic.Exceptions;
using Bolay.Elastic.QueryDSL.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Sorting.Field
{
    public class FieldSerializer : JsonConverter
    {
        private const string _MODE = "mode";
        private const string _IGNORE_UNMAPPED = "ignore_unmapped";
        private const string _NESTED_FILTER = "nested_filter";
        private const string _NESTED_PATH = "nested_path";
        private const string _MISSING = "missing";

        internal const bool _IGNORE_UNMAPPED_DEFAULT = false;

        private static List<string> _KnownFields = new List<string>()
        {
            SortClauseSerializer._ORDER,
            SortClauseSerializer._REVERSE,
            _IGNORE_UNMAPPED,
            SortTypeEnum.GeoDistance.ToString(),
            SortTypeEnum.Script.ToString()
        };

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.ValueType == typeof(string))
                return new FieldSort(reader.Value.ToString());

            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            KeyValuePair<string, object> fieldKvp = fieldDict.First();
            string fieldName = fieldKvp.Key;
            try
            {
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldKvp.Value.ToString());
            }
            catch
            {
                SortOrderEnum order = SortOrderEnum.Ascending;
                order = SortOrderEnum.Find(fieldKvp.Value.ToString());
                return new FieldSort(fieldName) { SortOrder = order };
            }

            FieldSort sort = null;
            if(fieldDict.ContainsKey(_NESTED_FILTER))
            {
                IFilter nestedFilter = JsonConvert.DeserializeObject<IFilter>(fieldDict.GetString(_NESTED_FILTER));
                sort = new FieldSort(fieldName, nestedFilter);
            }
            else if (fieldDict.ContainsKey(_NESTED_PATH))
            {
                sort = new FieldSort(fieldName, fieldDict.GetString(_NESTED_PATH));
            }
            
            if(fieldDict.ContainsKey(_MODE))
            {
                SortModeEnum mode = SortModeEnum.Average;
                mode = SortModeEnum.Find(fieldDict.GetString(_MODE));
                sort.SortMode = mode;
            }

            sort.SortOrder = SortOrderEnum.Find(fieldDict.GetString(SortClauseSerializer._ORDER, SortClauseSerializer._ORDER_DEFAULT.ToString()));
            sort.Reverse = fieldDict.GetBoolOrDefault(SortClauseSerializer._REVERSE);
            sort.IgnoreUnmappedField = fieldDict.GetBool(_IGNORE_UNMAPPED, _IGNORE_UNMAPPED_DEFAULT);

            if (fieldDict.ContainsKey(_MISSING))
            {
                sort.Missing = JsonConvert.DeserializeObject<MissingValue>(fieldDict.GetString(_MISSING));
            }

            return sort;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is FieldSort))
                throw new SerializeTypeException<FieldSort>();

            FieldSort sort = value as FieldSort;
            Dictionary<string, object> fieldDict = new Dictionary<string,object>();
            if (sort.SortMode == null && sort.IgnoreUnmappedField == _IGNORE_UNMAPPED_DEFAULT &&
                sort.Missing == null && sort.NestedFilter == null && sort.NestedPath == null &&
                sort.Reverse == SortClauseSerializer._REVERSE_DEFAULT)
            {
                if (sort.SortOrder == SortClauseSerializer._ORDER_DEFAULT)
                {
                    serializer.Serialize(writer, sort.Field);
                    return;
                }
                else
                {
                    fieldDict.Add(sort.Field, sort.SortOrder.ToString());
                    serializer.Serialize(writer, fieldDict);
                    return;
                }
            }

            Dictionary<string, object> internalDict = new Dictionary<string, object>();
            internalDict.AddObject(SortClauseSerializer._ORDER, sort.SortOrder, SortClauseSerializer._ORDER_DEFAULT);
            if(sort.SortMode != null)
                internalDict.AddObject(_MODE, sort.SortMode.ToString());
            internalDict.AddObject(_IGNORE_UNMAPPED, sort.IgnoreUnmappedField, _IGNORE_UNMAPPED_DEFAULT);
            if(sort.Missing != null)
                internalDict.AddObject(_MISSING, JsonConvert.SerializeObject(sort.Missing));
            internalDict.AddObject(SortClauseSerializer._REVERSE, sort.Reverse, SortClauseSerializer._REVERSE_DEFAULT);
            internalDict.AddObject(_NESTED_FILTER, sort.NestedFilter);
            internalDict.AddObject(_NESTED_PATH, sort.NestedPath);

            fieldDict.Add(sort.Field, internalDict);

            serializer.Serialize(writer, fieldDict);
        }
    }
}
