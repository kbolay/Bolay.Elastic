using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Suggesters.Term
{
    public sealed class SortModeEnum : TypeSafeEnumBase<SortModeEnum>
    {
        public static readonly SortModeEnum Score = new SortModeEnum("score");
        public static readonly SortModeEnum Frequency = new SortModeEnum("frequency");

        private SortModeEnum(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
