﻿using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.Trim
{
    internal class TrimTokenFilterSerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> filterDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(filterDict.First().Value.ToString());

            TrimTokenFilter filter = new TrimTokenFilter(filterDict.First().Key);
            TokenFilterBase.Deserialize(filter, fieldDict);

            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is TrimTokenFilter))
                throw new SerializeTypeException<TrimTokenFilter>();

            TrimTokenFilter filter = value as TrimTokenFilter;
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            TokenFilterBase.Serialize(filter, fieldDict);

            Dictionary<string, object> filterDict = new Dictionary<string, object>();
            filterDict.Add(filter.Name, fieldDict);

            serializer.Serialize(writer, filterDict);
        }
    }
}
