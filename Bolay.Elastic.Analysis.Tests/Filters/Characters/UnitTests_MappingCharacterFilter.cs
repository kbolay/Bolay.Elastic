using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Filters.Characters.Mapping;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bolay.Elastic.Analysis.Tests.Filters.Characters
{
    [TestClass]
    public class UnitTests_MappingCharacterFilter
    {
        [TestMethod]
        public void PASS_Create()
        {
            MappingCharacterFilter filter = new MappingCharacterFilter("name",
                new List<CharacterMapping>() 
                { 
                    new CharacterMapping("ph", "f"),
                    new CharacterMapping("qu", "q")
                });

            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual("ph=>f", filter.Mappings.First().ToString());
            Assert.AreEqual("qu=>q", filter.Mappings.Last().ToString());
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            MappingCharacterFilter filter = new MappingCharacterFilter("name",
                new List<CharacterMapping>() 
                { 
                    new CharacterMapping("ph", "f"),
                    new CharacterMapping("qu", "q")
                });

            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"mapping\",\"mappings\":[\"ph=>f\",\"qu=>q\"]}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"mapping\",\"mappings\":[\"ph=>f\",\"qu=>q\"]}}";
            MappingCharacterFilter filter = JsonConvert.DeserializeObject<MappingCharacterFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual("ph=>f", filter.Mappings.First().ToString());
            Assert.AreEqual("qu=>q", filter.Mappings.Last().ToString());
        }
    }
}
