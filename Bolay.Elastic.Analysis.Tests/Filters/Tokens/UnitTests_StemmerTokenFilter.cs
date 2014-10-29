using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Bolay.Elastic.Analysis.Filters.Tokens.Stemmer;

namespace Bolay.Elastic.Analysis.Tests.Filters.Tokens
{
    [TestClass]
    public class UnitTests_StemmerTokenFilter
    {
        [TestMethod]
        public void PASS_Create()
        {
            StemmerTokenFilter filter = new StemmerTokenFilter("name", StemmerLanguageEnum.MinimalEnglish)
            {
                Version = 2.4
            };

            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual(2.4, filter.Version);
            Assert.AreEqual(StemmerLanguageEnum.MinimalEnglish, filter.Language);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            StemmerTokenFilter filter = new StemmerTokenFilter("name", StemmerLanguageEnum.MinimalEnglish)
            {
                Version = 2.4
            };

            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"stemmer\",\"version\":2.4,\"language\":\"minimal_english\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"stemmer\",\"version\":2.4,\"language\":\"minimal_english\"}}";
            StemmerTokenFilter filter = JsonConvert.DeserializeObject<StemmerTokenFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual(2.4, filter.Version);
            Assert.AreEqual(StemmerLanguageEnum.MinimalEnglish, filter.Language);
        }
    }
}
