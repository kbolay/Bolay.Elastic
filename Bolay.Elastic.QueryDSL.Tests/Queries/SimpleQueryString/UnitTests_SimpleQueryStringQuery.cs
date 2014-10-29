using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Queries.SimpleQueryString;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Queries.SimpleQueryString
{
    [TestClass]
    public class UnitTests_SimpleQueryStringQuery
    {
        [TestMethod]
        public void PASS_CreateQuery_NoField()
        {
            SimpleQueryStringQuery query = new SimpleQueryStringQuery("term1 term2");
            Assert.IsNotNull(query);
            Assert.AreEqual("term1 term2", query.Query);
        }

        [TestMethod]
        public void PASS_CreateQuery_Fields()
        {
            SimpleQueryStringQuery query = new SimpleQueryStringQuery(new List<string>(){"field"}, "term1 term2");
            Assert.IsNotNull(query);
            Assert.AreEqual(1, query.Fields.Count());
            Assert.AreEqual("term1 term2", query.Query);
        }

        [TestMethod]
        public void FAIL_CreateQuery_Query()
        {
            try
            {
                SimpleQueryStringQuery query = new SimpleQueryStringQuery(null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("query", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateQuery_Fields()
        {
            try
            {
                SimpleQueryStringQuery query = new SimpleQueryStringQuery(null, "query");
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("fields", ex.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            SimpleQueryStringQuery query = new SimpleQueryStringQuery(new List<string>(){"field"}, "term1 term2");
            string json = JsonConvert.SerializeObject(query);
            Assert.IsNotNull(json);

            string expectedJson = "{\"simple_query_string\":{\"fields\":[\"field\"],\"query\":\"term1 term2\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"simple_query_string\":{\"fields\":[\"field\"],\"query\":\"term1 term2\"}}";
            SimpleQueryStringQuery query = JsonConvert.DeserializeObject<SimpleQueryStringQuery>(json);
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Fields.First());
            Assert.AreEqual("term1 term2", query.Query);
        }
    }
}
