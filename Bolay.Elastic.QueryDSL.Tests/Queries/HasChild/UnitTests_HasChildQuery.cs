using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Queries.HasChild;
using Bolay.Elastic.QueryDSL.Queries.Term;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Queries.HasChild
{
    [TestClass]
    public class UnitTests_HasChildQuery
    {
        [TestMethod]
        public void PASS_CreateQuery()
        {
            HasChildQuery query = new HasChildQuery("type", new TermQuery("field", "value"))
            {
                ScoreType = ScoreTypeEnum.Maximum
            };

            Assert.IsNotNull(query);
            Assert.AreEqual("type", query.ChildType);
            Assert.AreEqual(ScoreTypeEnum.Maximum, query.ScoreType);
            Assert.IsTrue(query.Query is TermQuery);
            Assert.AreEqual("field", (query.Query as TermQuery).Field);
            Assert.AreEqual("value", (query.Query as TermQuery).Value);
        }

        [TestMethod]
        public void FAIL_CreateQuery_ChildType()
        {
            try
            {
                HasChildQuery query = new HasChildQuery(null, new TermQuery("field", "value"));
                Assert.Fail();
            }
            catch (ArgumentNullException argEx)
            {
                Assert.AreEqual("childType", argEx.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateQuery_Query()
        {
            try
            {
                HasChildQuery query = new HasChildQuery("type", null);
                Assert.Fail();
            }
            catch (ArgumentNullException argEx)
            {
                Assert.AreEqual("query", argEx.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            HasChildQuery query = new HasChildQuery("type", new TermQuery("field", "value"))
            {
                ScoreType = ScoreTypeEnum.Maximum
            };

            string json = JsonConvert.SerializeObject(query);

            Assert.IsNotNull(json);

            string expectedJson = "{\"has_child\":{\"type\":\"type\",\"score_type\":\"max\",\"query\":{\"term\":{\"field\":\"value\"}}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"has_child\":{\"type\":\"type\",\"score_type\":\"max\",\"query\":{\"term\":{\"field\":\"value\"}}}}";

            HasChildQuery query = JsonConvert.DeserializeObject<HasChildQuery>(json);
            Assert.IsNotNull(query);
            Assert.AreEqual("type", query.ChildType);
            Assert.AreEqual(ScoreTypeEnum.Maximum, query.ScoreType);
            Assert.IsTrue(query.Query is TermQuery);
            Assert.AreEqual("field", (query.Query as TermQuery).Field);
            Assert.AreEqual("value", (query.Query as TermQuery).Value);
        }
    }
}
