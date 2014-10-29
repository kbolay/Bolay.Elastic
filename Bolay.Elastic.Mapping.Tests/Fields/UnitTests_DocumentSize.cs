using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Mapping.Fields._Size;
using Newtonsoft.Json;

namespace Bolay.Elastic.Mapping.Tests.Fields
{
    [TestClass]
    public class UnitTests_DocumentSize
    {
        [TestMethod]
        public void PASS_Create()
        {
            DocumentSize size = new DocumentSize() { Store = true };
            Assert.IsNotNull(size);
            Assert.AreEqual(true, size.IsEnabled);
            Assert.AreEqual(true, size.Store);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            DocumentSize size = new DocumentSize() { Store = true };
            string json = JsonConvert.SerializeObject(size);
            Assert.IsNotNull(json);

            string expectedJson = "{\"enabled\":true,\"store\":true}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"enabled\":true,\"store\":true}";
            DocumentSize size = JsonConvert.DeserializeObject<DocumentSize>(json);
            Assert.AreEqual(true, size.IsEnabled);
            Assert.AreEqual(true, size.Store);
        }
    }
}
