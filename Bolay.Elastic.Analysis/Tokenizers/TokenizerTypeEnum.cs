using Bolay.Elastic.Analysis.Tokenizers.EdgeNGram;
using Bolay.Elastic.Analysis.Tokenizers.Icu;
using Bolay.Elastic.Analysis.Tokenizers.Keyword;
using Bolay.Elastic.Analysis.Tokenizers.Letter;
using Bolay.Elastic.Analysis.Tokenizers.Lowercase;
using Bolay.Elastic.Analysis.Tokenizers.NGram;
using Bolay.Elastic.Analysis.Tokenizers.PathHierarchy;
using Bolay.Elastic.Analysis.Tokenizers.Pattern;
using Bolay.Elastic.Analysis.Tokenizers.Standard;
using Bolay.Elastic.Analysis.Tokenizers.UaxEmailUrl;
using Bolay.Elastic.Analysis.Tokenizers.Whitespace;
using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Tokenizers
{
    public sealed class TokenizerTypeEnum : TypeSafeEnumBase<TokenizerTypeEnum>
    {
        public Uri Documentation { get; private set; }
        public Type ImplementationType {get; private set; }

        public static readonly TokenizerTypeEnum Standard = new TokenizerTypeEnum("standard", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-standard-tokenizer.html", typeof(StandardTokenizer));
        public static readonly TokenizerTypeEnum EdgeNGram = new TokenizerTypeEnum("edgeNGram", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-edgengram-tokenizer.html", typeof(EdgeNGramTokenizer));
        public static readonly TokenizerTypeEnum Keyword = new TokenizerTypeEnum("keyword", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-keyword-tokenizer.html", typeof(KeywordTokenizer));
        public static readonly TokenizerTypeEnum Letter = new TokenizerTypeEnum("letter", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-letter-tokenizer.html", typeof(LetterTokenizer));
        public static readonly TokenizerTypeEnum Lowercase = new TokenizerTypeEnum("lowercase", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-lowercase-tokenizer.html", typeof(LowercaseTokenizer));
        public static readonly TokenizerTypeEnum NGram = new TokenizerTypeEnum("nGram", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-ngram-tokenizer.html", typeof(NGramTokenizer));
        public static readonly TokenizerTypeEnum Whitespace = new TokenizerTypeEnum("whitespace", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-whitespace-tokenizer.html", typeof(WhitespaceTokenizer));
        public static readonly TokenizerTypeEnum Pattern = new TokenizerTypeEnum("pattern", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-pattern-tokenizer.html", typeof(PatternTokenizer));
        public static readonly TokenizerTypeEnum UaxEmailUrl = new TokenizerTypeEnum("uax_url_email", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-uaxurlemail-tokenizer.html", typeof(UaxEmailUrlTokenizer));
        public static readonly TokenizerTypeEnum PathHierarchy = new TokenizerTypeEnum("path_hierarchy", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-pathhierarchy-tokenizer.html", typeof(PathHierarchyTokenizer));
        public static readonly TokenizerTypeEnum IcuTokenizer = new TokenizerTypeEnum("icu_tokenizer", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-icu-plugin.html#_icu_tokenizer", typeof(IcuTokenizer));

        private TokenizerTypeEnum(string value, string docUri, Type implementationType)
            : this(value, new Uri(docUri), implementationType)
        { }
        private TokenizerTypeEnum(string value, Uri documentation, Type implementationType)
            : base(value)
        {
            Documentation = documentation;
            ImplementationType = implementationType;
            _AllItems.Add(this);
        }
    }
}
