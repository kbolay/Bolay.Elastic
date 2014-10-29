using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.Tokenizers
{
    public class EdgeNGram : TokenizerBase
    {
        private const Int64 _MINIMUM_SIZE_DEFAULT = 1;
        private const Int64 _MAXIMUM_SIZE_DEFAULT = 2;

        private Int64? _MinimumSize { get; set; }
        private Int64? _MaximumSize { get; set; }
        private List<CharacterClassEnum> _TokenCharacters { get; set; }

        [JsonProperty("min_gram")]
        [DefaultValue(_MINIMUM_SIZE_DEFAULT)]
        public Int64 MinimumSize
        {
            get
            {
                if (_MinimumSize.HasValue)
                    return _MinimumSize.Value;
                return _MINIMUM_SIZE_DEFAULT;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("MinimumSize");
                _MinimumSize = value;
            }
        }

        [JsonProperty("max_gram")]
        [DefaultValue(_MAXIMUM_SIZE_DEFAULT)]
        public Int64 MaximumSize 
        {
            get
            {
                if (_MaximumSize.HasValue)
                    return _MaximumSize.Value;
                return _MAXIMUM_SIZE_DEFAULT;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("MaximumSize");
                _MaximumSize = value;
            }
        }

        [JsonProperty("token_chars")]
        public List<string> TokenCharacters
        {
            get
            {
                if (_TokenCharacters == null || !_TokenCharacters.Any())
                    return new List<string>();

                List<string> list = new List<string>();
                foreach (CharacterClassEnum character in _TokenCharacters)
                {
                    list.Add(character.ToString());
                }

                if (!list.Any())
                    return new List<string>();

                return list;
            }
            set
            {
                if (value != null || !value.Any())
                {
                    CharacterClassEnum temp = CharacterClassEnum.Letter;
                    List<CharacterClassEnum> list = new List<CharacterClassEnum>();
                    foreach (string charClass in value)
                    {
                        temp = CharacterClassEnum.Find(charClass);
                        if (temp != null)
                            list.Add(temp);
                        else
                            throw new ArgumentOutOfRangeException("TokenCharacters", charClass + " is not a value Character Class for Tokenizers.");
                    }

                    if (list.Any())
                        _TokenCharacters = list;
                    else
                        _TokenCharacters = null;
                }
                else
                    _TokenCharacters = null;
            }
        }

        public EdgeNGram() : base(TokenizerEnum.EdgeNGram) { }
    }
}
