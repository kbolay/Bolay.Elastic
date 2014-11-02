using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Document.Models
{
    public class FieldSerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {         
            var instance = Activator.CreateInstance(objectType);

            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            foreach (KeyValuePair<string, object> kvp in fieldDict)
            {
                PropertyInfo property = FindProperty(objectType, kvp.Key);
                if (!property.PropertyType.IsInstanceOfType(typeof(IEnumerable)))
                {
                    string value = kvp.Value.ToString().Trim(new char[] { '[', ']' });

                    instance.GetType().GetProperty(property.Name).SetValue(instance, JsonConvert.DeserializeObject(value, property.PropertyType));
                }
            }

            return instance;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        private PropertyInfo FindProperty(Type objectType, string propertyName)
        {
            PropertyInfo property = objectType.GetProperties().FirstOrDefault(x => x.Name.Equals(propertyName));
            
            if (property == null)
            {
                IEnumerable<PropertyInfo> props = objectType.GetProperties().Where(x => x.GetCustomAttribute<JsonPropertyAttribute>() != null);
                if(props != null)
                {
                    foreach(PropertyInfo prop in props)
                    {
                        JsonPropertyAttribute jsonAttribute = prop.GetCustomAttribute<JsonPropertyAttribute>();
                        
                        if(jsonAttribute.PropertyName.Equals(propertyName))
                        {
                            property = prop;
                            break;
                        }
                    }
                }
                
                if(property == null)
                {
                    throw new PropertyNotFoundException(objectType, propertyName);
                }                
            }

            return property;
        }
    }
}
