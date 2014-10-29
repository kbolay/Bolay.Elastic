using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Filters.Range;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Filters.Range
{
    [TestClass]
    public class UnitTests_DoubleRangeFilter
    {
        [TestMethod]
        public void PASS_CreateQuery()
        {
            DoubleRangeFilter query = new DoubleRangeFilter("field", 1.1);
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Field);
            Assert.AreEqual(1.1, query.GreaterThan);
        }

        [TestMethod]
        public void FAIL_CreateQuery_Field()
        {
            try
            {
                DoubleRangeFilter query = new DoubleRangeFilter(null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("field", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateQuery_Range()
        {
            try
            {
                DoubleRangeFilter query = new DoubleRangeFilter("field");
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
            DoubleRangeFilter query = new DoubleRangeFilter("field", 1.1);
            string json = JsonConvert.SerializeObject(query);
            Assert.IsNotNull(json);

            string expectedJson = "{\"range\":{\"field\":{\"gt\":1.1}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserializer()
        {
            string json = "{\"range\":{\"field\":{\"gt\":1.1}}}";
            DoubleRangeFilter query = JsonConvert.DeserializeObject<DoubleRangeFilter>(json);
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Field);
            Assert.AreEqual(1.1, query.GreaterThan);
        }
    }
}
