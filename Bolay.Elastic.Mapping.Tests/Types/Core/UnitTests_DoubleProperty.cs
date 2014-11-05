using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Bolay.Elastic.Mapping.Properties.Numbers.Doubles;

namespace Bolay.Elastic.Mapping.Tests.Types.Core
{
    [TestClass]
    public class UnitTests_DoubleProperty
    {
        [TestMethod]
        public void PASS_Create()
        {
            DoubleProperty prop = new DoubleProperty("doubleme")
            {
                PrecisionStep = 5
            };

            Assert.IsNotNull(prop);
            Assert.AreEqual("doubleme", prop.Name);
            Assert.AreEqual((int)5, prop.PrecisionStep);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            DoubleProperty prop = new DoubleProperty("doubleme")
            {
                PrecisionStep = 5
            };
            string json = JsonConvert.SerializeObject(prop);
            Assert.IsNotNull(json);

            string expectedJson = "{\"doubleme\":{\"type\":\"double\",\"precision_step\":5}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"doubleme\":{\"type\":\"double\",\"precision_step\":5}}";
            DoubleProperty prop = JsonConvert.DeserializeObject<DoubleProperty>(json);
            Assert.IsNotNull(prop);
            Assert.AreEqual("doubleme", prop.Name);
            Assert.AreEqual((int)5, prop.PrecisionStep);
        }
    }
}
