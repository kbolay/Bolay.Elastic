using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting
{
    public class FacetResponseSerializer : JsonConverter
    {
        private const string _TYPE = "_type";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> facetsDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            List<IFacetResponse> responses = new List<IFacetResponse>();
            FacetTypeEnum facetType = FacetTypeEnum.TermsStatistics;
            foreach (KeyValuePair<string, object> facetKvp in facetsDict)
            {
                Dictionary<string, object> facetDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(facetKvp.Value.ToString());

                facetType = FacetTypeEnum.Find(facetDict.GetString(_TYPE));
                if (facetType == null)
                    throw new Exception(facetDict.GetString(_TYPE) + " is not a valid facet type.");

                Dictionary<string, object> facetHolderDict = new Dictionary<string, object>();
                facetHolderDict.Add(facetKvp.Key, facetKvp.Value);

                string facetJson = JsonConvert.SerializeObject(facetHolderDict);

                responses.Add(JsonConvert.DeserializeObject(facetJson, facetType.ResponseType) as IFacetResponse);
            }

            return new FacetsResponse(responses);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
