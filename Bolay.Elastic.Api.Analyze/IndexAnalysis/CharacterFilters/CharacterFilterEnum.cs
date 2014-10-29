using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.CharacterFilters
{
    public sealed class CharacterFilterEnum : TypeSafeEnumBase<CharacterFilterEnum>
    {
        public Uri Documentation { get; set; }

        public static readonly CharacterFilterEnum Mapping = new CharacterFilterEnum("mapping", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-mapping-charfilter.html");
        public static readonly CharacterFilterEnum HtmlStrip = new CharacterFilterEnum("html_strip", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-htmlstrip-charfilter.html");
        public static readonly CharacterFilterEnum PatternReplace = new CharacterFilterEnum("pattern_replace", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-pattern-replace-charfilter.html");

        private CharacterFilterEnum(string value, string docUri)
            : this(value, new Uri(docUri))
        { }
        private CharacterFilterEnum(string value, Uri documentation)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
