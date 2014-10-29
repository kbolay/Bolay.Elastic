using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Tokenizers.Letter;
using Newtonsoft.Json;

namespace Bolay.Elastic.Analysis.Tests.Tokenizers
{
    [TestClass]
    public class UnitTests_LetterTokenizer
    {
        [TestMethod]
        public void PASS_Create()
        {
            LetterTokenizer token = new LetterTokenizer("name");

            Assert.IsNotNull(token);
            Assert.AreEqual("name", token.Name);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            LetterTokenizer token = new LetterTokenizer("name");

            string json = JsonConvert.SerializeObject(token);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"letter\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"letter\"}}";
            LetterTokenizer token = JsonConvert.DeserializeObject<LetterTokenizer>(json);

            Assert.IsNotNull(token);
            Assert.AreEqual("name", token.Name);
        }
    }
}
