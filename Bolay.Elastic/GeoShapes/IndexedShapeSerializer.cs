using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.GeoShapes
{
    public class IndexedShapeSerializer : JsonConverter
    {
        private const string _INDEX = "index";
        private const string _TYPE = "type";
        private const string _ID = "id";
        private const string _PATH = "path";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(GeoShapeTypeEnum.IndexedShape.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            return new IndexedShape(fieldDict[_INDEX].ToString(),
                                    fieldDict[_TYPE].ToString(),
                                    fieldDict[_ID].ToString(),
                                    fieldDict[_PATH].ToString());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is IndexedShape))
                throw new SerializeTypeException<IndexedShape>();

            IndexedShape shape = value as IndexedShape;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.Add(_ID, shape.DocumentId);
            fieldDict.Add(_INDEX, shape.Index);
            fieldDict.Add(_TYPE, shape.DocumentType);
            fieldDict.Add(_PATH, shape.PropertyPath);

            serializer.Serialize(writer, fieldDict);
        }
    }
}
