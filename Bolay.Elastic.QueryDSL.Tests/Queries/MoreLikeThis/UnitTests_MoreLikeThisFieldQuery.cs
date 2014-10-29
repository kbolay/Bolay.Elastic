using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Queries.MoreLikeThis;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Queries.MoreLikeThis
{
    [TestClass]
    public class UnitTests_MoreLikeThisFieldQuery
    {
        [TestMethod]
        public void PASS_CreateRequest()
        {
            MoreLikeThisFieldQuery query = new MoreLikeThisFieldQuery("field", "value");

            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Fields.First());
            Assert.AreEqual("value", query.Query);
        }

        [TestMethod]
        public void FAIL_CreateRequest_Fields()
        {
            try
            {
                MoreLikeThisFieldQuery query = new MoreLikeThisFieldQuery(null, "value");
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
                MoreLikeThisFieldQuery query = new MoreLikeThisFieldQuery("field", null);
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
            MoreLikeThisFieldQuery query = new MoreLikeThisFieldQuery("field", "value");

            string json = JsonConvert.SerializeObject(query);
            Assert.IsNotNull(json);
            string expectedJson = "{\"more_like_this_field\":{\"field\":{\"like_text\":\"value\"}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"more_like_this_field\":{\"field\":{\"like_text\":\"value\"}}}";

            MoreLikeThisFieldQuery query = JsonConvert.DeserializeObject<MoreLikeThisFieldQuery>(json);
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Fields.First());
            Assert.AreEqual("value", query.Query);
        }
    }
}
