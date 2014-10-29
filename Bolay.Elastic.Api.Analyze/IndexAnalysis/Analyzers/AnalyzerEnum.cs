using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.Analyzers
{
    public sealed class AnalyzerEnum : TypeSafeEnumBase<AnalyzerEnum>
    {
        public Uri Documentation { get; set; }

        public static readonly AnalyzerEnum Standard = new AnalyzerEnum("standard", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-standard-analyzer.html");
        public static readonly AnalyzerEnum Simple = new AnalyzerEnum("simple", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-simple-analyzer.html");
        public static readonly AnalyzerEnum Whitespace = new AnalyzerEnum("whitespace", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-whitespace-analyzer.html");
        public static readonly AnalyzerEnum StopWord = new AnalyzerEnum("stop", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-stop-analyzer.html");
        public static readonly AnalyzerEnum Keyword = new AnalyzerEnum("keyword", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-keyword-analyzer.html");
        public static readonly AnalyzerEnum Pattern = new AnalyzerEnum("pattern", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-pattern-analyzer.html");
        public static readonly AnalyzerEnum Arabic = new AnalyzerEnum("arabic", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly AnalyzerEnum Armenian = new AnalyzerEnum("armenian", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly AnalyzerEnum Basque = new AnalyzerEnum("basque", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly AnalyzerEnum Brazilian = new AnalyzerEnum("brazilian", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly AnalyzerEnum Bulgarian = new AnalyzerEnum("bulgarian", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly AnalyzerEnum Catalan = new AnalyzerEnum("catalan", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly AnalyzerEnum Chinese = new AnalyzerEnum("chinese", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly AnalyzerEnum Cjk = new AnalyzerEnum("cjk", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly AnalyzerEnum Czech = new AnalyzerEnum("czech", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly AnalyzerEnum Danish = new AnalyzerEnum("danish", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly AnalyzerEnum Dutch = new AnalyzerEnum("dutch", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly AnalyzerEnum English = new AnalyzerEnum("english", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly AnalyzerEnum Finnish = new AnalyzerEnum("finnish", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly AnalyzerEnum French = new AnalyzerEnum("french", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly AnalyzerEnum Galician = new AnalyzerEnum("galician", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly AnalyzerEnum German = new AnalyzerEnum("german", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly AnalyzerEnum Greek = new AnalyzerEnum("greek", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly AnalyzerEnum Hindi = new AnalyzerEnum("hindi", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly AnalyzerEnum Hungarian = new AnalyzerEnum("hungarian", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly AnalyzerEnum Indonesian = new AnalyzerEnum("indonesian", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly AnalyzerEnum Italian = new AnalyzerEnum("italian", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly AnalyzerEnum Norwegian = new AnalyzerEnum("norwegian", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly AnalyzerEnum Persian = new AnalyzerEnum("persian", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly AnalyzerEnum Portuguese = new AnalyzerEnum("portuguese", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly AnalyzerEnum Romanian = new AnalyzerEnum("romanian", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly AnalyzerEnum Russian = new AnalyzerEnum("russian", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly AnalyzerEnum Spanish = new AnalyzerEnum("spanish", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly AnalyzerEnum Swedish = new AnalyzerEnum("swedish", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly AnalyzerEnum Turkish = new AnalyzerEnum("turkish", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly AnalyzerEnum Thai = new AnalyzerEnum("thai", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly AnalyzerEnum Snowball = new AnalyzerEnum("snowball", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-snowball-analyzer.html");
        public static readonly AnalyzerEnum Custom = new AnalyzerEnum("custom", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-custom-analyzer.html");

        private AnalyzerEnum(string value, string docUri)
            : this(value, new Uri(docUri))
        {
        }

        private AnalyzerEnum(string value, Uri documentation)
            : base(value)
        {
            Documentation = documentation;
            _AllItems.Add(this);
        }
    }
}
