using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Filters.Term;
using Newtonsoft.Json;

namespace Bolay.Elastic.filterDSL.Tests.Filters.Term
{
    [TestClass]
    public class UnitTests_TermFilter
    {
        [TestMethod]
        public void PASS_CreateFilter()
        {
            TermFilter filter = new TermFilter("field", "value1");
            Assert.IsNotNull(filter);
            Assert.AreEqual("field", filter.Field);
            Assert.AreEqual("value1", filter.Value.ToString());
        }

        [TestMethod]
        public void FAIL_CreateFilter_Field()
        {
            try
            {
                TermFilter filter = new TermFilter(null, "value1");
                Assert.Fail();
            }
            catch (ArgumentNullException argEx)
            {
                Assert.AreEqual("field", argEx.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateFilter_Value()
        {
            try
            {
                TermFilter filter = new TermFilter("field", null);
                Assert.Fail();
            }
            catch (ArgumentNullException argEx)
            {
                Assert.AreEqual("value", argEx.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            TermFilter filter = new TermFilter("field", "value1");

            string result = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(result);

            string expectedJson = "{\"term\":{\"field\":\"value1\"}}";
            Assert.AreEqual(expectedJson, result);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string TermFilter = "{\"term\":{\"field\":\"value1\"}}";

            TermFilter filter = JsonConvert.DeserializeObject<TermFilter>(TermFilter);

            Assert.IsNotNull(filter);
            Assert.AreEqual("field", filter.Field);
            Assert.AreEqual("value1", filter.Value.ToString());
        }

        [TestMethod]
        public void PASS_Serialize_Cache()
        {
            TermFilter filter = new TermFilter("field", "value") { Cache = false };
            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"term\":{\"field\":\"value\",\"_cache\":false}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_Cache()
        {
            string json = "{\"term\":{\"field\":\"value\",\"_cache\":false}}";
            TermFilter filter = JsonConvert.DeserializeObject<TermFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("field", filter.Field);
            Assert.AreEqual("value", filter.Value.ToString());
            Assert.AreEqual(false, filter.Cache);
        }

        [TestMethod]
        public void PASS_Serialize_Number()
        {
            TermFilter filter = new TermFilter("field", 15); ;
            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"term\":{\"field\":15}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_Number()
        {
            string json = "{\"term\":{\"field\":15}}";
            TermFilter filter = JsonConvert.DeserializeObject<TermFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("field", filter.Field);
            Assert.AreEqual(15.ToString(), filter.Value.ToString());
        }
    }
}
