using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Queries.FuzzyLikeThis;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Queries.FuzzyLikeThis
{
    [TestClass]
    public class UnitTests_FuzzyLikeThisField
    {
        [TestMethod]
        public void PASS_CreateRequest()
        {
            FuzzyLikeThisFieldQuery query = new FuzzyLikeThisFieldQuery("field", "value");
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Fields.First());
            Assert.AreEqual("value", query.Query);
        }

        [TestMethod]
        public void FAIL_CreateRequest_Field()
        {
            try
            {
                FuzzyLikeThisFieldQuery query = new FuzzyLikeThisFieldQuery(null, "value");
                Assert.Fail();
            }
            catch (ArgumentNullException argEx)
            {
                Assert.AreEqual("field", argEx.ParamName); 
            }
        }

        [TestMethod]
        public void FAIL_CreateRequest_Query()
        {
            try
            {
                FuzzyLikeThisFieldQuery query = new FuzzyLikeThisFieldQuery("field", null);
                Assert.Fail();
            }
            catch (ArgumentNullException argEx)
            {
                Assert.AreEqual("query", argEx.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            FuzzyLikeThisFieldQuery query = new FuzzyLikeThisFieldQuery("field", "value");
            string json = JsonConvert.SerializeObject(query);
            Assert.IsNotNull(json);

            string expectedJson = "{\"fuzzy_like_this_field\":{\"field\":{\"like_text\":\"value\"}}}";

            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"fuzzy_like_this_field\":{\"field\":{\"like_text\":\"value\"}}}";
            FuzzyLikeThisFieldQuery query = JsonConvert.DeserializeObject<FuzzyLikeThisFieldQuery>(json);

            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Fields.First());
            Assert.AreEqual("value", query.Query);
        }
    }
}
