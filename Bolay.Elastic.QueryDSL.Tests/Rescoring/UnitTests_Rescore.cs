using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Rescoring;
using System.Collections.Generic;
using Bolay.Elastic.QueryDSL.Queries.Term;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Rescoring
{
    [TestClass]
    public class UnitTests_Rescore
    {
        [TestMethod]
        public void PASS_Create()
        {
            Rescore rescore = new Rescore(new List<RescoreQuery>()
                {
                    new RescoreQuery(new TermQuery("field", "value"))
                });
            Assert.IsNotNull(rescore);
            Assert.AreEqual(1, rescore.RescoreQueries.Count());
            Assert.IsTrue(rescore.RescoreQueries.First().Query is TermQuery);
            Assert.AreEqual("field", (rescore.RescoreQueries.First().Query as TermQuery).Field);
            Assert.AreEqual("value", (rescore.RescoreQueries.First().Query as TermQuery).Value);
        }

        [TestMethod]
        public void FAIL_Create()
        {
            try
            {
                Rescore rescore = new Rescore(null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("rescoreQueries", ex.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            Rescore rescore = new Rescore(new List<RescoreQuery>()
                {
                    new RescoreQuery(new TermQuery("field", "value"))
                    {
                        QueryWeight = 0.7,
                        RescoreQueryWeight = 1.3,
                        ScoreMode = ScoreModeEnum.Maximum,
                        WindowSize = 10
                    }
                });

            string json = JsonConvert.SerializeObject(rescore);
            Assert.IsNotNull(json);
            string expectedJson = "{\"window_size\":10,\"query\":{\"score_mode\":\"max\",\"rescore_query\":{\"term\":{\"field\":\"value\"}},\"query_weight\":0.7,\"rescore_query_weight\":1.3}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"window_size\":10,\"query\":{\"score_mode\":\"max\",\"rescore_query\":{\"term\":{\"field\":\"value\"}},\"query_weight\":0.7,\"rescore_query_weight\":1.3}}";
            Rescore rescore = JsonConvert.DeserializeObject<Rescore>(json);
            Assert.IsNotNull(rescore);
            Assert.AreEqual(1, rescore.RescoreQueries.Count());
            Assert.IsTrue(rescore.RescoreQueries.First().Query is TermQuery);
            Assert.AreEqual("field", (rescore.RescoreQueries.First().Query as TermQuery).Field);
            Assert.AreEqual("value", (rescore.RescoreQueries.First().Query as TermQuery).Value);
            Assert.AreEqual((int)10, (int)rescore.RescoreQueries.First().WindowSize);
            Assert.AreEqual(0.7, rescore.RescoreQueries.First().QueryWeight);
            Assert.AreEqual(1.3, rescore.RescoreQueries.First().RescoreQueryWeight);
            Assert.AreEqual(ScoreModeEnum.Maximum, rescore.RescoreQueries.First().ScoreMode);
        }

        [TestMethod]
        public void PASS_Serialize_List()
        {
            Rescore rescore = new Rescore(new List<RescoreQuery>()
                {
                    new RescoreQuery(new TermQuery("field", "value"))
                    {
                        QueryWeight = 0.7,
                        RescoreQueryWeight = 1.3,
                        ScoreMode = ScoreModeEnum.Maximum,
                        WindowSize = 10
                    },
                    new RescoreQuery(new TermQuery("field1", "value1"))
                });

            string json = JsonConvert.SerializeObject(rescore);
            Assert.IsNotNull(json);
            string expectedJson = "[{\"window_size\":10,\"query\":{\"score_mode\":\"max\",\"rescore_query\":{\"term\":{\"field\":\"value\"}},\"query_weight\":0.7,\"rescore_query_weight\":1.3}},{\"query\":{\"rescore_query\":{\"term\":{\"field1\":\"value1\"}}}}]";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_List()
        {
            string json = "[{\"window_size\":10,\"query\":{\"score_mode\":\"max\",\"rescore_query\":{\"term\":{\"field\":\"value\"}},\"query_weight\":0.7,\"rescore_query_weight\":1.3}},{\"query\":{\"rescore_query\":{\"term\":{\"field1\":\"value1\"}}}}]";
            Rescore rescore = JsonConvert.DeserializeObject<Rescore>(json);
            Assert.IsNotNull(rescore);
            Assert.AreEqual(2, rescore.RescoreQueries.Count());
            Assert.IsTrue(rescore.RescoreQueries.First().Query is TermQuery);
            Assert.AreEqual("field", (rescore.RescoreQueries.First().Query as TermQuery).Field);
            Assert.AreEqual("value", (rescore.RescoreQueries.First().Query as TermQuery).Value);
            Assert.AreEqual((int)10, (int)rescore.RescoreQueries.First().WindowSize);
            Assert.AreEqual(0.7, rescore.RescoreQueries.First().QueryWeight);
            Assert.AreEqual(1.3, rescore.RescoreQueries.First().RescoreQueryWeight);
            Assert.AreEqual(ScoreModeEnum.Maximum, rescore.RescoreQueries.First().ScoreMode);

            Assert.IsTrue(rescore.RescoreQueries.Last().Query is TermQuery);
            Assert.AreEqual("field1", (rescore.RescoreQueries.Last().Query as TermQuery).Field);
            Assert.AreEqual("value1", (rescore.RescoreQueries.Last().Query as TermQuery).Value);
        }
    }
}
