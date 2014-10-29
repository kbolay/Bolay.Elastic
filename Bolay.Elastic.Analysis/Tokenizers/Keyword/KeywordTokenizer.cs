using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Tokenizers.Keyword
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-keyword-tokenizer.html
    /// </summary>
    [JsonConverter(typeof(KeywordTokenizerSerializer))]
    public class KeywordTokenizer : TokenizerBase
    {
        internal const Int64 _BUFFER_SIZE_DEFAULT = 256;

        private Int64 _BufferSize { get; set; }

        /// <summary>
        /// Gets or sets the buffer size.
        /// </summary>
        public Int64 BufferSize
        {
            get { return _BufferSize; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("BufferSize", "BufferSize must be greater than zero.");

                _BufferSize = value;
            }
        }

        /// <summary>
        /// Create a keyword tokenizer.
        /// </summary>
        /// <param name="name">Sets the name of the tokenizer.</param>
        public KeywordTokenizer(string name) 
            : base(name, TokenizerTypeEnum.Keyword) 
        {
            _BufferSize = _BUFFER_SIZE_DEFAULT;
        }
    }
}
