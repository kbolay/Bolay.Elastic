using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Tokenizers.Letter
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-letter-tokenizer.html
    /// </summary>
    [JsonConverter(typeof(LetterTokenizerSerializer))]
    public class LetterTokenizer : TokenizerBase
    {
        /// <summary>
        /// Create letter tokenizer.
        /// </summary>
        /// <param name="name">Sets the name of the tokenizer.</param>
        public LetterTokenizer(string name) : base(name, TokenizerTypeEnum.Letter) { }
    }
}