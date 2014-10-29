﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Tokenizers.Standard;
using Newtonsoft.Json;

namespace Bolay.Elastic.Analysis.Tests.Tokenizers
{
    [TestClass]
    public class UnitTests_StandardTokenizer
    {
        [TestMethod]
        public void PASS_Create()
        {
            StandardTokenizer token = new StandardTokenizer("name")
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
            StandardTokenizer token = new StandardTokenizer("name")
            {
                MaximumTokenLength = 31
            };

            string json = JsonConvert.SerializeObject(token);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"standard\",\"max_token_length\":31}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"standard\",\"max_token_length\":31}}";
            StandardTokenizer token = JsonConvert.DeserializeObject<StandardTokenizer>(json);

            Assert.IsNotNull(token);
            Assert.AreEqual("name", token.Name);
            Assert.AreEqual((int)31, token.MaximumTokenLength);
        }
    }
}
