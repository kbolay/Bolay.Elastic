using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Filters.Characters.HtmlStrip;
using Newtonsoft.Json;

namespace Bolay.Elastic.Analysis.Tests.Filters.Characters
{
    [TestClass]
    public class UnitTests_HtmlStripCharacterFilter
    {
        [TestMethod]
        public void PASS_Create()
        {
            HtmlStripCharacterFilter filter = new HtmlStripCharacterFilter("name")
            {
                Version = 2.4
            };

            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual(2.4, filter.Version);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            HtmlStripCharacterFilter filter = new HtmlStripCharacterFilter("name")
            {
                Version = 2.4
            };

            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"html_strip\",\"version\":2.4}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"html_strip\",\"version\":2.4}}";
            HtmlStripCharacterFilter filter = JsonConvert.DeserializeObject<HtmlStripCharacterFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual(2.4, filter.Version);
        }
    }
}
