using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Analyzers.Pattern;
using System.Collections.Generic;
using Bolay.Elastic.Models;
using Newtonsoft.Json;

namespace Bolay.Elastic.Analysis.Tests.Analyzers
{
    [TestClass]
    public class UnitTests_PatternAnalyzer
    {
        [TestMethod]
        public void PASS_Create()
        {
            PatternAnalyzer analyzer = new PatternAnalyzer("name", "pattern")
            {
                Version = 4.6,
                Lowercase = false,
                Flags = new List<RegexFlagEnum>(){ RegexFlagEnum.CaseInsensitive, RegexFlagEnum.CannonEq },
                Stopwords = new List<string>() { "stop1", "stop2" },
                Aliases = new List<string>() { "alias1", "alias2" }
            };

            Assert.IsNotNull(analyzer);
            Assert.AreEqual("name", analyzer.Name);
            Assert.AreEqual(4.6, analyzer.Version);
            Assert.AreEqual("pattern", analyzer.Pattern);
            Assert.AreEqual("stop1", analyzer.Stopwords.First());
            Assert.AreEqual("stop2", analyzer.Stopwords.Last());
            Assert.AreEqual("alias1", analyzer.Aliases.First());
            Assert.AreEqual("alias2", analyzer.Aliases.Last());
            Assert.AreEqual(RegexFlagEnum.CaseInsensitive, analyzer.Flags.First());
            Assert.AreEqual(RegexFlagEnum.CannonEq, analyzer.Flags.Last());
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            PatternAnalyzer analyzer = new PatternAnalyzer("name", "pattern")
            {
                Version = 4.6,
                Lowercase = false,
                Flags = new List<RegexFlagEnum>() { RegexFlagEnum.CaseInsensitive, RegexFlagEnum.CannonEq },
                Stopwords = new List<string>() { "stop1", "stop2" },
                Aliases = new List<string>() { "alias1", "alias2" }
            };

            string json = JsonConvert.SerializeObject(analyzer);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"pattern\",\"version\":4.6,\"aliases\":[\"alias1\",\"alias2\"],\"lowercase\":false,\"pattern\":\"pattern\",\"flags\":\"CASE_INSENSITIVE|CANNON_EQ\",\"stopwords\":[\"stop1\",\"stop2\"]}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"pattern\",\"version\":4.6,\"aliases\":[\"alias1\",\"alias2\"],\"lowercase\":false,\"pattern\":\"pattern\",\"flags\":\"CASE_INSENSITIVE|CANNON_EQ\",\"stopwords\":[\"stop1\",\"stop2\"]}}";

            PatternAnalyzer analyzer = JsonConvert.DeserializeObject<PatternAnalyzer>(json);
            Assert.IsNotNull(analyzer);
            Assert.AreEqual("name", analyzer.Name);
            Assert.AreEqual(4.6, analyzer.Version);
            Assert.AreEqual("pattern", analyzer.Pattern);
            Assert.AreEqual("stop1", analyzer.Stopwords.First());
            Assert.AreEqual("stop2", analyzer.Stopwords.Last());
            Assert.AreEqual("alias1", analyzer.Aliases.First());
            Assert.AreEqual("alias2", analyzer.Aliases.Last());
            Assert.AreEqual(RegexFlagEnum.CaseInsensitive, analyzer.Flags.First());
            Assert.AreEqual(RegexFlagEnum.CannonEq, analyzer.Flags.Last());
        }
    }
}
