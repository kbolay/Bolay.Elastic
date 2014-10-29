using Bolay.Elastic.Exceptions;
using Bolay.Elastic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.GeoShapes
{
    public class MultiPolygonSerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> coordDict = serializer.Deserialize<Dictionary<string, object>>(reader);

            List<List<List<List<Double>>>> coordinateLists = null;
            try
            {
                coordinateLists = JsonConvert.DeserializeObject<List<List<List<List<Double>>>>>(coordDict.GetString(GeoShapeSerializer._COORDINATES));
            }
            catch (Exception ex)
            {
                throw new Exception("Coordinates malformed. Example of MultiPolygon is [[[[1.1, 1.1],[2.2, 2.2],[3.3, 3.3]]],[[[1.1, 1.1],[2.2, 2.2],[3.3, 3.3]]]]");
            }

            List<Polygon> polygons = new List<Polygon>();
            foreach (List<List<List<Double>>> polygonPoints in coordinateLists)
            {
                polygons.Add(GeoShapeBase.BuildPolygonFromCoordinatesList(polygonPoints));
            }

            return new MultiPolygon(polygons);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is MultiPolygon))
                throw new SerializeTypeException<MultiPolygon>();

            MultiPolygon shape = value as MultiPolygon;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.Add(GeoShapeSerializer._TYPE, shape.Type.ToString());

            List<List<List<List<Double>>>> multiPolygonPoints = new List<List<List<List<Double>>>>();
            foreach (Polygon polygon in shape.Polygons)
            {
                multiPolygonPoints.Add(GeoShapeBase.BuildPolygonCoordinates(polygon));
            }

            fieldDict.Add(GeoShapeSerializer._COORDINATES, multiPolygonPoints);

            serializer.Serialize(writer, fieldDict);
        }
    }
}
