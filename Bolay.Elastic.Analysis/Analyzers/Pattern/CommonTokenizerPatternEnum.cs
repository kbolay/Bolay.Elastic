using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Analyzers.Pattern
{
    public sealed class CommonTokenizerPatternEnum : TypeSafeEnumBase<CommonTokenizerPatternEnum>
    {
        public static readonly CommonTokenizerPatternEnum Whitespace = new CommonTokenizerPatternEnum(@"\\\\s+");
        public static readonly CommonTokenizerPatternEnum NonWordCharacter = new CommonTokenizerPatternEnum(@"[^\\\\w]+");
        public static readonly CommonTokenizerPatternEnum Camelcase = new CommonTokenizerPatternEnum(@"([^\\\\p{L}\\\\d]+)|(?<=\\\\D)(?=\\\\d)|(?<=\\\\d)(?=\\\\D)|(?<=[\\\\p{L}&&[^\\\\p{Lu}]])(?=\\\\p{Lu})|(?<=\\\\p{Lu})(?=\\\\p{Lu}[\\\\p{L}&&[^\\\\p{Lu}]])");

        private CommonTokenizerPatternEnum(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
