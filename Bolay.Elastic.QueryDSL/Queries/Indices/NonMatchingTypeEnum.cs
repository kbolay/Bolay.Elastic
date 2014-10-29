using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Indices
{
    public sealed class NonMatchingTypeEnum : TypeSafeEnumBase<NonMatchingTypeEnum>
    {
        public static readonly NonMatchingTypeEnum None = new NonMatchingTypeEnum("none");
        public static readonly NonMatchingTypeEnum All = new NonMatchingTypeEnum("all");

        private NonMatchingTypeEnum(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
