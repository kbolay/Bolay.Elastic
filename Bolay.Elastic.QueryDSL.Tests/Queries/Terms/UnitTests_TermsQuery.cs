using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Queries.Terms;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Queries.Terms
{
    [TestClass]
    public class UnitTests_TermsQuery
    {
        [TestMethod]
        public void PASS_CreateQuery()
        {
            TermsQuery query = new TermsQuery("field", new List<object>() { "value1", "value2" });
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Field);
            Assert.AreEqual(2, query.Values.Count());
            Assert.AreEqual(1, query.MinimumShouldMatch.GetValue());
        }

        [TestMethod]
        public void FAIL_CreateQuery_Field()
        {
            try
            {
                TermsQuery query = new TermsQuery(null, null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("field", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateQuery_Values()
        {
            try
            {
                TermsQuery query = new TermsQuery("field", null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("values", ex.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            TermsQuery query = new TermsQuery("field", new List<object>() { "value1", "value2" });
            string json = JsonConvert.SerializeObject(query);
            Assert.IsNotNull(json);

            string expectedJson = "{\"terms\":{\"field\":[\"value1\",\"value2\"]}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"terms\":{\"field\":[\"value1\",\"value2\"]}}";
            TermsQuery query = JsonConvert.DeserializeObject<TermsQuery>(json);
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Field);
            Assert.AreEqual(2, query.Values.Count());
            Assert.AreEqual("value1", query.Values.First());
            Assert.AreEqual("value2", query.Values.Last());
            Assert.AreEqual(1, query.MinimumShouldMatch.GetValue());
        }
    }
}
