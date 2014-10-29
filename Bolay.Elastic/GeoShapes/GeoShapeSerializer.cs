using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.GeoShapes
{
    public class GeoShapeSerializer : JsonConverter
    {
        internal const string _TYPE = "type";
        internal const string _COORDINATES = "coordinates";
        internal const int _LATITUDE_INDEX = 1;
        internal const int _LONGITUDE_INDEX = 0;

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (!fieldDict.ContainsKey(_TYPE))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            GeoShapeTypeEnum shapeType = GeoShapeTypeEnum.Point;
            shapeType = GeoShapeTypeEnum.Find(fieldDict.GetString(_TYPE));
            if (shapeType == null)
                throw new Exception("Failed to find the shape type to deserialize into.");

            GeoShapeBase shape = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(fieldDict), shapeType.ImplementingType) as GeoShapeBase;

            return shape;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
