using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Filters.Tokens.Unique;
using Newtonsoft.Json;

namespace Bolay.Elastic.Analysis.Tests.Filters.Tokens
{
    [TestClass]
    public class UnitTests_UniqueTokenFilter
    {
        [TestMethod]
        public void PASS_Create()
        {
            UniqueTokenFilter filter = new UniqueTokenFilter("name")
            {
                Version = 2.4,
                OnlyOnSamePosition = true
            };

            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual(2.4, filter.Version);
            Assert.AreEqual(true, filter.OnlyOnSamePosition);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            UniqueTokenFilter filter = new UniqueTokenFilter("name")
            {
                Version = 2.4,
                OnlyOnSamePosition = true
            };

            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"unique\",\"version\":2.4,\"only_on_same_position\":true}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"unique\",\"version\":2.4,\"only_on_same_position\":true}}";
            UniqueTokenFilter filter = JsonConvert.DeserializeObject<UniqueTokenFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual(2.4, filter.Version);
            Assert.AreEqual(true, filter.OnlyOnSamePosition);
        }
    }
}
