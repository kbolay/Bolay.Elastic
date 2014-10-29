using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Queries.Range;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Queries.Range
{
    [TestClass]
    public class UnitTests_DoubleRangeQuery
    {
        [TestMethod]
        public void PASS_CreateQuery()
        {
            DoubleRangeQuery query = new DoubleRangeQuery("field", 1.1);
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Field);
            Assert.AreEqual(1.1, query.GreaterThan);
        }

        [TestMethod]
        public void FAIL_CreateQuery_Field()
        {
            try
            {
                DoubleRangeQuery query = new DoubleRangeQuery(null);
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
                DoubleRangeQuery query = new DoubleRangeQuery("field");
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
            DoubleRangeQuery query = new DoubleRangeQuery("field", 1.1);
            string json = JsonConvert.SerializeObject(query);
            Assert.IsNotNull(json);

            string expectedJson = "{\"range\":{\"field\":{\"gt\":1.1}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"range\":{\"field\":{\"gt\":1.1}}}";
            DoubleRangeQuery query = JsonConvert.DeserializeObject<DoubleRangeQuery>(json);
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Field);
            Assert.AreEqual(1.1, query.GreaterThan);
        }
    }
}
