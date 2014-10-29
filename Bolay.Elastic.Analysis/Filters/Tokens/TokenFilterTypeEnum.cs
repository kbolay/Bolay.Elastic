using Bolay.Elastic.Analysis.Filters.Tokens.AsciiFolding;
using Bolay.Elastic.Analysis.Filters.Tokens.CommonGrams;
using Bolay.Elastic.Analysis.Filters.Tokens.CompoundWord;
using Bolay.Elastic.Analysis.Filters.Tokens.DelimitedPayload;
using Bolay.Elastic.Analysis.Filters.Tokens.EdgeNGram;
using Bolay.Elastic.Analysis.Filters.Tokens.Elision;
using Bolay.Elastic.Analysis.Filters.Tokens.Hunspell;
using Bolay.Elastic.Analysis.Filters.Tokens.IcuCollation;
using Bolay.Elastic.Analysis.Filters.Tokens.IcuFolding;
using Bolay.Elastic.Analysis.Filters.Tokens.KeepWords;
using Bolay.Elastic.Analysis.Filters.Tokens.KeywordMarker;
using Bolay.Elastic.Analysis.Filters.Tokens.KeywordRepeat;
using Bolay.Elastic.Analysis.Filters.Tokens.KStem;
using Bolay.Elastic.Analysis.Filters.Tokens.Length;
using Bolay.Elastic.Analysis.Filters.Tokens.LimitTokenCount;
using Bolay.Elastic.Analysis.Filters.Tokens.Lowercase;
using Bolay.Elastic.Analysis.Filters.Tokens.NGram;
using Bolay.Elastic.Analysis.Filters.Tokens.Normalization;
using Bolay.Elastic.Analysis.Filters.Tokens.PatternCapture;
using Bolay.Elastic.Analysis.Filters.Tokens.PatternReplace;
using Bolay.Elastic.Analysis.Filters.Tokens.Phonetic;
using Bolay.Elastic.Analysis.Filters.Tokens.PorterStem;
using Bolay.Elastic.Analysis.Filters.Tokens.Reverse;
using Bolay.Elastic.Analysis.Filters.Tokens.Shingle;
using Bolay.Elastic.Analysis.Filters.Tokens.Snowball;
using Bolay.Elastic.Analysis.Filters.Tokens.Standard;
using Bolay.Elastic.Analysis.Filters.Tokens.Stemmer;
using Bolay.Elastic.Analysis.Filters.Tokens.StemmerOverride;
using Bolay.Elastic.Analysis.Filters.Tokens.Stop;
using Bolay.Elastic.Analysis.Filters.Tokens.Synonym;
using Bolay.Elastic.Analysis.Filters.Tokens.Trim;
using Bolay.Elastic.Analysis.Filters.Tokens.Truncate;
using Bolay.Elastic.Analysis.Filters.Tokens.Unique;
using Bolay.Elastic.Analysis.Filters.Tokens.WordDelimiter;
using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens
{
    public class TokenFilterTypeEnum : TypeSafeEnumBase<TokenFilterTypeEnum>
    {
        public Uri Documentation { get; private set; }
        public Type ImplementationType { get; private set; }

        public static readonly TokenFilterTypeEnum Standard = new TokenFilterTypeEnum("standard", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-standard-tokenfilter.html", typeof(StandardTokenFilter));
        public static readonly TokenFilterTypeEnum AsciiFolding = new TokenFilterTypeEnum("asciifolding", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-asciifolding-tokenfilter.html", typeof(AsciiFoldingTokenFilter));
        public static readonly TokenFilterTypeEnum Length = new TokenFilterTypeEnum("length", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-length-tokenfilter.html", typeof(LengthTokenFilter));
        public static readonly TokenFilterTypeEnum Lowercase = new TokenFilterTypeEnum("lowercase", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-lowercase-tokenfilter.html", typeof(LowercaseTokenFilter));
        public static readonly TokenFilterTypeEnum Ngram = new TokenFilterTypeEnum("nGram", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-ngram-tokenfilter.html", typeof(NGramTokenFilter));
        public static readonly TokenFilterTypeEnum EdgeNGram = new TokenFilterTypeEnum("edgeNGram", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-edgengram-tokenfilter.html", typeof(EdgeNGramTokenFilter));
        public static readonly TokenFilterTypeEnum PorterStem = new TokenFilterTypeEnum("porter_stem", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-porterstem-tokenfilter.html", typeof(PorterStemTokenFilter));
        public static readonly TokenFilterTypeEnum Shingle = new TokenFilterTypeEnum("shingle", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-shingle-tokenfilter.html", typeof(ShingleTokenFilter));
        public static readonly TokenFilterTypeEnum StopWord = new TokenFilterTypeEnum("stop", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-stop-tokenfilter.html", typeof(StopTokenFilter));
        public static readonly TokenFilterTypeEnum WordDelimeter = new TokenFilterTypeEnum("word_delimiter", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-word-delimeter-tokenfilter.html", typeof(WordDelimiterTokenFilter));
        public static readonly TokenFilterTypeEnum Stemmer = new TokenFilterTypeEnum("stemmer", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-stemmer-tokenfilter.html", typeof(StemmerTokenFilter));
        public static readonly TokenFilterTypeEnum StemmerOverride = new TokenFilterTypeEnum("stemmer_override", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-stemmer-override-tokenfilter.html", typeof(StemmerOverrideTokenFilter));
        public static readonly TokenFilterTypeEnum KeywordMarker = new TokenFilterTypeEnum("keyword_marker", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-keyword-marker-tokenfilter.html", typeof(KeywordMarkerTokenFilter));
        public static readonly TokenFilterTypeEnum KeywordRepeat = new TokenFilterTypeEnum("keyword_repeat", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-keyword-repeat-tokenfilter.html", typeof(KeywordRepeatTokenFilter));
        public static readonly TokenFilterTypeEnum KStem = new TokenFilterTypeEnum("kstem", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-kstem-tokenfilter.html", typeof(KStemTokenFilter));
        public static readonly TokenFilterTypeEnum Snowball = new TokenFilterTypeEnum("snowball", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-snowball-tokenfilter.html", typeof(SnowballTokenFilter));
        public static readonly TokenFilterTypeEnum Phonetic = new TokenFilterTypeEnum("phonetic", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-phonetic-tokenfilter.html", typeof(PhoneticTokenFilter));
        public static readonly TokenFilterTypeEnum Synonym = new TokenFilterTypeEnum("synonym", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-synonym-tokenfilter.html", typeof(SynonymTokenFilter));
        //public static readonly TokenFilterTypeEnum CompoundWord = new TokenFilterTypeEnum("compound_word", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-compound-word-tokenfilter.html", typeof(StandardTokenFilter));
        public static readonly TokenFilterTypeEnum DictionaryDecompounder = new TokenFilterTypeEnum("dictionary_decompounder", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-compound-word-tokenfilter.html", typeof(DictionaryDecompounderTokenFilter));
        public static readonly TokenFilterTypeEnum HyphenationDecompounder = new TokenFilterTypeEnum("hyphenation_decompounder", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-compound-word-tokenfilter.html", typeof(HyphenationDecompounderTokenFilter));
        public static readonly TokenFilterTypeEnum Reverse = new TokenFilterTypeEnum("reverse", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-reverse-tokenfilter.html", typeof(ReverseTokenFilter));
        public static readonly TokenFilterTypeEnum Elision = new TokenFilterTypeEnum("elision", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-elision-tokenfilter.html", typeof(ElisionTokenFilter));
        public static readonly TokenFilterTypeEnum Truncate = new TokenFilterTypeEnum("truncate", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-truncate-tokenfilter.html", typeof(TruncateTokenFilter));
        public static readonly TokenFilterTypeEnum Unique = new TokenFilterTypeEnum("unique", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-unique-tokenfilter.html", typeof(UniqueTokenFilter));
        public static readonly TokenFilterTypeEnum PatternCapture = new TokenFilterTypeEnum("pattern_capture", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-pattern-capture-tokenfilter.html", typeof(PatternCaptureTokenFilter));
        public static readonly TokenFilterTypeEnum PatternReplace = new TokenFilterTypeEnum("pattern_replace", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-pattern-replace-tokenfilter.html", typeof(PatternReplaceTokenFilter));
        public static readonly TokenFilterTypeEnum Trim = new TokenFilterTypeEnum("trim", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-trim-tokenfilter.html", typeof(TrimTokenFilter));
        public static readonly TokenFilterTypeEnum LimitTokenCount = new TokenFilterTypeEnum("limit", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-limit-tokenfilter.html", typeof(LimitTokenCountTokenFilter));
        public static readonly TokenFilterTypeEnum Hunspell = new TokenFilterTypeEnum("hunspell", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-hunspell-tokenfilter.html", typeof(HunspellTokenFilter));
        public static readonly TokenFilterTypeEnum CommonGrams = new TokenFilterTypeEnum("common_grams", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-common-grams-tokenfilter.html", typeof(CommonGramsTokenFilter));
        public static readonly TokenFilterTypeEnum ArabicNormalization = new TokenFilterTypeEnum("arabic_normalization", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-normalization-tokenfilter.html", typeof(ArabicNormalizationTokenFilter));
        public static readonly TokenFilterTypeEnum PersianNormalization = new TokenFilterTypeEnum("persian_normalization", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-normalization-tokenfilter.html", typeof(PersianNormalizationTokenFilter));
        public static readonly TokenFilterTypeEnum DelimitedPayload = new TokenFilterTypeEnum("delimited_payload_filter", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-delimited-payload-tokenfilter.html", typeof(DelimitedPayloadTokenFilter));
        public static readonly TokenFilterTypeEnum KeepWords = new TokenFilterTypeEnum("keep", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-keep-words-tokenfilter.html", typeof(KeepWordsTokenFilter));
        public static readonly TokenFilterTypeEnum IcuFolding = new TokenFilterTypeEnum("icu_folding", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-icu-plugin.html#icu-folding", typeof(IcuFoldingTokenFilter));
        public static readonly TokenFilterTypeEnum IcuCollation = new TokenFilterTypeEnum("icu_collation", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-icu-plugin.html#icu-collation", typeof(IcuCollationTokenFilter));

        private TokenFilterTypeEnum(string value, string docUri, Type implementationType)
            : this(value, new Uri(docUri), implementationType)
        { }

        private TokenFilterTypeEnum(string value, Uri documentation, Type implementationType)
            :base(value)
        {
            Documentation = documentation;
            ImplementationType = implementationType;
            _AllItems.Add(this);
        }
    }
}
