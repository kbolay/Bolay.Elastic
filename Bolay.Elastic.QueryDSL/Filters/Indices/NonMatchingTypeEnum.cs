using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.Indices
{
    // I know this is duplicate of the query but i want this specific to the filter so that changes to one don't break the other.

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
