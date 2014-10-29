using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Queries.Match;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Queries.Match
{
    [TestClass]
    public class UnitTests_MatchPhrasePrefixQuery
    {
        [TestMethod]
        public void PASS_CreateQuery()
        {
            MatchPhrasePrefixQuery query = new MatchPhrasePrefixQuery("field1", "value1");
            Assert.IsNotNull(query);
            Assert.AreEqual("field1", query.Field);
            Assert.AreEqual("value1", query.Query);
        }

        [TestMethod]
        public void FAIL_CreateQuery_Field()
        {
            try
            {
                MatchPhrasePrefixQuery query = new MatchPhrasePrefixQuery(null, "value1");
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
                MatchPhrasePrefixQuery query = new MatchPhrasePrefixQuery("field1", null);
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
            MatchPhrasePrefixQuery query = new MatchPhrasePrefixQuery("field1", "value1");
            string json = JsonConvert.SerializeObject(query);

            Assert.IsNotNull(json);

            string expectedJson = "{\"match_phrase_prefix\":{\"field1\":\"value1\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"match_phrase_prefix\":{\"field1\":\"value1\"}}";

            MatchPhrasePrefixQuery query = JsonConvert.DeserializeObject<MatchPhrasePrefixQuery>(json);
            Assert.IsNotNull(query);
            Assert.AreEqual("field1", query.Field);
            Assert.AreEqual("value1", query.Query);
        }
    }
}
