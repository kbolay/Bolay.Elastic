using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Queries.Filtered;
using Bolay.Elastic.QueryDSL.Queries.Term;
using Bolay.Elastic.QueryDSL.Filters.Term;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Queries.Filtered
{
    [TestClass]
    public class UnitTests_FilteredQuery
    {
        [TestMethod]
        public void PASS_CreateQuery()
        {
            FilteredQuery query = new FilteredQuery(
                    new TermQuery("field", "value"),
                    new TermFilter("field", "value")
                );

            Assert.IsNotNull(query);
            Assert.AreEqual("field", (query.Query as TermQuery).Field);
            Assert.AreEqual("value", (query.Query as TermQuery).Value);
            Assert.AreEqual("field", (query.Filter as TermFilter).Field);
            Assert.AreEqual("value", (query.Filter as TermFilter).Value);
        }

        [TestMethod]
        public void FAIL_CreateQuery_Query()
        {
            try
            {
                FilteredQuery query = new FilteredQuery(
                    null,
                    new TermFilter("field", "value")
                );
                Assert.Fail();
            }
            catch (ArgumentNullException argEx)
            {
                Assert.AreEqual("query", argEx.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateQuery_Filter()
        {
            try
            {
                FilteredQuery query = new FilteredQuery(
                    new TermQuery("field", "value"),
                    null
                );
                Assert.Fail();
            }
            catch (ArgumentNullException argEx)
            {
                Assert.AreEqual("filter", argEx.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            FilteredQuery query = new FilteredQuery(
                        new TermQuery("field", "value"),
                        new TermFilter("field", "value")
                    );

            string json = JsonConvert.SerializeObject(query);
            Assert.IsNotNull(json);

            string expectedJson = "{\"filtered\":{\"query\":{\"term\":{\"field\":\"value\"}},\"filter\":{\"term\":{\"field\":\"value\"}}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"filtered\":{\"query\":{\"term\":{\"field\":\"value\"}},\"filter\":{\"term\":{\"field\":\"value\"}}}}";

            FilteredQuery query = JsonConvert.DeserializeObject<FilteredQuery>(json);
            Assert.IsNotNull(query);
            Assert.AreEqual("field", (query.Query as TermQuery).Field);
            Assert.AreEqual("value", (query.Query as TermQuery).Value);
            Assert.AreEqual("field", (query.Filter as TermFilter).Field);
            Assert.AreEqual("value", (query.Filter as TermFilter).Value);
        }
    }
}
