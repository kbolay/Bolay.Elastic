using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Filters.Tokens.KeepWords;
using Newtonsoft.Json;

namespace Bolay.Elastic.Analysis.Tests.Filters.Tokens
{
    [TestClass]
    public class UnitTests_KeepWordsTokenFilter
    {
        [TestMethod]
        public void PASS_Create()
        {
            KeepWordsTokenFilter filter = new KeepWordsTokenFilter("name", "path") 
            { 
                Lowercase = true
            };
            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual("path", filter.KeepWordsPath);
            Assert.AreEqual(true, filter.Lowercase);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            KeepWordsTokenFilter filter = new KeepWordsTokenFilter("name", "path")
            {
                Lowercase = true
            };

            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"keep\",\"keep_words_case\":true,\"keep_words_path\":\"path\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"keep\",\"keep_words_case\":true,\"keep_words_path\":\"path\"}}";
            KeepWordsTokenFilter filter = JsonConvert.DeserializeObject<KeepWordsTokenFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual("path", filter.KeepWordsPath);
            Assert.AreEqual(true, filter.Lowercase);
        }
    }
}
