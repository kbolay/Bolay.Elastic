using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Filters.Tokens.Snowball;
using Newtonsoft.Json;

namespace Bolay.Elastic.Analysis.Tests.Filters.Tokens
{
    [TestClass]
    public class UnitTests_SnowballTokenFilter
    {
        [TestMethod]
        public void PASS_Create()
        {
            SnowballTokenFilter filter = new SnowballTokenFilter("name", SnowballLanguageEnum.English)
            {
                Version = 2.4
            };

            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual(2.4, filter.Version);
            Assert.AreEqual(SnowballLanguageEnum.English, filter.Language);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            SnowballTokenFilter filter = new SnowballTokenFilter("name", SnowballLanguageEnum.English)
            {
                Version = 2.4
            };

            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"snowball\",\"version\":2.4,\"language\":\"English\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"snowball\",\"version\":2.4,\"language\":\"English\"}}";
            SnowballTokenFilter filter = JsonConvert.DeserializeObject<SnowballTokenFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual(2.4, filter.Version);
            Assert.AreEqual(SnowballLanguageEnum.English, filter.Language);
        }
    }
}
