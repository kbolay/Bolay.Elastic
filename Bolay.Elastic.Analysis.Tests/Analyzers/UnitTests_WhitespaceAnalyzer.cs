using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Analyzers.Whitespace;
using Newtonsoft.Json;

namespace Bolay.Elastic.Analysis.Tests.Analyzers
{
    [TestClass]
    public class UnitTests_WhitespaceAnalyzer
    {
        [TestMethod]
        public void PASS_Create()
        {
            WhitespaceAnalyzer analyzer = new WhitespaceAnalyzer("name")
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
            WhitespaceAnalyzer analyzer = new WhitespaceAnalyzer("name")
            {
                Version = 4.6,
            };

            string json = JsonConvert.SerializeObject(analyzer);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"whitespace\",\"version\":4.6}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"whitespace\",\"version\":4.6}}";

            WhitespaceAnalyzer analyzer = JsonConvert.DeserializeObject<WhitespaceAnalyzer>(json);
            Assert.IsNotNull(analyzer);
            Assert.AreEqual("name", analyzer.Name);
            Assert.AreEqual(4.6, analyzer.Version);
        }
    }
}
