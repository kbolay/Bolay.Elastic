using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Filters.Tokens.KeywordMarker;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bolay.Elastic.Analysis.Tests.Filters.Tokens
{
    [TestClass]
    public class UnitTests_KeywordMarkerTokenFilter
    {
        [TestMethod]
        public void PASS_Create()
        {
            KeywordMarkerTokenFilter filter = new KeywordMarkerTokenFilter("name")
            {
                Version = 2.4,
                IgnoreCase = true,
                Keywords = new List<string>() { "keyword1", "keyword2" },
                KeywordsPath = "path"
            };

            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual(2.4, filter.Version);
            Assert.AreEqual(true, filter.IgnoreCase);
            Assert.AreEqual("keyword1", filter.Keywords.First());
            Assert.AreEqual("keyword2", filter.Keywords.Last());
            Assert.AreEqual("path", filter.KeywordsPath);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            KeywordMarkerTokenFilter filter = new KeywordMarkerTokenFilter("name")
            {
                Version = 2.4,
                IgnoreCase = true,
                Keywords = new List<string>() { "keyword1", "keyword2" },
                KeywordsPath = "path"
            };

            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"keyword_marker\",\"version\":2.4,\"keywords\":[\"keyword1\",\"keyword2\"],\"keywords_path\":\"path\",\"ignore_case\":true}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"keyword_marker\",\"version\":2.4,\"keywords\":[\"keyword1\",\"keyword2\"],\"keywords_path\":\"path\",\"ignore_case\":true}}";
            KeywordMarkerTokenFilter filter = JsonConvert.DeserializeObject<KeywordMarkerTokenFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual(2.4, filter.Version);
            Assert.AreEqual(true, filter.IgnoreCase);
            Assert.AreEqual("keyword1", filter.Keywords.First());
            Assert.AreEqual("keyword2", filter.Keywords.Last());
            Assert.AreEqual("path", filter.KeywordsPath);
        }
    }
}
