using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Filters.Tokens.Trim;
using Newtonsoft.Json;

namespace Bolay.Elastic.Analysis.Tests.Filters.Tokens
{
    [TestClass]
    public class UnitTests_TrimTokenFilter
    {
        [TestMethod]
        public void PASS_Create()
        {
            TrimTokenFilter filter = new TrimTokenFilter("name")
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
            TrimTokenFilter filter = new TrimTokenFilter("name")
            {
                Version = 2.4
            };

            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"trim\",\"version\":2.4}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"trim\",\"version\":2.4}}";
            TrimTokenFilter filter = JsonConvert.DeserializeObject<TrimTokenFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual(2.4, filter.Version);
        }
    }
}
