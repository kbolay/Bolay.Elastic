using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Mapping.Fields._Index;
using Newtonsoft.Json;

namespace Bolay.Elastic.Mapping.Tests.Fields
{
    [TestClass]
    public class UnitTests_DocumentIndex
    {
        [TestMethod]
        public void PASS_Create()
        {
            DocumentIndex index = new DocumentIndex();
            Assert.IsNotNull(index);
            Assert.AreEqual(false, index.IsEnabled);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            DocumentIndex index = new DocumentIndex() { IsEnabled = true };
            string json = JsonConvert.SerializeObject(index);
            Assert.IsNotNull(json);

            string expectedJson = "{\"enabled\":true}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"enabled\":true}";
            DocumentIndex index = JsonConvert.DeserializeObject<DocumentIndex>(json);
            Assert.IsNotNull(index);
            Assert.AreEqual(true, index.IsEnabled);
        }
    }
}
