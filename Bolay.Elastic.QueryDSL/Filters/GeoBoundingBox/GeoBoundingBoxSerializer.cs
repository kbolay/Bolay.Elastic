using Bolay.Elastic.Coordinates;
using Bolay.Elastic.Exceptions;
using Bolay.Elastic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.GeoBoundingBox
{
    public class GeoBoundingBoxSerializer : JsonConverter
    {
        private const string _DELIMETER = ", ";
        private static List<string> _DELIMETERS = new List<string>() { " , ", " ,", ", ", "," };

        private const string _TYPE = "type";
        private const string _TOP_LEFT = "top_left";
        private const string _BOTTOM_RIGHT = "bottom_right";
        private const string _TOP_RIGHT = "top_right";
        private const string _BOTTOM_LEFT = "bottom_left";
        private const string _TOP = "top";
        private const string _BOTTOM = "bottom";
        private const string _LEFT = "left";
        private const string _RIGHT = "right";
        private const string _LATITUDE = "lat";
        private const string _LONGITUDE = "lon";

        internal const bool _CACHED_DEFAULT = false;
        internal static ExecutionTypeEnum _TYPE_DEFAULT = ExecutionTypeEnum.Memory;

        private List<string> _NonFieldKeys = new List<string>() { _TYPE, FilterSerializer._CACHE, FilterSerializer._CACHE_KEY };

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(FilterTypeEnum.GeoBoundingBox.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            KeyValuePair<string, object> fieldKvp = fieldDict.FirstOrDefault(x => !_NonFieldKeys.Contains(x.Key));
            string fieldName = fieldKvp.Key;

            GeoBoundingBoxFilter filter = RetrieveBox(fieldName, JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldKvp.Value.ToString()));

            filter.Type = ExecutionTypeEnum.Find(fieldDict.GetString(_TYPE, _TYPE_DEFAULT.ToString()));
            FilterSerializer.DeserializeBaseValues(filter, _CACHED_DEFAULT, fieldDict);

            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is GeoBoundingBoxFilter))
                throw new SerializeTypeException<GeoBoundingBoxFilter>();

            GeoBoundingBoxFilter filter = value as GeoBoundingBoxFilter;
            Dictionary<string, object> pointDict = new Dictionary<string, object>();
            pointDict.Add(_TOP_LEFT, filter.TopLeft);
            pointDict.Add(_BOTTOM_RIGHT, filter.BottomRight);

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.Add(filter.Field, pointDict);
            fieldDict.AddObject(_TYPE, filter.Type.ToString(), _TYPE_DEFAULT.ToString());

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add(FilterTypeEnum.GeoBoundingBox.ToString(), fieldDict);

            serializer.Serialize(writer, queryDict);
        }

        private GeoBoundingBoxFilter RetrieveBox(string fieldName, Dictionary<string, object> boxDict)
        {
            // maybe are are dealing with the vertices
            if (boxDict.Count() == 4)
                return GetBoxFromVertices(fieldName, boxDict);

            CoordinatePoint topLeft;
            CoordinatePoint bottomRight;
            if (boxDict.ContainsKey(_TOP_LEFT))
            {
                topLeft = CoordinatePointSerializer.DeserializeCoordinatePoint(boxDict.GetString(_TOP_LEFT));
                bottomRight = CoordinatePointSerializer.DeserializeCoordinatePoint(boxDict.GetString(_BOTTOM_RIGHT));
            }
            else if (boxDict.ContainsKey(_TOP_RIGHT))
            {
                CoordinatePoint topRight = CoordinatePointSerializer.DeserializeCoordinatePoint(boxDict.GetString(_TOP_RIGHT));
                CoordinatePoint bottomLeft = CoordinatePointSerializer.DeserializeCoordinatePoint(boxDict.GetString(_BOTTOM_LEFT));

                topLeft = new CoordinatePoint(topRight.Latitude, bottomLeft.Longitude);
                bottomRight = new CoordinatePoint(bottomLeft.Latitude, topRight.Longitude);
            }
            else
            {
                throw new Exception("No bounding box formed by current properties.");
            }

            return new GeoBoundingBoxFilter(fieldName, topLeft, bottomRight);
        }

        // "prop1":{ "top": lat, "left":lon, "bottom": lat, "right": lon } - vertices
        private GeoBoundingBoxFilter GetBoxFromVertices(string fieldName, Dictionary<string, object> boxDict)
        {
            CoordinatePoint topLeft = new CoordinatePoint(boxDict.GetDouble(_TOP), boxDict.GetDouble(_LEFT));
            CoordinatePoint bottomRight = new CoordinatePoint(boxDict.GetDouble(_BOTTOM), boxDict.GetDouble(_RIGHT));

            return new GeoBoundingBoxFilter(fieldName, topLeft, bottomRight);
        }        
    }
}
