using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Queries.FuzzyLikeThis;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Bolay.Elastic.QueryDSL.Tests.Queries.FuzzyLikeThis
{
    [TestClass]
    public class UnitTests_FuzzyLikeThis
    {
        [TestMethod]
        public void PASS_CreateRequest()
        {
            FuzzyLikeThisQuery query = new FuzzyLikeThisQuery(new List<string>(){"field"}, "value");
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Fields.First());
            Assert.AreEqual("value", query.Query);
        }

        [TestMethod]
        public void FAIL_CreateRequest_Field()
        {
            try
            {
                FuzzyLikeThisQuery query = new FuzzyLikeThisQuery(null, "value");
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
                FuzzyLikeThisQuery query = new FuzzyLikeThisQuery(new List<string>() { "field" }, null);
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
            FuzzyLikeThisQuery query = new FuzzyLikeThisQuery(new List<string>() { "field" }, "value");
            string json = JsonConvert.SerializeObject(query);
            Assert.IsNotNull(json);

            string expectedJson = "{\"fuzzy_like_this\":{\"fields\":[\"field\"],\"like_text\":\"value\"}}";

            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"fuzzy_like_this\":{\"fields\":[\"field\"],\"like_text\":\"value\"}}";
            FuzzyLikeThisQuery query = JsonConvert.DeserializeObject<FuzzyLikeThisQuery>(json);

            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Fields.First());
            Assert.AreEqual("value", query.Query);
        }
    }
}
