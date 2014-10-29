using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Suggesters.Suggestions
{
    /// <summary>
    /// Deserializes the content of a suggest response.
    /// </summary>
    public class SuggestionCollectionSerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> suggestDict = serializer.Deserialize<Dictionary<string, object>>(reader);

            SuggestionCollection suggestions = new SuggestionCollection();
            foreach (KeyValuePair<string, object> suggestKvp in suggestDict)
            {
                suggestions.Add(new Suggestion(
                    suggestKvp.Key,
                    JsonConvert.DeserializeObject<IEnumerable<TermSuggestion>>(suggestKvp.Value.ToString())));
            }

            return suggestions;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
