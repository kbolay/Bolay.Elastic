using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Filters.Tokens.LimitTokenCount;
using Newtonsoft.Json;

namespace Bolay.Elastic.Analysis.Tests.Filters.Tokens
{
    [TestClass]
    public class UnitTests_LimitTokenCountTokenFilter
    {
        [TestMethod]
        public void PASS_Create()
        {
            LimitTokenCountTokenFilter filter = new LimitTokenCountTokenFilter("name")
            {
                Version = 2.4,
                MaximumTokenCount = 2,
                ConsumeAllTokens = true
            };

            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual(2.4, filter.Version);
            Assert.AreEqual((int)2, filter.MaximumTokenCount);
            Assert.AreEqual(true, filter.ConsumeAllTokens);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            LimitTokenCountTokenFilter filter = new LimitTokenCountTokenFilter("name")
            {
                Version = 2.4,
                MaximumTokenCount = 2,
                ConsumeAllTokens = true
            };

            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"limit\",\"version\":2.4,\"max_token_count\":2,\"consume_all_tokens\":true}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"limit\",\"version\":2.4,\"max_token_count\":2,\"consume_all_tokens\":true}}";
            LimitTokenCountTokenFilter filter = JsonConvert.DeserializeObject<LimitTokenCountTokenFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual(2.4, filter.Version);
            Assert.AreEqual((int)2, filter.MaximumTokenCount);
            Assert.AreEqual(true, filter.ConsumeAllTokens);
        }
    }
}
