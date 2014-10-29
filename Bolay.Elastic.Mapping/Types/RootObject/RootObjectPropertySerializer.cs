using Bolay.Elastic.Analysis;
using Bolay.Elastic.Exceptions;
using Bolay.Elastic.Mapping.Fields;
using Bolay.Elastic.Mapping.Types.Object;
using Bolay.Elastic.Time;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Types.RootObject
{
    internal class RootObjectPropertySerializer : JsonConverter
    {
        private const string _DYNAMIC = "dynamic";
        private const string _IS_ENABLED = "enabled";
        private const string _COPY_TO = "copy_to";
        private const string _INCLUDE_IN_ALL = "include_in_all";
        private const string _PROPERTIES = "properties";
        private const string _DYNAMIC_DATE_FORMATS = "dynamic_date_formats";
        private const string _DETECT_DATES = "date_detection";
        private const string _DETECT_NUMBERS = "numeric_detection";
        private const string _DYNAMIC_TEMPLATES = "dynamic_templates";
        private const string _META = "_meta";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> propDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(propDict.First().Value.ToString());

            ObjectProperty objProp = new ObjectProperty(propDict.First().Key);
            ObjectProperty.Deserialize(objProp, fieldDict);

            RootObjectProperty prop = new RootObjectProperty(objProp);
            prop.Fields = DocumentMapping.Deserialize(fieldDict);
            prop.Analyzer = PropertyAnalyzer.Deserialize(fieldDict);
            prop.DetectDates = fieldDict.GetBool(_DETECT_DATES, RootObjectProperty._DETECT_DATES_DEFAULT);
            prop.DetectNumbers = fieldDict.GetBool(_DETECT_NUMBERS, RootObjectProperty._DETECT_NUMBERS_DEFAULT);
            if (fieldDict.ContainsKey(_DYNAMIC_DATE_FORMATS))
            {
                prop.DynamicDateFormats = JsonConvert.DeserializeObject<IEnumerable<DateFormat>>(fieldDict.GetString(_DYNAMIC_DATE_FORMATS));
            }
            if (fieldDict.ContainsKey(_DYNAMIC_TEMPLATES))
            {
                prop.DynamicTemplates = JsonConvert.DeserializeObject<IEnumerable<DynamicTemplate>>(fieldDict.GetString(_DYNAMIC_TEMPLATES));
            }
            if (fieldDict.ContainsKey(_META))
            {
                prop.MetaData = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.GetString(_META));
            }
            return prop;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is RootObjectProperty))
                throw new SerializeTypeException<RootObjectProperty>();

            RootObjectProperty prop = value as RootObjectProperty;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            DocumentMapping.Serialize(prop.Fields, fieldDict);
            PropertyAnalyzer.Serialize(prop.Analyzer, fieldDict);
            fieldDict.AddObject(_DETECT_DATES, prop.DetectDates, RootObjectProperty._DETECT_DATES_DEFAULT);
            fieldDict.AddObject(_DETECT_NUMBERS, prop.DetectNumbers, RootObjectProperty._DETECT_NUMBERS_DEFAULT);
            if (prop.DynamicDateFormats != null && prop.DynamicDateFormats.Any(x => x != null))
                fieldDict.Add(_DYNAMIC_DATE_FORMATS, prop.DynamicDateFormats.Where(x => x != null));
            if (prop.DynamicTemplates != null && prop.DynamicTemplates.Any(x => x != null))
                fieldDict.Add(_DYNAMIC_TEMPLATES, prop.DynamicTemplates.Where(x => x != null));
            
            ObjectProperty objProp = new ObjectProperty(prop.Name)
            {
                CopyTo = prop.CopyTo,
                Dynamic = prop.Dynamic,
                IncludeInAll = prop.IncludeInAll,
                IsEnabled = prop.IsEnabled,
                Properties = prop.Properties
            };
            ObjectProperty.Serialize(objProp, fieldDict);

            if (prop.MetaData != null && prop.MetaData.Any())
                fieldDict.Add(_META, prop.MetaData);

            Dictionary<string, object> propDict = new Dictionary<string,object>();
            propDict.Add(prop.Name, fieldDict);

            serializer.Serialize(writer, propDict);
        }
    }
}
