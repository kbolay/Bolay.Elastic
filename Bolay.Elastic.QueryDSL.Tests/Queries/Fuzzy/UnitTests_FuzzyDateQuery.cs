using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Queries;
using Newtonsoft.Json;
using Bolay.Elastic.Models;
using Bolay.Elastic.Time;

namespace Bolay.Elastic.QueryDSL.Tests.Queries.Fuzzy
{
    [TestClass]
    public class UnitTests_FuzzyDateQuery
    {
        [TestMethod]
        public void PASS_CreateQuery()
        {
            DateTime utcNow = DateTime.UtcNow;
            string utcNowStr = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");
            FuzzyDateQuery query = new FuzzyDateQuery("field", utcNow);
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Field);
            Assert.AreEqual(utcNowStr, query.Value.ToString());
        }

        [TestMethod]
        public void PASS_CreateQuery_Fuzziness()
        {
            DateTime utcNow = DateTime.UtcNow;
            string utcNowStr = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");
            FuzzyDateQuery query = new FuzzyDateQuery("field", utcNow, new TimeValue("1ms").TimeSpan);
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Field);
            Assert.AreEqual(utcNowStr, query.Value);
            Assert.AreEqual("1ms", query.Fuzziness);
        }

        [TestMethod]
        public void FAIL_CreateQuery_Field()
        {
            try
            {
                FuzzyDateQuery query = new FuzzyDateQuery(null, DateTime.UtcNow);
                Assert.Fail();
            }
            catch (ArgumentNullException argEx)
            {
                Assert.AreEqual("field", argEx.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateQuery_Value()
        {
            try
            {
                FuzzyDateQuery query = new FuzzyDateQuery("field", new DateTime());
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
            DateTime utcNow = DateTime.UtcNow;
            string utcNowStr = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");
            FuzzyDateQuery query = new FuzzyDateQuery("field", utcNow);
            string json = JsonConvert.SerializeObject(query);

            Assert.IsNotNull(json);
            string expectedJson = "{\"fuzzy\":{\"field\":\"" + utcNowStr + "\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Serialize_Fuzziness()
        {
            DateTime utcNow = DateTime.UtcNow;
            string utcNowStr = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");
            FuzzyDateQuery query = new FuzzyDateQuery("field", utcNow, new TimeValue("1ms").TimeSpan);
            string json = JsonConvert.SerializeObject(query);

            Assert.IsNotNull(json);
            string expectedJson = "{\"fuzzy\":{\"field\":{\"value\":\"" + utcNowStr + "\",\"fuzziness\":\"1ms\"}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            DateTime utcNow = DateTime.UtcNow;
            string utcNowStr = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");
            string json = "{\"fuzzy\":{\"field\":\"" + utcNowStr + "\"}}";
            FuzzyDateQuery query = JsonConvert.DeserializeObject<FuzzyDateQuery>(json);
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Field);
            Assert.AreEqual(utcNowStr, query.Value);
        }

        [TestMethod]
        public void PASS_Deserialize_Fuzziness()
        {
            DateTime utcNow = DateTime.UtcNow;
            string utcNowStr = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");
            string json = "{\"fuzzy\":{\"field\":{\"value\":\"" + utcNowStr + "\",\"fuzziness\":\"1ms\"}}}";
            FuzzyDateQuery query = JsonConvert.DeserializeObject<FuzzyDateQuery>(json);
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Field);
            Assert.AreEqual(utcNowStr, query.Value);
            Assert.AreEqual("1ms", query.Fuzziness);
        }
    }
}
