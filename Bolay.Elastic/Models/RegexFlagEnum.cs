using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Models
{
    /// <summary>
    /// http://docs.oracle.com/javase/6/docs/api/java/util/regex/Pattern.html#field_summary
    /// </summary>
    public sealed class RegexFlagEnum : TypeSafeEnumBase<RegexFlagEnum>
    {
        public static readonly RegexFlagEnum CannonEq = new RegexFlagEnum("CANNON_EQ");
        public static readonly RegexFlagEnum CaseInsensitive = new RegexFlagEnum("CASE_INSENSITIVE");
        public static readonly RegexFlagEnum Comments = new RegexFlagEnum("COMMENTS");
        public static readonly RegexFlagEnum DotAll = new RegexFlagEnum("DOTALL");
        public static readonly RegexFlagEnum Literal = new RegexFlagEnum("LITERAL");
        public static readonly RegexFlagEnum Multiline = new RegexFlagEnum("MULTILINE");
        public static readonly RegexFlagEnum UnicodeCase = new RegexFlagEnum("UNICODE_CASE");
        public static readonly RegexFlagEnum UnicodeCharacterClass = new RegexFlagEnum("UNICODE_CHARACTER_CLASS");
        public static readonly RegexFlagEnum UnixLines = new RegexFlagEnum("UNIX_LINES");

        private RegexFlagEnum(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
