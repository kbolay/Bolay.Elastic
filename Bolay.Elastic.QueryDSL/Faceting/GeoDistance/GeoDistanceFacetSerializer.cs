using Bolay.Elastic.Coordinates;
using Bolay.Elastic.Exceptions;
using Bolay.Elastic.Scripts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting.GeoDistance
{
    public class GeoDistanceFacetSerializer : JsonConverter
    {
        private const string _RANGES = "ranges";
        private const string _DISTANCE_UNIT = "unit";
        private const string _DISTANCE_COMPUTE_TYPE = "distance_type";
        private const string _VALUE_FIELD = "value_field";
        private const string _VALUE_SCRIPT = "value_script";
        private const string _PARAMETERS = "params";
        private const string _GREATER_THAN = "gt";
        private const string _LESS_THAN = "lt";
        private const string _GREATER_THAN_OR_EQUAL = "gte";
        private const string _LESS_THAN_OR_EQUAL = "lte";
        private const string _LANGUAGE = "lang";

        internal static ExtendedDistanceUnitEnum _DISTANCE_UNIT_DEFAULT = ExtendedDistanceUnitEnum.KilometersAbbr;
        internal static DistanceComputeTypeEnum _DISTANCE_COMPUTE_TYPE_DEFAULT = DistanceComputeTypeEnum.SloppyArc;

        private static List<string> _KnownFields = new List<string>()
        {
            _RANGES,
            _DISTANCE_UNIT,
            _DISTANCE_COMPUTE_TYPE,
            _VALUE_FIELD,
            _VALUE_SCRIPT,
            _PARAMETERS
        };

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> wholeDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> facetDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(wholeDict.First().Value.ToString());
            Dictionary<string, object> geoDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(facetDict.GetString(FacetTypeEnum.GeoDistance.ToString()));

            List<DistanceBucket> rangeBuckets = new List<DistanceBucket>();
            string rangeBucketsJson = null;
            if (geoDict.ContainsKey(_RANGES))
            {
                rangeBucketsJson = geoDict.GetString(_RANGES);
            }
            else
            {
                throw new RequiredPropertyMissingException(_RANGES);
            }

            foreach (Dictionary<string, object> bucketDict in JsonConvert.DeserializeObject<IEnumerable<Dictionary<string, object>>>(rangeBucketsJson))
            {
                DistanceBucket bucket = new DistanceBucket();
                if (bucketDict.ContainsKey(_GREATER_THAN))
                    bucket.GreaterThan = bucketDict.GetDouble(_GREATER_THAN);
                if (bucketDict.ContainsKey(_LESS_THAN))
                    bucket.LessThan = bucketDict.GetDouble(_LESS_THAN);
                if (bucketDict.ContainsKey(_GREATER_THAN_OR_EQUAL))
                    bucket.GreaterThanOrEqualTo = bucketDict.GetDouble(_GREATER_THAN_OR_EQUAL);
                if (bucketDict.ContainsKey(_LESS_THAN_OR_EQUAL))
                    bucket.LessThanOrEqualTo = bucketDict.GetDouble(_LESS_THAN_OR_EQUAL);

                rangeBuckets.Add(bucket);
            }

            KeyValuePair<string, object> fieldKvp = geoDict.FirstOrDefault(x => !_KnownFields.Contains(x.Key, StringComparer.OrdinalIgnoreCase));
            if (string.IsNullOrWhiteSpace(fieldKvp.Key))
                throw new RequiredPropertyMissingException("field");

            GeoDistanceFacet facet = new GeoDistanceFacet(
                wholeDict.First().Key,
                fieldKvp.Key,
                CoordinatePointSerializer.DeserializeCoordinatePoint(fieldKvp.Value.ToString()),
                rangeBuckets);

            facet.ValueField = geoDict.GetStringOrDefault(_VALUE_FIELD);
            facet.ValueScript = geoDict.GetStringOrDefault(_VALUE_SCRIPT);
            if (geoDict.ContainsKey(_PARAMETERS))
            {
                facet.ScriptParameters = JsonConvert.DeserializeObject<ScriptParameterCollection>(geoDict.GetString(_PARAMETERS));
            }
            facet.ScriptLanguage = geoDict.GetStringOrDefault(_LANGUAGE);

            return facet;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is GeoDistanceFacet))
                throw new SerializeTypeException<GeoDistanceFacet>();

            GeoDistanceFacet facet = value as GeoDistanceFacet;

            Dictionary<string, object> distDict = new Dictionary<string, object>();
            distDict.Add(facet.Field, facet.CenterPoint);
            distDict.AddObject(_VALUE_FIELD, facet.ValueField);
            distDict.AddObject(_VALUE_SCRIPT, facet.ValueScript);
            if (facet.ScriptParameters != null && facet.ScriptParameters.Any())
                distDict.AddObject(_PARAMETERS, new ScriptParameterCollection(facet.ScriptParameters));
            distDict.AddObject(_LANGUAGE, facet.ScriptLanguage);

            List<DistanceBucket> buckets = new List<DistanceBucket>();
            List<Dictionary<string, object>> bucketDictList = new List<Dictionary<string, object>>();
            foreach (DistanceBucket bucket in facet.RangeBuckets)
            {
                Dictionary<string, object> bucketDict = new Dictionary<string, object>();
                bucketDict.AddObject(_GREATER_THAN, bucket.GreaterThan);
                bucketDict.AddObject(_GREATER_THAN_OR_EQUAL, bucket.GreaterThanOrEqualTo);
                bucketDict.AddObject(_LESS_THAN, bucket.LessThan);
                bucketDict.AddObject(_LESS_THAN_OR_EQUAL, bucket.LessThanOrEqualTo);

                if (bucketDict.Any())
                    bucketDictList.Add(bucketDict);
            }

            distDict.Add(_RANGES, bucketDictList);

            Dictionary<string, object> wrapDict = new Dictionary<string, object>();
            wrapDict.Add(FacetTypeEnum.GeoDistance.ToString(), distDict);

            Dictionary<string, object> facetDict = new Dictionary<string, object>();
            facetDict.Add(facet.FacetName, wrapDict);

            serializer.Serialize(writer, facetDict);
        }
    }
}
