using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Filters.Query;
using Bolay.Elastic.QueryDSL.Queries.Term;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Filters.Query
{
    [TestClass]
    public class UnitTests_QueryFilter
    {
        [TestMethod]
        public void PASS_CreateFilter()
        {
            QueryFilter filter = new QueryFilter(new TermQuery("field", "value"));
            Assert.IsNotNull(filter);
            Assert.IsTrue(filter.Query is TermQuery);
            Assert.AreEqual("field", (filter.Query as TermQuery).Field);
            Assert.AreEqual("value", (filter.Query as TermQuery).Value);
        }

        [TestMethod]
        public void FAIL_CreateFilter()
        {
            try
            {
                QueryFilter filter = new QueryFilter(null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("query", ex.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            QueryFilter filter = new QueryFilter(new TermQuery("field", "value"));
            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);
            string expectedJson = "{\"query\":{\"term\":{\"field\":\"value\"}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"query\":{\"term\":{\"field\":\"value\"}}}";
            QueryFilter filter = JsonConvert.DeserializeObject<QueryFilter>(json);
            Assert.IsNotNull(filter);
            Assert.IsTrue(filter.Query is TermQuery);
            Assert.AreEqual("field", (filter.Query as TermQuery).Field);
            Assert.AreEqual("value", (filter.Query as TermQuery).Value);
        }

        [TestMethod]
        public void PASS_Serialize_Cache()
        {
            QueryFilter filter = new QueryFilter(new TermQuery("field", "value"))
            {
                Cache = true
            };
            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);
            string expectedJson = "{\"fquery\":{\"query\":{\"term\":{\"field\":\"value\"}},\"_cache\":true}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_Cache()
        {
            string json = "{\"fquery\":{\"query\":{\"term\":{\"field\":\"value\"}},\"_cache\":true}}";
            QueryFilter filter = JsonConvert.DeserializeObject<QueryFilter>(json);
            Assert.IsNotNull(filter);
            Assert.IsTrue(filter.Query is TermQuery);
            Assert.AreEqual("field", (filter.Query as TermQuery).Field);
            Assert.AreEqual("value", (filter.Query as TermQuery).Value);
            Assert.AreEqual(true, filter.Cache);
        }
    }
}
