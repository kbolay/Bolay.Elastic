using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Filters.Prefix;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Filters.Prefix
{
    [TestClass]
    public class UnitTests_PrefixFilter
    {
        [TestMethod]
        public void PASS_CreateFilter()
        {
            PrefixFilter filter = new PrefixFilter("field", "value");
            Assert.AreEqual("field", filter.Field);
            Assert.AreEqual("value", filter.Value);
        }

        [TestMethod]
        public void FAIL_CreateFilter_Field()
        {
            try
            {
                PrefixFilter filter = new PrefixFilter(null, "value");
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("field", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateFilter_Value()
        {
            try
            {
                PrefixFilter filter = new PrefixFilter("field", null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("value", ex.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            PrefixFilter filter = new PrefixFilter("field", "value");
            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);
            string expectedJson = "{\"prefix\":{\"field\":\"value\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"prefix\":{\"field\":\"value\"}}";
            PrefixFilter filter = JsonConvert.DeserializeObject<PrefixFilter>(json);
            Assert.AreEqual("field", filter.Field);
            Assert.AreEqual("value", filter.Value);
        }
    }
}
