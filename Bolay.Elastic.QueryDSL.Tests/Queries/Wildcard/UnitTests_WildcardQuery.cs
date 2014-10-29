using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Queries.Wildcard;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Queries.Wildcard
{
    [TestClass]
    public class UnitTests_WildcardQuery
    {
        [TestMethod]
        public void PASS_CreateQuery()
        {
            WildcardQuery query = new WildcardQuery("field", "value");
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Field);
            Assert.AreEqual("value", query.Value);
            Assert.AreEqual(1.0, query.Boost);
        }

        [TestMethod]
        public void FAIL_CreateQuery_Field()
        {
            try
            {
                WildcardQuery query = new WildcardQuery(null, "value");
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("field", ex.ParamName);
            }            
        }

        [TestMethod]
        public void FAIL_CreateQuery_Value()
        {
            try
            {
                WildcardQuery query = new WildcardQuery("field", null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("value", ex.ParamName);
            }  
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            WildcardQuery query = new WildcardQuery("field", "value");
            string json = JsonConvert.SerializeObject(query);
            Assert.IsNotNull(json);
            string expectedJson = "{\"wildcard\":{\"field\":\"value\"}}";
            Assert.IsNotNull(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"wildcard\":{\"field\":\"value\"}}";
            WildcardQuery query = JsonConvert.DeserializeObject<WildcardQuery>(json);
            Assert.AreEqual("field", query.Field);
            Assert.AreEqual("value", query.Value);
            Assert.AreEqual(1.0, query.Boost);
        }
    }
}
