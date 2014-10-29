using Bolay.Elastic.Exceptions;
using Bolay.Elastic.Scripts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting.Range
{
    public class RangeFacetSerializer : JsonConverter
    {
        private const string _FIELD = "field";
        private const string _KEY_FIELD = "key_field";
        private const string _VALUE_FIELD = "value_field";
        private const string _KEY_SCRIPT = "key_script";
        private const string _VALUE_SCRIPT = "value_script";
        private const string _RANGES = "ranges";
        private const string _GREATER_THAN = "gt";
        private const string _LESS_THAN = "lt";
        private const string _GREATER_THAN_OR_EQUAL = "gte";
        private const string _LESS_THAN_OR_EQUAL = "lte";
        private const string _PARAMETERS = "params";
        private const string _LANGUAGE = "lang";

        private static List<string> _KnownFields = new List<string>()
        {
            _FIELD,
            _KEY_FIELD,
            _VALUE_FIELD,
            _KEY_SCRIPT,
            _VALUE_SCRIPT,
            _RANGES
        };

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> wholeDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> facetDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(wholeDict.First().Value.ToString());
            Dictionary<string, object> rangeDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(facetDict.GetString(FacetTypeEnum.Range.ToString()));

            List<RangeBucket> rangeBuckets = new List<RangeBucket>();
            string rangeBucketsJson = null;
            if (rangeDict.ContainsKey(_RANGES))
            {
                rangeBucketsJson = rangeDict.GetString(_RANGES);
            }
            else
            {
                KeyValuePair<string, object> rangesKvp = rangeDict.FirstOrDefault(x => !_KnownFields.Contains(x.Key, StringComparer.OrdinalIgnoreCase));
                if(string.IsNullOrWhiteSpace(rangesKvp.Key))
                    throw new RequiredPropertyMissingException("field/ranges");
                rangeBucketsJson = rangesKvp.Value.ToString();
            }

            foreach (Dictionary<string, object> bucketDict in JsonConvert.DeserializeObject<IEnumerable<Dictionary<string, object>>>(rangeBucketsJson))
            {
                RangeBucket bucket = new RangeBucket();
                if (bucketDict.ContainsKey(_GREATER_THAN))
                    bucket.GreaterThan = bucketDict[_GREATER_THAN];
                if (bucketDict.ContainsKey(_LESS_THAN))
                    bucket.LessThan = bucketDict[_LESS_THAN];
                if (bucketDict.ContainsKey(_GREATER_THAN_OR_EQUAL))
                    bucket.GreaterThanOrEqualTo = bucketDict[_GREATER_THAN_OR_EQUAL];
                if (bucketDict.ContainsKey(_LESS_THAN_OR_EQUAL))
                    bucket.LessThanOrEqualTo = bucketDict[_LESS_THAN_OR_EQUAL];

                rangeBuckets.Add(bucket);
            }

            string facetName = wholeDict.First().Key;

            RangeFacet facet = null;

            if (rangeDict.ContainsKey(_FIELD))
                facet = new RangeFacet(facetName, rangeDict.GetString(_FIELD), rangeBuckets);
            else
            {
                facet = new RangeFacet(facetName, rangeBuckets);                
            }

            facet.KeyField = rangeDict.GetStringOrDefault(_KEY_FIELD);
            facet.ValueField = rangeDict.GetStringOrDefault(_VALUE_FIELD);
            facet.KeyScript = rangeDict.GetStringOrDefault(_KEY_SCRIPT);
            facet.ValueScript = rangeDict.GetStringOrDefault(_VALUE_SCRIPT);
            if (rangeDict.ContainsKey(_PARAMETERS))
                facet.ScriptParameters = JsonConvert.DeserializeObject<ScriptParameterCollection>(rangeDict.GetString(_PARAMETERS));
            facet.ScriptLanguage = rangeDict.GetStringOrDefault(_LANGUAGE);

            //FacetSerializer.DeserializeFacetInfo(facet, rangeDict);

            return facet;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is RangeFacet))
                throw new SerializeTypeException<RangeFacet>();

            RangeFacet facet = value as RangeFacet;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.AddObject(_FIELD, facet.Field);
            fieldDict.AddObject(_KEY_FIELD, facet.KeyField);
            fieldDict.AddObject(_VALUE_FIELD, facet.ValueField);
            fieldDict.AddObject(_KEY_SCRIPT, facet.KeyScript);
            fieldDict.AddObject(_VALUE_SCRIPT, facet.ValueScript);

            if (facet.ScriptParameters != null && facet.ScriptParameters.Any(x => x != null))
                fieldDict.Add(_PARAMETERS, new ScriptParameterCollection(facet.ScriptParameters));
            fieldDict.AddObject(_LANGUAGE, facet.ScriptLanguage);

            List<Dictionary<string, object>> rangesList = new List<Dictionary<string,object>>();
            foreach (RangeBucket bucket in facet.RangeBuckets)
            {
                Dictionary<string, object> rangeDict = new Dictionary<string, object>();
                rangeDict.AddObject(_GREATER_THAN, bucket.GreaterThan);
                rangeDict.AddObject(_GREATER_THAN_OR_EQUAL, bucket.GreaterThanOrEqualTo);
                rangeDict.AddObject(_LESS_THAN, bucket.LessThan);
                rangeDict.AddObject(_LESS_THAN_OR_EQUAL, bucket.LessThanOrEqualTo);

                if (rangeDict.Any())
                    rangesList.Add(rangeDict);
            }

            fieldDict.AddObject(_RANGES, rangesList);
            //FacetSerializer.SerializeFacetInfo(facet, fieldDict);

            Dictionary<string, object> rangeFacetDict = new Dictionary<string, object>();
            rangeFacetDict.Add(FacetTypeEnum.Range.ToString(), fieldDict);

            Dictionary<string, object> facetDict = new Dictionary<string, object>();
            facetDict.Add(facet.FacetName, rangeFacetDict);

            serializer.Serialize(writer, facetDict);
        }
    }
}
