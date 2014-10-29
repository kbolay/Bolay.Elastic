using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Tokenizers.Whitespace;
using Newtonsoft.Json;

namespace Bolay.Elastic.Analysis.Tests.Tokenizers
{
    [TestClass]
    public class UnitTests_WhitespaceTokenizer
    {
        [TestMethod]
        public void PASS_Create()
        {
            WhitespaceTokenizer token = new WhitespaceTokenizer("name");

            Assert.IsNotNull(token);
            Assert.AreEqual("name", token.Name);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            WhitespaceTokenizer token = new WhitespaceTokenizer("name");

            string json = JsonConvert.SerializeObject(token);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"whitespace\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"whitespace\"}}";
            WhitespaceTokenizer token = JsonConvert.DeserializeObject<WhitespaceTokenizer>(json);

            Assert.IsNotNull(token);
            Assert.AreEqual("name", token.Name);
        }
    }
}
