using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Mapping.Properties.Numbers.Bytes;
using Newtonsoft.Json;

namespace Bolay.Elastic.Mapping.Tests.Types.Core
{
    [TestClass]
    public class UnitTests_ByteProperty
    {
        [TestMethod]
        public void PASS_Create()
        {
            ByteProperty prop = new ByteProperty("byteme")
            {
                PrecisionStep = 5
            };

            Assert.IsNotNull(prop);
            Assert.AreEqual("byteme", prop.Name);
            Assert.AreEqual((int)5, prop.PrecisionStep);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            ByteProperty prop = new ByteProperty("byteme")
            {
                PrecisionStep = 5
            };
            string json = JsonConvert.SerializeObject(prop);
            Assert.IsNotNull(json);

            string expectedJson = "{\"byteme\":{\"type\":\"byte\",\"precision_step\":5}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"byteme\":{\"type\":\"byte\",\"precision_step\":5}}";
            ByteProperty prop = JsonConvert.DeserializeObject<ByteProperty>(json);
            Assert.IsNotNull(prop);
            Assert.AreEqual("byteme", prop.Name);
            Assert.AreEqual((int)5, prop.PrecisionStep);
        }
    }
}
