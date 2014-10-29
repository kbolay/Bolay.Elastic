using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Queries.Span;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Queries.Span
{
    [TestClass]
    public class UnitTests_SpanNotQuery
    {
        [TestMethod]
        public void PASS_CreateQuery()
        {
            SpanNotQuery query = new SpanNotQuery(
                new SpanTermQuery("field", "value"),
                new SpanTermQuery("field2", "value"));

            Assert.IsNotNull(query);
            Assert.IsTrue(query.Include is SpanTermQuery);
            Assert.IsTrue(query.Exclude is SpanTermQuery);
            SpanTermQuery include = query.Include as SpanTermQuery;
            SpanTermQuery exclude = query.Exclude as SpanTermQuery;
            Assert.AreEqual("field", include.Field);
            Assert.AreEqual("value", include.Value);
            Assert.AreEqual("field2", exclude.Field);
            Assert.AreEqual("value", exclude.Value);
        }

        [TestMethod]
        public void FAIL_CreateQuery_Include()
        {
            try
            {
                SpanNotQuery query = new SpanNotQuery(null, null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("include", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateQuery_Exclude()
        {
            try
            {
                SpanNotQuery query = new SpanNotQuery(new SpanTermQuery("field", "value"), null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("exclude", ex.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            SpanNotQuery query = new SpanNotQuery(
                new SpanTermQuery("field", "value"),
                new SpanTermQuery("field2", "value"));

            string json = JsonConvert.SerializeObject(query);
            Assert.IsNotNull(json);

            string expectedJson = "{\"span_not\":{\"include\":{\"span_term\":{\"field\":\"value\"}},\"exclude\":{\"span_term\":{\"field2\":\"value\"}}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"span_not\":{\"include\":{\"span_term\":{\"field\":\"value\"}},\"exclude\":{\"span_term\":{\"field2\":\"value\"}}}}";

            SpanNotQuery query = JsonConvert.DeserializeObject<SpanNotQuery>(json);
            Assert.IsNotNull(query);
            Assert.IsTrue(query.Include is SpanTermQuery);
            Assert.IsTrue(query.Exclude is SpanTermQuery);
            SpanTermQuery include = query.Include as SpanTermQuery;
            SpanTermQuery exclude = query.Exclude as SpanTermQuery;
            Assert.AreEqual("field", include.Field);
            Assert.AreEqual("value", include.Value);
            Assert.AreEqual("field2", exclude.Field);
            Assert.AreEqual("value", exclude.Value);

        }
    }
}
