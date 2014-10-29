using Bolay.Elastic.Exceptions;
using Bolay.Elastic.Scripts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.Histogram
{
    public class HistogramSerializer : JsonConverter
    {
        private const string _FIELD = "field";
        private const string _INTERVAL = "interval";
        private const string _ORDER = "order";
        private const string _MINIMUM_DOCUMENT_COUNT = "min_doc_count";
        private const string _KEYED = "keyed";

        internal const int _MINIMUM_DOCUMENT_COUNT_DEFAULT = 1;
        internal const bool _KEYED_DEFAULT = false;

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> wholeDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> aggDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(wholeDict.First().Value.ToString());
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(aggDict.GetString(AggregationTypeEnum.Histogram.ToString()));

            string aggName = wholeDict.First().Key;
            string field = fieldDict.GetStringOrDefault(_FIELD);
            Script script = ScriptSerializer.Deserialize(fieldDict);
            Double interval = fieldDict.GetDouble(_INTERVAL);

            HistogramAggregate agg = new HistogramAggregate(
                wholeDict.First().Key,
                fieldDict.GetStringOrDefault(_FIELD),
                fieldDict.GetDouble(_INTERVAL));

            if (!string.IsNullOrWhiteSpace(field) && script != null)
                agg = new HistogramAggregate(aggName, field, script, interval);
            else if (!string.IsNullOrWhiteSpace(field))
                agg = new HistogramAggregate(aggName, field, interval);
            else if (script != null)
                agg = new HistogramAggregate(aggName, script, interval);
            else
                throw new RequiredPropertyMissingException(_FIELD + "/" + ScriptSerializer._SCRIPT); 

            agg.MinimumDocumentCount = fieldDict.GetInt32(_MINIMUM_DOCUMENT_COUNT, _MINIMUM_DOCUMENT_COUNT_DEFAULT);
            if (fieldDict.ContainsKey(_ORDER))
            {
                Dictionary<string, object> orderDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.GetString(_ORDER));
                if (orderDict.Count != 1)
                    throw new Exception("The order parameter must be a dictionary of one key value pair.");

                agg.SortValue = orderDict.First().Key;
                agg.SortOrder = SortOrderEnum.Find(orderDict.First().Value.ToString());
            }
            agg.AreBucketsKeyed = fieldDict.GetBool(_KEYED, _KEYED_DEFAULT);
            agg.SubAggregations = BucketAggregationBase.DeserializeSubAggregations(aggDict);
            return agg;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is HistogramAggregate))
                throw new SerializeTypeException<HistogramAggregate>();

            HistogramAggregate agg = value as HistogramAggregate;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.AddObject(_FIELD, agg.Field);
            ScriptSerializer.Serialize(agg.Script, fieldDict);
            fieldDict.AddObject(_INTERVAL, agg.Interval);
            fieldDict.AddObject(_MINIMUM_DOCUMENT_COUNT, agg.MinimumDocumentCount, _MINIMUM_DOCUMENT_COUNT_DEFAULT);
            fieldDict.AddObject(_KEYED, agg.AreBucketsKeyed, _KEYED_DEFAULT);

            if (!string.IsNullOrWhiteSpace(agg.SortValue))
            {
                Dictionary<string, object> orderDict = new Dictionary<string, object>();
                orderDict.Add(agg.SortValue, agg.SortOrder.ToString());
                fieldDict.Add(_ORDER, orderDict);
            }

            Dictionary<string, object> aggDict = new Dictionary<string, object>();
            aggDict.Add(AggregationTypeEnum.Histogram.ToString(), fieldDict);

            Dictionary<string, object> subAggsDict = agg.SerializeSubAggregations();
            if (subAggsDict != null)
            {
                aggDict.Add(BucketAggregationBase._SUB_AGGREGATIONS, subAggsDict);
            }

            Dictionary<string, object> aggNameDict = new Dictionary<string, object>();
            aggNameDict.Add(agg.Name, aggDict);

            serializer.Serialize(writer, aggNameDict);
        }
    }
}
