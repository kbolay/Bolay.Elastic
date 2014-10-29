using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Queries.Match;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Queries.Match
{
    [TestClass]
    public class UnitTests_MatchPhraseQuery
    {
        [TestMethod]
        public void PASS_CreateQuery()
        {
            MatchPhraseQuery query = new MatchPhraseQuery("field1", "value1");
            Assert.IsNotNull(query);
            Assert.AreEqual("field1", query.Field);
            Assert.AreEqual("value1", query.Query);
        }

        [TestMethod]
        public void FAIL_CreateQuery_Field()
        {
            try
            {
                MatchPhraseQuery query = new MatchPhraseQuery(null, "value1");
                Assert.Fail();
            }
            catch (ArgumentNullException argEx)
            {
                Assert.AreEqual("field", argEx.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateQuery_Query()
        {
            try
            {
                MatchPhraseQuery query = new MatchPhraseQuery("field1", null);
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
            MatchPhraseQuery query = new MatchPhraseQuery("field1", "value1");
            string json = JsonConvert.SerializeObject(query);

            Assert.IsNotNull(json);

            string expectedJson = "{\"match_phrase\":{\"field1\":\"value1\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"match_phrase\":{\"field1\":\"value1\"}}";

            MatchPhraseQuery query = JsonConvert.DeserializeObject<MatchPhraseQuery>(json);
            Assert.IsNotNull(query);
            Assert.AreEqual("field1", query.Field);
            Assert.AreEqual("value1", query.Query);
        }
    }
}
