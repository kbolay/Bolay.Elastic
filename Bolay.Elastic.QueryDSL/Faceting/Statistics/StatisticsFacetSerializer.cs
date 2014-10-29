using Bolay.Elastic.Exceptions;
using Bolay.Elastic.Scripts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting.Statistics
{
    public class StatisticsFacetSerializer : JsonConverter
    {
        private const string _FIELD = "field";
        private const string _SCRIPT = "script";
        private const string _PARAMETERS = "params";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> wholeDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> facetNameDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(wholeDict.First().Value.ToString());
            Dictionary<string, object> scriptDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(facetNameDict.GetString(FacetTypeEnum.Statistical.ToString()));

            StatisticsFacet facet = null;
            string facetName = wholeDict.First().Key;
            if (scriptDict.ContainsKey(_FIELD))
            {
                facet = new StatisticsFacet(facetName, scriptDict.GetString(_FIELD));
            }
            else if (scriptDict.ContainsKey(_SCRIPT))
            {
                facet = new StatisticsFacet(facetName, ScriptSerializer.Deserialize(scriptDict));
            }
            else
                throw new RequiredPropertyMissingException(_FIELD + "/" + _SCRIPT);

            return facet;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is StatisticsFacet))
                throw new SerializeTypeException<StatisticsFacet>();

            StatisticsFacet facet = value as StatisticsFacet;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.AddObject(_FIELD, facet.Field);
            ScriptSerializer.Serialize(facet.Script, fieldDict);            

            Dictionary<string, object> statDict = new Dictionary<string, object>();
            statDict.Add(FacetTypeEnum.Statistical.ToString(), fieldDict);

            Dictionary<string, object> facetDict = new Dictionary<string, object>();
            facetDict.Add(facet.FacetName, statDict);

            serializer.Serialize(writer, facetDict);
        }
    }
}
