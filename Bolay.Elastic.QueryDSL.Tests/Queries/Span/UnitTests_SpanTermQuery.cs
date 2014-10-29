using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Queries.Span;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Queries.Span
{
    [TestClass]
    public class UnitTests_SpanTermQuery
    {
        [TestMethod]
        public void PASS_CreateQuery()
        {
            SpanTermQuery query = new SpanTermQuery("field", "value");
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Field);
            Assert.AreEqual("value", query.Value);
        }

        [TestMethod]
        public void FAIL_CreateQuery_Field()
        {
            try
            {
                SpanTermQuery query = new SpanTermQuery(null, "value");
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("field", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateQuery_Value()
        {
            try
            {
                SpanTermQuery query = new SpanTermQuery("field", null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("value", ex.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            SpanTermQuery query = new SpanTermQuery("field", "value");
            string json = JsonConvert.SerializeObject(query);

            Assert.IsNotNull(json);
            string expectedJson = "{\"span_term\":{\"field\":\"value\"}}";

            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"span_term\":{\"field\":\"value\"}}";
            SpanTermQuery query = JsonConvert.DeserializeObject<SpanTermQuery>(json);
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Field);
            Assert.AreEqual("value", query.Value);
        }

        [TestMethod]
        public void PASS_Serialize_Boost()
        {
            SpanTermQuery query = new SpanTermQuery("field", "value") { Boost = 1.2 };
            string json = JsonConvert.SerializeObject(query);

            Assert.IsNotNull(json);
            string expectedJson = "{\"span_term\":{\"field\":{\"value\":\"value\",\"boost\":1.2}}}";

            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_Boost()
        {
            string json = "{\"span_term\":{\"field\":{\"value\":\"value\",\"boost\":1.2}}}";
            SpanTermQuery query = JsonConvert.DeserializeObject<SpanTermQuery>(json);
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Field);
            Assert.AreEqual("value", query.Value);
            Assert.AreEqual(1.2, query.Boost);
        }
    }
}
