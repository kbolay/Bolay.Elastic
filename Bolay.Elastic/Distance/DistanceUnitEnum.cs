using Bolay.Elastic;
using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Distance
{
    public sealed class DistanceUnitEnum : TypeSafeEnumBase<DistanceUnitEnum>
    {
        public static readonly DistanceUnitEnum Inch = new DistanceUnitEnum("in");
        public static readonly DistanceUnitEnum Yard = new DistanceUnitEnum("yd");
        public static readonly DistanceUnitEnum Mile = new DistanceUnitEnum("mi");
        public static readonly DistanceUnitEnum Kilometer = new DistanceUnitEnum("km");
        public static readonly DistanceUnitEnum Meter = new DistanceUnitEnum("m");
        public static readonly DistanceUnitEnum Centimeter = new DistanceUnitEnum("cm");
        public static readonly DistanceUnitEnum Millimeter = new DistanceUnitEnum("mm");

        private DistanceUnitEnum(string value) : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
