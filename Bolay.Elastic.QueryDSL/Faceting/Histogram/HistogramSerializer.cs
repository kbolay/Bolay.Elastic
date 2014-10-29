using Bolay.Elastic.Exceptions;
using Bolay.Elastic.Scripts;
using Bolay.Elastic.Time;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting.Histogram
{
    public class HistogramSerializer : JsonConverter
    {
        private const string _FIELD = "field";
        private const string _KEY_FIELD = "key_field";
        private const string _VALUE_FIELD = "value_field";
        private const string _KEY_SCRIPT = "key_script";
        private const string _VALUE_SCRIPT = "value_script";
        private const string _INTERVAL = "interval";
        private const string _TIME_INTERVAL = "time_interval";
        private const string _PARAMETERS = "params";
        private const string _LANGUAGE = "lang";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> wholeDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> facetDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(wholeDict.First().Value.ToString());
            Dictionary<string, object> histoDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(facetDict.GetString(FacetTypeEnum.Histogram.ToString()));

            HistogramFacet facet = null;

            string facetName = wholeDict.First().Key;
            string field = histoDict.GetStringOrDefault(_FIELD);
            if (histoDict.ContainsKey(_INTERVAL))
            {
                Int64 interval = histoDict.GetInt64(_INTERVAL);
                if (string.IsNullOrWhiteSpace(field))
                    facet = new HistogramFacet(facetName, interval);
                else
                    facet = new HistogramFacet(facetName, field, interval);
            }
            else if (histoDict.ContainsKey(_TIME_INTERVAL))
            {
                TimeValue timeInterval = new TimeValue(histoDict.GetString(_TIME_INTERVAL));
                if (string.IsNullOrWhiteSpace(field))
                    facet = new HistogramFacet(facetName, timeInterval);
                else
                    facet = new HistogramFacet(facetName, field, timeInterval);
            }

            facet.KeyField = histoDict.GetStringOrDefault(_KEY_FIELD);
            facet.ValueField = histoDict.GetStringOrDefault(_VALUE_FIELD);
            facet.KeyScript = histoDict.GetStringOrDefault(_KEY_SCRIPT);
            facet.ValueScript = histoDict.GetStringOrDefault(_VALUE_SCRIPT);
            facet.ScriptLanguage = histoDict.GetStringOrDefault(_LANGUAGE);
            if (histoDict.ContainsKey(_PARAMETERS))
            {
                facet.ScriptParameters = JsonConvert.DeserializeObject<ScriptParameterCollection>(histoDict.GetString(_PARAMETERS));
            }

            FacetSerializer.DeserializeFacetInfo(facet, histoDict);

            return facet;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is HistogramFacet))
                throw new SerializeTypeException<HistogramFacet>();

            HistogramFacet facet = value as HistogramFacet;

            Dictionary<string, object> histoDict = new Dictionary<string, object>();
            histoDict.AddObject(_FIELD, facet.Field);
            histoDict.AddObject(_KEY_FIELD, facet.KeyField);
            histoDict.AddObject(_VALUE_FIELD, facet.ValueField);
            histoDict.AddObject(_KEY_SCRIPT, facet.KeyScript);
            histoDict.AddObject(_VALUE_SCRIPT, facet.ValueScript);
            histoDict.AddObject(_INTERVAL, facet.Interval);
            histoDict.AddObject(_TIME_INTERVAL, facet.TimeInterval);

            FacetSerializer.SerializeFacetInfo(facet, histoDict);

            if(facet.ScriptParameters != null && facet.ScriptParameters.Any(x => x != null))
            {
                histoDict.Add(_PARAMETERS, new ScriptParameterCollection(facet.ScriptParameters));
            }
            histoDict.AddObject(_LANGUAGE, facet.ScriptLanguage);

            Dictionary<string, object> facetDict = new Dictionary<string, object>();
            facetDict.Add(FacetTypeEnum.Histogram.ToString(), histoDict);

            Dictionary<string, object> nameDict = new Dictionary<string, object>();
            nameDict.Add(facet.FacetName, facetDict);

            serializer.Serialize(writer, nameDict);
        }
    }
}
