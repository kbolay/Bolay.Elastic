using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Queries.Span;
using Bolay.Elastic.QueryDSL.Queries.Prefix;
using Bolay.Elastic.QueryDSL.Queries;
using Bolay.Elastic.QueryDSL.Queries.Term;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Queries.Span
{
    [TestClass]
    public class UnitTests_SpanMultiTermQuery
    {
        [TestMethod]
        public void PASS_CreateQuery()
        {
            SpanMultiTermQuery query = new SpanMultiTermQuery(new PrefixQuery("field", "value"));
            Assert.IsNotNull(query);
            Assert.IsTrue(query.Match is PrefixQuery);
            PrefixQuery prefixQuery = query.Match as PrefixQuery;
            Assert.AreEqual("field", prefixQuery.Field);
            Assert.AreEqual("value", prefixQuery.Value);
        }

        [TestMethod]
        public void FAIL_CreateQuery_Null()
        {
            try
            {
                SpanMultiTermQuery query = new SpanMultiTermQuery((IQuery)null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("match", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateQuery_TermQuery()
        {
            try
            {
                SpanMultiTermQuery query = new SpanMultiTermQuery(new TermQuery("field", "value"));
                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("match", ex.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            SpanMultiTermQuery query = new SpanMultiTermQuery(new PrefixQuery("field", "value"));
            string json = JsonConvert.SerializeObject(query);
            Assert.IsNotNull("query");

            string expectedJson = "{\"span_multi\":{\"match\":{\"prefix\":{\"field\":\"value\"}}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"span_multi\":{\"match\":{\"prefix\":{\"field\":\"value\"}}}}";
            SpanMultiTermQuery query = JsonConvert.DeserializeObject<SpanMultiTermQuery>(json);
            Assert.IsNotNull(query);
            Assert.IsTrue(query.Match is PrefixQuery);
            PrefixQuery prefixQuery = query.Match as PrefixQuery;
            Assert.AreEqual("field", prefixQuery.Field);
            Assert.AreEqual("value", prefixQuery.Value);
        }
    }
}
