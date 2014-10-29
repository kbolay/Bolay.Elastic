using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Mapping.Fields._Routing;
using Newtonsoft.Json;

namespace Bolay.Elastic.Mapping.Tests.Fields
{
    [TestClass]
    public class UnitTests_DocumentRouting
    {
        [TestMethod]
        public void PASS_Create()
        {
            DocumentRouting routing = new DocumentRouting()
            {
                Index = IndexSettingEnum.Analyzed,
                Store = true,
                IsRequired = true,
                Path = "obj.prop"
            };

            Assert.IsNotNull(routing);
            Assert.AreEqual(IndexSettingEnum.Analyzed, routing.Index);
            Assert.AreEqual(true, routing.Store);
            Assert.AreEqual(true, routing.IsRequired);
            Assert.AreEqual("obj.prop", routing.Path);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            DocumentRouting routing = new DocumentRouting()
            {
                Index = IndexSettingEnum.Analyzed,
                Store = true,
                IsRequired = true,
                Path = "obj.prop"
            };

            string json = JsonConvert.SerializeObject(routing);
            Assert.IsNotNull(json);

            string expectedJson = "{\"index\":\"analyzed\",\"store\":true,\"required\":true,\"path\":\"obj.prop\"}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"index\":\"analyzed\",\"store\":true,\"required\":true,\"path\":\"obj.prop\"}";
            DocumentRouting routing = JsonConvert.DeserializeObject<DocumentRouting>(json);
            Assert.IsNotNull(routing);
            Assert.AreEqual(IndexSettingEnum.Analyzed, routing.Index);
            Assert.AreEqual(true, routing.Store);
            Assert.AreEqual(true, routing.IsRequired);
            Assert.AreEqual("obj.prop", routing.Path);
        }
    }
}
