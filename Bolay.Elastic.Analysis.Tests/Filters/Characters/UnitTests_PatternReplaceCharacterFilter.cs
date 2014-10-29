using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Filters.Characters.PatternReplace;
using Newtonsoft.Json;

namespace Bolay.Elastic.Analysis.Tests.Filters.Characters
{
    [TestClass]
    public class UnitTests_PatternReplaceCharacterFilter
    {
        [TestMethod]
        public void PASS_Create()
        {
            PatternReplaceCharacterFilter filter = new PatternReplaceCharacterFilter("name", "pattern", "replace")
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
            PatternReplaceCharacterFilter filter = new PatternReplaceCharacterFilter("name", "pattern", "replace")
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
            PatternReplaceCharacterFilter filter = JsonConvert.DeserializeObject<PatternReplaceCharacterFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual(2.4, filter.Version);
            Assert.AreEqual("pattern", filter.Pattern);
            Assert.AreEqual("replace", filter.Replacement);
        }
    }
}
