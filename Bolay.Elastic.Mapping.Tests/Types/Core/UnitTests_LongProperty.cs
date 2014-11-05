using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Bolay.Elastic.Mapping.Properties.Numbers.Longs;

namespace Bolay.Elastic.Mapping.Tests.Types.Core
{
    [TestClass]
    public class UnitTests_LongProperty
    {
        [TestMethod]
        public void PASS_Create()
        {
            LongProperty prop = new LongProperty("longme")
            {
                PrecisionStep = 5
            };

            Assert.IsNotNull(prop);
            Assert.AreEqual("longme", prop.Name);
            Assert.AreEqual((int)5, prop.PrecisionStep);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            LongProperty prop = new LongProperty("longme")
            {
                PrecisionStep = 5
            };
            string json = JsonConvert.SerializeObject(prop);
            Assert.IsNotNull(json);

            string expectedJson = "{\"longme\":{\"type\":\"long\",\"precision_step\":5}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"longme\":{\"type\":\"long\",\"precision_step\":5}}";
            LongProperty prop = JsonConvert.DeserializeObject<LongProperty>(json);
            Assert.IsNotNull(prop);
            Assert.AreEqual("longme", prop.Name);
            Assert.AreEqual((int)5, prop.PrecisionStep);
        }
    }
}
