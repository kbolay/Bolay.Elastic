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
    [JsonConverter(typeof(GeoShapeSerializer))]
    public class GeoShapeBase
    {       
        public GeoShapeTypeEnum Type { get; private set; }

        public GeoShapeBase(GeoShapeTypeEnum type)
        {
            if (type == null)
                throw new ArgumentNullException("type", "GeoShapeBase requires a type.");

            Type = type;
        }

        internal static List<List<List<Double>>> BuildPolygonCoordinates(Polygon polygon)
        {
            List<List<Double>> borderPoints = new List<List<double>>();
            List<List<List<Double>>> multipleHolePoints = new List<List<List<double>>>();

            foreach (CoordinatePoint point in polygon.Border)
            {
                borderPoints.Add(BuildCoordinate(point));
            }

            foreach (IEnumerable<CoordinatePoint> hole in polygon.Holes)
            {
                List<List<Double>> holePoints = new List<List<double>>();
                foreach (CoordinatePoint point in hole)
                {
                    holePoints.Add(BuildCoordinate(point));
                }

                multipleHolePoints.Add(holePoints);
            }

            List<List<List<Double>>> result = new List<List<List<double>>>();
            result.Add(borderPoints);
            result.AddRange(multipleHolePoints);

            return result;
        }
        internal static List<Double> BuildCoordinate(CoordinatePoint point)
        {
            return new List<Double>()
            {
                point.Longitude,
                point.Latitude
            };
        }
        internal static Polygon BuildPolygonFromCoordinatesList(List<List<List<Double>>> polygonPoints)
        {
            List<CoordinatePoint> borderPoints = BuildCoordinateList(polygonPoints.First());
            List<List<CoordinatePoint>> polygonHoles = new List<List<CoordinatePoint>>();
            foreach (List<List<Double>> holePoints in polygonPoints.Skip(1))
            {
                if (!holePoints.Any())
                    continue;

                polygonHoles.Add(BuildCoordinateList(holePoints));
            }

            if (!polygonHoles.Any())
                polygonHoles = null;

            return new Polygon(borderPoints, polygonHoles);
        }
        internal static List<CoordinatePoint> BuildCoordinateList(List<List<Double>> coordinateListPoints)
        {
            List<CoordinatePoint> coordinatePoints = new List<CoordinatePoint>();
            foreach (List<Double> point in coordinateListPoints)
            {
                coordinatePoints.Add(BuildSingleCoordinate(point));
            }

            if (!coordinatePoints.Any())
                return null;

            return coordinatePoints;
        }
        internal static CoordinatePoint BuildSingleCoordinate(List<Double> point)
        {
            if (point.Count() != 2)
                throw new Exception("Each coordinate must consist of the latitude and longitude.");

            return new CoordinatePoint(
                    point[GeoShapeSerializer._LATITUDE_INDEX],
                    point[GeoShapeSerializer._LONGITUDE_INDEX]);
        }
    }
}
