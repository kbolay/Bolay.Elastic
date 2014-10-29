using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Queries.Term;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Queries.Term
{
    [TestClass]
    public class UnitTests_TermQuery
    {
        [TestMethod]
        public void PASS_CreateQuery()
        {
            TermQuery query = new TermQuery("field", "value1");
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Field);
            Assert.AreEqual("value1", query.Value.ToString());
        }

        [TestMethod]
        public void FAIL_CreateQuery_Field()
        {
            try
            {
                TermQuery query = new TermQuery(null, "value1");
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
                TermQuery query = new TermQuery("field", null);
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
            TermQuery query = new TermQuery("field", "value1");

            string result = JsonConvert.SerializeObject(query);
            Assert.IsNotNull(result);

            string expectedJson = "{\"term\":{\"field\":\"value1\"}}";
            Assert.AreEqual(expectedJson, result);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string termQuery = "{\"term\":{\"field\":\"value1\"}}";

            TermQuery query = JsonConvert.DeserializeObject<TermQuery>(termQuery);

            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Field);
            Assert.AreEqual("value1", query.Value.ToString());
        }

        [TestMethod]
        public void PASS_Serialize_Boost()
        {
            TermQuery query = new TermQuery("field", "value") { Boost = 1.2 };
            string json = JsonConvert.SerializeObject(query);
            Assert.IsNotNull(json);

            string expectedJson = "{\"term\":{\"field\":{\"value\":\"value\",\"boost\":1.2}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_Boost()
        {
            string json = "{\"term\":{\"field\":{\"value\":\"value\",\"boost\":1.2}}}";
            TermQuery query = JsonConvert.DeserializeObject<TermQuery>(json);
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Field);
            Assert.AreEqual("value", query.Value.ToString());
            Assert.AreEqual(1.2, query.Boost);
        }

        [TestMethod]
        public void PASS_Serialize_Number()
        {
            TermQuery query = new TermQuery("field", 15); ;
            string json = JsonConvert.SerializeObject(query);
            Assert.IsNotNull(json);

            string expectedJson = "{\"term\":{\"field\":15}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_Number()
        {
            string json = "{\"term\":{\"field\":15}}";
            TermQuery query = JsonConvert.DeserializeObject<TermQuery>(json);
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Field);
            Assert.AreEqual(15.ToString(), query.Value.ToString());
        }
    }
}
