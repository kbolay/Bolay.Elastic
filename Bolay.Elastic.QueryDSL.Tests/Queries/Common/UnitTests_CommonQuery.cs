using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Queries.Common;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Queries.Common
{
    [TestClass]
    public class UnitTests_CommonQuery
    {
        [TestMethod]
        public void PASS_CreateQuery()
        {
            CommonQuery query = new CommonQuery("the quick brown fox");
            Assert.IsNotNull(query);
        }

        [TestMethod]
        public void FAIL_CreateQuery_Query()
        {
            try
            {
                CommonQuery query = new CommonQuery(null);
                Assert.Fail();
            }
            catch (ArgumentNullException argEx)
            {
                Assert.AreEqual("query", argEx.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateQuery_CutOffFrequency()
        {
            try
            {
                CommonQuery query = new CommonQuery("the quick brown fox", 0);
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException argEx)
            {
                Assert.AreEqual("cutOffFrequency", argEx.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            CommonQuery query = new CommonQuery("the quick brown fox");

            string json = JsonConvert.SerializeObject(query);

            Assert.IsNotNull(json);

            string expectedJson = "{\"common\":{\"body\":{\"query\":\"the quick brown fox\",\"cutoff_frequency\":0.001}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"common\":{\"body\":{\"query\":\"the quick brown fox\",\"cutoff_frequency\":0.001}}}";

            CommonQuery query = JsonConvert.DeserializeObject<CommonQuery>(json);

            Assert.IsNotNull(query);
            Assert.AreEqual("the quick brown fox", query.Query);
            Assert.AreEqual(0.001, query.CutOffFrequency);
        }
    }
}
