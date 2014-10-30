using Bolay.Elastic.Exceptions;
using Bolay.Elastic.Scripts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting.TermsStatistics
{
    public class TermsStatisticsSerializer : JsonConverter
    {
        private const string _KEY_FIELD = "key_field";
        private const string _VALUE_FIELD = "value_field";
        private const string _VALUE_SCRIPT = "value_script";
        private const string _SIZE = "size";
        private const string _SHARD_SIZE = "shard_size";
        private const string _ORDER = "order";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> wholeDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> facetDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(wholeDict.First().Value.ToString());
            Dictionary<string, object> termDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(facetDict.GetString(FacetTypeEnum.TermsStatistics.ToString()));

            string facetName = wholeDict.First().Key;
            string keyField = termDict.GetString(_KEY_FIELD);
            string valueField = termDict.GetStringOrDefault(_VALUE_FIELD);

            TermsStatisticsFacet facet = null;
            if (!string.IsNullOrWhiteSpace(valueField))
            {
                facet = new TermsStatisticsFacet(facetName, keyField, valueField);
            }
            else
            {
                facet = new TermsStatisticsFacet(facetName, keyField, termDict.DeserializeObject<Script>(new Dictionary<string,string>(){ {_VALUE_SCRIPT, Script.SCRIPT} }));
            }

            FacetSerializer.DeserializeFacetInfo(facet, termDict);
            facet.ShardSize = termDict.GetInt32(_SHARD_SIZE, facet.Size);
            OrderOptionEnum order = OrderOptionEnum.Count;
            facet.Order = OrderOptionEnum.Find(termDict.GetString(_ORDER, TermsStatisticsFacet._ORDER_DEFAULT.ToString()));

            return facet;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if(!(value is TermsStatisticsFacet))
                throw new SerializeTypeException<TermsStatisticsFacet>();

            TermsStatisticsFacet facet = value as TermsStatisticsFacet;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.Add(_KEY_FIELD, facet.KeyField);
            fieldDict.AddObject(_VALUE_FIELD, facet.ValueField);
            facet.ValueScript.Serialize(fieldDict, _VALUE_SCRIPT);
            fieldDict.AddObject(_SIZE, facet.Size, TermsStatisticsFacet._SIZE_DEFAULT);
            fieldDict.AddObject(_SHARD_SIZE, facet.ShardSize, facet.Size);
            fieldDict.AddObject(_ORDER, facet.Order.ToString(), TermsStatisticsFacet._ORDER_DEFAULT.ToString());

            Dictionary<string, object> termStats = new Dictionary<string, object>();
            termStats.Add(FacetTypeEnum.TermsStatistics.ToString(), fieldDict);

            Dictionary<string, object> facetDict = new Dictionary<string, object>();
            facetDict.Add(facet.FacetName, termStats);

            serializer.Serialize(writer, facetDict);
        }
    }
}
