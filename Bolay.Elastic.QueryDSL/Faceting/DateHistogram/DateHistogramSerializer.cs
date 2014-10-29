using Bolay.Elastic.Exceptions;
using Bolay.Elastic.Scripts;
using Bolay.Elastic.Time;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting.DateHistogram
{
    public class DateHistogramSerializer : JsonConverter
    {
        private const string _TIMESPAN_FORMAT = "hh\\:mm";
        private const string _FIELD = "field";
        private const string _KEY_FIELD = "key_field";
        private const string _VALUE_FIELD = "value_field";
        private const string _VALUE_SCRIPT = "value_script";
        private const string _FACTOR = "factor";
        private const string _PRE_ZONE = "pre_zone";
        private const string _POST_ZONE = "post_zone";
        private const string _POST_OFFSET = "post_offset";
        private const string _PRE_OFFSET = "pre_offset";
        private const string _TIME_ZONE = "time_zone";
        private const string _PRE_ZONE_ADJUST_LARGE_INTERVAL = "pre_zone_adjust_large_interval";
        private const string _INTERVAL = "interval";
        private const string _PARAMETERS = "params";
        private const string _LANGUAGE = "lang";

        internal const bool _PRE_ZONE_ADJUST_LARGE_INTERVAL_DEFAULT = false;
        internal static TimeSpan _TIMESPAN_DEFAULT = new TimeSpan(0, 0, 0);

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> wholeDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> facetDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(wholeDict.First().Value.ToString());
            Dictionary<string, object> histoDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(facetDict.GetString(FacetTypeEnum.DateHistogram.ToString()));

            DateHistogramFacet facet = null;

            string facetName = wholeDict.First().Key;
            string field = histoDict.GetStringOrDefault(_FIELD);

            DateIntervalEnum intervalType = DateIntervalEnum.Day;
            string intervalValue = histoDict.GetString(_INTERVAL);
            intervalType = DateIntervalEnum.Find(intervalValue);
            if (intervalType != null)
            {
                if (string.IsNullOrWhiteSpace(field))
                    facet = new DateHistogramFacet(facetName, intervalType);
                else
                    facet = new DateHistogramFacet(facetName, field, intervalType);
            }
            else
            {
                TimeValue timeValue = new TimeValue(intervalValue);
                if (string.IsNullOrWhiteSpace(field))
                    facet = new DateHistogramFacet(facetName, timeValue);
                else
                    facet = new DateHistogramFacet(facetName, field, timeValue);
            }

            FacetSerializer.DeserializeFacetInfo(facet, histoDict);
            facet.KeyField = histoDict.GetStringOrDefault(_KEY_FIELD);
            if (histoDict.ContainsKey(_POST_OFFSET))
                facet.PostOffset = JsonConvert.DeserializeObject<TimeValue>(histoDict.GetString(_POST_OFFSET));
            if(histoDict.ContainsKey(_PRE_OFFSET))
                facet.PreOffset = JsonConvert.DeserializeObject<TimeValue>(histoDict.GetString(_PRE_OFFSET));
            facet.PostZone = histoDict.GetTimeSpan(_POST_ZONE, _TIMESPAN_DEFAULT);
            facet.PreZone = histoDict.GetTimeSpan(_PRE_ZONE, _TIMESPAN_DEFAULT);
            facet.PreZoneAdjustLargeInterval = histoDict.GetBool(_PRE_ZONE_ADJUST_LARGE_INTERVAL, _PRE_ZONE_ADJUST_LARGE_INTERVAL_DEFAULT);
            if (histoDict.ContainsKey(_PARAMETERS))
                facet.ScriptParameters = JsonConvert.DeserializeObject<ScriptParameterCollection>(histoDict.GetString(_PARAMETERS));
            facet.TimeZone = histoDict.GetTimeSpan(_TIME_ZONE, _TIMESPAN_DEFAULT);
            facet.ValueField = histoDict.GetStringOrDefault(_VALUE_FIELD);
            facet.ValueScript = histoDict.GetStringOrDefault(_VALUE_SCRIPT);
            facet.ScriptLanguage = histoDict.GetStringOrDefault(_LANGUAGE);

            return facet;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is DateHistogramFacet))
                throw new SerializeTypeException<DateHistogramFacet>();

            DateHistogramFacet facet = value as DateHistogramFacet;

            Dictionary<string, object> histoDict = new Dictionary<string, object>();
            histoDict.AddObject(_FIELD, facet.Field);
            histoDict.AddObject(_FACTOR, facet.Factor);
            if (facet.ConstantInterval != null)
                histoDict.AddObject(_INTERVAL, facet.ConstantInterval.ToString());
            else
                histoDict.AddObject(_INTERVAL, facet.TimeInterval);
            histoDict.AddObject(_KEY_FIELD, facet.KeyField);
            if (facet.ScriptParameters != null && facet.ScriptParameters.Any(x => x != null))
                histoDict.AddObject(_PARAMETERS, new ScriptParameterCollection(facet.ScriptParameters));
            histoDict.AddObject(_POST_OFFSET, facet.PostOffset);
            histoDict.AddObject(_POST_ZONE, facet.PostZone.ToString(_TIMESPAN_FORMAT), _TIMESPAN_DEFAULT.ToString(_TIMESPAN_FORMAT));
            histoDict.AddObject(_PRE_OFFSET, facet.PreOffset);
            histoDict.AddObject(_PRE_ZONE, facet.PreZone.ToString(_TIMESPAN_FORMAT), _TIMESPAN_DEFAULT.ToString(_TIMESPAN_FORMAT));
            histoDict.AddObject(_PRE_ZONE_ADJUST_LARGE_INTERVAL, facet.PreZoneAdjustLargeInterval, _PRE_ZONE_ADJUST_LARGE_INTERVAL_DEFAULT);
            histoDict.AddObject(_TIME_ZONE, facet.TimeZone.ToString(_TIMESPAN_FORMAT), _TIMESPAN_DEFAULT.ToString(_TIMESPAN_FORMAT));
            histoDict.AddObject(_VALUE_FIELD, facet.ValueField);
            histoDict.AddObject(_VALUE_SCRIPT, facet.ValueScript);
            histoDict.AddObject(_LANGUAGE, facet.ScriptLanguage);

            FacetSerializer.SerializeFacetInfo(facet, histoDict);

            Dictionary<string, object> facetNameDict = new Dictionary<string, object>();
            facetNameDict.Add(FacetTypeEnum.DateHistogram.ToString(), histoDict);

            Dictionary<string, object> facetDict = new Dictionary<string, object>();
            facetDict.Add(facet.FacetName, facetNameDict);

            serializer.Serialize(writer, facetDict);
        }
    }
}
