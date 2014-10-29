using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Tokenizers.PathHierarchy
{
    internal class PathHierarchyTokenizerSerializer : JsonConverter
    {
        private const string _DELIMITER = "delimiter";
        private const string _REPLACEMENT = "replacement";
        private const string _BUFFER_SIZE = "buffer_size";
        private const string _REVERSE = "reverse";
        private const string _SKIP = "skip";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> tokenDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(tokenDict.First().Value.ToString());

            PathHierarchyTokenizer token = new PathHierarchyTokenizer(tokenDict.First().Key);
            TokenizerBase.Deserialize(token, fieldDict);
            token.BufferSize = fieldDict.GetInt64(_BUFFER_SIZE, PathHierarchyTokenizer._BUFFER_SIZE_DEFAULT);
            token.Delimeter = fieldDict.GetString(_DELIMITER, PathHierarchyTokenizer._DELIMITER_DEFAULT);
            token.Replacement = fieldDict.GetString(_REPLACEMENT, token.Delimeter);
            token.Reverse = fieldDict.GetBool(_REVERSE, PathHierarchyTokenizer._REVERSE_DEFAULT);
            token.Skip = fieldDict.GetInt64(_SKIP, PathHierarchyTokenizer._SKIP_DEFAULT);

            return token;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is PathHierarchyTokenizer))
                throw new SerializeTypeException<PathHierarchyTokenizer>();

            PathHierarchyTokenizer token = value as PathHierarchyTokenizer;
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            TokenizerBase.Serialize(token, fieldDict);
            fieldDict.AddObject(_DELIMITER, token.Delimeter, PathHierarchyTokenizer._DELIMITER_DEFAULT);
            fieldDict.AddObject(_REPLACEMENT, token.Replacement, token.Delimeter);
            fieldDict.AddObject(_BUFFER_SIZE, token.BufferSize, PathHierarchyTokenizer._BUFFER_SIZE_DEFAULT);
            fieldDict.AddObject(_REVERSE, token.Reverse, PathHierarchyTokenizer._REVERSE_DEFAULT);
            fieldDict.AddObject(_SKIP, token.Skip, PathHierarchyTokenizer._SKIP_DEFAULT);

            Dictionary<string, object> tokenDict = new Dictionary<string, object>();
            tokenDict.Add(token.Name, fieldDict);

            serializer.Serialize(writer, tokenDict);
        }
    }
}
