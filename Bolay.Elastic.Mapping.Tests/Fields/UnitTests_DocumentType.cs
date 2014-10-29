using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Mapping.Fields._Type;
using Newtonsoft.Json;

namespace Bolay.Elastic.Mapping.Tests.Fields
{
    [TestClass]
    public class UnitTests_DocumentType
    {
        [TestMethod]
        public void PASS_Create()
        {
            DocumentType type = new DocumentType()
            {
                Index = IndexSettingEnum.Analyzed,
                Store = true
            };

            Assert.IsNotNull(type);
            Assert.AreEqual(IndexSettingEnum.Analyzed, type.Index);
            Assert.AreEqual(true, type.Store);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            DocumentType type = new DocumentType()
            {
                Index = IndexSettingEnum.Analyzed,
                Store = true
            };

            string json = JsonConvert.SerializeObject(type);
            Assert.IsNotNull(json);

            string expectedJson = "{\"index\":\"analyzed\",\"store\":true}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"index\":\"analyzed\",\"store\":true}";
            DocumentType type = JsonConvert.DeserializeObject<DocumentType>(json);

            Assert.IsNotNull(type);
            Assert.AreEqual(IndexSettingEnum.Analyzed, type.Index);
            Assert.AreEqual(true, type.Store);
        }
    }
}
