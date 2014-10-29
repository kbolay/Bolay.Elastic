using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Filters.Tokens.Lowercase;
using Newtonsoft.Json;

namespace Bolay.Elastic.Analysis.Tests.Filters.Tokens
{
    [TestClass]
    public class UnitTests_LowercaseTokenFilter
    {
        [TestMethod]
        public void PASS_Create()
        {
            LowercaseTokenFilter filter = new LowercaseTokenFilter("name")
            {
                Language = LowercaseSupportedLanguageEnum.Greek
            };

            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual(LowercaseSupportedLanguageEnum.Greek, filter.Language);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            LowercaseTokenFilter filter = new LowercaseTokenFilter("name")
            {
                Language = LowercaseSupportedLanguageEnum.Greek
            };

            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"lowercase\",\"language\":\"Greek\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"lowercase\",\"language\":\"Greek\"}}";
            LowercaseTokenFilter filter = JsonConvert.DeserializeObject<LowercaseTokenFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual(LowercaseSupportedLanguageEnum.Greek, filter.Language);
        }
    }
}
