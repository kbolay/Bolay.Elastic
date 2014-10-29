using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Rescoring
{
    public sealed class ScoreModeEnum : TypeSafeEnumBase<ScoreModeEnum>
    {
        public static readonly ScoreModeEnum Total = new ScoreModeEnum("total");
        public static readonly ScoreModeEnum Multiply = new ScoreModeEnum("multiply");
        public static readonly ScoreModeEnum Average = new ScoreModeEnum("avg");
        public static readonly ScoreModeEnum Maximum = new ScoreModeEnum("max");
        public static readonly ScoreModeEnum Minimum = new ScoreModeEnum("min");

        private ScoreModeEnum(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
