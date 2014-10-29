using Bolay.Elastic.Exceptions;
using Bolay.Elastic.GeoShapes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.GeoShape
{
    public class GeoShapeSerializer : JsonConverter
    {
        private const string _GEO_SHAPE = "geo_shape";
        private const string _SHAPE = "shape";
        private const string _INDEXED_SHAPE = "indexed_shape";

        internal const bool _CACHE_DEFAULT = false;

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> shapeDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (shapeDict.ContainsKey(_GEO_SHAPE))
                shapeDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(shapeDict.First().Value.ToString());

            string fieldName = shapeDict.First().Key;
            GeoShapeBase shape = JsonConvert.DeserializeObject<GeoShapeBase>(shapeDict.First().Value.ToString());

            GeoShapeFilter filter = new GeoShapeFilter(fieldName, shape); 
            FilterSerializer.DeserializeBaseValues(filter, _CACHE_DEFAULT, shapeDict);
            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is GeoShapeFilter))
                throw new SerializeTypeException<GeoShapeFilter>();
            GeoShapeFilter filter = value as GeoShapeFilter;

            Dictionary<string, object> shapeDict = new Dictionary<string, object>();
            if (filter.GeoShape.Type != GeoShapeTypeEnum.IndexedShape)
                shapeDict.Add(_SHAPE, filter.GeoShape);
            else
                shapeDict.Add(_INDEXED_SHAPE, filter.GeoShape);

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.Add(filter.Field, shapeDict);

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add(_GEO_SHAPE, fieldDict);

            FilterSerializer.SerializeBaseValues(filter, _CACHE_DEFAULT, fieldDict);

            serializer.Serialize(writer, queryDict);
        }
    }
}
