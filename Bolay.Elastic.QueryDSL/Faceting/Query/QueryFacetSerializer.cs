using Bolay.Elastic.Exceptions;
using Bolay.Elastic.QueryDSL.Queries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting.Query
{
    public class QueryFacetSerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> wholeDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> facetNameDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(wholeDict.First().Value.ToString());

            return new QueryFacet(wholeDict.First().Key, JsonConvert.DeserializeObject<IQuery>(facetNameDict.GetString(FacetTypeEnum.Query.ToString())));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is QueryFacet))
                throw new SerializeTypeException<QueryFacet>();

            QueryFacet facet = value as QueryFacet;

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add(FacetTypeEnum.Query.ToString(), facet.Query);

            Dictionary<string, object> facetDict = new Dictionary<string, object>();
            facetDict.Add(facet.FacetName, queryDict);

            serializer.Serialize(writer, facetDict);
        }
    }
}
