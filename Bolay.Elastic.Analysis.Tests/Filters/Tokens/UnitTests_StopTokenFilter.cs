using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Filters.Tokens.Stop;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bolay.Elastic.Analysis.Tests.Filters.Tokens
{
    [TestClass]
    public class UnitTests_StopTokenFilter
    {
        [TestMethod]
        public void PASS_Create()
        {
            StopTokenFilter filter = new StopTokenFilter("name")
            {
                Version = 2.4,
                IgnoreCase = true,
                RemoveTrailing = false,
                Stopwords = new List<string>() { "stop1", "stop2" },
                StopwordsPath = "stopword/path"
            };

            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual(2.4, filter.Version);
            Assert.AreEqual(true, filter.IgnoreCase);
            Assert.AreEqual(false, filter.RemoveTrailing);
            Assert.AreEqual("stop1", filter.Stopwords.First());
            Assert.AreEqual("stop2", filter.Stopwords.Last());
            Assert.AreEqual("stopword/path", filter.StopwordsPath);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            StopTokenFilter filter = new StopTokenFilter("name")
            {
                Version = 2.4,
                IgnoreCase = true,
                RemoveTrailing = false,
                Stopwords = new List<string>() { "stop1", "stop2" },
                StopwordsPath = "stopword/path"
            };

            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"stop\",\"version\":2.4,\"stopwords\":[\"stop1\",\"stop2\"],\"stopwords_path\":\"stopword/path\",\"ignore_case\":true,\"remove_trailing\":false}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"stop\",\"version\":2.4,\"stopwords\":[\"stop1\",\"stop2\"],\"stopwords_path\":\"stopword/path\",\"ignore_case\":true,\"remove_trailing\":false}}";
            StopTokenFilter filter = JsonConvert.DeserializeObject<StopTokenFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual(2.4, filter.Version);
            Assert.AreEqual(true, filter.IgnoreCase);
            Assert.AreEqual(false, filter.RemoveTrailing);
            Assert.AreEqual("stop1", filter.Stopwords.First());
            Assert.AreEqual("stop2", filter.Stopwords.Last());
            Assert.AreEqual("stopword/path", filter.StopwordsPath);
        }
    }
}
