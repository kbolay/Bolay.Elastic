using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Queries.Boosting;
using Bolay.Elastic.QueryDSL.Queries.Term;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Queries.Boosting
{
    [TestClass]
    public class UnitTests_BoostingQuery
    {
        [TestMethod]
        public void PASS_CreateQuery()
        {
            BoostingQuery query = new BoostingQuery(new TermQuery("field1", "value1"), null, 1.0);
            Assert.IsNotNull(query);
        }

        [TestMethod]
        public void FAIL_CreateQuery_NoQueries()
        {
            try
            {
                BoostingQuery query = new BoostingQuery(null, null, 1.0);
                Assert.Fail();
            }
            catch(ArgumentNullException argEx)
            {
                Assert.AreEqual("query", argEx.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateQuery_NegativeBoost()
        {
            try
            {
                BoostingQuery query = new BoostingQuery(
                    new TermQuery("field1", "value1"), 
                    new TermQuery("field1", "value1"), 
                    0);
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException argEx)
            {
                Assert.AreEqual("negativeBoost", argEx.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            BoostingQuery query = new BoostingQuery(
                    new TermQuery("field1", "value1"),
                    new TermQuery("field1", "value2"),
                    1.0);

            string json = JsonConvert.SerializeObject(query);

            Assert.IsNotNull(json);

            string expectedJson = "{\"boosting\":{\"positive\":{\"term\":{\"field1\":\"value2\"}},\"negative\":{\"term\":{\"field1\":\"value1\"}},\"negative_boost\":1.0}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"boosting\":{\"positive\":{\"term\":{\"field1\":\"value1\"}},\"negative\":{\"term\":{\"field1\":\"value2\"}},\"negative_boost\":1.0}}";

            BoostingQuery query = JsonConvert.DeserializeObject<BoostingQuery>(json);

            Assert.IsNotNull(query);
            Assert.AreEqual("field1", (query.PositiveQuery as TermQuery).Field);
            Assert.AreEqual("value1", (query.PositiveQuery as TermQuery).Value.ToString());
            Assert.AreEqual("field1", (query.NegativeQuery as TermQuery).Field);
            Assert.AreEqual("value2", (query.NegativeQuery as TermQuery).Value.ToString());
            Assert.AreEqual(1.0, query.NegativeBoost);
        }
    }
}
