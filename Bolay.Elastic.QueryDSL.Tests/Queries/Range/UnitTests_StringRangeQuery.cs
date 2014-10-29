using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Queries.Range;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Queries.Range
{
    [TestClass]
    public class UnitTests_StringRangeQuery
    {
        [TestMethod]
        public void PASS_CreateQuery()
        {
            StringRangeQuery query = new StringRangeQuery("field", "1");
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Field);
            Assert.AreEqual("1", query.GreaterThan);
        }

        [TestMethod]
        public void FAIL_CreateQuery_Field()
        {
            try
            {
                StringRangeQuery query = new StringRangeQuery(null);
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
                StringRangeQuery query = new StringRangeQuery("field");
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
            StringRangeQuery query = new StringRangeQuery("field", "1");
            string json = JsonConvert.SerializeObject(query);
            Assert.IsNotNull(json);

            string expectedJson = "{\"range\":{\"field\":{\"gt\":\"1\"}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserializer()
        {
            string json = "{\"range\":{\"field\":{\"gt\":\"1\"}}}";
            StringRangeQuery query = JsonConvert.DeserializeObject<StringRangeQuery>(json);
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Field);
            Assert.AreEqual("1", query.GreaterThan);
        }
    }
}
