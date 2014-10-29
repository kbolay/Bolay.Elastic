using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Analyzers.Standard;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bolay.Elastic.Analysis.Tests.Analyzers
{
    [TestClass]
    public class UnitTests_StandardAnalyzer
    {
        [TestMethod]
        public void PASS_Create()
        {
            StandardAnalyzer analyzer = new StandardAnalyzer("name")
            {
                Version = 4.6,
                MaximumTokenLength = 25,
                Stopwords = new List<string>() { "stop1", "stop2" },
                Aliases = new List<string>() { "alias1", "alias2" }
            };

            Assert.IsNotNull(analyzer);
            Assert.AreEqual("name", analyzer.Name);
            Assert.AreEqual(4.6, analyzer.Version);
            Assert.AreEqual(25, analyzer.MaximumTokenLength);
            Assert.AreEqual("stop1", analyzer.Stopwords.First());
            Assert.AreEqual("stop2", analyzer.Stopwords.Last());
            Assert.AreEqual("alias1", analyzer.Aliases.First());
            Assert.AreEqual("alias2", analyzer.Aliases.Last());
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            StandardAnalyzer analyzer = new StandardAnalyzer("name")
            {
                Version = 4.6,
                MaximumTokenLength = 25,
                Stopwords = new List<string>() { "stop1", "stop2" },
                Aliases = new List<string>() { "alias1", "alias2" }
            };

            string json = JsonConvert.SerializeObject(analyzer);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"standard\",\"version\":4.6,\"aliases\":[\"alias1\",\"alias2\"],\"stopwords\":[\"stop1\",\"stop2\"],\"max_token_length\":25}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"standard\",\"version\":4.6,\"aliases\":[\"alias1\",\"alias2\"],\"stopwords\":[\"stop1\",\"stop2\"],\"max_token_length\":25}}";

            StandardAnalyzer analyzer = JsonConvert.DeserializeObject<StandardAnalyzer>(json);
            Assert.IsNotNull(analyzer);
            Assert.AreEqual("name", analyzer.Name);
            Assert.AreEqual(4.6, analyzer.Version);
            Assert.AreEqual(25, analyzer.MaximumTokenLength);
            Assert.AreEqual("stop1", analyzer.Stopwords.First());
            Assert.AreEqual("stop2", analyzer.Stopwords.Last());
            Assert.AreEqual("alias1", analyzer.Aliases.First());
            Assert.AreEqual("alias2", analyzer.Aliases.Last());
        }
    }
}
