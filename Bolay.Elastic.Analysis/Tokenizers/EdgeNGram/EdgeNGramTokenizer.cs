using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Tokenizers.EdgeNGram
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-edgengram-tokenizer.html
    /// </summary>
    [JsonConverter(typeof(EdgeNGramTokenizerSerializer))]
    public class EdgeNGramTokenizer : TokenizerBase
    {
        internal const Int64 _MINIMUM_SIZE_DEFAULT = 1;
        internal const Int64 _MAXIMUM_SIZE_DEFAULT = 2;

        private Int64 _MinimumSize { get; set; }
        private Int64 _MaximumSize { get; set; }

        /// <summary>
        /// Gets or sets the minimum size in codepoints of an ngram.
        /// Defaults to 1.
        /// </summary>
        public Int64 MinimumSize
        {
            get { return _MinimumSize; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("MinimumSize", "MinimumSize must be greater than 0.");
                _MinimumSize = value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum size in codepoints of an ngram.
        /// </summary>
        public Int64 MaximumSize 
        {
            get { return _MaximumSize; }
            set
            {
                if (value <= 0 || value < _MinimumSize)
                {
                    throw new ArgumentOutOfRangeException("MaximumSize", "MaximumSize must be greater than or equal to the minimum size.");
                }
                    
                _MaximumSize = value;
            }
        }

        /// <summary>
        /// Gets or sets the token character classes to keep in the tokens.
        /// </summary>
        public List<CharacterClassEnum> TokenCharacters { get; set; }

        /// <summary>
        /// Create an edge n gram tokenizer.
        /// </summary>
        /// <param name="name">Sets the name of the tokenizer.</param>
        public EdgeNGramTokenizer(string name) 
            : base(name, TokenizerTypeEnum.EdgeNGram) 
        {
            MinimumSize = _MINIMUM_SIZE_DEFAULT;
            MaximumSize = _MAXIMUM_SIZE_DEFAULT;
        }
    }
}
