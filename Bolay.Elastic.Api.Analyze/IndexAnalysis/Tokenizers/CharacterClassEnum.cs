using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.Tokenizers
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-edgengram-tokenizer.html
    /// </summary>
    public sealed class CharacterClassEnum : TypeSafeEnumBase<CharacterClassEnum>
    {
        public static readonly CharacterClassEnum Letter = new CharacterClassEnum("letter");
        public static readonly CharacterClassEnum Digit = new CharacterClassEnum("digit");
        public static readonly CharacterClassEnum Whitespace = new CharacterClassEnum("whitespace");
        public static readonly CharacterClassEnum Punctuation = new CharacterClassEnum("punctuation");
        public static readonly CharacterClassEnum Symbol = new CharacterClassEnum("symbol");

        private CharacterClassEnum(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
