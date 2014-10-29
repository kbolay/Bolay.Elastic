using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Queries.Scoring;
using Bolay.Elastic.QueryDSL.Filters.Term;
using Bolay.Elastic.QueryDSL.Queries.Scoring.Functions;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Queries.Scoring
{
    [TestClass]
    public class UnitTests_FunctionScoreFilter
    {
        [TestMethod]
        public void PASS_CreateQuery()
        {
            FunctionScoreFilter query = new FunctionScoreFilter(
                new TermFilter("field", "value"),
                new List<ScoreFunctionBase>() { new RandomScoreFunction(11111) });

            Assert.IsNotNull(query);
            Assert.IsTrue(query.Filter is TermFilter);
            Assert.AreEqual("field", (query.Filter as TermFilter).Field);
            Assert.AreEqual("value", (query.Filter as TermFilter).Value.ToString());
            Assert.AreEqual(1, query.ScoreFunctions.Count());
            Assert.IsTrue(query.ScoreFunctions.First() is RandomScoreFunction);
            Assert.AreEqual(11111, (query.ScoreFunctions.First() as RandomScoreFunction).Seed);
        }

        [TestMethod]
        public void FAIL_CreateQuery_Filter()
        {
            try
            {
                FunctionScoreFilter query = new FunctionScoreFilter(
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
                FunctionScoreFilter query = new FunctionScoreFilter(
                new TermFilter("field", "value"),
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
            FunctionScoreFilter query = new FunctionScoreFilter(
                new TermFilter("field", "value"),
                new List<ScoreFunctionBase>() { new RandomScoreFunction(11111) });

            string json = JsonConvert.SerializeObject(query);

            Assert.IsNotNull(json);

            string expectedJson = "{\"function_score\":{\"filter\":{\"term\":{\"field\":\"value\"}},\"random_score\":{\"seed\":11111}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"function_score\":{\"filter\":{\"term\":{\"field\":\"value\"}},\"random_score\":{\"seed\":11111}}}";
            FunctionScoreFilter query = JsonConvert.DeserializeObject<FunctionScoreFilter>(json);

            Assert.IsNotNull(query);
            Assert.IsTrue(query.Filter is TermFilter);
            Assert.AreEqual("field", (query.Filter as TermFilter).Field);
            Assert.AreEqual("value", (query.Filter as TermFilter).Value.ToString());
            Assert.AreEqual(1, query.ScoreFunctions.Count());
            Assert.IsTrue(query.ScoreFunctions.First() is RandomScoreFunction);
            Assert.AreEqual(11111, (query.ScoreFunctions.First() as RandomScoreFunction).Seed);
        }
    }
}
