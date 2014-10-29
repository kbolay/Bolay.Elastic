using Bolay.Elastic.Analysis.Analyzers.Custom;
using Bolay.Elastic.Analysis.Analyzers.Keyword;
using Bolay.Elastic.Analysis.Analyzers.Language;
using Bolay.Elastic.Analysis.Analyzers.Pattern;
using Bolay.Elastic.Analysis.Analyzers.Simple;
using Bolay.Elastic.Analysis.Analyzers.Snowball;
using Bolay.Elastic.Analysis.Analyzers.Standard;
using Bolay.Elastic.Analysis.Analyzers.Stop;
using Bolay.Elastic.Analysis.Analyzers.Whitespace;
using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Analyzers
{
    public sealed class AnalyzerTypeEnum : TypeSafeEnumBase<AnalyzerTypeEnum>
    {
        public Uri Documentation { get; private set; }
        public Type ImplementationType { get; private set; }

        public static readonly AnalyzerTypeEnum Standard = new AnalyzerTypeEnum("standard", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-standard-analyzer.html", typeof(StandardAnalyzer));
        public static readonly AnalyzerTypeEnum Simple = new AnalyzerTypeEnum("simple", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-simple-analyzer.html", typeof(SimpleAnalyzer));
        public static readonly AnalyzerTypeEnum Whitespace = new AnalyzerTypeEnum("whitespace", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-whitespace-analyzer.html", typeof(WhitespaceAnalyzer));
        public static readonly AnalyzerTypeEnum Stop = new AnalyzerTypeEnum("stop", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-stop-analyzer.html", typeof(StopAnalyzer));
        public static readonly AnalyzerTypeEnum Keyword = new AnalyzerTypeEnum("keyword", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-keyword-analyzer.html", typeof(KeywordAnalyzer));
        public static readonly AnalyzerTypeEnum Pattern = new AnalyzerTypeEnum("pattern", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-pattern-analyzer.html", typeof(PatternAnalyzer));
        public static readonly AnalyzerTypeEnum Arabic = new AnalyzerTypeEnum("arabic", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html", typeof(ArabicAnalyzer));
        public static readonly AnalyzerTypeEnum Armenian = new AnalyzerTypeEnum("armenian", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html", typeof(ArmenianAnalyzer));
        public static readonly AnalyzerTypeEnum Basque = new AnalyzerTypeEnum("basque", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html", typeof(BasqueAnalyzer));
        public static readonly AnalyzerTypeEnum Brazilian = new AnalyzerTypeEnum("brazilian", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html", typeof(BrazilianAnalyzer));
        public static readonly AnalyzerTypeEnum Bulgarian = new AnalyzerTypeEnum("bulgarian", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html", typeof(BulgarianAnalyzer));
        public static readonly AnalyzerTypeEnum Catalan = new AnalyzerTypeEnum("catalan", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html", typeof(CatalanAnalyzer));
        public static readonly AnalyzerTypeEnum Chinese = new AnalyzerTypeEnum("chinese", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html", typeof(ChineseAnalyzer));
        public static readonly AnalyzerTypeEnum Cjk = new AnalyzerTypeEnum("cjk", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html", typeof(CJKAnalyzer));
        public static readonly AnalyzerTypeEnum Czech = new AnalyzerTypeEnum("czech", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html", typeof(CzechAnalyzer));
        public static readonly AnalyzerTypeEnum Danish = new AnalyzerTypeEnum("danish", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html", typeof(DanishAnalyzer));
        public static readonly AnalyzerTypeEnum Dutch = new AnalyzerTypeEnum("dutch", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html", typeof(DutchAnalyzer));
        public static readonly AnalyzerTypeEnum English = new AnalyzerTypeEnum("english", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html", typeof(EnglishAnalyzer));
        public static readonly AnalyzerTypeEnum Finnish = new AnalyzerTypeEnum("finnish", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html", typeof(FinnishAnalyzer));
        public static readonly AnalyzerTypeEnum French = new AnalyzerTypeEnum("french", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html", typeof(FrenchAnalyzer));
        public static readonly AnalyzerTypeEnum Galician = new AnalyzerTypeEnum("galician", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html", typeof(GalicianAnalyzer));
        public static readonly AnalyzerTypeEnum German = new AnalyzerTypeEnum("german", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html", typeof(GermanAnalyzer));
        public static readonly AnalyzerTypeEnum Greek = new AnalyzerTypeEnum("greek", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html", typeof(GreekAnalyzer));
        public static readonly AnalyzerTypeEnum Hindi = new AnalyzerTypeEnum("hindi", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html", typeof(HindiAnalyzer));
        public static readonly AnalyzerTypeEnum Hungarian = new AnalyzerTypeEnum("hungarian", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html", typeof(HungarianAnalyzer));
        public static readonly AnalyzerTypeEnum Indonesian = new AnalyzerTypeEnum("indonesian", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html", typeof(IndonesianAnalyzer));
        public static readonly AnalyzerTypeEnum Italian = new AnalyzerTypeEnum("italian", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html", typeof(ItalianAnalyzer));
        public static readonly AnalyzerTypeEnum Norwegian = new AnalyzerTypeEnum("norwegian", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html", typeof(NorwegianAnalyzer));
        public static readonly AnalyzerTypeEnum Persian = new AnalyzerTypeEnum("persian", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html", typeof(PersianAnalyzer));
        public static readonly AnalyzerTypeEnum Portuguese = new AnalyzerTypeEnum("portuguese", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html", typeof(PortugueseAnalyzer));
        public static readonly AnalyzerTypeEnum Romanian = new AnalyzerTypeEnum("romanian", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html", typeof(RomanianAnalyzer));
        public static readonly AnalyzerTypeEnum Russian = new AnalyzerTypeEnum("russian", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html", typeof(RussianAnalyzer));
        public static readonly AnalyzerTypeEnum Spanish = new AnalyzerTypeEnum("spanish", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html", typeof(SpanishAnalyzer));
        public static readonly AnalyzerTypeEnum Swedish = new AnalyzerTypeEnum("swedish", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html", typeof(SwedishAnalyzer));
        public static readonly AnalyzerTypeEnum Turkish = new AnalyzerTypeEnum("turkish", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html", typeof(TurkishAnalyzer));
        public static readonly AnalyzerTypeEnum Thai = new AnalyzerTypeEnum("thai", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html", typeof(ThaiAnalyzer));
        public static readonly AnalyzerTypeEnum Snowball = new AnalyzerTypeEnum("snowball", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-snowball-analyzer.html", typeof(SnowballAnalyzer));
        public static readonly AnalyzerTypeEnum Custom = new AnalyzerTypeEnum("custom", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-custom-analyzer.html", typeof(CustomAnalyzer));

        private AnalyzerTypeEnum(string value, string docUri, Type implementationType)
            : this(value, new Uri(docUri), implementationType)
        {
        }

        private AnalyzerTypeEnum(string value, Uri documentation, Type implementationType)
            : base(value)
        {
            Documentation = documentation;
            ImplementationType = implementationType;
            _AllItems.Add(this);
        }
    }
}
