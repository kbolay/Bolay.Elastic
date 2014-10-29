using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Filters.Tokens.Synonym;
using Newtonsoft.Json;

namespace Bolay.Elastic.Analysis.Tests.Filters.Tokens
{
    [TestClass]
    public class UnitTests_SynonymTokenFilter
    {
        [TestMethod]
        public void PASS_Create()
        {
            SynonymTokenFilter filter = new SynonymTokenFilter("name", "path")
            {
                Version = 2.4
            };

            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual(2.4, filter.Version);
            Assert.AreEqual("path", filter.SynonymsPath);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            SynonymTokenFilter filter = new SynonymTokenFilter("name", "path")
            {
                Version = 2.4
            };

            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"synonym\",\"version\":2.4,\"synonyms_path\":\"path\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"synonym\",\"version\":2.4,\"synonyms_path\":\"path\"}}";
            SynonymTokenFilter filter = JsonConvert.DeserializeObject<SynonymTokenFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual(2.4, filter.Version);
            Assert.AreEqual("path", filter.SynonymsPath);
        }
    }
}
