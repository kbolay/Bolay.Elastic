using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.Tokenizers
{
    public sealed class TokenizerEnum : TypeSafeEnumBase<TokenizerEnum>
    {
        public Uri Documentation { get; set; }

        public static readonly TokenizerEnum Standard = new TokenizerEnum("standard", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-standard-tokenizer.html");
        public static readonly TokenizerEnum EdgeNGram = new TokenizerEnum("edgeNGram", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-edgengram-tokenizer.html");
        public static readonly TokenizerEnum Keyword = new TokenizerEnum("keyword", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-keyword-tokenizer.html");
        public static readonly TokenizerEnum Letter = new TokenizerEnum("letter", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-letter-tokenizer.html");
        public static readonly TokenizerEnum Lowercase = new TokenizerEnum("lowercase", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-lowercase-tokenizer.html");
        public static readonly TokenizerEnum NGram = new TokenizerEnum("nGram", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-ngram-tokenizer.html");
        public static readonly TokenizerEnum Whitespace = new TokenizerEnum("whitespace", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-whitespace-tokenizer.html");
        public static readonly TokenizerEnum Pattern = new TokenizerEnum("pattern", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-pattern-tokenizer.html");
        public static readonly TokenizerEnum UaxEmailUrl = new TokenizerEnum("uax_url_email", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-uaxurlemail-tokenizer.html");
        public static readonly TokenizerEnum PathHierarchy = new TokenizerEnum("path_hierarchy", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-pathhierarchy-tokenizer.html");

        private TokenizerEnum(string value, string docUri)
            : this(value, new Uri(docUri))
        { }
        private TokenizerEnum(string value, Uri documentation)
            : base(value)
        {
            Documentation = documentation;
            _AllItems.Add(this);
        }
    }
}
