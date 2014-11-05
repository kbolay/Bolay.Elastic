using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Properties.Ip
{
    internal class IpAddressPropertySerializer : JsonConverter
    {
        private const string _PRECISION_STEP = "precision_step";
        private const string _BOOST = "boost";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> propDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            IpAddressProperty prop = new IpAddressProperty(propDict.First().Key);

            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(propDict.First().Value.ToString());

            DocumentPropertyBase.Deserialize(prop, fieldDict);
            prop.Boost = fieldDict.GetDouble(_BOOST, IpAddressProperty._BOOST_DEFAULT);
            prop.PrecisionStep = fieldDict.GetInt32(_PRECISION_STEP, IpAddressProperty._PRECISION_STEP_DEFAULT);

            return prop;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is IpAddressProperty))
                throw new SerializeTypeException<IpAddressProperty>();

            IpAddressProperty prop = value as IpAddressProperty;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            DocumentPropertyBase.Serialize(prop, fieldDict);
            fieldDict.AddObject(_BOOST, prop.Boost, IpAddressProperty._BOOST_DEFAULT);
            fieldDict.AddObject(_PRECISION_STEP, prop.PrecisionStep, IpAddressProperty._PRECISION_STEP_DEFAULT);

            Dictionary<string, object> propDict = new Dictionary<string, object>();
            propDict.Add(prop.Name, fieldDict);

            serializer.Serialize(writer, propDict);
        }
    }
}
