using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Filters.Tokens.NGram;
using Newtonsoft.Json;

namespace Bolay.Elastic.Analysis.Tests.Filters.Tokens
{
    [TestClass]
    public class UnitTests_NGramTokenFilter
    {
        [TestMethod]
        public void PASS_Create()
        {
            NGramTokenFilter filter = new NGramTokenFilter("name")
            {
                MinimumSize = 2,
                MaximumSize = 5
            };

            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual((int)2, filter.MinimumSize);
            Assert.AreEqual((int)5, filter.MaximumSize);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            NGramTokenFilter filter = new NGramTokenFilter("name")
            {
                MinimumSize = 2,
                MaximumSize = 5
            };

            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"nGram\",\"min_gram\":2,\"max_gram\":5}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"nGram\",\"min_gram\":2,\"max_gram\":5}}";
            NGramTokenFilter filter = JsonConvert.DeserializeObject<NGramTokenFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual((int)2, filter.MinimumSize);
            Assert.AreEqual((int)5, filter.MaximumSize);
        }
    }
}
