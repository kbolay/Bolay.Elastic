using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Scripts
{
    public class ScriptParameterCollectionSerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> paramDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            ScriptParameterCollection parameters = new ScriptParameterCollection();
            foreach (KeyValuePair<string, object> paramKvp in paramDict)
            {
                parameters.Add(new ScriptParameter(paramKvp.Key, paramKvp.Value));
            }

            if (!parameters.Any())
                return null;

            return parameters;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is ScriptParameterCollection))
                throw new SerializeTypeException<IEnumerable<ScriptParameter>>();

            ScriptParameterCollection parameters = value as ScriptParameterCollection;

            Dictionary<string, object> paramDict = new Dictionary<string, object>();
            foreach (ScriptParameter parameter in parameters)
            {
                paramDict.Add(parameter.Name, parameter.Value);
            }

            if (paramDict.Any())
                serializer.Serialize(writer, paramDict);
        }
    }
}
