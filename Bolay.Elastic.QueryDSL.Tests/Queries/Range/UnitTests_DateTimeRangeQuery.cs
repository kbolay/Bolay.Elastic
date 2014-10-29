using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Queries.Range;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Queries.Range
{
    [TestClass]
    public class UnitTests_DateTimeRangeQuery
    {
        private const string _DATE_TIME_FORMAT = "yyyy-MM-ddTHH:mm:ss";
        private DateTime utcNow { get; set; }
        private string utcNowStr { get; set; }

        [TestInitialize]
        public void Init()
        {
            utcNow = DateTime.UtcNow;
            utcNowStr = utcNow.ToString(_DATE_TIME_FORMAT);
        }

        [TestMethod]
        public void PASS_CreateQuery()
        {
            DateTimeRangeQuery query = new DateTimeRangeQuery("field", utcNow);
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Field);
            Assert.AreEqual(utcNowStr, query.GreaterThan);
        }

        [TestMethod]
        public void FAIL_CreateQuery_Field()
        {
            try
            {
                DateTimeRangeQuery query = new DateTimeRangeQuery(null);
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
                DateTimeRangeQuery query = new DateTimeRangeQuery("field");
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
            DateTimeRangeQuery query = new DateTimeRangeQuery("field", utcNow);
            string json = JsonConvert.SerializeObject(query);
            Assert.IsNotNull(json);

            string expectedJson = "{\"range\":{\"field\":{\"gt\":\"" + utcNowStr + "\"}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserializer()
        {
            string json = "{\"range\":{\"field\":{\"gt\":\"" + utcNowStr + "\"}}}";
            DateTimeRangeQuery query = JsonConvert.DeserializeObject<DateTimeRangeQuery>(json);
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Field);
            Assert.AreEqual(utcNowStr, query.GreaterThan.ToString());
        }
    }
}
