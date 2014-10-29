using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Queries.HasParent;
using Bolay.Elastic.QueryDSL.Queries.Term;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Queries.HasParent
{
    [TestClass]
    public class UnitTests_HasParentQuery
    {
        [TestMethod]
        public void PASS_CreateQuery()
        {
            HasParentQuery query = new HasParentQuery("type", new TermQuery("field", "value"))
            {
                ScoreType = ScoreTypeEnum.Score
            };

            Assert.IsNotNull(query);
            Assert.AreEqual("type", query.ParentType);
            Assert.AreEqual(ScoreTypeEnum.Score, query.ScoreType);
            Assert.IsTrue(query.Query is TermQuery);
            Assert.AreEqual("field", (query.Query as TermQuery).Field);
            Assert.AreEqual("value", (query.Query as TermQuery).Value);
        }

        [TestMethod]
        public void FAIL_CreateQuery_ChildType()
        {
            try
            {
                HasParentQuery query = new HasParentQuery(null, new TermQuery("field", "value"));
                Assert.Fail();
            }
            catch (ArgumentNullException argEx)
            {
                Assert.AreEqual("parentType", argEx.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateQuery_Query()
        {
            try
            {
                HasParentQuery query = new HasParentQuery("type", null);
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
            HasParentQuery query = new HasParentQuery("type", new TermQuery("field", "value"))
            {
                ScoreType = ScoreTypeEnum.Score
            };

            string json = JsonConvert.SerializeObject(query);

            Assert.IsNotNull(json);

            string expectedJson = "{\"has_parent\":{\"parent_type\":\"type\",\"score_type\":\"score\",\"query\":{\"term\":{\"field\":\"value\"}}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"has_parent\":{\"parent_type\":\"type\",\"score_type\":\"score\",\"query\":{\"term\":{\"field\":\"value\"}}}}";

            HasParentQuery query = JsonConvert.DeserializeObject<HasParentQuery>(json);
            Assert.IsNotNull(query);
            Assert.AreEqual("type", query.ParentType);
            Assert.AreEqual(ScoreTypeEnum.Score, query.ScoreType);
            Assert.IsTrue(query.Query is TermQuery);
            Assert.AreEqual("field", (query.Query as TermQuery).Field);
            Assert.AreEqual("value", (query.Query as TermQuery).Value);
        }
    }
}
