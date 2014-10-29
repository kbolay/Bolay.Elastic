using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Filters.Limit;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Filters.Limit
{
    [TestClass]
    public class UnitTests_LimitFilter
    {
        [TestMethod]
        public void PASS_CreateFilter()
        {
            LimitFilter filter = new LimitFilter(100);
            Assert.IsNotNull(filter);
            Assert.AreEqual((Int64)100, filter.Size);
        }

        [TestMethod]
        public void FAIL_CreateFilter_Filter()
        {
            try
            {
                LimitFilter filter = new LimitFilter(0);
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.AreEqual("size", ex.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            LimitFilter filter = new LimitFilter(100);
            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);
            string expectedJson = "{\"limit\":{\"value\":100}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"limit\":{\"value\":100}}";
            LimitFilter filter = JsonConvert.DeserializeObject<LimitFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual((Int64)100, filter.Size);
        }
    }
}
