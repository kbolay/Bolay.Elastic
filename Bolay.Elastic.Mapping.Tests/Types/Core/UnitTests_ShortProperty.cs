using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Mapping.Properties.Numbers.Shorts;
using Newtonsoft.Json;

namespace Bolay.Elastic.Mapping.Tests.Types.Core
{
    [TestClass]
    public class UnitTests_ShortProperty
    {
        [TestMethod]
        public void PASS_Create()
        {
            ShortProperty prop = new ShortProperty("shortme")
            {
                PrecisionStep = 5
            };

            Assert.IsNotNull(prop);
            Assert.AreEqual("shortme", prop.Name);
            Assert.AreEqual((int)5, prop.PrecisionStep);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            ShortProperty prop = new ShortProperty("shortme")
            {
                PrecisionStep = 5
            };
            string json = JsonConvert.SerializeObject(prop);
            Assert.IsNotNull(json);

            string expectedJson = "{\"shortme\":{\"type\":\"short\",\"precision_step\":5}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"shortme\":{\"type\":\"short\",\"precision_step\":5}}";
            ShortProperty prop = JsonConvert.DeserializeObject<ShortProperty>(json);
            Assert.IsNotNull(prop);
            Assert.AreEqual("shortme", prop.Name);
            Assert.AreEqual((int)5, prop.PrecisionStep);
        }
    }
}
