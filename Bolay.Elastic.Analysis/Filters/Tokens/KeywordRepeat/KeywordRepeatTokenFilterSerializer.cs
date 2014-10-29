﻿using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.KeywordRepeat
{
    internal class KeywordRepeatTokenFilterSerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> filterDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(filterDict.First().Value.ToString());

            KeywordRepeatTokenFilter filter = new KeywordRepeatTokenFilter(filterDict.First().Key);
            TokenFilterBase.Deserialize(filter, fieldDict);

            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is KeywordRepeatTokenFilter))
                throw new SerializeTypeException<KeywordRepeatTokenFilter>();

            KeywordRepeatTokenFilter filter = value as KeywordRepeatTokenFilter;
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            TokenFilterBase.Serialize(filter, fieldDict);

            Dictionary<string, object> filterDict = new Dictionary<string, object>();
            filterDict.Add(filter.Name, fieldDict);

            serializer.Serialize(writer, filterDict);
        }
    }
}
