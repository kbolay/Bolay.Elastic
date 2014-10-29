using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.GeoShapes
{
    public class GeoShapeTypeEnum : TypeSafeEnumBase<GeoShapeTypeEnum>
    {
        public Type ImplementingType { get; private set; }

        public static readonly GeoShapeTypeEnum Point = new GeoShapeTypeEnum("point", typeof(Point));
        public static readonly GeoShapeTypeEnum LineString = new GeoShapeTypeEnum("linestring", typeof(LineString));
        public static readonly GeoShapeTypeEnum Envelope = new GeoShapeTypeEnum("envelope", typeof(Envelope));
        public static readonly GeoShapeTypeEnum Polygon = new GeoShapeTypeEnum("polygon", typeof(Polygon));
        public static readonly GeoShapeTypeEnum MultiPoint = new GeoShapeTypeEnum("multipoint", typeof(MultiPoint));
        public static readonly GeoShapeTypeEnum MultiPolygon = new GeoShapeTypeEnum("multipolygon", typeof(MultiPolygon));
        public static readonly GeoShapeTypeEnum IndexedShape = new GeoShapeTypeEnum("indexed_shape", typeof(IndexedShape));

        private GeoShapeTypeEnum(string value, Type type)
            : base(value)
        {
            ImplementingType = type;
            _AllItems.Add(this);
        }
    }
}
