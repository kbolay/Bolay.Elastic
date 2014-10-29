using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Queries.TopChildren;
using Bolay.Elastic.QueryDSL.Queries.Term;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Queries.TopChildren
{
    [TestClass]
    public class UnitTests_TopChildrenQuery
    {
        [TestMethod]
        public void PASS_CreateQuery()
        {
            TopChildrenQuery query = new TopChildrenQuery("child", new TermQuery("field", "value"));
            Assert.IsNotNull(query);
            Assert.AreEqual("child", query.DocumentType);
            Assert.IsTrue(query.Query is TermQuery);
            TermQuery termQuery = query.Query as TermQuery;
            Assert.AreEqual("field", termQuery.Field);
            Assert.AreEqual("value", termQuery.Value);
            Assert.AreEqual(ScoreTypeEnum.Maximum, query.Score);
            Assert.IsNull(query.Scope);
            Assert.AreEqual(5, query.Factor);
            Assert.AreEqual(2, query.IncrementalFactor);
        }

        [TestMethod]
        public void FAIL_CreateQuery_DocumentType()
        {
            try
            {
                TopChildrenQuery query = new TopChildrenQuery(null, null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("documentType", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateQuery_Query()
        {
            try
            {
                TopChildrenQuery query = new TopChildrenQuery("child", null);
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
            TopChildrenQuery query = new TopChildrenQuery("child", new TermQuery("field", "value"));
            string json = JsonConvert.SerializeObject(query);
            Assert.IsNotNull(json);

            string expectedJson = "{\"top_children\":{\"type\":\"child\",\"query\":{\"term\":{\"field\":\"value\"}}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"top_children\":{\"type\":\"child\",\"query\":{\"term\":{\"field\":\"value\"}}}}";
            TopChildrenQuery query = JsonConvert.DeserializeObject<TopChildrenQuery>(json);
            Assert.IsNotNull(query);
            Assert.AreEqual("child", query.DocumentType);
            Assert.IsTrue(query.Query is TermQuery);
            TermQuery termQuery = query.Query as TermQuery;
            Assert.AreEqual("field", termQuery.Field);
            Assert.AreEqual("value", termQuery.Value);
            Assert.AreEqual(ScoreTypeEnum.Maximum, query.Score);
            Assert.IsNull(query.Scope);
            Assert.AreEqual(5, query.Factor);
            Assert.AreEqual(2, query.IncrementalFactor);
        }
    }
}
