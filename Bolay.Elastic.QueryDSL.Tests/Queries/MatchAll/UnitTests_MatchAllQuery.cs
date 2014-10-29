using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Queries.MatchAll;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Queries.MatchAll
{
    [TestClass]
    public class UnitTests_MatchAllQuery
    {
        [TestMethod]
        public void PASS_CreateQuery()
        {
            MatchAllQuery query = new MatchAllQuery()
            {
                Boost = 1.2
            };

            Assert.IsNotNull(query);
            Assert.AreEqual(1.2, query.Boost);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            MatchAllQuery query = new MatchAllQuery()
            {
                Boost = 1.2
            };

            string json = JsonConvert.SerializeObject(query);
            Assert.IsNotNull(json);

            string expectedJson = "{\"match_all\":{\"boost\":1.2}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"match_all\":{\"boost\":1.2}}";
            MatchAllQuery query = JsonConvert.DeserializeObject<MatchAllQuery>(json);
            Assert.IsNotNull(query);
            Assert.AreEqual(1.2, query.Boost);
        }
    }
}
