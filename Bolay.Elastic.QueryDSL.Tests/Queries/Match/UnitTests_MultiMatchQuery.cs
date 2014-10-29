using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Queries.Match;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Queries.Match
{
    [TestClass]
    public class UnitTests_MultiMatchQuery
    {
        [TestMethod]
        public void PASS_CreateQuery()
        {
            MultiMatchQuery query = new MultiMatchQuery(
                new List<BoostedField>()
                {
                    new BoostedField("field1")
                }, "value1");
            Assert.IsNotNull(query);
            Assert.AreEqual("field1", query.Fields.First());
            Assert.AreEqual("value1", query.Query);
        }

        [TestMethod]
        public void FAIL_CreateQuery_Field()
        {
            try
            {
                MultiMatchQuery query = new MultiMatchQuery(new List<BoostedField>(), "value1");
                Assert.Fail();
            }
            catch (ArgumentNullException argEx)
            {
                Assert.AreEqual("boostedFields", argEx.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateQuery_Query()
        {
            try
            {
                MultiMatchQuery query = new MultiMatchQuery(
                new List<BoostedField>()
                {
                    new BoostedField("field1")
                }, null);
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
            MultiMatchQuery query = new MultiMatchQuery(
                new List<BoostedField>()
                {
                    new BoostedField("field1")
                }, "value1");
            string json = JsonConvert.SerializeObject(query);

            Assert.IsNotNull(json);

            string expectedJson = "{\"multi_match\":{\"fields\":[\"field1\"],\"query\":\"value1\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"multi_match\":{\"fields\":[\"field1\"],\"query\":\"value1\"}}";

            MultiMatchQuery query = JsonConvert.DeserializeObject<MultiMatchQuery>(json);
            Assert.IsNotNull(query);
            Assert.AreEqual("field1", query.Fields.First());
            Assert.AreEqual("value1", query.Query);
        }
    }
}
