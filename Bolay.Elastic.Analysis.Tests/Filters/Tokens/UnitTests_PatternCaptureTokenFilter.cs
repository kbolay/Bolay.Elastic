using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Filters.Tokens.PatternCapture;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Bolay.Elastic.Analysis.Tests.Filters.Tokens
{
    [TestClass]
    public class UnitTests_PatternCaptureTokenFilter
    {
        [TestMethod]
        public void PASS_Create()
        {
            PatternCaptureTokenFilter filter = new PatternCaptureTokenFilter("name", new List<string>() { "p1", "p2" })
            {
                Version = 2.4,
                PreserveOriginal = false
            };

            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual(2.4, filter.Version);
            Assert.AreEqual("p1", filter.Patterns.First());
            Assert.AreEqual("p2", filter.Patterns.Last());
            Assert.AreEqual(false, filter.PreserveOriginal);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            PatternCaptureTokenFilter filter = new PatternCaptureTokenFilter("name", new List<string>() { "p1", "p2" })
            {
                Version = 2.4,
                PreserveOriginal = false
            };

            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"pattern_capture\",\"version\":2.4,\"preserve_original\":false,\"patterns\":[\"p1\",\"p2\"]}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"pattern_capture\",\"version\":2.4,\"preserve_original\":false,\"patterns\":[\"p1\",\"p2\"]}}";
            PatternCaptureTokenFilter filter = JsonConvert.DeserializeObject<PatternCaptureTokenFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual(2.4, filter.Version);
            Assert.AreEqual("p1", filter.Patterns.First());
            Assert.AreEqual("p2", filter.Patterns.Last());
            Assert.AreEqual(false, filter.PreserveOriginal);
        }
    }
}
