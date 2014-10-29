using Bolay.Elastic.Exceptions;
using Bolay.Elastic.QueryDSL.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting.Filter
{
    public class FilterFacetSerializer : JsonConverter
    {       
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> wholeDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> facetNameDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(wholeDict.First().Value.ToString());

            return new FilterFacet(wholeDict.First().Key, JsonConvert.DeserializeObject<IFilter>(facetNameDict.GetString(FacetTypeEnum.Filter.ToString())));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is FilterFacet))
                throw new SerializeTypeException<FilterFacet>();

            FilterFacet facet = value as FilterFacet;

            Dictionary<string, object> filterDict = new Dictionary<string, object>();
            filterDict.Add(FacetTypeEnum.Filter.ToString(), facet.Filter);

            Dictionary<string, object> facetDict = new Dictionary<string, object>();
            facetDict.Add(facet.FacetName, filterDict);

            serializer.Serialize(writer, facetDict);
        }
    }
}
