using Bolay.Elastic.Exceptions;
using Bolay.Elastic.QueryDSL.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting
{
    public class FacetSerializer : JsonConverter
    {
        internal const string _SIZE = "size";
        internal const string _GLOBAL = "global";
        internal const string _NESTED = "nested";
        internal const string _FACET_FILTER = "facet_filter";

        internal const int _SIZE_DEFAULT = 5;
        internal const bool _GLOBAL_DEFAULT = false;

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> facetsDict = serializer.Deserialize<Dictionary<string, object>>(reader);

            List<IFacet> facetGenerators = new List<IFacet>();
            FacetTypeEnum facetType = FacetTypeEnum.DateHistogram;
            foreach (KeyValuePair<string, object> facetKvp in facetsDict)
            {
                // dig down to get the type i am dealing with
                Dictionary<string, object> facetDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(facetKvp.Value.ToString());
                KeyValuePair<string, object> facetTypeKvp = facetDict.First();

                // create a dictionary just for this one
                Dictionary<string, object> internalDict = new Dictionary<string, object>();
                internalDict.Add(facetKvp.Key, facetKvp.Value);
                Dictionary<string, object> facetTypeDict = new Dictionary<string, object>();
                facetTypeDict.Add("junk", internalDict);

                facetType = FacetTypeEnum.Find(facetTypeKvp.Key);
                if (facetType == null)
                    throw new Exception(facetTypeKvp.Key + " is not a valid type of facet.");

                string facetTypeJsonStr = JsonConvert.SerializeObject(facetTypeDict.First().Value);
                facetGenerators.Add(JsonConvert.DeserializeObject(facetTypeJsonStr, facetType.ImplementationType) as IFacet);
            }

            Facets facets = new Facets(facetGenerators);

            return facets;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is Facets))
                throw new SerializeTypeException<Facets>();

            Facets facets = value as Facets;

            Dictionary<string, object> facetsDict = new Dictionary<string, object>();

            foreach (IFacet facet in facets.FacetGenerators)
            {
                string suggestorJson = JsonConvert.SerializeObject(facet);
                Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(suggestorJson);
                facetsDict.Add(fieldDict.First().Key, fieldDict.First().Value);
            }

            serializer.Serialize(writer, facetsDict);
        }

        internal static void DeserializeFacetInfo(FacetBase facet, Dictionary<string, object> facetDict)
        {
            if (facetDict.ContainsKey(_FACET_FILTER))
                facet.FacetFilter = JsonConvert.DeserializeObject<IFilter>(facetDict.GetString(_FACET_FILTER));
            facet.IsScopeGlobal = facetDict.GetBool(_GLOBAL, _GLOBAL_DEFAULT);
            facet.NestedObject = facetDict.GetStringOrDefault(_NESTED);
            facet.Size = facetDict.GetInt32(_SIZE, _SIZE_DEFAULT);
        }

        internal static void SerializeFacetInfo(FacetBase facet, Dictionary<string, object> facetDict)
        {
            facetDict.AddObject(_SIZE, facet.Size, _SIZE_DEFAULT);
            facetDict.AddObject(_NESTED, facet.NestedObject);
            facetDict.AddObject(_GLOBAL, facet.IsScopeGlobal, _GLOBAL_DEFAULT);
            facetDict.AddObject(_FACET_FILTER, facet.FacetFilter);
        }
    }
}
