using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Analyzers.Simple;
using Newtonsoft.Json;

namespace Bolay.Elastic.Analysis.Tests.Analyzers
{
    [TestClass]
    public class UnitTests_SimpleAnalyzer
    {
        [TestMethod]
        public void PASS_Create()
        {
            SimpleAnalyzer analyzer = new SimpleAnalyzer("name")
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
            SimpleAnalyzer analyzer = new SimpleAnalyzer("name")
            {
                Version = 4.6,
            };

            string json = JsonConvert.SerializeObject(analyzer);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"simple\",\"version\":4.6}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"simple\",\"version\":4.6}}";

            SimpleAnalyzer analyzer = JsonConvert.DeserializeObject<SimpleAnalyzer>(json);
            Assert.IsNotNull(analyzer);
            Assert.AreEqual("name", analyzer.Name);
            Assert.AreEqual(4.6, analyzer.Version);
        }
    }
}
