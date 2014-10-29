using Bolay.Elastic.Analysis.Filters.Characters.HtmlStrip;
using Bolay.Elastic.Analysis.Filters.Characters.Mapping;
using Bolay.Elastic.Analysis.Filters.Characters.PatternReplace;
using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Characters
{
    public sealed class CharacterFilterTypeEnum : TypeSafeEnumBase<CharacterFilterTypeEnum>
    {
        public Uri Documentation { get; set; }
        public Type ImplementationType { get; set; }

        public static readonly CharacterFilterTypeEnum Mapping = new CharacterFilterTypeEnum("mapping", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-mapping-charfilter.html", typeof(MappingCharacterFilter));
        public static readonly CharacterFilterTypeEnum HtmlStrip = new CharacterFilterTypeEnum("html_strip", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-htmlstrip-charfilter.html", typeof(HtmlStripCharacterFilter));
        public static readonly CharacterFilterTypeEnum PatternReplace = new CharacterFilterTypeEnum("pattern_replace", "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/analysis-pattern-replace-charfilter.html", typeof(PatternReplaceCharacterFilter));

        private CharacterFilterTypeEnum(string value, string docUri, Type implementationType)
            : this(value, new Uri(docUri), implementationType)
        { }
        private CharacterFilterTypeEnum(string value, Uri documentation, Type implementationType)
            : base(value)
        {
            Documentation = documentation;
            ImplementationType = implementationType;
            _AllItems.Add(this);
        }
    }
}
