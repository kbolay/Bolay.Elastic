using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.HasParent
{
    public class ScoreTypeEnum : TypeSafeEnumBase<ScoreTypeEnum>
    {
        public static readonly ScoreTypeEnum Score = new ScoreTypeEnum("score");
        public static readonly ScoreTypeEnum None = new ScoreTypeEnum("none");

        private ScoreTypeEnum(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
