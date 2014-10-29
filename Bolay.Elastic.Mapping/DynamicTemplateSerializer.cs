using Bolay.Elastic.Exceptions;
using Bolay.Elastic.Mapping.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping
{
    internal class DynamicTemplateSerializer : JsonConverter
    {
        private const string _NAME = "name";
        private const string _MATCH = "match";
        private const string _UNMATCH = "unmatch";
        private const string _PATH_MATCH = "path_match";
        private const string _PATH_UNMATCH = "path_unmatch";
        private const string _MATCH_MAPPING_TYPE = "match_mapping_type";
        private const string _MATCH_PATTERN = "match_pattern";
        private const string _MAPPING = "mapping";
        private const string _DYNAMIC_NAME = "{name}";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> templateDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(templateDict.First().Value.ToString());

            bool useDynamicName = false;
            string mappingKey = _MAPPING;
            if (fieldDict.ContainsKey(_DYNAMIC_NAME) && !fieldDict.ContainsKey(_MAPPING))
            {
                mappingKey = _DYNAMIC_NAME;
                useDynamicName = true;
            }               

            string templateName = templateDict.First().Key;

            DynamicTemplate template = null;
            try
            {
                Dictionary<string, object> docPropDict = new Dictionary<string, object>();
                docPropDict.Add("name", JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.GetString(mappingKey)));
                string docPropJson = JsonConvert.SerializeObject(docPropDict);
                template = new DynamicTemplate(templateName, JsonConvert.DeserializeObject<IDocumentProperty>(docPropJson));
            }
            catch
            {
                template = new DynamicTemplate(templateName, fieldDict.GetString(mappingKey));
            }

            template.UseDynamicName = useDynamicName;

            if (fieldDict.ContainsKey(_MATCH_PATTERN) && fieldDict.GetString(_MATCH_PATTERN).Equals(DynamicTemplate._IS_MATCH_PATTERN_REGEX_TRUE_VALUE))
            {
                template.IsMatchPatternRegex = true;
            }

            if (fieldDict.ContainsKey(_MATCH) || fieldDict.ContainsKey(_PATH_MATCH) || fieldDict.ContainsKey(_UNMATCH) || fieldDict.ContainsKey(_PATH_UNMATCH))
            {
                template.Match = fieldDict.GetStringOrDefault(_MATCH);
                template.PathMatch = fieldDict.GetStringOrDefault(_PATH_MATCH);
                template.UnMatch = fieldDict.GetStringOrDefault(_UNMATCH);
                template.PathUnMatch = fieldDict.GetStringOrDefault(_PATH_UNMATCH);
            }
            else
            {
                throw new RequiredPropertyMissingException(string.Join("/", new string[] { _MATCH, _PATH_MATCH, _UNMATCH, _PATH_UNMATCH }));
            }

            template.MatchMappingType = PropertyTypeEnum.Find(fieldDict.GetString(_MATCH_MAPPING_TYPE, PropertyTypeEnum.Object.ToString()));

            return template;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is DynamicTemplate))
                throw new SerializeTypeException<DynamicTemplate>();

            DynamicTemplate template = value as DynamicTemplate;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.AddObject(_MATCH, template.Match);
            fieldDict.AddObject(_PATH_MATCH, template.PathMatch);
            fieldDict.AddObject(_UNMATCH, template.UnMatch);
            fieldDict.AddObject(_PATH_UNMATCH, template.PathUnMatch);
            if (template.IsMatchPatternRegex)
                fieldDict.Add(_MATCH_PATTERN, DynamicTemplate._IS_MATCH_PATTERN_REGEX_TRUE_VALUE);
            fieldDict.AddObject(_MATCH_MAPPING_TYPE, template.MatchMappingType);

            string mappingKey = _MAPPING;
            if(template.UseDynamicName)
                mappingKey = _DYNAMIC_NAME;

            if (template.Mapping != null)
            {
                string docPropJson = JsonConvert.SerializeObject(template.Mapping);
                Dictionary<string, object> propDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(docPropJson);
                fieldDict.Add(mappingKey, JsonConvert.DeserializeObject<Dictionary<string, object>>(propDict.First().Value.ToString()));
            }
            else
                fieldDict.Add(mappingKey, JsonConvert.DeserializeObject<Dictionary<string, object>>(template.MappingJson));

            Dictionary<string, object> templateDict = new Dictionary<string, object>();
            templateDict.Add(template.Name, fieldDict);

            serializer.Serialize(writer, templateDict);
        }
    }
}
