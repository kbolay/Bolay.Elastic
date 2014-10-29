using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.Models.IndexAnalysis
{
    public sealed class RegexFlagEnum : TypeSafeEnumBase<RegexFlagEnum>
    {
        public static readonly RegexFlagEnum CanonicalEquivalence = new RegexFlagEnum("CANON_EQ");
        public static readonly RegexFlagEnum CaseInsensitiveMatching = new RegexFlagEnum("CASE_INSENSITIVE");
        public static readonly RegexFlagEnum Comments = new RegexFlagEnum("COMMENTS");
        public static readonly RegexFlagEnum DotAll = new RegexFlagEnum("DOTALL");
        public static readonly RegexFlagEnum LiteralParsing = new RegexFlagEnum("LITERAL");
        public static readonly RegexFlagEnum Multiline = new RegexFlagEnum("MULTILINE");
        public static readonly RegexFlagEnum UnicodeCaseFolding = new RegexFlagEnum("UNICODE_CASE");
        public static readonly RegexFlagEnum UnixLines = new RegexFlagEnum("UNIX_LINES");

        private RegexFlagEnum(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
