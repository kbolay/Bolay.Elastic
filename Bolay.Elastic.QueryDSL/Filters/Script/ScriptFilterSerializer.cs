using Bolay.Elastic.Exceptions;
using Bolay.Elastic.Scripts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.Script
{
    public class ScriptFilterSerializer : JsonConverter
    {
        private const string _SCRIPT = "script";
        private const string _PARAMETERS = "params";

        internal const bool _CACHE_DEFAULT = false;

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // why can script have the same name as its property... dumb.
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if(fieldDict.ContainsKey(FilterTypeEnum.Script.ToString()))
            {
                try
                {
                    fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());
                }
                catch { }
            }

            ScriptFilter filter = new ScriptFilter(fieldDict.DeserializeObject<Bolay.Elastic.Scripts.Script>());

            FilterSerializer.DeserializeBaseValues(filter, _CACHE_DEFAULT, fieldDict);
            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is ScriptFilter))
                throw new SerializeTypeException<ScriptFilter>();

            ScriptFilter filter = value as ScriptFilter;
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            ScriptExtensions.Serialize(filter.Script, fieldDict);            
            FilterSerializer.SerializeBaseValues(filter, _CACHE_DEFAULT, fieldDict);

            Dictionary<string, object> filterDict = new Dictionary<string, object>();
            filterDict.Add(FilterTypeEnum.Script.ToString(), fieldDict);

            serializer.Serialize(writer, filterDict);
        }
    }
}
