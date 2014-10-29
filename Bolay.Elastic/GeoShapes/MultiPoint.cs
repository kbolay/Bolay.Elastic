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
    [JsonConverter(typeof(MultiPointSerializer))]
    public class MultiPoint : GeoShapeBase
    {
        public IEnumerable<CoordinatePoint> Points { get; private set; }

        public MultiPoint(IEnumerable<CoordinatePoint> points)
            : base(GeoShapeTypeEnum.MultiPoint)
        {
            if (points == null || points.Count(x => x != null) < 2)
                throw new ArgumentNullException("points", "MultiPoint requires at least two coordinate points.");

            Points = points.Where(x => x != null);
        }
    }
}
