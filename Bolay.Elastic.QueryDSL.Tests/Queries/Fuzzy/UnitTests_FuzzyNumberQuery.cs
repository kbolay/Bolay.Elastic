using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Queries.Fuzzy;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Queries.Fuzzy
{
    [TestClass]
    public class UnitTests_FuzzyNumberQuery
    {
        [TestMethod]
        public void PASS_CreateQuery()
        {
            FuzzyNumberQuery query = new FuzzyNumberQuery("field", 1);
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Field);
            Assert.AreEqual((Int64)1, query.Value);
        }

        [TestMethod]
        public void PASS_CreateQuery_Fuzziness()
        {
            FuzzyNumberQuery query = new FuzzyNumberQuery("field", 1, 1);
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Field);
            Assert.AreEqual((Int64)1, query.Value);
            Assert.AreEqual((Int64)1, query.Fuzziness);
        }

        [TestMethod]
        public void FAIL_CreateQuery_Field()
        {
            try
            {
                FuzzyNumberQuery query = new FuzzyNumberQuery(null, 1, 1);
                Assert.Fail();
            }
            catch (ArgumentNullException argEx)
            {
                Assert.AreEqual("field", argEx.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateQuery_Fuzziness()
        {
            try
            {
                FuzzyNumberQuery query = new FuzzyNumberQuery("field", 0, -1);
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
            FuzzyNumberQuery query = new FuzzyNumberQuery("field", 1);
            string json = JsonConvert.SerializeObject(query);

            Assert.IsNotNull(json);
            string expectedJson = "{\"fuzzy\":{\"field\":1}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Serialize_Fuzziness()
        {
            FuzzyNumberQuery query = new FuzzyNumberQuery("field", 1, 2);
            string json = JsonConvert.SerializeObject(query);

            Assert.IsNotNull(json);
            string expectedJson = "{\"fuzzy\":{\"field\":{\"value\":1,\"fuzziness\":2}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"fuzzy\":{\"field\":1}}";
            FuzzyNumberQuery query = JsonConvert.DeserializeObject<FuzzyNumberQuery>(json);
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Field);
            Assert.AreEqual((Int64)1, query.Value);
        }

        [TestMethod]
        public void PASS_Deserialize_Fuzziness()
        {
            string json = "{\"fuzzy\":{\"field\":{\"value\":1,\"fuzziness\":2}}}";
            FuzzyNumberQuery query = JsonConvert.DeserializeObject<FuzzyNumberQuery>(json);
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Field);
            Assert.AreEqual((Int64)1, query.Value);
            Assert.AreEqual((Int64)2, query.Fuzziness);
        }
    }
}
