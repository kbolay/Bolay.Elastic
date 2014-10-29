using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Queries;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Queries.Fuzzy
{
    [TestClass]
    public class UnitTests_FuzzyStringQuery
    {
        [TestMethod]
        public void PASS_CreateQuery()
        {
            FuzzyStringQuery query = new FuzzyStringQuery("field", "value");
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Field);
            Assert.AreEqual("value", query.Value);
        }

        [TestMethod]
        public void PASS_CreateQuery_Fuzziness()
        {
            FuzzyStringQuery query = new FuzzyStringQuery("field", "value", 1);
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Field);
            Assert.AreEqual("value", query.Value);
            Assert.AreEqual(1, query.Fuzziness);
        }

        [TestMethod]
        public void FAIL_CreateQuery_Field()
        {
            try
            {
                FuzzyStringQuery query = new FuzzyStringQuery(null, "value", 1);
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
                FuzzyStringQuery query = new FuzzyStringQuery("field", null);
                Assert.Fail();
            }
            catch (ArgumentNullException argEx)
            {
                Assert.AreEqual("value", argEx.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateQuery_Fuzziness()
        {
            try
            {
                FuzzyStringQuery query = new FuzzyStringQuery("field", "asdf", -1);
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException argEx)
            {
                Assert.AreEqual("fuzziness", argEx.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            FuzzyStringQuery query = new FuzzyStringQuery("field", "value");
            string json = JsonConvert.SerializeObject(query);

            Assert.IsNotNull(json);
            string expectedJson = "{\"fuzzy\":{\"field\":\"value\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Serialize_Fuzziness()
        {
            FuzzyStringQuery query = new FuzzyStringQuery("field", "value", 2);
            string json = JsonConvert.SerializeObject(query);

            Assert.IsNotNull(json);
            string expectedJson = "{\"fuzzy\":{\"field\":{\"value\":\"value\",\"fuzziness\":2}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"fuzzy\":{\"field\":\"value\"}}";
            FuzzyStringQuery query = JsonConvert.DeserializeObject<FuzzyStringQuery>(json);
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Field);
            Assert.AreEqual("value", query.Value);
        }

        [TestMethod]
        public void PASS_Deserialize_Fuzziness()
        {
            string json = "{\"fuzzy\":{\"field\":{\"value\":\"value\",\"fuzziness\":2}}}";
            FuzzyStringQuery query = JsonConvert.DeserializeObject<FuzzyStringQuery>(json);
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Field);
            Assert.AreEqual("value", query.Value);
            Assert.AreEqual(2, query.Fuzziness);
        }
    }
}
