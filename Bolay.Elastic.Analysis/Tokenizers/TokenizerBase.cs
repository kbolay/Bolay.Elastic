using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Tokenizers
{
    public abstract class TokenizerBase : ITokenizer
    {
        private const string _TYPE = "type";
        private const string _VERSION = "version";

        /// <summary>
        /// Gets the name of the tokenizer.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the type of the tokenizer.
        /// </summary>
        public TokenizerTypeEnum Type { get; private set; }

        /// <summary>
        /// Gets the version of the tokenizer.
        /// </summary>
        public double? Version { get; set; }

        /// <summary>
        /// Create a tokenizer.
        /// </summary>
        /// <param name="name">Sets the name of the tokenizer.</param>
        /// <param name="type">Sets the type of the tokenizer.</param>
        public TokenizerBase(string name, TokenizerTypeEnum type)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name", "All tokenizers require a name.");
            if (type == null)
                throw new ArgumentNullException("type", "All tokenizers require a type.");

            Name = name;
            Type = type;
        }

        internal static void Serialize(TokenizerBase tokenizer, Dictionary<string, object> fieldDict)
        {
            if (tokenizer == null)
                return;

            if (fieldDict == null)
                fieldDict = new Dictionary<string, object>();

            fieldDict.AddObject(_TYPE, tokenizer.Type.ToString());
            fieldDict.AddObject(_VERSION, tokenizer.Version);
        }

        internal static void Deserialize(TokenizerBase tokenizer, Dictionary<string, object> fieldDict)
        {
            if (fieldDict == null || !fieldDict.Any())
                return;

            tokenizer.Version = fieldDict.GetDoubleOrNull(_VERSION);
        }
    }
}
