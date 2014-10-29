using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Filters.Tokens.Shingle;
using Newtonsoft.Json;

namespace Bolay.Elastic.Analysis.Tests.Filters.Tokens
{
    [TestClass]
    public class UnitTests_ShingleTokenFilter
    {
        [TestMethod]
        public void PASS_Create()
        {
            ShingleTokenFilter filter = new ShingleTokenFilter("name")
            {
                Version = 2.4,
                FillerToken = "a",
                MaximumSize = 25,
                MinimumSize = 4,
                OutputUnigrams = false,
                OutputUnigramsIfNoShingles = true,
                TokenSeparator = "-"
            };

            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual(2.4, filter.Version);
            Assert.AreEqual("a", filter.FillerToken);
            Assert.AreEqual(25, filter.MaximumSize);
            Assert.AreEqual(4, filter.MinimumSize);
            Assert.AreEqual(false, filter.OutputUnigrams);
            Assert.AreEqual(true, filter.OutputUnigramsIfNoShingles);
            Assert.AreEqual("-", filter.TokenSeparator);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            ShingleTokenFilter filter = new ShingleTokenFilter("name")
            {
                Version = 2.4,
                FillerToken = "a",
                MaximumSize = 25,
                MinimumSize = 4,
                OutputUnigrams = false,
                OutputUnigramsIfNoShingles = true,
                TokenSeparator = "-"
            };

            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"shingle\",\"version\":2.4,\"min_shingle_size\":4,\"max_shingle_size\":25,\"output_unigrams\":false,\"output_unigrams_if_no_shingles\":true,\"token_separator\":\"-\",\"filler_token\":\"a\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"shingle\",\"version\":2.4,\"min_shingle_size\":4,\"max_shingle_size\":25,\"output_unigrams\":false,\"output_unigrams_if_no_shingles\":true,\"token_separator\":\"-\",\"filler_token\":\"a\"}}";
            ShingleTokenFilter filter = JsonConvert.DeserializeObject<ShingleTokenFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual(2.4, filter.Version);
            Assert.AreEqual("a", filter.FillerToken);
            Assert.AreEqual(25, filter.MaximumSize);
            Assert.AreEqual(4, filter.MinimumSize);
            Assert.AreEqual(false, filter.OutputUnigrams);
            Assert.AreEqual(true, filter.OutputUnigramsIfNoShingles);
            Assert.AreEqual("-", filter.TokenSeparator);
        }
    }
}
