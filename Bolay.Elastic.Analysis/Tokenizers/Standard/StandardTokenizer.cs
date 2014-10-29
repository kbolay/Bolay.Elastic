using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Tokenizers.Standard
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-standard-tokenizer.html
    /// </summary>
    [JsonConverter(typeof(StandardTokenizerSerializer))]
    public class StandardTokenizer : TokenizerBase
    {
        internal const int _MAXIMUM_TOKEN_LENGTH_DEFAULT = 255;

        /// <summary>
        /// Gets or sets the maximum token length.
        /// Defaults to 255.
        /// </summary>
        public int MaximumTokenLength { get; set; }

        /// <summary>
        /// Create a standard tokenizer.
        /// </summary>
        /// <param name="name">Sets the name of the tokenizer.</param>
        public StandardTokenizer(string name) 
            : base(name, TokenizerTypeEnum.Standard) 
        {
            MaximumTokenLength = _MAXIMUM_TOKEN_LENGTH_DEFAULT;
        }
    }
}
