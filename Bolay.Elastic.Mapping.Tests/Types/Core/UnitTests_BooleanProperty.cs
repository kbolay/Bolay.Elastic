using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Mapping.Properties.Boolean;
using Newtonsoft.Json;

namespace Bolay.Elastic.Mapping.Tests.Types.Core
{
    [TestClass]
    public class UnitTests_BooleanProperty
    {
        [TestMethod]
        public void PASS_Create()
        {
            BooleanProperty prop = new BooleanProperty("boolme");

            Assert.IsNotNull(prop);
            Assert.AreEqual("boolme", prop.Name);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            BooleanProperty prop = new BooleanProperty("boolme");
            string json = JsonConvert.SerializeObject(prop);
            Assert.IsNotNull(json);

            string expectedJson = "{\"boolme\":{\"type\":\"boolean\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"boolme\":{\"type\":\"boolean\"}}";
            BooleanProperty prop = JsonConvert.DeserializeObject<BooleanProperty>(json);
            Assert.IsNotNull(prop);
            Assert.AreEqual("boolme", prop.Name);
        }
    }
}
