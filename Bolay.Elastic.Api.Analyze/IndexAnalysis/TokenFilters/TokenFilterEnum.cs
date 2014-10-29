using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.TokenFilters
{
    public class TokenFilterEnum : TypeSafeEnumBase<TokenFilterEnum>
    {
        public Uri Documentation { get; set; }

        public static readonly TokenFilterEnum Standard = new TokenFilterEnum("standard", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-standard-tokenfilter.html");
        public static readonly TokenFilterEnum AsciiFolding = new TokenFilterEnum("asciifolding", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-asciifolding-tokenfilter.html");
        public static readonly TokenFilterEnum Length = new TokenFilterEnum("length", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-length-tokenfilter.html");
        public static readonly TokenFilterEnum Lowercase = new TokenFilterEnum("standard", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-lowercase-tokenfilter.html");
        public static readonly TokenFilterEnum Ngram = new TokenFilterEnum("nGram", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-ngram-tokenfilter.html");
        public static readonly TokenFilterEnum EdgeNGram = new TokenFilterEnum("edgeNGram", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-edgengram-tokenfilter.html");
        public static readonly TokenFilterEnum PorterStem = new TokenFilterEnum("porterStem", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-porterstem-tokenfilter.html");
        public static readonly TokenFilterEnum Shingle = new TokenFilterEnum("shingle", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-shingle-tokenfilter.html");
        public static readonly TokenFilterEnum StopWord = new TokenFilterEnum("stop", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-stop-tokenfilter.html");
        public static readonly TokenFilterEnum WordDelimeter = new TokenFilterEnum("word_delimeter", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-word-delimeter-tokenfilter.html");
        public static readonly TokenFilterEnum Stemmer = new TokenFilterEnum("stemmer", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-stemmer-tokenfilter.html");
        public static readonly TokenFilterEnum StemmerOverride = new TokenFilterEnum("stemmer_override", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-stemmer-override-tokenfilter.html");
        public static readonly TokenFilterEnum KeywordMarker = new TokenFilterEnum("keyword_marker", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-keyword-marker-tokenfilter.html");
        public static readonly TokenFilterEnum KeywordRepeat = new TokenFilterEnum("keyword_repeat", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-keyword-repeat-tokenfilter.html");
        public static readonly TokenFilterEnum KStem = new TokenFilterEnum("kstem", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-kstem-tokenfilter.html");
        public static readonly TokenFilterEnum Snowball = new TokenFilterEnum("snowball", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-snowball-tokenfilter.html");
        public static readonly TokenFilterEnum Phonetic = new TokenFilterEnum("phonetic", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-phonetic-tokenfilter.html");
        public static readonly TokenFilterEnum Synonym = new TokenFilterEnum("synonym", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-synonym-tokenfilter.html");
        public static readonly TokenFilterEnum CompoundWord = new TokenFilterEnum("compound_word", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-compound-word-tokenfilter.html");
        public static readonly TokenFilterEnum Reverse = new TokenFilterEnum("reverse", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-reverse-tokenfilter.html");
        public static readonly TokenFilterEnum Elision = new TokenFilterEnum("elision", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-elision-tokenfilter.html");
        public static readonly TokenFilterEnum Truncate = new TokenFilterEnum("truncate", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-truncate-tokenfilter.html");
        public static readonly TokenFilterEnum Unique = new TokenFilterEnum("unique", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-unique-tokenfilter.html");
        public static readonly TokenFilterEnum PatternCapture = new TokenFilterEnum("pattern_capture", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-pattern-capture-tokenfilter.html");
        public static readonly TokenFilterEnum PatternReplace = new TokenFilterEnum("pattern_replace", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-pattern-replace-tokenfilter.html");
        public static readonly TokenFilterEnum Trim = new TokenFilterEnum("trim", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-trim-tokenfilter.html");
        public static readonly TokenFilterEnum LimitTokenCount = new TokenFilterEnum("limit", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-limit-tokenfilter.html");
        public static readonly TokenFilterEnum Hunspell = new TokenFilterEnum("hunspell", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-hunspell-tokenfilter.html");
        public static readonly TokenFilterEnum CommonGrams = new TokenFilterEnum("common_grams", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-common-grams-tokenfilter.html");
        public static readonly TokenFilterEnum ArabicNormalization = new TokenFilterEnum("arabic_normalization", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-normalization-tokenfilter.html");
        public static readonly TokenFilterEnum PersianNormalization = new TokenFilterEnum("persian_normalization", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-normalization-tokenfilter.html");
        public static readonly TokenFilterEnum KeepWords = new TokenFilterEnum("keep", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-keep-words-tokenfilter.html");

        private TokenFilterEnum(string value, string docUri)
            : this(value, new Uri(docUri))
        { }

        private TokenFilterEnum(string value, Uri documentation)
            :base(value)
        {
            Documentation = documentation;

            _AllItems.Add(this);
        }
    }
}
