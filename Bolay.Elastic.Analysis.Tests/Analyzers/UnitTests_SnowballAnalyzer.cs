﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Analyzers.Snowball;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bolay.Elastic.Analysis.Tests.Analyzers
{
    [TestClass]
    public class UnitTests_SnowballAnalyzer
    {
        [TestMethod]
        public void PASS_Create()
        {
            SnowballAnalyzer analyzer = new SnowballAnalyzer("name")
            {
                Version = 4.6,
                Language = SnowballLanguageEnum.Finnish,
                Stopwords = new List<string>() { "stop1", "stop2" },
                Aliases = new List<string>() { "alias1", "alias2" },
                StopwordsPath = "path"
            };

            Assert.IsNotNull(analyzer);
            Assert.AreEqual("name", analyzer.Name);
            Assert.AreEqual(4.6, analyzer.Version);
            Assert.AreEqual("path", analyzer.StopwordsPath);
            Assert.AreEqual(SnowballLanguageEnum.Finnish, analyzer.Language);
            Assert.AreEqual("stop1", analyzer.Stopwords.First());
            Assert.AreEqual("stop2", analyzer.Stopwords.Last());
            Assert.AreEqual("alias1", analyzer.Aliases.First());
            Assert.AreEqual("alias2", analyzer.Aliases.Last());
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            SnowballAnalyzer analyzer = new SnowballAnalyzer("name")
            {
                Version = 4.6,
                Language = SnowballLanguageEnum.Finnish,
                Stopwords = new List<string>() { "stop1", "stop2" },
                Aliases = new List<string>() { "alias1", "alias2" },
                StopwordsPath = "path"
            };

            string json = JsonConvert.SerializeObject(analyzer);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"snowball\",\"version\":4.6,\"aliases\":[\"alias1\",\"alias2\"],\"language\":\"Finnish\",\"stopwords\":[\"stop1\",\"stop2\"],\"stopwords_path\":\"path\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"snowball\",\"version\":4.6,\"aliases\":[\"alias1\",\"alias2\"],\"language\":\"Finnish\",\"stopwords\":[\"stop1\",\"stop2\"],\"stopwords_path\":\"path\"}}";

            SnowballAnalyzer analyzer = JsonConvert.DeserializeObject<SnowballAnalyzer>(json);
            Assert.IsNotNull(analyzer);
            Assert.AreEqual("name", analyzer.Name);
            Assert.AreEqual(4.6, analyzer.Version);
            Assert.AreEqual("path", analyzer.StopwordsPath);
            Assert.AreEqual("stop1", analyzer.Stopwords.First());
            Assert.AreEqual("stop2", analyzer.Stopwords.Last());
            Assert.AreEqual("alias1", analyzer.Aliases.First());
            Assert.AreEqual("alias2", analyzer.Aliases.Last());
            Assert.AreEqual(SnowballLanguageEnum.Finnish, analyzer.Language);
        }

        [TestMethod]
        public void PASS_Serialize_No_Stopwords()
        {
            SnowballAnalyzer analyzer = new SnowballAnalyzer("name")
            {
                Version = 4.6,
                Language = SnowballLanguageEnum.Finnish,
                StopwordsPath = "path",
                Stopwords = new List<string>(),
                Aliases = new List<string>() { "alias1", "alias2" }
            };

            string json = JsonConvert.SerializeObject(analyzer);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"snowball\",\"version\":4.6,\"aliases\":[\"alias1\",\"alias2\"],\"language\":\"Finnish\",\"stopwords\":\"_none_\",\"stopwords_path\":\"path\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_No_Stopwords()
        {
            string json = "{\"name\":{\"type\":\"snowball\",\"version\":4.6,\"aliases\":[\"alias1\",\"alias2\"],\"language\":\"Finnish\",\"stopwords\":\"_none_\",\"stopwords_path\":\"path\"}}";

            SnowballAnalyzer analyzer = JsonConvert.DeserializeObject<SnowballAnalyzer>(json);
            Assert.IsNotNull(analyzer);
            Assert.AreEqual("name", analyzer.Name);
            Assert.AreEqual(4.6, analyzer.Version);
            Assert.AreEqual("path", analyzer.StopwordsPath);
            Assert.AreEqual(0, analyzer.Stopwords.Count());
            Assert.AreEqual("alias1", analyzer.Aliases.First());
            Assert.AreEqual("alias2", analyzer.Aliases.Last());
            Assert.AreEqual(SnowballLanguageEnum.Finnish, analyzer.Language);
        }
    }
}
