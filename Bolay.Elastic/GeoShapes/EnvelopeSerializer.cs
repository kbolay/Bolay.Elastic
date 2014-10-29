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
    public class EnvelopeSerializer : JsonConverter
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
            catch(Exception ex)
            {
                throw new Exception("Coordinates malformed. Ex. [[1.1, 1.1][2.2, 2.2]]", ex);
            }

            if(coordinateLists.Count != 2)
                throw new Exception("Coordinates malformed. Envelope expects two coordinate points.");

            List<CoordinatePoint> coordinatePoints = GeoShapeBase.BuildCoordinateList(coordinateLists);

            return new Envelope(coordinatePoints[0], coordinatePoints[1]);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is Envelope))
                throw new SerializeTypeException<Envelope>();

            Envelope shape = value as Envelope;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.Add(GeoShapeSerializer._TYPE, shape.Type.ToString());

            List<List<Double>> coordinates = new List<List<double>>();
            coordinates.Add(new List<Double>() { shape.TopLeft.Longitude, shape.TopLeft.Latitude });
            coordinates.Add(new List<Double>() { shape.BottomRight.Longitude, shape.BottomRight.Latitude });

            fieldDict.Add(GeoShapeSerializer._COORDINATES, coordinates);

            serializer.Serialize(writer, fieldDict);
        }
    }
}
