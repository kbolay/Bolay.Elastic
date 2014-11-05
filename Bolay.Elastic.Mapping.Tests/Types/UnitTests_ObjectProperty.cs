using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Mapping.Properties.Object;
using System.Collections.Generic;
using Bolay.Elastic.Mapping.Properties;
using Bolay.Elastic.Mapping.Properties.String;
using Newtonsoft.Json;

namespace Bolay.Elastic.Mapping.Tests.Types
{
    [TestClass]
    public class UnitTests_ObjectProperty
    {
        [TestMethod]
        public void PASS_Create()
        {
            ObjectProperty prop = new ObjectProperty("subobj")
            {
                Properties = new List<IDocumentProperty>() 
                { 
                    new StringProperty("title")
                }
            };

            Assert.IsNotNull(prop);
            Assert.AreEqual("subobj", prop.Name);
            Assert.AreEqual((int)1, prop.Properties.Count());
            Assert.IsTrue(prop.Properties.First() is StringProperty);
            Assert.AreEqual("title", prop.Properties.First().Name);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            ObjectProperty prop = new ObjectProperty("subobj")
            {
                Properties = new List<IDocumentProperty>() 
                { 
                    new StringProperty("title")
                }
            };

            string json = JsonConvert.SerializeObject(prop);
            Assert.IsNotNull(json);

            string expectedJson = "{\"subobj\":{\"properties\":{\"title\":{\"type\":\"string\"}}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"subobj\":{\"properties\":{\"title\":{\"type\":\"string\"}}}}";
            ObjectProperty prop = JsonConvert.DeserializeObject<ObjectProperty>(json);

            Assert.IsNotNull(prop);
            Assert.AreEqual("subobj", prop.Name);
            Assert.AreEqual((int)1, prop.Properties.Count());
            Assert.IsTrue(prop.Properties.First() is StringProperty);
            Assert.AreEqual("title", prop.Properties.First().Name);
        }
    }
}
