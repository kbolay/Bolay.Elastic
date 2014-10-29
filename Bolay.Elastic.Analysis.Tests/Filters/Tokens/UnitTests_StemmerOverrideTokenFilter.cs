using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Filters.Tokens.StemmerOverride;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bolay.Elastic.Analysis.Tests.Filters.Tokens
{
    [TestClass]
    public class UnitTests_StemmerOverrideTokenFilter
    {
        [TestMethod]
        public void PASS_Create()
        {
            StemmerOverrideTokenFilter filter = new StemmerOverrideTokenFilter("name")
            {
                Version = 2.4,
                Rules = new List<string>() { "rule1", "rule2" },
                RulesPath = "path/rules"
            };

            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual(2.4, filter.Version);
            Assert.AreEqual("rule1", filter.Rules.First());
            Assert.AreEqual("rule2", filter.Rules.Last());
            Assert.AreEqual("path/rules", filter.RulesPath);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            StemmerOverrideTokenFilter filter = new StemmerOverrideTokenFilter("name")
            {
                Version = 2.4,
                Rules = new List<string>() { "rule1", "rule2" },
                RulesPath = "path/rules"
            };

            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"stemmer_override\",\"version\":2.4,\"rules\":[\"rule1\",\"rule2\"],\"rules_path\":\"path/rules\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"stemmer_override\",\"version\":2.4,\"rules\":[\"rule1\",\"rule2\"],\"rules_path\":\"path/rules\"}}";
            StemmerOverrideTokenFilter filter = JsonConvert.DeserializeObject<StemmerOverrideTokenFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual(2.4, filter.Version);
            Assert.AreEqual("rule1", filter.Rules.First());
            Assert.AreEqual("rule2", filter.Rules.Last());
            Assert.AreEqual("path/rules", filter.RulesPath);
        }
    }
}
