using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Filters.Tokens.Length;
using Newtonsoft.Json;

namespace Bolay.Elastic.Analysis.Tests.Filters.Tokens
{
    [TestClass]
    public class UnitTests_LengthTokenFilter
    {
        [TestMethod]
        public void PASS_Create()
        {
            LengthTokenFilter filter = new LengthTokenFilter("name")
            {
                Minimum = 1,
                Maximum = 13
            };

            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual((int)1, filter.Minimum);
            Assert.AreEqual((int)13, filter.Maximum);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            LengthTokenFilter filter = new LengthTokenFilter("name")
            {
                Minimum = 1,
                Maximum = 13
            };

            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"length\",\"min\":1,\"max\":13}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"length\",\"min\":1,\"max\":13}}";
            LengthTokenFilter filter = JsonConvert.DeserializeObject<LengthTokenFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual((int)1, filter.Minimum);
            Assert.AreEqual((int)13, filter.Maximum);
        }
    }
}
