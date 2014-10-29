using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Queries.Span;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Queries.Span
{
    [TestClass]
    public class UnitTests_SpanOrQuery
    {
        [TestMethod]
        public void PASS_CreateQuery()
        {
            SpanOrQuery query = new SpanOrQuery(new List<SpanQueryBase>(){
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
                SpanOrQuery query = new SpanOrQuery(new List<SpanQueryBase>() { null });
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("clauses", ex.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            SpanOrQuery query = new SpanOrQuery(new List<SpanQueryBase>(){
                    new SpanTermQuery("field", "value")
                });
            string json = JsonConvert.SerializeObject(query);
            Assert.IsNotNull(json);

            string expectedJson = "{\"span_or\":{\"clauses\":[{\"span_term\":{\"field\":\"value\"}}]}}";

            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"span_or\":{\"clauses\":[{\"span_term\":{\"field\":\"value\"}}]}}";

            SpanOrQuery query = JsonConvert.DeserializeObject<SpanOrQuery>(json);
            Assert.IsNotNull(query);
            Assert.AreEqual(1, query.Clauses.Count());
            Assert.IsTrue(query.Clauses.First() is SpanTermQuery);
            SpanTermQuery termQuery = query.Clauses.First() as SpanTermQuery;
            Assert.AreEqual("field", termQuery.Field);
            Assert.AreEqual("value", termQuery.Value);
        }
    }
}
