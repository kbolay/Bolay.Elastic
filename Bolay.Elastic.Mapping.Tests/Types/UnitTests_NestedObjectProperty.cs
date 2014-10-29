using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Mapping.Types.Nested;
using Newtonsoft.Json;

namespace Bolay.Elastic.Mapping.Tests.Types
{
    [TestClass]
    public class UnitTests_NestedObjectProperty
    {
        [TestMethod]
        public void PASS_Create()
        {
            NestedObjectProperty prop = new NestedObjectProperty("nestedObj")
            {
                IncludeInParent = true,
                IncludeInRoot = true
            };
            Assert.IsNotNull(prop);
            Assert.AreEqual("nestedObj", prop.Name);
            Assert.AreEqual(true, prop.IncludeInParent);
            Assert.AreEqual(true, prop.IncludeInRoot);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            NestedObjectProperty prop = new NestedObjectProperty("nestedObj")
            {
                IncludeInParent = true,
                IncludeInRoot = true
            };
            string json = JsonConvert.SerializeObject(prop);
            Assert.IsNotNull(json);

            string expectedJson = "{\"nestedObj\":{\"include_in_parent\":true,\"include_in_root\":true,\"type\":\"nested\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"nestedObj\":{\"include_in_parent\":true,\"include_in_root\":true,\"type\":\"nested\"}}";
            NestedObjectProperty prop = JsonConvert.DeserializeObject<NestedObjectProperty>(json);
            Assert.IsNotNull(prop);
            Assert.AreEqual("nestedObj", prop.Name);
            Assert.AreEqual(true, prop.IncludeInParent);
            Assert.AreEqual(true, prop.IncludeInRoot);
        }
    }
}
