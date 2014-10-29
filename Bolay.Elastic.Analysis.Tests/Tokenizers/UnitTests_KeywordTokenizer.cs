using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Tokenizers.Keyword;
using Newtonsoft.Json;

namespace Bolay.Elastic.Analysis.Tests.Tokenizers
{
    [TestClass]
    public class UnitTests_KeywordTokenizer
    {
        [TestMethod]
        public void PASS_Create()
        {
            KeywordTokenizer token = new KeywordTokenizer("name")
            {
                BufferSize = 31
            };

            Assert.IsNotNull(token);
            Assert.AreEqual("name", token.Name);
            Assert.AreEqual((Int64)31, token.BufferSize);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            KeywordTokenizer token = new KeywordTokenizer("name")
            {
                BufferSize = 31
            };

            string json = JsonConvert.SerializeObject(token);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"keyword\",\"buffer_size\":31}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"keyword\",\"buffer_size\":31}}";
            KeywordTokenizer token = JsonConvert.DeserializeObject<KeywordTokenizer>(json);

            Assert.IsNotNull(token);
            Assert.AreEqual("name", token.Name);
            Assert.AreEqual((Int64)31, token.BufferSize);
        }
    }
}
