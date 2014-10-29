using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bolay.Elastic.Api.Bulk.Response
{
    public class ActionResponseSerializer : JsonConverter
    {
        private const string _INDEX = "_index";
        private const string _TYPE = "_type";
        private const string _DOCUMENT_ID = "_id";
        private const string _VERSION = "_version";
        private const string _STATUS = "status";
        private const string _FOUND = "found";
        private const string _OK = "ok";
        private const string _ERROR = "error";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> itemDict = serializer.Deserialize<Dictionary<string, object>>(reader);

            ActionResponse item = new ActionResponse();
            item.Action = itemDict.First().Key;

            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(itemDict.First().Value.ToString());

            item.DocumentId = GetFromDictionary<string>(fieldDict, _DOCUMENT_ID);
            item.Error = GetFromDictionary<string>(fieldDict, _ERROR);
            item.Found = GetFromDictionary<bool>(fieldDict, _FOUND);
            item.Index = GetFromDictionary<string>(fieldDict, _INDEX);
            item.Ok = GetFromDictionary<bool>(fieldDict, _OK);
            item.Status = GetFromDictionary<int>(fieldDict, _STATUS);
            item.Type = GetFromDictionary<string>(fieldDict, _TYPE);
            item.Version = GetFromDictionary<int>(fieldDict, _VERSION);

            return item;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        private static T GetFromDictionary<T>(Dictionary<string, object> dictionary, string key)
        {
            T result = default(T);
            if (dictionary.ContainsKey(key))
            {
                string valueStr = dictionary[key].ToString();

                if (typeof(T) == typeof(bool))
                {
                    valueStr = valueStr.ToLower();
                }
                else if (typeof(T) == typeof(string))
                {
                    valueStr = "\"" + valueStr + "\"";
                }
                result = JsonConvert.DeserializeObject<T>(valueStr);
            }

            return result;
        }
    }
}
