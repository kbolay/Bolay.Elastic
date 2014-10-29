using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Mapping.Types.Ip;
using Newtonsoft.Json;

namespace Bolay.Elastic.Mapping.Tests.Types
{
    [TestClass]
    public class UnitTests_IpAddressProperty
    {
        [TestMethod]
        public void PASS_Create()
        {
            IpAddressProperty prop = new IpAddressProperty("ip-address")
            {
                Boost = 1.2,
                PrecisionStep = 2
            };

            Assert.IsNotNull(prop);
            Assert.AreEqual("ip-address", prop.Name);
            Assert.AreEqual(1.2, prop.Boost);
            Assert.AreEqual(2, prop.PrecisionStep);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            IpAddressProperty prop = new IpAddressProperty("ip-address")
            {
                Boost = 1.2,
                PrecisionStep = 2
            };

            string json = JsonConvert.SerializeObject(prop);
            Assert.IsNotNull(json);

            string expectedJson = "{\"ip-address\":{\"type\":\"ip\",\"boost\":1.2,\"precision_step\":2}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"ip-address\":{\"type\":\"ip\",\"boost\":1.2,\"precision_step\":2}}";
            IpAddressProperty prop = JsonConvert.DeserializeObject<IpAddressProperty>(json);
            Assert.IsNotNull(prop);
            Assert.AreEqual("ip-address", prop.Name);
            Assert.AreEqual(1.2, prop.Boost);
            Assert.AreEqual(2, prop.PrecisionStep);
        }
    }
}
