using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Filters.Range;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Filters.Range
{
    [TestClass]
    public class UnitTests_StringRangeFilter
    {
        [TestMethod]
        public void PASS_CreateFilter()
        {
            StringRangeFilter filter = new StringRangeFilter("field", "1");
            Assert.IsNotNull(filter);
            Assert.AreEqual("field", filter.Field);
            Assert.AreEqual("1", filter.GreaterThan);
        }

        [TestMethod]
        public void FAIL_CreateFilter_Field()
        {
            try
            {
                StringRangeFilter filter = new StringRangeFilter(null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("field", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateFilter_Range()
        {
            try
            {
                StringRangeFilter filter = new StringRangeFilter("field");
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("range", ex.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serializer()
        {
            StringRangeFilter filter = new StringRangeFilter("field", "1");
            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"range\":{\"field\":{\"gt\":\"1\"}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserializer()
        {
            string json = "{\"range\":{\"field\":{\"gt\":\"1\"}}}";
            StringRangeFilter filter = JsonConvert.DeserializeObject<StringRangeFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("field", filter.Field);
            Assert.AreEqual("1", filter.GreaterThan);
        }

        [TestMethod]
        public void PASS_Serialize_Execution_Index()
        {
            StringRangeFilter filter = new StringRangeFilter("field", "1")
            {
                ExecutionType = ExecutionTypeEnum.Index
            };

            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"range\":{\"field\":{\"gt\":\"1\"},\"execution\":\"index\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_Execution_Index()
        {
            string json = "{\"range\":{\"field\":{\"gt\":\"1\"},\"execution\":\"index\"}}";
            StringRangeFilter filter = JsonConvert.DeserializeObject<StringRangeFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("field", filter.Field);
            Assert.AreEqual("1", filter.GreaterThan);
            Assert.AreEqual(ExecutionTypeEnum.Index, filter.ExecutionType);
            Assert.AreEqual(true, filter.Cache);
        }
    }
}
