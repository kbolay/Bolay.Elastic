using Bolay.Elastic.Exceptions;
using Bolay.Elastic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Tokenizers.Pattern
{
    internal class PatternTokenizerSerializer : JsonConverter
    {
        private const string _PATTERN = "pattern";
        private const string _FLAGS = "flags";
        private const string _GROUP = "group";

        private const string _FLAG_DELIMITER = "|";
        private static List<string> _FLAG_DELIMITERS = new List<string>() { " | ", "| ", " |", "|" }; 

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> tokenDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(tokenDict.First().Value.ToString());

            PatternTokenizer token = new PatternTokenizer(tokenDict.First().Key);
            TokenizerBase.Deserialize(token, fieldDict);
            token.Pattern = fieldDict.GetString(_PATTERN, PatternTokenizer._REGEX_PATTERN_DEFAULT);
            token.Group = fieldDict.GetInt64(_GROUP, PatternTokenizer._GROUP_DEFAULT);
            if (fieldDict.ContainsKey(_FLAGS))
            {
                List<RegexFlagEnum> flagList = new List<RegexFlagEnum>();
                string flagsStr = fieldDict.GetString(_FLAGS);
                foreach (string flagValue in flagsStr.Split(_FLAG_DELIMITERS.ToArray(), StringSplitOptions.RemoveEmptyEntries))
                {
                    flagList.Add(RegexFlagEnum.Find(flagValue));
                }

                if (flagList.Any())
                    token.Flags = flagList;
            }

            return token;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is PatternTokenizer))
                throw new SerializeTypeException<PatternTokenizer>();

            PatternTokenizer token = value as PatternTokenizer;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            TokenizerBase.Serialize(token, fieldDict);
            fieldDict.AddObject(_PATTERN, token.Pattern, PatternTokenizer._REGEX_PATTERN_DEFAULT);
            if (token.Flags != null && token.Flags.Any(x => x != null))
            {
                fieldDict.AddObject(_FLAGS, string.Join(_FLAG_DELIMITER, token.Flags.Where(x => x != null).Select(x => x.ToString())));
            }
            fieldDict.AddObject(_GROUP, token.Group, PatternTokenizer._GROUP_DEFAULT);

            Dictionary<string, object> tokenDict = new Dictionary<string, object>();
            tokenDict.Add(token.Name, fieldDict);

            serializer.Serialize(writer, tokenDict);
        }
    }
}
