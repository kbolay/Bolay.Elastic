using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Faceting.Query;
using Bolay.Elastic.QueryDSL.Queries.Term;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Faceting
{
    [TestClass]
    public class UnitTests_QueryFacet
    {
        [TestMethod]
        public void PASS_Create()
        {
            QueryFacet facet = new QueryFacet("name", new TermQuery("field", "value"));
            Assert.IsNotNull(facet);
            Assert.AreEqual("name", facet.FacetName);
            Assert.AreEqual("field", (facet.Query as TermQuery).Field);
            Assert.AreEqual("value", (facet.Query as TermQuery).Value);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            QueryFacet facet = new QueryFacet("name", new TermQuery("field", "value"));
            string json = JsonConvert.SerializeObject(facet);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"query\":{\"term\":{\"field\":\"value\"}}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"query\":{\"term\":{\"field\":\"value\"}}}}";
            QueryFacet facet = JsonConvert.DeserializeObject<QueryFacet>(json);
            Assert.IsNotNull(facet);
            Assert.AreEqual("name", facet.FacetName);
            Assert.AreEqual("field", (facet.Query as TermQuery).Field);
            Assert.AreEqual("value", (facet.Query as TermQuery).Value);
        }

        [TestMethod]
        public void PASS_Deserialize_Response()
        {
            string json = "{\"name\":{\"_type\":\"query\",\"count\":15}}";
            QueryResponse facet = JsonConvert.DeserializeObject<QueryResponse>(json);
            Assert.IsNotNull(facet);
            Assert.AreEqual("name", facet.Name);
            Assert.AreEqual((Int64)15, facet.Count);
        }
    }
}
