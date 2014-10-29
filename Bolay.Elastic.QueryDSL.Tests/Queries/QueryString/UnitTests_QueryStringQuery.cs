using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Queries.QueryString;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Queries.QueryString
{
    [TestClass]
    public class UnitTests_QueryStringQuery
    {
        [TestMethod]
        public void PASS_CreateQuery_NoField()
        {
            QueryStringQuery query = new QueryStringQuery("term1 term2");
            Assert.IsNotNull(query);
            Assert.AreEqual("term1 term2", query.Query);
        }

        [TestMethod]
        public void PASS_CreateQuery_DefaultField()
        {
            QueryStringQuery query = new QueryStringQuery("field", "term1 term2");
            Assert.IsNotNull(query);
            Assert.AreEqual(1, query.NumberOfFields);
            Assert.AreEqual("term1 term2", query.Query);
        }

        [TestMethod]
        public void PASS_CreateQuery_Fields()
        {
            QueryStringQuery query = new QueryStringQuery(new List<string>() { "field1", "field2" }, "term1 term2");
            Assert.IsNotNull(query);
            Assert.AreEqual(2, query.NumberOfFields);
            Assert.AreEqual("term1 term2", query.Query);
        }

        [TestMethod]
        public void FAIL_CreateQuery_Query()
        {
            try
            {
                QueryStringQuery query = new QueryStringQuery(null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("query", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateQuery_Field()
        {
            try
            {
                QueryStringQuery query = new QueryStringQuery("", "query");
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("field", ex.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            QueryStringQuery query = new QueryStringQuery("field", "term1 term2");
            string json = JsonConvert.SerializeObject(query);
            Assert.IsNotNull(json);

            string expectedJson = "{\"query_string\":{\"default_field\":\"field\",\"query\":\"term1 term2\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"query_string\":{\"default_field\":\"field\",\"query\":\"term1 term2\"}}";
            QueryStringQuery query = JsonConvert.DeserializeObject<QueryStringQuery>(json);
            Assert.IsNotNull(query);
            Assert.AreEqual(1, query.NumberOfFields);
            Assert.AreEqual("term1 term2", query.Query);
        }
    }
}
