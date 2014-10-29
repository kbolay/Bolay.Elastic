using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Tokenizers.UaxEmailUrl;
using Newtonsoft.Json;

namespace Bolay.Elastic.Analysis.Tests.Tokenizers
{
    [TestClass]
    public class UnitTests_UaxEmailUrlTokenizer
    {
        [TestMethod]
        public void PASS_Create()
        {
            UaxEmailUrlTokenizer token = new UaxEmailUrlTokenizer("name")
            {
                MaximumTokenLength = 31
            };

            Assert.IsNotNull(token);
            Assert.AreEqual("name", token.Name);
            Assert.AreEqual((int)31, token.MaximumTokenLength);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            UaxEmailUrlTokenizer token = new UaxEmailUrlTokenizer("name")
            {
                MaximumTokenLength = 31
            };

            string json = JsonConvert.SerializeObject(token);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"uax_url_email\",\"max_token_length\":31}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"uax_url_email\",\"max_token_length\":31}}";
            UaxEmailUrlTokenizer token = JsonConvert.DeserializeObject<UaxEmailUrlTokenizer>(json);

            Assert.IsNotNull(token);
            Assert.AreEqual("name", token.Name);
            Assert.AreEqual((int)31, token.MaximumTokenLength);
        }
    }
}
