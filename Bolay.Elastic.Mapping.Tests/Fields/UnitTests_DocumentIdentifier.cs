using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Mapping.Fields._Id;
using Newtonsoft.Json;

namespace Bolay.Elastic.Mapping.Tests.Fields
{
    [TestClass]
    public class UnitTests_DocumentIdentifier
    {
        [TestMethod]
        public void PASS_Create()
        {
            DocumentIdentifier id = new DocumentIdentifier()
            {
                Index = IndexSettingEnum.Analyzed,
                Store = true,
                Path = "obj.prop"
            };

            Assert.IsNotNull(id);
            Assert.AreEqual(IndexSettingEnum.Analyzed, id.Index);
            Assert.AreEqual(true, id.Store);
            Assert.AreEqual("obj.prop", id.Path);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            DocumentIdentifier id = new DocumentIdentifier()
            {
                Index = IndexSettingEnum.Analyzed,
                Store = true,
                Path = "obj.prop"
            };

            string json = JsonConvert.SerializeObject(id);
            Assert.IsNotNull(json);

            string expectedJson = "{\"index\":\"analyzed\",\"store\":true,\"path\":\"obj.prop\"}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"index\":\"analyzed\",\"store\":true,\"path\":\"obj.prop\"}";
            DocumentIdentifier id = JsonConvert.DeserializeObject<DocumentIdentifier>(json);

            Assert.IsNotNull(id);
            Assert.AreEqual(IndexSettingEnum.Analyzed, id.Index);
            Assert.AreEqual(true, id.Store);
            Assert.AreEqual("obj.prop", id.Path);
        }
    }
}
