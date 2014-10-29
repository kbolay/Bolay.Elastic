using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Nested
{
    public sealed class ScoreTypeEnum : TypeSafeEnumBase<ScoreTypeEnum>
    {
        public static readonly ScoreTypeEnum Maximum = new ScoreTypeEnum("max");
        public static readonly ScoreTypeEnum Total = new ScoreTypeEnum("total");
        public static readonly ScoreTypeEnum Average = new ScoreTypeEnum("avg");
        public static readonly ScoreTypeEnum None = new ScoreTypeEnum("none");

        private ScoreTypeEnum(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
