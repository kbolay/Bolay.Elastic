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
    [JsonConverter(typeof(LineStringSerializer))]
    public class LineString : GeoShapeBase
    {
        public IEnumerable<CoordinatePoint> Points { get; private set; }

        public LineString(IEnumerable<CoordinatePoint> points)
            : base(GeoShapeTypeEnum.LineString)
        {
            if (points == null || points.Count(x => x != null) < 2)
                throw new ArgumentNullException("points", "LineString requires multiple coordinate points.");

            Points = points;
        }
    }
}
