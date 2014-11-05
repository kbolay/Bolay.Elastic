using Bolay.Elastic.Exceptions;
using Bolay.Elastic.Time;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Properties.Date
{
    public class DatePropertySerializer : JsonConverter
    {
        private const string _PRECISION_STEP = "precision_step";
        private const string _FORMAT = "format";
        private const string _IGNORE_MALFORMED = "ignore_malformed";
        private const string _DOC_VALUES = "doc_values";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> propDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            DateProperty prop = new DateProperty(propDict.First().Key);

            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(propDict.First().Value.ToString());
            FieldProperty.Deserialize(prop, fieldDict);
            prop.PrecisionStep = fieldDict.GetInt32(_PRECISION_STEP, DateProperty._PRECISION_STEP_DEFAULT);
            if (fieldDict.ContainsKey(_FORMAT))
                prop.Format = new DateFormat(fieldDict.GetString(_FORMAT));
            prop.IgnoreMalformed = fieldDict.GetBool(_IGNORE_MALFORMED, DateProperty._IGNORE_MALFORMED_DEFAULT);
            prop.DocValues = fieldDict.GetBool(_DOC_VALUES, DateProperty._DOCS_VALUE_DEFAULT);

            return prop;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is DateProperty))
                throw new SerializeTypeException<DateProperty>();

            DateProperty prop = value as DateProperty;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            FieldProperty.Serialize(prop, fieldDict);
            fieldDict.AddObject(_PRECISION_STEP, prop.PrecisionStep, DateProperty._PRECISION_STEP_DEFAULT);
            if (prop.Format != null)
            {
                string formatJson = JsonConvert.SerializeObject(prop.Format);
                string expectedFormatJson = JsonConvert.SerializeObject(DateProperty._FORMAT_DEFAULT);
                if(formatJson != expectedFormatJson)
                    fieldDict.Add(_FORMAT, prop.Format);
            }
            fieldDict.AddObject(_IGNORE_MALFORMED, prop.IgnoreMalformed, DateProperty._IGNORE_MALFORMED_DEFAULT);
            fieldDict.AddObject(_DOC_VALUES, prop.DocValues, DateProperty._DOCS_VALUE_DEFAULT);

            Dictionary<string, object> propDict = new Dictionary<string, object>();
            propDict.Add(prop.Name, fieldDict);

            serializer.Serialize(writer, propDict);
        }
    }
}
