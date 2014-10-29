using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Filters.Tokens.PatternReplace;
using Newtonsoft.Json;

namespace Bolay.Elastic.Analysis.Tests.Filters.Tokens
{
    [TestClass]
    public class UnitTests_PatternReplaceTokenFilter
    {
        [TestMethod]
        public void PASS_Create()
        {
            PatternReplaceTokenFilter filter = new PatternReplaceTokenFilter("name", "pattern", "replace")
            {
                Version = 2.4
            };

            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual(2.4, filter.Version);
            Assert.AreEqual("pattern", filter.Pattern);
            Assert.AreEqual("replace", filter.Replacement);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            PatternReplaceTokenFilter filter = new PatternReplaceTokenFilter("name", "pattern", "replace")
            {
                Version = 2.4
            };

            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"pattern_replace\",\"version\":2.4,\"pattern\":\"pattern\",\"replacement\":\"replace\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"pattern_replace\",\"version\":2.4,\"pattern\":\"pattern\",\"replacement\":\"replace\"}}";
            PatternReplaceTokenFilter filter = JsonConvert.DeserializeObject<PatternReplaceTokenFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual(2.4, filter.Version);
            Assert.AreEqual("pattern", filter.Pattern);
            Assert.AreEqual("replace", filter.Replacement);
        }
    }
}
