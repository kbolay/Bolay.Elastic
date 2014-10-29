using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Queries.Span;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Queries.Span
{
    [TestClass]
    public class UnitTests_SpanFirstQuery
    {
        [TestMethod]
        public void PASS_CreateQuery()
        {
            SpanFirstQuery query = new SpanFirstQuery(new SpanTermQuery("field", "value"), 3);
            Assert.IsNotNull(query);
            Assert.AreEqual(3, query.End);
            Assert.IsTrue(query.Match is SpanTermQuery);
            SpanTermQuery termQuery = query.Match as SpanTermQuery;
            Assert.AreEqual("field", termQuery.Field);
            Assert.AreEqual("value", termQuery.Value);
        }

        [TestMethod]
        public void FAIL_CreateQuery_MatchNull()
        {
            try
            {
                SpanFirstQuery query = new SpanFirstQuery(null, 3);
                Assert.Fail();
            }
            catch(ArgumentNullException ex)
            {
                Assert.AreEqual("match", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateQuery_MatchFirst()
        {
            try
            {
                SpanFirstQuery query = new SpanFirstQuery(new SpanFirstQuery(new SpanTermQuery("field", "value"), 3), 3);
                Assert.Fail();
            }
            catch(ArgumentException ex)
            {
                Assert.AreEqual("match", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateQuery_End()
        {
            try
            {
                SpanFirstQuery query = new SpanFirstQuery(new SpanTermQuery("field", "value"), -1);
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.AreEqual("end", ex.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            SpanFirstQuery query = new SpanFirstQuery(new SpanTermQuery("field", "value"), 3);
            string json = JsonConvert.SerializeObject(query);
            Assert.IsNotNull(json);

            string expectedJson = "{\"span_first\":{\"match\":{\"span_term\":{\"field\":\"value\"}},\"end\":3}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"span_first\":{\"match\":{\"span_term\":{\"field\":\"value\"}},\"end\":3}}";
            SpanFirstQuery query = JsonConvert.DeserializeObject<SpanFirstQuery>(json);
            Assert.IsNotNull(query);
            Assert.AreEqual(3, query.End);
            Assert.IsTrue(query.Match is SpanTermQuery);
            SpanTermQuery termQuery = query.Match as SpanTermQuery;
            Assert.AreEqual("field", termQuery.Field);
            Assert.AreEqual("value", termQuery.Value);
        }
    }
}
