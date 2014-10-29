using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.MinimumShouldMatch
{
    public sealed class MinimumShouldMatchTypeEnum : TypeSafeEnumBase<MinimumShouldMatchTypeEnum>
    {
        public static readonly MinimumShouldMatchTypeEnum Integer = new MinimumShouldMatchTypeEnum("integer");
        public static readonly MinimumShouldMatchTypeEnum Percentage = new MinimumShouldMatchTypeEnum("percentage");
        public static readonly MinimumShouldMatchTypeEnum Combination = new MinimumShouldMatchTypeEnum("combination");
        public static readonly MinimumShouldMatchTypeEnum MultipleCombinations = new MinimumShouldMatchTypeEnum("multiple_combinations");

        private MinimumShouldMatchTypeEnum(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
