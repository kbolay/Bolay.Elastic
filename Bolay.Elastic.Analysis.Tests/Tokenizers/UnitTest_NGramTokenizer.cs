using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Tokenizers.NGram;
using System.Collections.Generic;
using Bolay.Elastic.Analysis.Tokenizers;
using Newtonsoft.Json;

namespace Bolay.Elastic.Analysis.Tests.Tokenizers
{
    [TestClass]
    public class UnitTest_NGramTokenizer
    {
        [TestMethod]
        public void PASS_Create()
        {
            NGramTokenizer token = new NGramTokenizer("name")
            {
                MinimumSize = 3,
                MaximumSize = 12,
                TokenCharacters = new List<CharacterClassEnum>()
                {
                    CharacterClassEnum.Letter
                }
            };

            Assert.IsNotNull(token);
            Assert.AreEqual("name", token.Name);
            Assert.AreEqual((int)3, token.MinimumSize);
            Assert.AreEqual((int)12, token.MaximumSize);
            Assert.AreEqual(CharacterClassEnum.Letter, token.TokenCharacters.First());
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            NGramTokenizer token = new NGramTokenizer("name")
            {
                MinimumSize = 3,
                MaximumSize = 12,
                TokenCharacters = new List<CharacterClassEnum>()
                {
                    CharacterClassEnum.Letter
                }
            };

            string json = JsonConvert.SerializeObject(token);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"nGram\",\"min_gram\":3,\"max_gram\":12,\"token_chars\":[\"letter\"]}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"nGram\",\"min_gram\":3,\"max_gram\":12,\"token_chars\":[\"letter\"]}}";
            NGramTokenizer token = JsonConvert.DeserializeObject<NGramTokenizer>(json);

            Assert.IsNotNull(token);
            Assert.AreEqual("name", token.Name);
            Assert.AreEqual((int)3, token.MinimumSize);
            Assert.AreEqual((int)12, token.MaximumSize);
            Assert.AreEqual(CharacterClassEnum.Letter, token.TokenCharacters.First());
        }
    }
}
