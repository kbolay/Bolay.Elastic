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
    public class PolygonSerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> coordDict = serializer.Deserialize<Dictionary<string, object>>(reader);

            List<List<List<Double>>> coordinateLists = null;
            try
            {
                coordinateLists = JsonConvert.DeserializeObject<List<List<List<Double>>>>(coordDict.GetString(GeoShapeSerializer._COORDINATES));
            }
            catch (Exception ex)
            {
                throw new Exception("Coordinates malformed. Example of Polygon is [[[1.1, 1.1],[2.2, 2.2],[3.3, 3.3]],[[1.1, 1.1],[2.2, 2.2],[3.3, 3.3]]]");
            }

            return GeoShapeBase.BuildPolygonFromCoordinatesList(coordinateLists);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is Polygon))
                throw new SerializeTypeException<Polygon>();

            Polygon shape = value as Polygon;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.Add(GeoShapeSerializer._TYPE, shape.Type.ToString());

            List<List<List<Double>>> polygonPoints = GeoShapeBase.BuildPolygonCoordinates(shape);

            fieldDict.Add(GeoShapeSerializer._COORDINATES, polygonPoints);

            serializer.Serialize(writer, fieldDict);
        }
    }
}
