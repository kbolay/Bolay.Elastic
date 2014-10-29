using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Filters.Exists;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Filters.Exists
{
    [TestClass]
    public class UnitTests_ExistsFilter
    {
        [TestMethod]
        public void PASS_CreateFilter()
        {
            ExistsFilter filter = new ExistsFilter("field", "value");
            Assert.IsNotNull(filter);
            Assert.AreEqual("field", filter.Field);
            Assert.AreEqual("value", filter.Value.ToString());
        }

        [TestMethod]
        public void FAIL_CreateFilter_Field()
        {
            try
            {
                ExistsFilter filter = new ExistsFilter(null, "value");
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
                ExistsFilter filter = new ExistsFilter("field", "");
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
            ExistsFilter filter = new ExistsFilter("field", "value");
            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"exists\":{\"field\":\"value\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"exists\":{\"field\":\"value\"}}";
            ExistsFilter filter = JsonConvert.DeserializeObject<ExistsFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("field", filter.Field);
            Assert.AreEqual("value", filter.Value.ToString());
        }
    }
}
