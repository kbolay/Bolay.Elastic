using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Types
{
    public class DocumentPropertySerializer : JsonConverter
    {
        private const string _TYPE = "type";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> propertyDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> internalDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(propertyDict.First().Value.ToString());

            PropertyTypeEnum propertyType = PropertyTypeEnum.Object;
            if (internalDict.ContainsKey(_TYPE))
            {
                string propertyTypeStr = internalDict.GetString(_TYPE);
                propertyType = PropertyTypeEnum.Find(propertyTypeStr);
                if (propertyType == null)
                    throw new Exception(propertyTypeStr + " is not a valid document property type.");
            }

            string propertyStr = JsonConvert.SerializeObject(propertyDict);
            return JsonConvert.DeserializeObject(propertyStr, propertyType.ImplementationType) as IDocumentProperty;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
