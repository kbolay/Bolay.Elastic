using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Tokenizers.UaxEmailUrl
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-uaxurlemail-tokenizer.html
    /// </summary>
    [JsonConverter(typeof(UaxEmailUrlTokenizerSerializer))]
    public class UaxEmailUrlTokenizer : TokenizerBase
    {
        internal const Int64 _MAXIMUM_TOKEN_LENGTH_DEFAULT = 255;

        private Int64 _MaximumTokenLength { get; set; }
        
        /// <summary>
        /// Gets or sets the maximum token length.
        /// </summary>
        public Int64 MaximumTokenLength
        {
            get { return _MaximumTokenLength; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("MaxTokenLength", "MaxTokenLength must be greater than zero.");

                _MaximumTokenLength = value;
            }
        }

        /// <summary>
        /// Creates a uax email url tokenizer.
        /// </summary>
        /// <param name="name">Sets the name of the tokenizer.</param>
        public UaxEmailUrlTokenizer(string name) 
            : base(name, TokenizerTypeEnum.UaxEmailUrl) 
        {
            _MaximumTokenLength = _MAXIMUM_TOKEN_LENGTH_DEFAULT;
        }
    }
}
