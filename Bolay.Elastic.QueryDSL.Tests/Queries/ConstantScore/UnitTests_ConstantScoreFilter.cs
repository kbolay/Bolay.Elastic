using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Queries.ConstantScore;
using Newtonsoft.Json;
using Bolay.Elastic.QueryDSL.Filters.Term;

namespace Bolay.Elastic.QueryDSL.Tests.Queries.ConstantScore
{
    [TestClass]
    public class UnitTests_ConstantScoreFilter
    {
        [TestMethod]
        public void PASS_CreateQuery()
        {
            ConstantScoreFilter query = new ConstantScoreFilter(new TermFilter("field", "value"))
            {
                Boost = 1.2
            };

            Assert.IsNotNull(query);
            Assert.AreEqual(1.2, query.Boost);
            Assert.IsTrue(query.SearchPiece is TermFilter);
            Assert.AreEqual("field", (query.SearchPiece as TermFilter).Field);
            Assert.AreEqual("value", (query.SearchPiece as TermFilter).Value);
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
            ConstantScoreFilter query = new ConstantScoreFilter(new TermFilter("field", "value"))
            {
                Boost = 1.2
            };

            string json = JsonConvert.SerializeObject(query);

            Assert.IsNotNull(json);

            string expectedJson = "{\"constant_score\":{\"filter\":{\"term\":{\"field\":\"value\"}},\"boost\":1.2}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"constant_score\":{\"filter\":{\"term\":{\"field\":\"value\"}},\"boost\":1.2}}";

            ConstantScoreFilter query = JsonConvert.DeserializeObject<ConstantScoreFilter>(json);
            Assert.IsNotNull(query);
            Assert.AreEqual(1.2, query.Boost);
            Assert.AreEqual("field", (query.SearchPiece as TermFilter).Field);
            Assert.AreEqual("value", (query.SearchPiece as TermFilter).Value);
        }
    }
}
