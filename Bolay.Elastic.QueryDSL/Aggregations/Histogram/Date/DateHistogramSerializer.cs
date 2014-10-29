using Bolay.Elastic.Exceptions;
using Bolay.Elastic.Scripts;
using Bolay.Elastic.Time;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.Histogram.Date
{
    public class DateHistogramSerializer : JsonConverter
    {
        private const string _TIMESPAN_FORMAT = "hh\\:mm";
        private const string _FIELD = "field";
        private const string _FACTOR = "factor";
        private const string _PRE_ZONE = "pre_zone";
        private const string _POST_ZONE = "post_zone";
        private const string _POST_OFFSET = "post_offset";
        private const string _PRE_OFFSET = "pre_offset";
        private const string _TIME_ZONE = "time_zone";
        private const string _PRE_ZONE_ADJUST_LARGE_INTERVAL = "pre_zone_adjust_large_interval";
        private const string _INTERVAL = "interval";
        private const string _ORDER = "order";
        private const string _MINIMUM_DOCUMENT_COUNT = "min_doc_count";

        internal const bool _PRE_ZONE_ADJUST_LARGE_INTERVAL_DEFAULT = false;
        internal static TimeSpan _TIMESPAN_DEFAULT = new TimeSpan(0, 0, 0);
        internal const int _MINIMUM_DOCUMENT_COUNT_DEFAULT = 1;

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> wholeDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> facetDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(wholeDict.First().Value.ToString());
            Dictionary<string, object> histoDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(facetDict.GetString(AggregationTypeEnum.DateHistogram.ToString()));

            DateHistogramAggregate agg = null;

            string aggName = wholeDict.First().Key;
            string field = histoDict.GetStringOrDefault(_FIELD);
            Script script = ScriptSerializer.Deserialize(histoDict);

            DateIntervalEnum intervalType = DateIntervalEnum.Day;
            string intervalValue = histoDict.GetString(_INTERVAL);
            intervalType = DateIntervalEnum.Find(intervalValue);
            if (intervalType != null)
            {
                if (!string.IsNullOrWhiteSpace(field) && script != null)
                    agg = new DateHistogramAggregate(aggName, field, script, intervalType);
                else if (!string.IsNullOrWhiteSpace(field))
                    agg = new DateHistogramAggregate(aggName, field, intervalType);
                else if(script != null)
                    agg = new DateHistogramAggregate(aggName, script, intervalType);
            }
            else
            {
                TimeValue timeValue = new TimeValue(intervalValue);
                if (!string.IsNullOrWhiteSpace(field) && script != null)
                    agg = new DateHistogramAggregate(aggName, field, script, timeValue);
                else if (!string.IsNullOrWhiteSpace(field))
                    agg = new DateHistogramAggregate(aggName, field, timeValue);
                else if(script != null)
                    agg = new DateHistogramAggregate(aggName, script, timeValue);
            }

            if (agg == null)
                throw new RequiredPropertyMissingException(_FIELD + "/" + ScriptSerializer._SCRIPT);

            if (histoDict.ContainsKey(_POST_OFFSET))
                agg.PostOffset = JsonConvert.DeserializeObject<TimeValue>(histoDict.GetString(_POST_OFFSET));
            if (histoDict.ContainsKey(_PRE_OFFSET))
                agg.PreOffset = JsonConvert.DeserializeObject<TimeValue>(histoDict.GetString(_PRE_OFFSET));
            agg.PostZone = histoDict.GetTimeSpan(_POST_ZONE, _TIMESPAN_DEFAULT);
            agg.PreZone = histoDict.GetTimeSpan(_PRE_ZONE, _TIMESPAN_DEFAULT);
            agg.PreZoneAdjustLargeInterval = histoDict.GetBool(_PRE_ZONE_ADJUST_LARGE_INTERVAL, _PRE_ZONE_ADJUST_LARGE_INTERVAL_DEFAULT);
            agg.TimeZone = histoDict.GetTimeSpan(_TIME_ZONE, _TIMESPAN_DEFAULT);
            agg.MinimumDocumentCount = histoDict.GetInt32(_MINIMUM_DOCUMENT_COUNT, _MINIMUM_DOCUMENT_COUNT_DEFAULT);
            if (histoDict.ContainsKey(_ORDER))
            {
                Dictionary<string, object> orderDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(histoDict.GetString(_ORDER));
                if (orderDict.Count != 1)
                    throw new Exception("The order parameter must be a dictionary of one key value pair.");

                agg.SortValue = orderDict.First().Key;
                agg.SortOrder = SortOrderEnum.Find(orderDict.First().Value.ToString());
            }

            return agg;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is DateHistogramAggregate))
                throw new SerializeTypeException<DateHistogramAggregate>();

            DateHistogramAggregate agg = value as DateHistogramAggregate;

            Dictionary<string, object> histoDict = new Dictionary<string, object>();
            histoDict.AddObject(_FIELD, agg.Field);
            ScriptSerializer.Serialize(agg.Script, histoDict);
            histoDict.AddObject(_FACTOR, agg.Factor);
            if (agg.ConstantInterval != null)
                histoDict.AddObject(_INTERVAL, agg.ConstantInterval.ToString());
            else
                histoDict.AddObject(_INTERVAL, agg.TimeInterval);
            
            histoDict.AddObject(_POST_OFFSET, agg.PostOffset);
            histoDict.AddObject(_POST_ZONE, agg.PostZone.ToString(_TIMESPAN_FORMAT), _TIMESPAN_DEFAULT.ToString(_TIMESPAN_FORMAT));
            histoDict.AddObject(_PRE_OFFSET, agg.PreOffset);
            histoDict.AddObject(_PRE_ZONE, agg.PreZone.ToString(_TIMESPAN_FORMAT), _TIMESPAN_DEFAULT.ToString(_TIMESPAN_FORMAT));
            histoDict.AddObject(_PRE_ZONE_ADJUST_LARGE_INTERVAL, agg.PreZoneAdjustLargeInterval, _PRE_ZONE_ADJUST_LARGE_INTERVAL_DEFAULT);
            histoDict.AddObject(_TIME_ZONE, agg.TimeZone.ToString(_TIMESPAN_FORMAT), _TIMESPAN_DEFAULT.ToString(_TIMESPAN_FORMAT));
            histoDict.AddObject(_MINIMUM_DOCUMENT_COUNT, agg.MinimumDocumentCount, _MINIMUM_DOCUMENT_COUNT_DEFAULT);
            if (!string.IsNullOrWhiteSpace(agg.SortValue))
            {
                Dictionary<string, object> orderDict = new Dictionary<string, object>();
                orderDict.Add(agg.SortValue, agg.SortOrder.ToString());
                histoDict.Add(_ORDER, orderDict);
            }

            Dictionary<string, object> aggNameDict = new Dictionary<string, object>();
            aggNameDict.Add(AggregationTypeEnum.DateHistogram.ToString(), histoDict);

            Dictionary<string, object> aggDict = new Dictionary<string, object>();
            aggDict.Add(agg.Name, aggNameDict);

            serializer.Serialize(writer, aggDict);
        }
    }
}
