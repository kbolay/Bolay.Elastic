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
    [JsonConverter(typeof(PolygonSerializer))]
    public class Polygon : GeoShapeBase
    {
        public IEnumerable<CoordinatePoint> Border { get; private set; }
        public IEnumerable<IEnumerable<CoordinatePoint>> Holes { get; private set; }

        public Polygon(IEnumerable<CoordinatePoint> border, IEnumerable<IEnumerable<CoordinatePoint>> holes = null)
            : base(GeoShapeTypeEnum.Point)
        {
            if (border == null || border.Count(x => x != null) < 3)
                throw new ArgumentNullException("border", "Polygon requires a border of at least three coordinate points.");

            Border = border.Where(x => x != null);

            if (holes == null || holes.Any())
            {
                Holes = null;
            }                
            else
            {
                List<List<CoordinatePoint>> holesHolder = new List<List<CoordinatePoint>>();
                foreach (IEnumerable<CoordinatePoint> hole in holes)
                {
                    if (hole.Any(x => x != null))
                        holesHolder.Add(hole.Where(x => x != null).ToList());
                }

                if (holesHolder.Any())
                    Holes = holesHolder;
            }
        }
    }
}
