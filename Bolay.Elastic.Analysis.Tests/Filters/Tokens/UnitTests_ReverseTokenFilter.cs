using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Filters.Tokens.Reverse;
using Newtonsoft.Json;

namespace Bolay.Elastic.Analysis.Tests.Filters.Tokens
{
    [TestClass]
    public class UnitTests_ReverseTokenFilter
    {
        [TestMethod]
        public void PASS_Create()
        {
            ReverseTokenFilter filter = new ReverseTokenFilter("name")
            {
                Version = 2.4
            };

            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual(2.4, filter.Version);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            ReverseTokenFilter filter = new ReverseTokenFilter("name")
            {
                Version = 2.4
            };

            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"reverse\",\"version\":2.4}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"reverse\",\"version\":2.4}}";
            ReverseTokenFilter filter = JsonConvert.DeserializeObject<ReverseTokenFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual(2.4, filter.Version);
        }
    }
}
