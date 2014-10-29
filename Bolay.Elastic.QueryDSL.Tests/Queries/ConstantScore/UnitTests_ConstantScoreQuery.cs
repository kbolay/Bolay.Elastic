using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Queries.ConstantScore;
using Bolay.Elastic.QueryDSL.Queries.Term;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Queries.ConstantScore
{
    [TestClass]
    public class UnitTests_ConstantScoreQuery
    {
        [TestMethod]
        public void PASS_CreateQuery()
        {
            ConstantScoreQuery query = new ConstantScoreQuery(new TermQuery("field", "value"))
            {
                Boost = 1.2
            };

            Assert.IsNotNull(query);
            Assert.AreEqual(1.2, query.Boost);
            Assert.IsTrue(query.SearchPiece is TermQuery);
            Assert.AreEqual("field", (query.SearchPiece as TermQuery).Field);
            Assert.AreEqual("value", (query.SearchPiece as TermQuery).Value);
        }

        [TestMethod]
        public void FAIL_CreateQuery_SearchPiece()
        {
            try
            {
                ConstantScoreQuery query = new ConstantScoreQuery(null);
                Assert.Fail();
            }
            catch (ArgumentException argEx)
            {
                Assert.AreEqual("searchPiece", argEx.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            ConstantScoreQuery query = new ConstantScoreQuery(new TermQuery("field", "value"))
            {
                Boost = 1.2
            };

            string json = JsonConvert.SerializeObject(query);

            Assert.IsNotNull(json);

            string expectedJson = "{\"constant_score\":{\"query\":{\"term\":{\"field\":\"value\"}},\"boost\":1.2}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"constant_score\":{\"query\":{\"term\":{\"field\":\"value\"}},\"boost\":1.2}}";

            ConstantScoreQuery query = JsonConvert.DeserializeObject<ConstantScoreQuery>(json);
            Assert.IsNotNull(query);
            Assert.AreEqual(1.2, query.Boost);
            Assert.AreEqual("field", (query.SearchPiece as TermQuery).Field);
            Assert.AreEqual("value", (query.SearchPiece as TermQuery).Value);
        }
    }
}
