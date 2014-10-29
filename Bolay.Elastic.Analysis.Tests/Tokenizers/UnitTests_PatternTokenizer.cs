using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Tokenizers.Pattern;
using Bolay.Elastic.Models;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bolay.Elastic.Analysis.Tests.Tokenizers
{
    [TestClass]
    public class UnitTests_PatternTokenizer
    {
        [TestMethod]
        public void PASS_Create()
        {
            PatternTokenizer analyzer = new PatternTokenizer("name")
            {
                Pattern = "pattern",
                Flags = new List<RegexFlagEnum>() { RegexFlagEnum.CaseInsensitive, RegexFlagEnum.CannonEq },
                Group = 2
            };

            Assert.IsNotNull(analyzer);
            Assert.AreEqual("name", analyzer.Name);
            Assert.AreEqual("pattern", analyzer.Pattern);
            Assert.AreEqual(RegexFlagEnum.CaseInsensitive, analyzer.Flags.First());
            Assert.AreEqual(RegexFlagEnum.CannonEq, analyzer.Flags.Last());
            Assert.AreEqual((int)2, analyzer.Group);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            PatternTokenizer analyzer = new PatternTokenizer("name")
            {
                Pattern = "pattern",
                Flags = new List<RegexFlagEnum>() { RegexFlagEnum.CaseInsensitive, RegexFlagEnum.CannonEq },
                Group = 2
            };

            string json = JsonConvert.SerializeObject(analyzer);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"pattern\",\"pattern\":\"pattern\",\"flags\":\"CASE_INSENSITIVE|CANNON_EQ\",\"group\":2}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"pattern\",\"pattern\":\"pattern\",\"flags\":\"CASE_INSENSITIVE|CANNON_EQ\",\"group\":2}}";

            PatternTokenizer analyzer = JsonConvert.DeserializeObject<PatternTokenizer>(json);
            Assert.IsNotNull(analyzer);
            Assert.AreEqual("name", analyzer.Name);
            Assert.AreEqual("pattern", analyzer.Pattern);
            Assert.AreEqual(RegexFlagEnum.CaseInsensitive, analyzer.Flags.First());
            Assert.AreEqual(RegexFlagEnum.CannonEq, analyzer.Flags.Last());
            Assert.AreEqual((int)2, analyzer.Group);
        }
    }
}
