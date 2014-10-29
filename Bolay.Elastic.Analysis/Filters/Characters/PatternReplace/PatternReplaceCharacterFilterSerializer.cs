﻿using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Characters.PatternReplace
{
    internal class PatternReplaceCharacterFilterSerializer : JsonConverter
    {
        private const string _PATTERN = "pattern";
        private const string _REPLACEMENT = "replacement";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> filterDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(filterDict.First().Value.ToString());

            PatternReplaceCharacterFilter filter = 
                new PatternReplaceCharacterFilter(
                    filterDict.First().Key,
                    fieldDict.GetString(_PATTERN),
                    fieldDict.GetString(_REPLACEMENT));
            CharacterFilterBase.Deserialize(filter, fieldDict);

            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is PatternReplaceCharacterFilter))
                throw new SerializeTypeException<PatternReplaceCharacterFilter>();

            PatternReplaceCharacterFilter filter = value as PatternReplaceCharacterFilter;
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            CharacterFilterBase.Serialize(filter, fieldDict);

            fieldDict.AddObject(_PATTERN, filter.Pattern);
            fieldDict.AddObject(_REPLACEMENT, filter.Replacement);

            Dictionary<string, object> filterDict = new Dictionary<string, object>();
            filterDict.Add(filter.Name, fieldDict);

            serializer.Serialize(writer, filterDict);
        }
    }
}
