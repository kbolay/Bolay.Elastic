using Bolay.Elastic.Exceptions;
using Bolay.Elastic.Mapping.Properties;
using Bolay.Elastic.Time;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bolay.Elastic.Mapping.Defaults
{
    internal class DefaultMappingSerializer : JsonConverter
    {
        private const string _DEFAULT = "_default_";
        private const string _DYNAMIC = "dynamic";
        private const string _IS_ENABLED = "enabled";
        private const string _COPY_TO = "copy_to";
        private const string _INCLUDE_IN_ALL = "include_in_all";
        private const string _DATE_FORMATS = "date_formats";
        private const string _DETECT_DATES = "date_detection";
        private const string _DETECT_NUMBERS = "numeric_detection";
        private const string _DYNAMIC_TEMPLATES = "dynamic_templates";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> propDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(propDict.First().Value.ToString());

            DefaultMapping defaultMapping = new DefaultMapping();

            if (fieldDict.ContainsKey(_COPY_TO))
            {
                try
                {
                    defaultMapping.CopyTo = JsonConvert.DeserializeObject<IEnumerable<string>>(fieldDict.GetString(_COPY_TO));
                }
                catch
                {
                    defaultMapping.CopyTo = new List<string>() { fieldDict.GetString(_COPY_TO) };
                }
            }

            defaultMapping.Dynamic = DynamicSettingEnum.Find(fieldDict.GetString(_DYNAMIC, DefaultMapping._DYNAMIC_DEFAULT.ToString()));
            defaultMapping.IncludeInAll = fieldDict.GetBool(_INCLUDE_IN_ALL, DefaultMapping._INCLUDE_IN_ALL_DEFAULT);
            defaultMapping.IsEnabled = fieldDict.GetBool(_IS_ENABLED, DefaultMapping._IS_ENABLED_DEFAULT);

            defaultMapping.Analyzer = PropertyAnalyzer.Deserialize(fieldDict);
            defaultMapping.DetectDates = fieldDict.GetBool(_DETECT_DATES, TypeMapping._DETECT_DATES_DEFAULT);
            defaultMapping.DetectNumbers = fieldDict.GetBool(_DETECT_NUMBERS, TypeMapping._DETECT_NUMBERS_DEFAULT);
            if (fieldDict.ContainsKey(_DATE_FORMATS))
            {
                defaultMapping.DateFormats = JsonConvert.DeserializeObject<IEnumerable<DateFormat>>(fieldDict.GetString(_DATE_FORMATS));
            }
            if (fieldDict.ContainsKey(_DYNAMIC_TEMPLATES))
            {
                defaultMapping.DynamicTemplates = JsonConvert.DeserializeObject<IEnumerable<DynamicTemplate>>(fieldDict.GetString(_DYNAMIC_TEMPLATES));
            }
            return defaultMapping;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is DefaultMapping))
                throw new SerializeTypeException<DefaultMapping>();

            DefaultMapping prop = value as DefaultMapping;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();

            fieldDict.AddObject(_INCLUDE_IN_ALL, prop.IncludeInAll, DefaultMapping._INCLUDE_IN_ALL_DEFAULT);
            fieldDict.AddObject(_IS_ENABLED, prop.IsEnabled, DefaultMapping._IS_ENABLED_DEFAULT);
            fieldDict.AddObject(_DYNAMIC, prop.Dynamic.RealValue, DefaultMapping._DYNAMIC_DEFAULT.RealValue);

            if (prop.CopyTo != null && prop.CopyTo.Any(x => !string.IsNullOrWhiteSpace(x)))
            {
                int count = prop.CopyTo.Count(x => !string.IsNullOrWhiteSpace(x));

                if (count > 1)
                    fieldDict.AddObject(_COPY_TO, prop.CopyTo.Where(x => !string.IsNullOrWhiteSpace(x)));
                else
                    fieldDict.AddObject(_COPY_TO, prop.CopyTo.First(x => !string.IsNullOrWhiteSpace(x)));
            }

            PropertyAnalyzer.Serialize(prop.Analyzer, fieldDict);
            fieldDict.AddObject(_DETECT_DATES, prop.DetectDates, DefaultMapping._DETECT_DATES_DEFAULT);
            fieldDict.AddObject(_DETECT_NUMBERS, prop.DetectNumbers, DefaultMapping._DETECT_NUMBERS_DEFAULT);
            if (prop.DateFormats != null && prop.DateFormats.Any(x => x != null))
                fieldDict.Add(_DATE_FORMATS, prop.DateFormats.Where(x => x != null));
            if (prop.DynamicTemplates != null && prop.DynamicTemplates.Any(x => x != null))
                fieldDict.Add(_DYNAMIC_TEMPLATES, prop.DynamicTemplates.Where(x => x != null));

            Dictionary<string, object> propDict = new Dictionary<string, object>();
            propDict.Add(_DEFAULT, fieldDict);

            serializer.Serialize(writer, propDict);
        }
    }
}
