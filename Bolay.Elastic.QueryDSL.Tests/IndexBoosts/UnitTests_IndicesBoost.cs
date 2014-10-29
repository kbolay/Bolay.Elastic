using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.IndexBoosts;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.IndexBoosts
{
    [TestClass]
    public class UnitTests_IndicesBoost
    {
        [TestMethod]
        public void PASS_Create()
        {
            IndicesBoost boost = new IndicesBoost(new List<IndexBoost>()
                {
                    new IndexBoost("index", 1.1)
                });

            Assert.IsNotNull(boost);
            Assert.AreEqual(1, boost.BoostedIndices.Count());
            Assert.AreEqual("index", boost.BoostedIndices.First().Index);
            Assert.AreEqual(1.1, boost.BoostedIndices.First().Boost);
        }

        [TestMethod]
        public void FAIL_Create()
        {
            try
            {
                IndicesBoost boost = new IndicesBoost(new List<IndexBoost>());
                Assert.Fail();
            }
            catch(ArgumentNullException ex)
            {
                Assert.AreEqual("boostedIndices", ex.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            IndicesBoost boost = new IndicesBoost(new List<IndexBoost>()
                {
                    new IndexBoost("index", 1.1)
                });

            string json = JsonConvert.SerializeObject(boost);
            Assert.IsNotNull(json);
            string expectedJson = "{\"indices_boost\":{\"index\":1.1}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"indices_boost\":{\"index\":1.1}}";
            IndicesBoost boost = JsonConvert.DeserializeObject<IndicesBoost>(json);
            Assert.IsNotNull(boost);
            Assert.AreEqual(1, boost.BoostedIndices.Count());
            Assert.AreEqual("index", boost.BoostedIndices.First().Index);
            Assert.AreEqual(1.1, boost.BoostedIndices.First().Boost);
        }
    }
}
