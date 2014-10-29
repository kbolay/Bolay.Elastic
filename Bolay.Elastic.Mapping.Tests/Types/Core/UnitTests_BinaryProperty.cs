using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Mapping.Types.Binary;
using Newtonsoft.Json;

namespace Bolay.Elastic.Mapping.Tests.Types.Core
{
    [TestClass]
    public class UnitTests_BinaryProperty
    {
        [TestMethod]
        public void PASS_Create()
        {
            BinaryProperty prop = new BinaryProperty("binaryme");

            Assert.IsNotNull(prop);
            Assert.AreEqual("binaryme", prop.Name);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            BinaryProperty prop = new BinaryProperty("binaryme");
            string json = JsonConvert.SerializeObject(prop);
            Assert.IsNotNull(json);

            string expectedJson = "{\"binaryme\":{\"type\":\"binary\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"binaryme\":{\"type\":\"binary\"}}";
            BinaryProperty prop = JsonConvert.DeserializeObject<BinaryProperty>(json);
            Assert.IsNotNull(prop);
            Assert.AreEqual("binaryme", prop.Name);
        }
    }
}
