using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Queries.Prefix;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Queries.Prefix
{
    [TestClass]
    public class UnitTests_PrefixQuery
    {
        [TestMethod]
        public void PASS_CreateQuery()
        {
            PrefixQuery query = new PrefixQuery("field", "value");
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Field);
            Assert.AreEqual("value", query.Value);
        }

        [TestMethod]
        public void FAIL_CreateQuery_Field()
        {
            try
            {
                PrefixQuery query = new PrefixQuery(null, "value");
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("field", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateQuery_Query()
        {
            try
            {
                PrefixQuery query = new PrefixQuery("field", null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("value", ex.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            PrefixQuery query = new PrefixQuery("field", "value");
            string json = JsonConvert.SerializeObject(query);

            Assert.IsNotNull(json);
            string expectedJson = "{\"prefix\":{\"field\":\"value\"}}";

            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"prefix\":{\"field\":\"value\"}}";
            PrefixQuery query = JsonConvert.DeserializeObject<PrefixQuery>(json);
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Field);
            Assert.AreEqual("value", query.Value);
            Assert.AreEqual(1.0, query.Boost);
        }

        [TestMethod]
        public void PASS_Serialize_Boost()
        {
            PrefixQuery query = new PrefixQuery("field", "value") { Boost = 1.2 };
            string json = JsonConvert.SerializeObject(query);

            Assert.IsNotNull(json);
            string expectedJson = "{\"prefix\":{\"field\":{\"value\":\"value\",\"boost\":1.2}}}";

            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_Boost()
        {
            string json = "{\"prefix\":{\"field\":{\"value\":\"value\",\"boost\":1.2}}}";
            PrefixQuery query = JsonConvert.DeserializeObject<PrefixQuery>(json);
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Field);
            Assert.AreEqual("value", query.Value);
            Assert.AreEqual(1.2, query.Boost);
        }
    }
}
