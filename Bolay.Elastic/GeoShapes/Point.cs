using Bolay.Elastic.Coordinates;
using Bolay.Elastic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.GeoShapes
{
    [JsonConverter(typeof(PointSerializer))]
    public class Point : GeoShapeBase
    {
        public CoordinatePoint Coordinate { get; private set; }

        public Point(CoordinatePoint point)
            : base(GeoShapeTypeEnum.Point)
        {
            if (point == null)
                throw new ArgumentNullException("point", "Point requires a coordinate point.");
            
            Coordinate = point;
        }
    }
}
