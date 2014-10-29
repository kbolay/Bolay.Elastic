using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Queries.Range;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Queries.Range
{
    [TestClass]
    public class UnitTests_IntegerRangeQuery
    {
        [TestMethod]
        public void PASS_CreateQuery()
        {
            IntegerRangeQuery query = new IntegerRangeQuery("field", 1);
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Field);
            Assert.AreEqual((Int64)1, (Int64)query.GreaterThan);
        }

        [TestMethod]
        public void FAIL_CreateQuery_Field()
        {
            try
            {
                IntegerRangeQuery query = new IntegerRangeQuery(null);
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
                IntegerRangeQuery query = new IntegerRangeQuery("field");
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
            IntegerRangeQuery query = new IntegerRangeQuery("field", 1);
            string json = JsonConvert.SerializeObject(query);
            Assert.IsNotNull(json);

            string expectedJson = "{\"range\":{\"field\":{\"gt\":1}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"range\":{\"field\":{\"gt\":1}}}";
            IntegerRangeQuery query = JsonConvert.DeserializeObject<IntegerRangeQuery>(json);
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Field);
            Assert.AreEqual((Int64)1, (Int64)query.GreaterThan);
        }
    }
}
