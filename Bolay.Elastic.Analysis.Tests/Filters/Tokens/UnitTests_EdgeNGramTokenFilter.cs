using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Filters.Tokens.EdgeNGram;
using Bolay.Elastic.Analysis.Filters.Tokens;
using Newtonsoft.Json;

namespace Bolay.Elastic.Analysis.Tests.Filters.Tokens
{
    [TestClass]
    public class UnitTests_EdgeEdgeNGramTokenFilter
    {
        [TestMethod]
        public void PASS_Create()
        {
            EdgeNGramTokenFilter filter = new EdgeNGramTokenFilter("name")
            {
                MinimumSize = 2,
                MaximumSize = 5,
                Side = SideEnum.Back
            };

            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual((int)2, filter.MinimumSize);
            Assert.AreEqual((int)5, filter.MaximumSize);
            Assert.AreEqual(SideEnum.Back, filter.Side);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            EdgeNGramTokenFilter filter = new EdgeNGramTokenFilter("name")
            {
                MinimumSize = 2,
                MaximumSize = 5,
                Side = SideEnum.Back
            };

            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"edgeNGram\",\"min_gram\":2,\"max_gram\":5,\"side\":\"back\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"edgeNGram\",\"min_gram\":2,\"max_gram\":5,\"side\":\"back\"}}";
            EdgeNGramTokenFilter filter = JsonConvert.DeserializeObject<EdgeNGramTokenFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual((int)2, filter.MinimumSize);
            Assert.AreEqual((int)5, filter.MaximumSize);
            Assert.AreEqual(SideEnum.Back, filter.Side);
        }
    }
}
