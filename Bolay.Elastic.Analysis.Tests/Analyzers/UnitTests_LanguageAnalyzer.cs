using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Analyzers.Language;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bolay.Elastic.Analysis.Tests.Analyzers
{
    [TestClass]
    public class UnitTests_LanguageAnalyzer
    {
        [TestMethod]
        public void PASS_Create_English()
        {
            EnglishAnalyzer analyzer = new EnglishAnalyzer("name")
            {
                Aliases = new List<string>() { "alias1", "alias2" },
                StemExclusions = new List<string>() { "stem1", "stem2" },
                Stopwords = new List<string>() { "stop1", "stop2" },
                StopwordsPath = "path",
                Version = 4.6
            };

            Assert.IsNotNull(analyzer);
            Assert.AreEqual("name", analyzer.Name);
            Assert.AreEqual("alias1", analyzer.Aliases.First());
            Assert.AreEqual("alias2", analyzer.Aliases.Last());
            Assert.AreEqual("stem1", analyzer.StemExclusions.First());
            Assert.AreEqual("stem2", analyzer.StemExclusions.Last());
            Assert.AreEqual("stop1", analyzer.Stopwords.First());
            Assert.AreEqual("stop2", analyzer.Stopwords.Last());
            Assert.AreEqual("path", analyzer.StopwordsPath);
            Assert.AreEqual(4.6, analyzer.Version);
        }

        [TestMethod]
        public void PASS_Serialize_English()
        {
            EnglishAnalyzer analyzer = new EnglishAnalyzer("name")
            {
                Aliases = new List<string>() { "alias1", "alias2" },
                StemExclusions = new List<string>() { "stem1", "stem2" },
                Stopwords = new List<string>() { "stop1", "stop2" },
                StopwordsPath = "path",
                Version = 4.6
            };

            string json = JsonConvert.SerializeObject(analyzer);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"english\",\"version\":4.6,\"aliases\":[\"alias1\",\"alias2\"],\"stopwords\":[\"stop1\",\"stop2\"],\"stopwords_path\":\"path\",\"stem_exclusion\":[\"stem1\",\"stem2\"]}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_English()
        {
            string json = "{\"name\":{\"type\":\"english\",\"version\":4.6,\"aliases\":[\"alias1\",\"alias2\"],\"stopwords\":[\"stop1\",\"stop2\"],\"stopwords_path\":\"path\",\"stem_exclusion\":[\"stem1\",\"stem2\"]}}";
            EnglishAnalyzer analyzer = JsonConvert.DeserializeObject<EnglishAnalyzer>(json);

            Assert.IsNotNull(analyzer);
            Assert.AreEqual("name", analyzer.Name);
            Assert.AreEqual("alias1", analyzer.Aliases.First());
            Assert.AreEqual("alias2", analyzer.Aliases.Last());
            Assert.AreEqual("stem1", analyzer.StemExclusions.First());
            Assert.AreEqual("stem2", analyzer.StemExclusions.Last());
            Assert.AreEqual("stop1", analyzer.Stopwords.First());
            Assert.AreEqual("stop2", analyzer.Stopwords.Last());
            Assert.AreEqual("path", analyzer.StopwordsPath);
            Assert.AreEqual(4.6, analyzer.Version);
        }
    }
}
