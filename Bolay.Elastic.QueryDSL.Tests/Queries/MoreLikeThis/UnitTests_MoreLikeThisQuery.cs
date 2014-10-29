using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Queries.MoreLikeThis;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Queries.MoreLikeThis
{
    [TestClass]
    public class UnitTests_MoreLikeThisQuery
    {
        [TestMethod]
        public void PASS_CreateRequest()
        {
            MoreLikeThisQuery query = new MoreLikeThisQuery("value");

            Assert.IsNotNull(query);
            Assert.AreEqual("value", query.Query);
        }

        [TestMethod]
        public void FAIL_CreateRequest_Fields()
        {
            try
            {
                MoreLikeThisQuery query = new MoreLikeThisQuery(null, "value");
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("field", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateRequest_Query()
        {
            try
            {
                MoreLikeThisQuery query = new MoreLikeThisQuery(null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("query", ex.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            MoreLikeThisQuery query = new MoreLikeThisQuery(
                new List<string>() { "field1", "field2" },
                "value");

            string json = JsonConvert.SerializeObject(query);
            Assert.IsNotNull(json);
            string expectedJson = "{\"more_like_this\":{\"fields\":[\"field1\",\"field2\"],\"like_text\":\"value\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"more_like_this\":{\"fields\":[\"field1\",\"field2\"],\"like_text\":\"value\"}}";

            MoreLikeThisQuery query = JsonConvert.DeserializeObject<MoreLikeThisQuery>(json);
            Assert.IsNotNull(query);
            Assert.AreEqual(2, query.Fields.Count());
            Assert.AreEqual("value", query.Query);
        }
    }
}
