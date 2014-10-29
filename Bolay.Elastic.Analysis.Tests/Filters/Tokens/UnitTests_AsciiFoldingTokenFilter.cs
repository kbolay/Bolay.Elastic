using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Filters.Tokens.AsciiFolding;
using Newtonsoft.Json;

namespace Bolay.Elastic.Analysis.Tests.Filters.Tokens
{
    [TestClass]
    public class UnitTests_AsciiFoldingTokenFilter
    {
        [TestMethod]
        public void PASS_Create()
        {
            AsciiFoldingTokenFilter filter = new AsciiFoldingTokenFilter("name")
            {
                Version = 2.4,
                PreserveOriginal = true
            };

            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual(2.4, filter.Version);
            Assert.AreEqual(true, filter.PreserveOriginal);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            AsciiFoldingTokenFilter filter = new AsciiFoldingTokenFilter("name")
            {
                Version = 2.4,
                PreserveOriginal = true
            };

            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"asciifolding\",\"version\":2.4,\"preserve_original\":true}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"asciifolding\",\"version\":2.4,\"preserve_original\":true}}";
            AsciiFoldingTokenFilter filter = JsonConvert.DeserializeObject<AsciiFoldingTokenFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual(2.4, filter.Version);
            Assert.AreEqual(true, filter.PreserveOriginal);
        }
    }
}
