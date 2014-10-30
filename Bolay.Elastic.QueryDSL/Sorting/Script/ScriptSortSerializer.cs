using Bolay.Elastic.Exceptions;
using Bolay.Elastic.QueryDSL.Sorting.Field;
using Bolay.Elastic.Scripts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Sorting.Script
{
    public class ScriptSortSerializer : JsonConverter
    {
        private const string _SCRIPT = "script";
        private const string _PARAMETERS = "params";
        private const string _TYPE = "type";
        private const string _MODE = "mode";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(SortTypeEnum.Script.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            ScriptSort sort = new ScriptSort(fieldDict.DeserializeObject<Bolay.Elastic.Scripts.Script>(), fieldDict.GetString(_TYPE));

            sort.Reverse = fieldDict.GetBool(SortClauseSerializer._REVERSE, SortClauseSerializer._REVERSE_DEFAULT);
            if (fieldDict.ContainsKey(_MODE))
                sort.SortMode = SortModeEnum.Find(fieldDict.GetString(_MODE));
            sort.SortOrder = SortOrderEnum.Find(fieldDict.GetString(SortClauseSerializer._ORDER, SortClauseSerializer._ORDER_DEFAULT.ToString()));

            return sort;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is ScriptSort))
                throw new SerializeTypeException<ScriptSort>();

            ScriptSort sort = value as ScriptSort;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.Add(_TYPE, sort.Type);

            sort.Script.Serialize(fieldDict);

            fieldDict.AddObject(SortClauseSerializer._ORDER, sort.SortOrder, SortClauseSerializer._ORDER_DEFAULT);
            if (sort.SortMode != null)
                fieldDict.Add(_MODE, sort.SortMode.ToString());
            fieldDict.AddObject(SortClauseSerializer._REVERSE, sort.Reverse, SortClauseSerializer._REVERSE_DEFAULT);

            Dictionary<string, object> sortDict = new Dictionary<string, object>();
            sortDict.Add(SortTypeEnum.Script.ToString(), fieldDict);

            serializer.Serialize(writer, sortDict);
        }
    }
}
