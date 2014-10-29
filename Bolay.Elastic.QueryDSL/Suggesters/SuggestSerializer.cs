using Bolay.Elastic.Exceptions;
using Bolay.Elastic.QueryDSL.Suggesters.Phrase;
using Bolay.Elastic.QueryDSL.Suggesters.Term;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Suggesters
{
    // TODO: finish this
    public class SuggestSerializer : JsonConverter
    {
        internal const string _TEXT = "text";
        internal const string _FIELD = "field";
        internal const string _SIZE = "size";

        internal const int _SIZE_DEFAULT = 5;

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> suggestDict = serializer.Deserialize<Dictionary<string, object>>(reader);

            List<ISuggester> suggestors = new List<ISuggester>();
            SuggestTypeEnum suggestType = SuggestTypeEnum.Term;
            foreach (KeyValuePair<string, object> fieldKvp in suggestDict.Where(x => !x.Key.Equals(_TEXT)))
            {
                // dig down to get the type i am dealing with
                Dictionary<string, object> fieldSuggestDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldKvp.Value.ToString());
                KeyValuePair<string, object> suggestTypeKvp = fieldSuggestDict.FirstOrDefault(x => !x.Key.Equals(_TEXT));

                // create a dictionary just for this one
                Dictionary<string, object> internalDict = new Dictionary<string, object>();
                internalDict.Add(fieldKvp.Key, fieldKvp.Value);
                Dictionary<string, object> suggestTypeDict = new Dictionary<string,object>();
                suggestTypeDict.Add("junk", internalDict);

                suggestType = SuggestTypeEnum.Find(suggestTypeKvp.Key);
                if (suggestType == null)
                    throw new Exception(suggestTypeKvp.Key + " is not a valid type of suggestor.");

                string suggestTypeJsonStr = JsonConvert.SerializeObject(suggestTypeDict.First().Value);
                suggestors.Add(JsonConvert.DeserializeObject(suggestTypeJsonStr, suggestType.ImplementationType) as ISuggester);
            }

            Suggest suggest = new Suggest(suggestors);
            suggest.Text = suggestDict.GetStringOrDefault(_TEXT);

            return suggest;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is Suggest))
                throw new SerializeTypeException<Suggest>();

            Suggest suggest = value as Suggest;

            Dictionary<string, object> suggestDict = new Dictionary<string, object>();
            suggestDict.AddObject(_TEXT, suggest.Text);

            foreach (ISuggester suggestor in suggest.Suggestors)
            {
                string suggestorJson = JsonConvert.SerializeObject(suggestor);
                Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(suggestorJson);
                suggestDict.Add(fieldDict.First().Key, fieldDict.First().Value);
            }

            serializer.Serialize(writer, suggestDict);
        }
    }
}
