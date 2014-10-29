using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Filters.DocumentType;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Filters.DocumentType
{
    [TestClass]
    public class UnitTests_TypeFilter
    {
        [TestMethod]
        public void PASS_CreateFilter()
        {
            TypeFilter filter = new TypeFilter("type");
            Assert.IsNotNull(filter);
            Assert.AreEqual("type", filter.DocumentType);
        }

        [TestMethod]
        public void FAIL_CreateFilter()
        {
            try
            {
                TypeFilter filter = new TypeFilter(null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("documentType", ex.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            TypeFilter filter = new TypeFilter("type");
            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"type\":{\"value\":\"type\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"type\":{\"value\":\"type\"}}";
            TypeFilter filter = JsonConvert.DeserializeObject<TypeFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("type", filter.DocumentType);
        }
    }
}
