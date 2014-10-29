using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Mapping.Types.Numbers.TokenCount;
using Newtonsoft.Json;

namespace Bolay.Elastic.Mapping.Tests.Types.Core
{
    [TestClass]
    public class UnitTests_TokenCount
    {
        [TestMethod]
        public void PASS_Create()
        {
            TokenCountProperty prop = new TokenCountProperty("tokencountme", "count-analyzer");

            Assert.IsNotNull(prop);
            Assert.AreEqual("tokencountme", prop.Name);
            Assert.AreEqual("count-analyzer", prop.Analyzer);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            TokenCountProperty prop = new TokenCountProperty("tokencountme", "count-analyzer");
            string json = JsonConvert.SerializeObject(prop);
            Assert.IsNotNull(json);

            string expectedJson = "{\"tokencountme\":{\"type\":\"token_count\",\"analyzer\":\"count-analyzer\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"tokencountme\":{\"type\":\"token_count\",\"analyzer\":\"count-analyzer\"}}";
            TokenCountProperty prop = JsonConvert.DeserializeObject<TokenCountProperty>(json);
            Assert.IsNotNull(prop);
            Assert.AreEqual("tokencountme", prop.Name);
            Assert.AreEqual("count-analyzer", prop.Analyzer);
        }
    }
}
