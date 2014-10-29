using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Filters.Tokens.Truncate;
using Newtonsoft.Json;

namespace Bolay.Elastic.Analysis.Tests.Filters.Tokens
{
    [TestClass]
    public class UnitTests_TruncateFilterToken
    {
        [TestMethod]
        public void PASS_Create()
        {
            TruncateTokenFilter filter = new TruncateTokenFilter("name")
            {
                Version = 2.4,
                Length = 9
            };

            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual(2.4, filter.Version);
            Assert.AreEqual((int)9, filter.Length);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            TruncateTokenFilter filter = new TruncateTokenFilter("name")
            {
                Version = 2.4,
                Length = 9
            };

            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"truncate\",\"version\":2.4,\"length\":9}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"truncate\",\"version\":2.4,\"length\":9}}";
            TruncateTokenFilter filter = JsonConvert.DeserializeObject<TruncateTokenFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual(2.4, filter.Version);
            Assert.AreEqual((int)9, filter.Length);
        }
    }
}
