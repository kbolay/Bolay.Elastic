using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Filters.Range;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Filters.Range
{
    [TestClass]
    public class UnitTests_IntegerRangeFilter
    {
        [TestMethod]
        public void PASS_CreateQuery()
        {
            IntegerRangeFilter query = new IntegerRangeFilter("field", 1);
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Field);
            Assert.AreEqual((Int64)1, (Int64)query.GreaterThan);
        }

        [TestMethod]
        public void FAIL_CreateQuery_Field()
        {
            try
            {
                IntegerRangeFilter query = new IntegerRangeFilter(null);
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
                IntegerRangeFilter query = new IntegerRangeFilter("field");
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
            IntegerRangeFilter query = new IntegerRangeFilter("field", 1);
            string json = JsonConvert.SerializeObject(query);
            Assert.IsNotNull(json);

            string expectedJson = "{\"range\":{\"field\":{\"gt\":1}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserializer()
        {
            string json = "{\"range\":{\"field\":{\"gt\":1}}}";
            IntegerRangeFilter query = JsonConvert.DeserializeObject<IntegerRangeFilter>(json);
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Field);
            Assert.AreEqual((Int64)1, (Int64)query.GreaterThan);
        }
    }
}
