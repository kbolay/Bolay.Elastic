using Bolay.Elastic.Exceptions;
using Bolay.Elastic.GeoShapes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.GeoShape
{
    public class GeoShapeSerializer : JsonConverter
    {
        private const string _GEO_SHAPE = "geo_shape";
        private const string _SHAPE = "shape";
        private const string _INDEXED_SHAPE = "indexed_shape";

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
            
            GeoShapeQuery query = new GeoShapeQuery(fieldName, shape); 
            query.QueryName = shapeDict.GetStringOrDefault(QuerySerializer._QUERY_NAME);
            return query;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is GeoShapeQuery))
                throw new SerializeTypeException<GeoShapeQuery>();
            GeoShapeQuery query = value as GeoShapeQuery;

            Dictionary<string, object> shapeDict = new Dictionary<string, object>();
            if (query.GeoShape.Type != GeoShapeTypeEnum.IndexedShape)
                shapeDict.Add(_SHAPE, query.GeoShape);
            else
                shapeDict.Add(_INDEXED_SHAPE, query.GeoShape);

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.Add(query.Field, shapeDict);
            fieldDict.AddObject(QuerySerializer._QUERY_NAME, query.QueryName);

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add(_GEO_SHAPE, fieldDict);

            serializer.Serialize(writer, queryDict);
        }
    }
}
