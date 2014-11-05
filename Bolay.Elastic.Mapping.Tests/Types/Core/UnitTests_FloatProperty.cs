using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Mapping.Properties.Numbers.Floats;
using Newtonsoft.Json;

namespace Bolay.Elastic.Mapping.Tests.Types.Core
{
    [TestClass]
    public class UnitTests_FloatProperty
    {
        [TestMethod]
        public void PASS_Create()
        {
            FloatProperty prop = new FloatProperty("floatme")
            {
                PrecisionStep = 5
            };

            Assert.IsNotNull(prop);
            Assert.AreEqual("floatme", prop.Name);
            Assert.AreEqual((int)5, prop.PrecisionStep);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            FloatProperty prop = new FloatProperty("floatme")
            {
                PrecisionStep = 5
            };
            string json = JsonConvert.SerializeObject(prop);
            Assert.IsNotNull(json);

            string expectedJson = "{\"floatme\":{\"type\":\"float\",\"precision_step\":5}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"floatme\":{\"type\":\"float\",\"precision_step\":5}}";
            FloatProperty prop = JsonConvert.DeserializeObject<FloatProperty>(json);
            Assert.IsNotNull(prop);
            Assert.AreEqual("floatme", prop.Name);
            Assert.AreEqual((int)5, prop.PrecisionStep);
        }
    }
}
