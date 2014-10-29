using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.Models
{
    public sealed class SupportedLanguageEnum : TypeSafeEnumBase<SupportedLanguageEnum>
    {
        public Uri Documentation { get; set; }

        public static readonly SupportedLanguageEnum Arabic = new SupportedLanguageEnum("arabic", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly SupportedLanguageEnum Armenian = new SupportedLanguageEnum("armenian", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly SupportedLanguageEnum Basque = new SupportedLanguageEnum("basque", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly SupportedLanguageEnum Brazilian = new SupportedLanguageEnum("brazilian", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly SupportedLanguageEnum Bulgarian = new SupportedLanguageEnum("bulgarian", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly SupportedLanguageEnum Catalan = new SupportedLanguageEnum("catalan", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly SupportedLanguageEnum Chinese = new SupportedLanguageEnum("chinese", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly SupportedLanguageEnum Cjk = new SupportedLanguageEnum("cjk", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly SupportedLanguageEnum Czech = new SupportedLanguageEnum("czech", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly SupportedLanguageEnum Danish = new SupportedLanguageEnum("danish", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly SupportedLanguageEnum Dutch = new SupportedLanguageEnum("dutch", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly SupportedLanguageEnum English = new SupportedLanguageEnum("english", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly SupportedLanguageEnum Finnish = new SupportedLanguageEnum("finnish", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly SupportedLanguageEnum French = new SupportedLanguageEnum("french", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly SupportedLanguageEnum Galician = new SupportedLanguageEnum("galician", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly SupportedLanguageEnum German = new SupportedLanguageEnum("german", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly SupportedLanguageEnum Greek = new SupportedLanguageEnum("greek", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly SupportedLanguageEnum Hindi = new SupportedLanguageEnum("hindi", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly SupportedLanguageEnum Hungarian = new SupportedLanguageEnum("hungarian", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly SupportedLanguageEnum Indonesian = new SupportedLanguageEnum("indonesian", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly SupportedLanguageEnum Italian = new SupportedLanguageEnum("italian", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly SupportedLanguageEnum Norwegian = new SupportedLanguageEnum("norwegian", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly SupportedLanguageEnum Persian = new SupportedLanguageEnum("persian", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly SupportedLanguageEnum Portuguese = new SupportedLanguageEnum("portuguese", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly SupportedLanguageEnum Romanian = new SupportedLanguageEnum("romanian", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly SupportedLanguageEnum Russian = new SupportedLanguageEnum("russian", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly SupportedLanguageEnum Spanish = new SupportedLanguageEnum("spanish", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly SupportedLanguageEnum Swedish = new SupportedLanguageEnum("swedish", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly SupportedLanguageEnum Turkish = new SupportedLanguageEnum("turkish", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        public static readonly SupportedLanguageEnum Thai = new SupportedLanguageEnum("thai", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-language-analyzer.html");
        
        private SupportedLanguageEnum(string value, string docUri)
            : this(value, new Uri(docUri))
        { }
        private SupportedLanguageEnum(string value, Uri documentation)
            : base(value)
        {
            Documentation = documentation;
            _AllItems.Add(this);
        }
    }
}
