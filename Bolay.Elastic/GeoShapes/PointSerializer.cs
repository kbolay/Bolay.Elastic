using Bolay.Elastic.Coordinates;
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
    public class PointSerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> coordDict = serializer.Deserialize<Dictionary<string, object>>(reader);

            List<Double> coordinateLists = null;
            try
            {
                coordinateLists = JsonConvert.DeserializeObject<List<Double>>(coordDict.GetString(GeoShapeSerializer._COORDINATES));
            }
            catch (Exception ex)
            {
                throw new Exception("Coordinates malformed. Point Ex. [1.1, 1.1]", ex);
            }

            CoordinatePoint point = GeoShapeBase.BuildSingleCoordinate(coordinateLists);

            return new Point(point);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is Point))
                throw new SerializeTypeException<Point>();

            Point shape = value as Point;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.Add(GeoShapeSerializer._TYPE, shape.Type.ToString());

            List<Double> coordinate = new List<double>();
            coordinate.AddRange(GeoShapeBase.BuildCoordinate(shape.Coordinate));

            fieldDict.Add(GeoShapeSerializer._COORDINATES, coordinate);

            serializer.Serialize(writer, fieldDict);
        }
    }
}
