using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Filters.Tokens.CompoundWord;
using Newtonsoft.Json;

namespace Bolay.Elastic.Analysis.Tests.Filters.Tokens
{
    [TestClass]
    public class UnitTests_CompoundWordFilterToken
    {
        [TestMethod]
        public void PASS_Create()
        {
            DictionaryDecompounderTokenFilter filter = new DictionaryDecompounderTokenFilter("name", "path");

            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual("path", filter.WordListPath);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            DictionaryDecompounderTokenFilter filter = new DictionaryDecompounderTokenFilter("name", "path");

            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"dictionary_decompounder\",\"word_list_path\":\"path\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"dictionary_decompounder\",\"word_list_path\":\"path\"}}";
            DictionaryDecompounderTokenFilter filter = JsonConvert.DeserializeObject<DictionaryDecompounderTokenFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual("path", filter.WordListPath);
        }
    }
}
