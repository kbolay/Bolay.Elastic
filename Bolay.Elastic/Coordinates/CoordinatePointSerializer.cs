using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Coordinates
{
    public class CoordinatePointSerializer : JsonConverter
    {
        private const string _DELIMETER = ", ";
        private static List<string> _DELIMETERS = new List<string>() { " , ", " ,", ", ", "," };
        private const string _LATITUDE = "lat";
        private const string _LONGITUDE = "lon";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new Exception("Use CoordinatePointSerializer.DeserializeCoordinatePoint.");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is CoordinatePoint))
                throw new SerializeTypeException<CoordinatePoint>();

            CoordinatePoint point = value as CoordinatePoint;

            Dictionary<string, object> pointDict = new Dictionary<string, object>();

            // always try to serialize latitude and longitude first
            if (point.Latitude != default(Double) || point.Longitude != default(Double) || string.IsNullOrWhiteSpace(point.GeoHash))
            {
                pointDict.Add(_LATITUDE, point.Latitude);
                pointDict.Add(_LONGITUDE, point.Longitude);
                serializer.Serialize(writer, pointDict);
                return;
            }

            serializer.Serialize(writer, point.GeoHash);
        }

        public static CoordinatePoint DeserializeCoordinatePoint(string value)
        {
            Dictionary<string, object> pointDict = null;
            try
            {
                pointDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(value);
            }
            catch { }

            if (pointDict != null)
                return GetPointFromDictionary(pointDict);

            IEnumerable<Double> pointList = null;
            try
            {
                pointList = JsonConvert.DeserializeObject<IEnumerable<Double>>(value);
            }
            catch { }

            if (pointList != null)
                return GetPointFromList(pointList);

            // only the strings are left.
            if (value.Contains(","))
                return GetPointFromString(value);
            else
                return new CoordinatePoint(value);
        }
        public static IEnumerable<CoordinatePoint> DeserializeCollectionCoordinatePiont(string value)
        {
            List<CoordinatePoint> points = new List<CoordinatePoint>();
            List<object> pointsJsonList = JsonConvert.DeserializeObject<List<object>>(value);
            foreach (object pointJson in pointsJsonList)
            {
                points.Add(DeserializeCoordinatePoint(pointJson.ToString()));
            }

            return points;
        }

        private static CoordinatePoint GetPointFromString(string point)
        {
            string[] pointValues = point.Split(_DELIMETER.ToArray(), StringSplitOptions.RemoveEmptyEntries);
            return new CoordinatePoint(Double.Parse(pointValues[0]), Double.Parse(pointValues[1]));
        }

        private static CoordinatePoint GetPointFromList(IEnumerable<Double> point)
        {
            if (point.Count() != 2)
                throw new Exception("Coordinate point lists require two items in the list.");
            return new CoordinatePoint(point.First(), point.Last());
        }

        private static CoordinatePoint GetPointFromDictionary(Dictionary<string, object> pointDict)
        {
            return new CoordinatePoint(pointDict.GetDouble(_LATITUDE), pointDict.GetDouble(_LONGITUDE));
        }
    }
}
