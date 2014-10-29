using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Filters.Missing;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Filters.Missing
{
    [TestClass]
    public class UnitTests_MissingFilter
    {
        [TestMethod]
        public void PASS_CreateFilter()
        {
            MissingFilter filter = new MissingFilter("field");
            Assert.IsNotNull(filter);
            Assert.AreEqual("field", filter.Field);
            Assert.AreEqual(false, filter.Existence);
            Assert.AreEqual(false, filter.NullValue);
        }

        [TestMethod]
        public void FAIL_CreateFilter_Field()
        {
            try
            {
                MissingFilter filter = new MissingFilter(null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("field", ex.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serializer()
        {
            MissingFilter filter = new MissingFilter("field");
            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);
            string expectedJson = "{\"missing\":{\"field\":\"field\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"missing\":{\"field\":\"field\"}}";
            MissingFilter filter = JsonConvert.DeserializeObject<MissingFilter>(json);
            Assert.AreEqual("field", filter.Field);
            Assert.AreEqual(false, filter.Existence);
            Assert.AreEqual(false, filter.NullValue);
        }

        [TestMethod]
        public void PASS_Serializer_Existence_NullValue()
        {
            MissingFilter filter = new MissingFilter("field") 
            { 
                Existence = true,
                NullValue = true
            };
            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);
            string expectedJson = "{\"missing\":{\"field\":\"field\",\"existence\":true,\"null_value\":true}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_Existence_NullValue()
        {
            string json = "{\"missing\":{\"field\":\"field\",\"existence\":true,\"null_value\":true}}";
            MissingFilter filter = JsonConvert.DeserializeObject<MissingFilter>(json);
            Assert.AreEqual("field", filter.Field);
            Assert.AreEqual(true, filter.Existence);
            Assert.AreEqual(true, filter.NullValue);
        }
    }
}
