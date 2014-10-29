using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Filters.Tokens.Hunspell;
using Newtonsoft.Json;

namespace Bolay.Elastic.Analysis.Tests.Filters.Tokens
{
    [TestClass]
    public class UnitTests_HunspellTokenFilter
    {
        [TestMethod]
        public void PASS_Create()
        {
            HunspellTokenFilter filter = new HunspellTokenFilter("name", "locale")
            {
                Deduplicate = false,
                Dictionary = "dict/path",
                IgnoreCase = true,
                RecursionLevel = 1,
                StrictAffixParsing = false                
            };

            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual("locale", filter.Locale);
            Assert.AreEqual(false, filter.Deduplicate);
            Assert.AreEqual("dict/path", filter.Dictionary);
            Assert.AreEqual(true, filter.IgnoreCase);
            Assert.AreEqual((int)1, filter.RecursionLevel);
            Assert.AreEqual(false, filter.StrictAffixParsing);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            HunspellTokenFilter filter = new HunspellTokenFilter("name", "locale")
            {
                Deduplicate = false,
                Dictionary = "dict/path",
                IgnoreCase = true,
                RecursionLevel = 1,
                StrictAffixParsing = false
            };

            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"hunspell\",\"ignore_case\":true,\"strict_affix_parsing\":false,\"locale\":\"locale\",\"dictionary\":\"dict/path\",\"dedup\":false,\"recursion_level\":1}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"hunspell\",\"ignore_case\":true,\"strict_affix_parsing\":false,\"locale\":\"locale\",\"dictionary\":\"dict/path\",\"dedup\":false,\"recursion_level\":1}}";
            HunspellTokenFilter filter = JsonConvert.DeserializeObject<HunspellTokenFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual(false, filter.Deduplicate);
            Assert.AreEqual("dict/path", filter.Dictionary);
            Assert.AreEqual(true, filter.IgnoreCase);
            Assert.AreEqual((int)1, filter.RecursionLevel);
            Assert.AreEqual(false, filter.StrictAffixParsing);
        }
    }
}
