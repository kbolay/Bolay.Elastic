using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Bolay.Elastic.Analysis.Filters.Tokens.KeywordRepeat;

namespace Bolay.Elastic.Analysis.Tests.Filters.Tokens
{
    [TestClass]
    public class UnitTests_KeywordRepeatTokenFilter
    {
        [TestMethod]
        public void PASS_Create()
        {
            KeywordRepeatTokenFilter filter = new KeywordRepeatTokenFilter("name")
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
            KeywordRepeatTokenFilter filter = new KeywordRepeatTokenFilter("name")
            {
                Version = 2.4
            };

            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"keyword_repeat\",\"version\":2.4}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"keyword_repeat\",\"version\":2.4}}";
            KeywordRepeatTokenFilter filter = JsonConvert.DeserializeObject<KeywordRepeatTokenFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual(2.4, filter.Version);
        }
    }
}
