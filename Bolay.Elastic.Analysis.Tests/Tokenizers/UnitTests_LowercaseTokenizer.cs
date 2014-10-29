using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Tokenizers.Lowercase;
using Newtonsoft.Json;

namespace Bolay.Elastic.Analysis.Tests.Tokenizers
{
    [TestClass]
    public class UnitTests_LowercaseTokenizer
    {
        [TestMethod]
        public void PASS_Create()
        {
            LowercaseTokenizer token = new LowercaseTokenizer("name");

            Assert.IsNotNull(token);
            Assert.AreEqual("name", token.Name);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            LowercaseTokenizer token = new LowercaseTokenizer("name");

            string json = JsonConvert.SerializeObject(token);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"lowercase\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"lowercase\"}}";
            LowercaseTokenizer token = JsonConvert.DeserializeObject<LowercaseTokenizer>(json);

            Assert.IsNotNull(token);
            Assert.AreEqual("name", token.Name);
        }
    }
}
