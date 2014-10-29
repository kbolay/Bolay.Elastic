using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Faceting;
using System.Collections.Generic;
using Bolay.Elastic.QueryDSL.Faceting.Filter;
using Bolay.Elastic.QueryDSL.Filters.Term;
using Bolay.Elastic.QueryDSL.Faceting.Query;
using Bolay.Elastic.QueryDSL.Queries.Term;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Faceting
{
    [TestClass]
    public class UnitTests_Facets
    {
        [TestMethod]
        public void PASS_Create()
        {
            Facets facets = new Facets(
                new List<IFacet>()
                {
                    new FilterFacet("filter-name", new TermFilter("field", "value")),
                    new QueryFacet("query-name", new TermQuery("field", "value"))
                });
            Assert.IsNotNull(facets);
            Assert.AreEqual("filter-name", facets.FacetGenerators.First().FacetName);
            Assert.AreEqual("query-name", facets.FacetGenerators.Last().FacetName);
            Assert.AreEqual("field", ((facets.FacetGenerators.First() as FilterFacet).Filter as TermFilter).Field);
            Assert.AreEqual("value", ((facets.FacetGenerators.First() as FilterFacet).Filter as TermFilter).Value);
            Assert.AreEqual("field", ((facets.FacetGenerators.Last() as QueryFacet).Query as TermQuery).Field);
            Assert.AreEqual("value", ((facets.FacetGenerators.Last() as QueryFacet).Query as TermQuery).Value);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            Facets facets = new Facets(
                new List<IFacet>()
                {
                    new FilterFacet("filter-name", new TermFilter("field", "value")),
                    new QueryFacet("query-name", new TermQuery("field", "value"))
                });

            string json = JsonConvert.SerializeObject(facets);
            Assert.IsNotNull(json);

            string expectedJson = "{\"filter-name\":{\"filter\":{\"term\":{\"field\":\"value\"}}},\"query-name\":{\"query\":{\"term\":{\"field\":\"value\"}}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"filter-name\":{\"filter\":{\"term\":{\"field\":\"value\"}}},\"query-name\":{\"query\":{\"term\":{\"field\":\"value\"}}}}";

            Facets facets = JsonConvert.DeserializeObject<Facets>(json);
            Assert.IsNotNull(facets);
            Assert.AreEqual("filter-name", facets.FacetGenerators.First().FacetName);
            Assert.AreEqual("query-name", facets.FacetGenerators.Last().FacetName);
            Assert.AreEqual("field", ((facets.FacetGenerators.First() as FilterFacet).Filter as TermFilter).Field);
            Assert.AreEqual("value", ((facets.FacetGenerators.First() as FilterFacet).Filter as TermFilter).Value);
            Assert.AreEqual("field", ((facets.FacetGenerators.Last() as QueryFacet).Query as TermQuery).Field);
            Assert.AreEqual("value", ((facets.FacetGenerators.Last() as QueryFacet).Query as TermQuery).Value);
        }

        [TestMethod]
        public void PASS_Deserialize_Response()
        {
            string json = "{\"query-name\":{\"_type\":\"query\",\"count\":15},\"filter-name\":{\"_type\":\"filter\",\"count\":15}}";
            FacetsResponse response = JsonConvert.DeserializeObject<FacetsResponse>(json);
            Assert.IsNotNull(response);
            Assert.AreEqual((Int32)2, response.FacetResults.Count());
            Assert.AreEqual("query-name", response.FacetResults.First().Name);
            QueryResponse queryResponse = response.FacetResults.First() as QueryResponse;
            Assert.AreEqual((Int64)15, queryResponse.Count);

            Assert.AreEqual("filter-name", response.FacetResults.Last().Name);
            FilterResponse filterResponse = response.FacetResults.Last() as FilterResponse;
            Assert.AreEqual((Int64)15, filterResponse.Count);
        }
    }
}
