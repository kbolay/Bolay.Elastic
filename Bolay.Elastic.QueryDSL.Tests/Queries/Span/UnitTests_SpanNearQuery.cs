using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Queries.Span;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Queries.Span
{
    [TestClass]
    public class UnitTests_SpanNearQuery
    {
        [TestMethod]
        public void PASS_CreateQuery()
        {
            SpanNearQuery query = new SpanNearQuery(new List<SpanQueryBase>(){
                    new SpanTermQuery("field", "value")
                });

            Assert.IsNotNull(query);
            Assert.AreEqual(1, query.Clauses.Count());
        }

        [TestMethod]
        public void FAIL_CreateQuery()
        {
            try
            {
                SpanNearQuery query = new SpanNearQuery(new List<SpanQueryBase>() { null });
                Assert.Fail();
            }
            catch(ArgumentNullException ex)
            {
                Assert.AreEqual("clauses", ex.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            SpanNearQuery query = new SpanNearQuery(new List<SpanQueryBase>(){
                    new SpanTermQuery("field", "value")
                });
            string json = JsonConvert.SerializeObject(query);
            Assert.IsNotNull(json);

            string expectedJson = "{\"span_near\":{\"clauses\":[{\"span_term\":{\"field\":\"value\"}}],\"in_order\":false,\"collect_payloads\":false}}";

            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"span_near\":{\"clauses\":[{\"span_term\":{\"field\":\"value\"}}],\"in_order\":false,\"collect_payloads\":false}}";

            SpanNearQuery query = JsonConvert.DeserializeObject<SpanNearQuery>(json);
            Assert.IsNotNull(query);
            Assert.AreEqual(1, query.Clauses.Count());
            Assert.AreEqual(false, query.RequireMatchesInOrder);
            Assert.AreEqual(false, query.CollectPayloads);
        }
    }
}
