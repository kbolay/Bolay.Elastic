using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.SourceFiltering
{
    public class SourceFilterSerializer : JsonConverter
    {
        private const string _INCLUDE = "include";
        private const string _EXCLUDE = "exclude";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.ValueType == typeof(bool))
            {
                bool sourceEnabled = (bool)reader.Value;
                if (!sourceEnabled)
                    return new SourceFilter();
                else
                    return null;
            }
            else if (reader.ValueType == typeof(string))
            {
                return new SourceFilter(reader.Value.ToString());
            }
            else if (reader.TokenType == JsonToken.StartArray)
            {
                return new SourceFilter(serializer.Deserialize<IEnumerable<string>>(reader));
            }

            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(SearchPieceTypeEnum.SourceFilter.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            return new SourceFilter(
                JsonConvert.DeserializeObject<IEnumerable<string>>(fieldDict.GetString(_INCLUDE)),
                JsonConvert.DeserializeObject<IEnumerable<string>>(fieldDict.GetString(_EXCLUDE)));            
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is SourceFilter))
                throw new SerializeTypeException<SourceFilter>();

            SourceFilter sourceFilter = value as SourceFilter;

            if (sourceFilter.DisableSourceRetrieval)
            {
                serializer.Serialize(writer, false);
            }
            else if (sourceFilter.ExcludeFields == null && sourceFilter.IncludeFields != null)
            {
                if (sourceFilter.IncludeFields.Count(x => !string.IsNullOrWhiteSpace(x)) > 1)
                    serializer.Serialize(writer, sourceFilter.IncludeFields.Where(x => !string.IsNullOrWhiteSpace(x)));
                else
                    serializer.Serialize(writer, sourceFilter.IncludeFields.First(x => !string.IsNullOrWhiteSpace(x)));
            }
            else if (sourceFilter.IncludeFields != null && sourceFilter.ExcludeFields != null)
            {
                Dictionary<string, object> fieldDict = new Dictionary<string, object>();
                fieldDict.Add(_INCLUDE, sourceFilter.IncludeFields.Where(x => !string.IsNullOrWhiteSpace(x)));
                fieldDict.Add(_EXCLUDE, sourceFilter.ExcludeFields.Where(x => !string.IsNullOrWhiteSpace(x)));
                serializer.Serialize(writer, fieldDict);
            }
            else
                throw new Exception("Failed to serialize SourceFilter.");

        }
    }
}
