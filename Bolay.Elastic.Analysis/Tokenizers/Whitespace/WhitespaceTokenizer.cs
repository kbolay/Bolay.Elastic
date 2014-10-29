using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Tokenizers.Whitespace
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-whitespace-tokenizer.html
    /// </summary>
    [JsonConverter(typeof(WhitespaceTokenizerSerializer))]
    public class WhitespaceTokenizer : TokenizerBase
    {
        /// <summary>
        /// Create a whitespace tokenizer.
        /// </summary>
        /// <param name="name">Sets the name of the tokenizer.</param>
        public WhitespaceTokenizer(string name) : base(name, TokenizerTypeEnum.Whitespace) { }
    }
}
