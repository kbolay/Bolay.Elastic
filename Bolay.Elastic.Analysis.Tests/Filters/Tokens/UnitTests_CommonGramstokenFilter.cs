using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Filters.Tokens.CommonGrams;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bolay.Elastic.Analysis.Tests.Filters.Tokens
{
    [TestClass]
    public class UnitTests_CommonGramstokenFilter
    {
        [TestMethod]
        public void PASS_Create()
        {
            CommonGramsTokenFilter filter = new CommonGramsTokenFilter("name", new List<string>() { "word1", "word2" })
            {
                IgnoreCase = true,
                QueryMode = true
            };

            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual("word1", filter.CommonWords.First());
            Assert.AreEqual("word2", filter.CommonWords.Last());
            Assert.AreEqual(true, filter.IgnoreCase);
            Assert.AreEqual(true, filter.QueryMode);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            CommonGramsTokenFilter filter = new CommonGramsTokenFilter("name", new List<string>() { "word1", "word2" })
            {
                IgnoreCase = true,
                QueryMode = true
            };

            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"common_grams\",\"ignore_case\":true,\"query_mode\":true,\"common_words\":[\"word1\",\"word2\"]}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"common_grams\",\"ignore_case\":true,\"query_mode\":true,\"common_words\":[\"word1\",\"word2\"]}}";
            CommonGramsTokenFilter filter = JsonConvert.DeserializeObject<CommonGramsTokenFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual("word1", filter.CommonWords.First());
            Assert.AreEqual("word2", filter.CommonWords.Last());
            Assert.AreEqual(true, filter.IgnoreCase);
            Assert.AreEqual(true, filter.QueryMode);
        }
    }
}
