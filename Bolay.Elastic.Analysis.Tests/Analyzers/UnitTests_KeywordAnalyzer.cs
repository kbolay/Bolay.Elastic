using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Analyzers.Keyword;
using Newtonsoft.Json;

namespace Bolay.Elastic.Analysis.Tests.Analyzers
{
    [TestClass]
    public class UnitTests_KeywordAnalyzer
    {
        [TestMethod]
        public void PASS_Create()
        {
            KeywordAnalyzer analyzer = new KeywordAnalyzer("name")
            {
                Version = 4.6,
            };

            Assert.IsNotNull(analyzer);
            Assert.AreEqual("name", analyzer.Name);
            Assert.AreEqual(4.6, analyzer.Version);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            KeywordAnalyzer analyzer = new KeywordAnalyzer("name")
            {
                Version = 4.6,
            };

            string json = JsonConvert.SerializeObject(analyzer);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"keyword\",\"version\":4.6}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"keyword\",\"version\":4.6}}";

            KeywordAnalyzer analyzer = JsonConvert.DeserializeObject<KeywordAnalyzer>(json);
            Assert.IsNotNull(analyzer);
            Assert.AreEqual("name", analyzer.Name);
            Assert.AreEqual(4.6, analyzer.Version);
        }
    }
}
