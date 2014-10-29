using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Queries.Nested;
using Bolay.Elastic.QueryDSL.Queries.Term;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Queries.Nested
{
    [TestClass]
    public class UnitTests_NestedQuery
    {
        [TestMethod]
        public void PASS_CreateQuery()
        {
            NestedQuery query = new NestedQuery("obj1", new TermQuery("field", "value"));
            Assert.IsNotNull(query);
            Assert.AreEqual("obj1", query.Path);
            Assert.AreEqual(ScoreTypeEnum.Average, query.ScoreMode);
            Assert.IsTrue(query.Query is TermQuery);
            TermQuery termQuery = query.Query as TermQuery;
            Assert.AreEqual("field", termQuery.Field);
            Assert.AreEqual("value", termQuery.Value);
        }

        [TestMethod]
        public void FAIL_CreateQuery_Path()
        {
            try
            {
                NestedQuery query = new NestedQuery(null, new TermQuery("field", "value"));
                Assert.Fail();
            }
            catch(ArgumentNullException ex)
            {
                Assert.AreEqual("path", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateQuery_Query()
        {
            try
            {
                NestedQuery query = new NestedQuery("obj1", null);
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
            NestedQuery query = new NestedQuery("obj1", new TermQuery("field", "value"));
            string json = JsonConvert.SerializeObject(query);

            Assert.IsNotNull(json);
            string expectedJson = "{\"nested\":{\"path\":\"obj1\",\"query\":{\"term\":{\"field\":\"value\"}}}}";

            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"nested\":{\"path\":\"obj1\",\"query\":{\"term\":{\"field\":\"value\"}}}}";
            NestedQuery query = JsonConvert.DeserializeObject<NestedQuery>(json);
            Assert.IsNotNull(query);
            Assert.AreEqual("obj1", query.Path);
            Assert.AreEqual(ScoreTypeEnum.Average, query.ScoreMode);
            Assert.IsTrue(query.Query is TermQuery);
            TermQuery termQuery = query.Query as TermQuery;
            Assert.AreEqual("field", termQuery.Field);
            Assert.AreEqual("value", termQuery.Value);
        }
    }
}
