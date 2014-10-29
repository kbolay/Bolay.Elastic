using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.GeoDistance
{
    public sealed class BoundingBoxOptimizeEnum : TypeSafeEnumBase<BoundingBoxOptimizeEnum>
    {
        public static readonly BoundingBoxOptimizeEnum Memory = new BoundingBoxOptimizeEnum("memory");
        public static readonly BoundingBoxOptimizeEnum Indexed = new BoundingBoxOptimizeEnum("indexed");
        public static readonly BoundingBoxOptimizeEnum None = new BoundingBoxOptimizeEnum("none");

        private BoundingBoxOptimizeEnum(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
