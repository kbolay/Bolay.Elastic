using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Mapping.Fields._Analyzer;
using Newtonsoft.Json;

namespace Bolay.Elastic.Mapping.Tests.Fields
{
    [TestClass]
    public class UnitTests_DocumentAnalyzer
    {
        [TestMethod]
        public void PASS_Create()
        {
            DocumentAnalyzer analyzer = new DocumentAnalyzer("analyzer_path");
            Assert.IsNotNull(analyzer);
            Assert.AreEqual("analyzer_path", analyzer.Path);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            DocumentAnalyzer analyzer = new DocumentAnalyzer("analyzer_path");
            string json = JsonConvert.SerializeObject(analyzer);
            Assert.IsNotNull(json);

            string expectedJson = "{\"path\":\"analyzer_path\"}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"path\":\"analyzer_path\"}";
            DocumentAnalyzer analyzer = JsonConvert.DeserializeObject<DocumentAnalyzer>(json);
            Assert.IsNotNull(analyzer);
            Assert.AreEqual("analyzer_path", analyzer.Path);
        }
    }
}
