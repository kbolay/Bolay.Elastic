using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Bolay.Elastic.Mapping.Types.Numbers.Integers;

namespace Bolay.Elastic.Mapping.Tests.Types.Core
{
    [TestClass]
    public class UnitTests_IntegerProperty
    {
        [TestMethod]
        public void PASS_Create()
        {
            IntegerProperty prop = new IntegerProperty("integerme")
            {
                PrecisionStep = 5
            };

            Assert.IsNotNull(prop);
            Assert.AreEqual("integerme", prop.Name);
            Assert.AreEqual((int)5, prop.PrecisionStep);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            IntegerProperty prop = new IntegerProperty("integerme")
            {
                PrecisionStep = 5
            };
            string json = JsonConvert.SerializeObject(prop);
            Assert.IsNotNull(json);

            string expectedJson = "{\"integerme\":{\"type\":\"integer\",\"precision_step\":5}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"integerme\":{\"type\":\"integer\",\"precision_step\":5}}";
            IntegerProperty prop = JsonConvert.DeserializeObject<IntegerProperty>(json);
            Assert.IsNotNull(prop);
            Assert.AreEqual("integerme", prop.Name);
            Assert.AreEqual((int)5, prop.PrecisionStep);
        }
    }
}
