using Bolay.Elastic.Distance;
using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Properties.GeoShape
{
    internal class GeoShapePropertySerializer : JsonConverter
    {
        private const string _TREE = "tree";
        private const string _PRECISION = "precision";
        private const string _TREE_LEVELS = "tree_levels";
        private const string _DISTANCE_ERROR_PERCENTAGE = "distance_error_pct";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> propDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            GeoShapeProperty prop = new GeoShapeProperty(propDict.First().Key);

            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(propDict.First().Value.ToString());
            DocumentPropertyBase.Deserialize(prop, fieldDict);
            prop.DistanceErrorPercentage = fieldDict.GetDouble(_DISTANCE_ERROR_PERCENTAGE, GeoShapeProperty._DISTANCE_ERROR_PERCENTAGE_DEFAULT);
            if (fieldDict.ContainsKey(_PRECISION))
                prop.Precision = new DistanceValue(fieldDict.GetString(_PRECISION));
            prop.Tree = PrefixTreeEnum.Find(fieldDict.GetString(_TREE, GeoShapeProperty._TREE_DEFAULT.ToString()));
            prop.TreeLevels = fieldDict.GetInt64OrNull(_TREE_LEVELS);

            return prop;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is GeoShapeProperty))
                throw new SerializeTypeException<GeoShapeProperty>();

            GeoShapeProperty prop = value as GeoShapeProperty;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            DocumentPropertyBase.Serialize(prop, fieldDict);
            fieldDict.AddObject(_TREE, prop.Tree.ToString(), GeoShapeProperty._TREE_DEFAULT.ToString());
            if (prop.Precision != null)
                fieldDict.AddObject(_PRECISION, prop.Precision);
            fieldDict.AddObject(_TREE_LEVELS, prop.TreeLevels);
            fieldDict.AddObject(_DISTANCE_ERROR_PERCENTAGE, prop.DistanceErrorPercentage, GeoShapeProperty._DISTANCE_ERROR_PERCENTAGE_DEFAULT);

            Dictionary<string, object> propDict = new Dictionary<string, object>();
            propDict.Add(prop.Name, fieldDict);

            serializer.Serialize(writer, propDict);
        }
    }
}
