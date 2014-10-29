using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Faceting.Filter;
using Bolay.Elastic.QueryDSL.Filters.Term;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Faceting
{
    [TestClass]
    public class UnitTests_FilterFacet
    {
        [TestMethod]
        public void PASS_Create()
        {
            FilterFacet facet = new FilterFacet("name", new TermFilter("field", "value"));
            Assert.IsNotNull(facet);
            Assert.AreEqual("name", facet.FacetName);
            Assert.AreEqual("field", (facet.Filter as TermFilter).Field);
            Assert.AreEqual("value", (facet.Filter as TermFilter).Value);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            FilterFacet facet = new FilterFacet("name", new TermFilter("field", "value"));
            string json = JsonConvert.SerializeObject(facet);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"filter\":{\"term\":{\"field\":\"value\"}}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"filter\":{\"term\":{\"field\":\"value\"}}}}";
            FilterFacet facet = JsonConvert.DeserializeObject<FilterFacet>(json);
            Assert.IsNotNull(facet);
            Assert.AreEqual("name", facet.FacetName);
            Assert.AreEqual("field", (facet.Filter as TermFilter).Field);
            Assert.AreEqual("value", (facet.Filter as TermFilter).Value);
        }

        [TestMethod]
        public void PASS_Deserialize_Response()
        {
            string json = "{\"name\":{\"_type\":\"filter\",\"count\":15}}";
            FilterResponse facet = JsonConvert.DeserializeObject<FilterResponse>(json);
            Assert.IsNotNull(facet);
            Assert.AreEqual("name", facet.Name);
            Assert.AreEqual((Int64)15, facet.Count);
        }
    }
}
