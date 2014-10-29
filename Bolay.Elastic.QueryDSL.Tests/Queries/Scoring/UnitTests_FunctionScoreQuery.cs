using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Queries.Scoring.Functions;
using Bolay.Elastic.QueryDSL.Queries.Term;
using Bolay.Elastic.QueryDSL.Queries.Scoring;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Bolay.Elastic.QueryDSL.Tests.Queries.Scoring
{
    [TestClass]
    public class UnitTests_FunctionScoreQuery
    {
        [TestMethod]
        public void PASS_CreateQuery()
        {
            FunctionScoreQuery query = new FunctionScoreQuery(
                new TermQuery("field", "value"),
                new List<ScoreFunctionBase>() { new RandomScoreFunction(11111) });

            Assert.IsNotNull(query);
            Assert.IsTrue(query.Query is TermQuery);
            Assert.AreEqual("field", (query.Query as TermQuery).Field);
            Assert.AreEqual("value", (query.Query as TermQuery).Value.ToString());
            Assert.AreEqual(1, query.ScoreFunctions.Count());
            Assert.IsTrue(query.ScoreFunctions.First() is RandomScoreFunction);
            Assert.AreEqual(11111, (query.ScoreFunctions.First() as RandomScoreFunction).Seed);
        }

        [TestMethod]
        public void FAIL_CreateQuery_Query()
        {
            try
            {
                FunctionScoreQuery query = new FunctionScoreQuery(
                null,
                new List<ScoreFunctionBase>() { new RandomScoreFunction(11111) });
                Assert.Fail();
            }
            catch (ArgumentNullException argEx)
            {
                Assert.AreEqual("searchPiece", argEx.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateQuery_ScoreFunction()
        {
            try
            {
                FunctionScoreQuery query = new FunctionScoreQuery(
                new TermQuery("field", "value"),
                null);
                Assert.Fail();
            }
            catch (ArgumentNullException argEx)
            {
                Assert.AreEqual("scoreFunctions", argEx.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            FunctionScoreQuery query = new FunctionScoreQuery(
                new TermQuery("field", "value"),
                new List<ScoreFunctionBase>() { new RandomScoreFunction(11111) });

            string json = JsonConvert.SerializeObject(query);

            Assert.IsNotNull(json);

            string expectedJson = "{\"function_score\":{\"query\":{\"term\":{\"field\":\"value\"}},\"random_score\":{\"seed\":11111}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"function_score\":{\"query\":{\"term\":{\"field\":\"value\"}},\"random_score\":{\"seed\":11111}}}";
            FunctionScoreQuery query = JsonConvert.DeserializeObject<FunctionScoreQuery>(json);

            Assert.IsNotNull(query);
            Assert.IsTrue(query.Query is TermQuery);
            Assert.AreEqual("field", (query.Query as TermQuery).Field);
            Assert.AreEqual("value", (query.Query as TermQuery).Value.ToString());
            Assert.AreEqual(1, query.ScoreFunctions.Count());
            Assert.IsTrue(query.ScoreFunctions.First() is RandomScoreFunction);
            Assert.AreEqual(11111, (query.ScoreFunctions.First() as RandomScoreFunction).Seed);
        }
    }
}
