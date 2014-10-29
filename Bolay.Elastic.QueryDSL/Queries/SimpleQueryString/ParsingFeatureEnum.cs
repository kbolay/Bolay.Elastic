using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.SimpleQueryString
{
    public sealed class ParsingFeatureEnum : TypeSafeEnumBase<ParsingFeatureEnum>
    {
        public static readonly ParsingFeatureEnum All = new ParsingFeatureEnum("ALL");
        public static ParsingFeatureEnum None = new ParsingFeatureEnum("NONE");
        public static ParsingFeatureEnum And = new ParsingFeatureEnum("AND");
        public static ParsingFeatureEnum Or = new ParsingFeatureEnum("OR");
        public static ParsingFeatureEnum Prefix = new ParsingFeatureEnum("PREFIX");
        public static ParsingFeatureEnum Phrase = new ParsingFeatureEnum("PHRASE");
        public static ParsingFeatureEnum Precendence = new ParsingFeatureEnum("PRECEDENCE");
        public static ParsingFeatureEnum Escape = new ParsingFeatureEnum("ESCAPE");
        public static ParsingFeatureEnum WhiteSpace = new ParsingFeatureEnum("WHITESPACE");
        public static ParsingFeatureEnum Fuzzy = new ParsingFeatureEnum("FUZZY");
        public static ParsingFeatureEnum Near = new ParsingFeatureEnum("NEAR");
        public static ParsingFeatureEnum Slop = new ParsingFeatureEnum("SLOP");

        private ParsingFeatureEnum(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
