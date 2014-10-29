using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Filters.Tokens;
using Newtonsoft.Json;
using Bolay.Elastic.Analysis.Filters.Tokens.KStem;

namespace Bolay.Elastic.Analysis.Tests.Filters.Tokens
{
    [TestClass]
    public class UnitTests_KStemFilterToken
    {
        [TestMethod]
        public void PASS_Create()
        {
            KStemTokenFilter filter = new KStemTokenFilter("name")
            {
                Version = 2.4
            };

            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual(2.4, filter.Version);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            KStemTokenFilter filter = new KStemTokenFilter("name")
            {
                Version = 2.4
            };

            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"kstem\",\"version\":2.4}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"kstem\",\"version\":2.4}}";
            KStemTokenFilter filter = JsonConvert.DeserializeObject<KStemTokenFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual(2.4, filter.Version);
        }
    }
}
