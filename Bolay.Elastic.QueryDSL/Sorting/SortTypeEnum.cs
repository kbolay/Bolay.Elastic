using Bolay.Elastic.Models;
using Bolay.Elastic.QueryDSL.Sorting.Field;
using Bolay.Elastic.QueryDSL.Sorting.GeoDistance;
using Bolay.Elastic.QueryDSL.Sorting.Script;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Sorting
{
    public sealed class SortTypeEnum : TypeSafeEnumBase<SortTypeEnum>
    {
        public Type ImplementationType { get; private set; }

        public static readonly SortTypeEnum Field = new SortTypeEnum("field", typeof(FieldSort));
        public static readonly SortTypeEnum GeoDistance = new SortTypeEnum("_geo_distance", typeof(GeoDistanceSort));
        public static readonly SortTypeEnum Script = new SortTypeEnum("_script", typeof(ScriptSort));
        
        private SortTypeEnum(string value, Type implementationType)
            : base(value)
        {
            ImplementationType = implementationType;
            _AllItems.Add(this);
        }
    }
}
