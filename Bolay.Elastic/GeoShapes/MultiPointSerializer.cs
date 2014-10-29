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
    public class MultiPointSerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> coordDict = serializer.Deserialize<Dictionary<string, object>>(reader);

            List<List<Double>> coordinateLists = null;
            try
            {
                coordinateLists = JsonConvert.DeserializeObject<List<List<Double>>>(coordDict.GetString(GeoShapeSerializer._COORDINATES));
            }
            catch (Exception ex)
            {
                throw new Exception("Coordinates malformed. Ex. [[1.1, 1.1][2.2, 2.2]]", ex);
            }

            List<CoordinatePoint> coordinatePoints = GeoShapeBase.BuildCoordinateList(coordinateLists);

            return new MultiPoint(coordinatePoints);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is MultiPoint))
                throw new SerializeTypeException<MultiPoint>();

            MultiPoint shape = value as MultiPoint;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.Add(GeoShapeSerializer._TYPE, shape.Type.ToString());

            List<List<Double>> coordinates = new List<List<double>>();
            foreach (CoordinatePoint point in shape.Points)
            {
                coordinates.Add(new List<Double>() { point.Longitude, point.Latitude });
            }

            fieldDict.Add(GeoShapeSerializer._COORDINATES, coordinates);

            serializer.Serialize(writer, fieldDict);
        }
    }
}
