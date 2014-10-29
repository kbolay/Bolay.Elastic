using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.GeoShapes
{
    [JsonConverter(typeof(MultiPolygonSerializer))]
    public class MultiPolygon : GeoShapeBase
    {
        public IEnumerable<Polygon> Polygons { get; private set; }

        public MultiPolygon(IEnumerable<Polygon> polygons)
            : base(GeoShapeTypeEnum.MultiPolygon)
        {
            if (polygons == null || polygons.Count(x => x != null) < 2)
                throw new ArgumentNullException("polygons", "MultiPolygon requires at least two polygons.");

            Polygons = polygons.Where(x => x != null);
        }
    }
}
