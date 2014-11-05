using Bolay.Elastic.Exceptions;
using Bolay.Elastic.Mapping.Properties.Object;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Properties.Nested
{
    internal class NestedObjectPropertySerializer : JsonConverter
    {
        private const string _INCLUDE_IN_PARENT = "include_in_parent";
        private const string _INCLUDE_IN_ROOT = "include_in_root";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> propDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            NestedObjectProperty prop = new NestedObjectProperty(propDict.First().Key);

            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(propDict.First().Value.ToString());

            ObjectProperty.Deserialize(prop, fieldDict);
            prop.IncludeInParent = fieldDict.GetBool(_INCLUDE_IN_PARENT, NestedObjectProperty._INCLUDE_IN_PARENT_DEFAULT);
            prop.IncludeInRoot = fieldDict.GetBool(_INCLUDE_IN_ROOT, NestedObjectProperty._INCLUDE_IN_ROOT_DEFAULT);

            return prop;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is NestedObjectProperty))
                throw new SerializeTypeException<NestedObjectProperty>();

            NestedObjectProperty prop = value as NestedObjectProperty;
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.AddObject(_INCLUDE_IN_PARENT, prop.IncludeInParent, NestedObjectProperty._INCLUDE_IN_PARENT_DEFAULT);
            fieldDict.AddObject(_INCLUDE_IN_ROOT, prop.IncludeInRoot, NestedObjectProperty._INCLUDE_IN_ROOT_DEFAULT);
            ObjectProperty.Serialize(prop, fieldDict);

            Dictionary<string, object> propDict = new Dictionary<string, object>();
            propDict.Add(prop.Name, fieldDict);

            serializer.Serialize(writer, propDict);
        }
    }
}
