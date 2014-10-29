using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Mapping.Fields._Parent;
using Newtonsoft.Json;

namespace Bolay.Elastic.Mapping.Tests.Fields
{
    [TestClass]
    public class UnitTests_ParentType
    {
        [TestMethod]
        public void PASS_Create()
        {
            ParentType parent = new ParentType("da-parent");
            Assert.IsNotNull(parent);
            Assert.AreEqual("da-parent", parent.Type);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            ParentType parent = new ParentType("da-parent");
            string json = JsonConvert.SerializeObject(parent);
            Assert.IsNotNull(json);

            string expectedJson = "{\"type\":\"da-parent\"}";
            Assert.IsNotNull(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"type\":\"da-parent\"}";
            ParentType parent = JsonConvert.DeserializeObject<ParentType>(json);
            Assert.IsNotNull(parent);
            Assert.AreEqual("da-parent", parent.Type);
        }
    }
}
