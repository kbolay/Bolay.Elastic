using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting.GeoDistance
{
    public sealed class ExtendedDistanceUnitEnum : TypeSafeEnumBase<ExtendedDistanceUnitEnum>
    {
        public static readonly ExtendedDistanceUnitEnum Miles = new ExtendedDistanceUnitEnum("miles");
        public static readonly ExtendedDistanceUnitEnum MilesAbbr = new ExtendedDistanceUnitEnum("mi");
        public static readonly ExtendedDistanceUnitEnum Inch = new ExtendedDistanceUnitEnum("inch");
        public static readonly ExtendedDistanceUnitEnum InchAbbr = new ExtendedDistanceUnitEnum("in");
        public static readonly ExtendedDistanceUnitEnum Yards = new ExtendedDistanceUnitEnum("yards");
        public static readonly ExtendedDistanceUnitEnum YardsAbbr = new ExtendedDistanceUnitEnum("yd");
        public static readonly ExtendedDistanceUnitEnum Feet = new ExtendedDistanceUnitEnum("feet");
        public static readonly ExtendedDistanceUnitEnum FeetAbbr = new ExtendedDistanceUnitEnum("ft");
        public static readonly ExtendedDistanceUnitEnum Kilometers = new ExtendedDistanceUnitEnum("kilometers");
        public static readonly ExtendedDistanceUnitEnum KilometersAbbr = new ExtendedDistanceUnitEnum("km");
        public static readonly ExtendedDistanceUnitEnum Millimeters = new ExtendedDistanceUnitEnum("millimeters");
        public static readonly ExtendedDistanceUnitEnum MillimetersAbbr = new ExtendedDistanceUnitEnum("mm");
        public static readonly ExtendedDistanceUnitEnum Centimeters = new ExtendedDistanceUnitEnum("centimeters");
        public static readonly ExtendedDistanceUnitEnum CentimetersAbbr = new ExtendedDistanceUnitEnum("cm");
        public static readonly ExtendedDistanceUnitEnum Meters = new ExtendedDistanceUnitEnum("meters");
        public static readonly ExtendedDistanceUnitEnum MetersAbbr = new ExtendedDistanceUnitEnum("m");

        private ExtendedDistanceUnitEnum(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
