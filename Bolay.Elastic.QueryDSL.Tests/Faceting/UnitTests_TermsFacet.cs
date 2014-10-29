using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Faceting;
using System.Collections.Generic;
using Bolay.Elastic.QueryDSL.Faceting.Terms;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Faceting
{
    [TestClass]
    public class UnitTests_TermsFacet
    {
        [TestMethod]
        public void PASS_Create()
        {
            TermsFacet facet = new TermsFacet("term-facet", new List<string>(){"field"});
            Assert.IsNotNull(facet);
            Assert.AreEqual("term-facet", facet.FacetName);
            Assert.AreEqual("field", facet.Fields.First());
        }

        [TestMethod]
        public void FAIL_Create_FacetName()
        {
            try
            {
                TermsFacet facet = new TermsFacet(null, "");
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("facetName", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_Create_Fields()
        {
            try
            {
                TermsFacet facet = new TermsFacet("terms-facet", new List<string>());
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("fields", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_Create_ScriptField()
        {
            try
            {
                TermsFacet facet = new TermsFacet("terms-facet", "");
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("scriptField", ex.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            TermsFacet facet = new TermsFacet("term-facet", new List<string>() { "field" });
            string json = JsonConvert.SerializeObject(facet);
            Assert.IsNotNull(json);

            string expectedJson = "{\"term-facet\":{\"terms\":{\"field\":\"field\"}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"term-facet\":{\"terms\":{\"field\":\"field\"}}}";
            TermsFacet facet = JsonConvert.DeserializeObject<TermsFacet>(json);
            Assert.IsNotNull(facet);
            Assert.AreEqual("term-facet", facet.FacetName);
            Assert.AreEqual("field", facet.Fields.First());
        }

        [TestMethod]
        public void PASS_Deserialize_Response()
        {
            string json = "{\"name\":{\"_type\":\"terms\",\"missing\":1,\"total\":12,\"other\":10,\"terms\":[{\"term\":\"term1\",\"count\":15},{\"term\":\"term2\",\"count\":1}]}}";
            TermsResponse result = JsonConvert.DeserializeObject<TermsResponse>(json);
            Assert.IsNotNull(result);
            Assert.AreEqual("name", result.Name);
            Assert.AreEqual((Int64)1, result.Missing);
            Assert.AreEqual((Int64)12, result.Total);
            Assert.AreEqual((Int64)10, result.Other);
            Assert.AreEqual("term1", result.Terms.First().Value);
            Assert.AreEqual("term2", result.Terms.Last().Value);
            Assert.AreEqual((Int64)15, result.Terms.First().Count);
            Assert.AreEqual((Int64)1, result.Terms.Last().Count);
        }
    }
}
