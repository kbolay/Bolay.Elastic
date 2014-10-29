using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Queries.Regex;
using Newtonsoft.Json;
using System.Collections.Generic;
using Bolay.Elastic.QueryDSL.Regex;

namespace Bolay.Elastic.QueryDSL.Tests.Queries.Regex
{
    [TestClass]
    public class UnitTests_RegexQuery
    {
        [TestMethod]
        public void PASS_CreateQuery()
        {
            RegexQuery query = new RegexQuery("field", "pattern");
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Field);
            Assert.AreEqual("pattern", query.Pattern);
        }

        [TestMethod]
        public void FAIL_CreateQuery_Field()
        {
            try
            {
                RegexQuery query = new RegexQuery(null, "pattern");
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("field", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateQuery_Pattern()
        {
            try
            {
                RegexQuery query = new RegexQuery("field", null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("pattern", ex.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            RegexQuery query = new RegexQuery("field", "pattern");
            string json = JsonConvert.SerializeObject(query);

            Assert.IsNotNull(json);

            string expectedJson = "{\"regexp\":{\"field\":\"pattern\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"regexp\":{\"field\":\"pattern\"}}";
            RegexQuery query = JsonConvert.DeserializeObject<RegexQuery>(json);
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Field);
            Assert.AreEqual("pattern", query.Pattern);
        }

        [TestMethod]
        public void PASS_Serialize_Boost_Flags()
        {
            RegexQuery query = new RegexQuery("field", "pattern")
            {
                Boost = 1.2,
                RegexOperatorFlags = new List<RegexOperatorEnum>()
                {
                    RegexOperatorEnum.AnyString,
                    RegexOperatorEnum.Automaton
                }
            };
            string json = JsonConvert.SerializeObject(query);

            Assert.IsNotNull(json);

            string expectedJson = "{\"regexp\":{\"field\":{\"value\":\"pattern\",\"boost\":1.2,\"flags\":\"ANYSTRING|AUTOMATON\"}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_Boost_Flags()
        {
            string json = "{\"regexp\":{\"field\":{\"value\":\"pattern\",\"boost\":1.2,\"flags\":\"ANYSTRING|AUTOMATON\"}}}";
            RegexQuery query = JsonConvert.DeserializeObject<RegexQuery>(json);
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Field);
            Assert.AreEqual("pattern", query.Pattern);
            Assert.AreEqual(1.2, query.Boost);
            Assert.AreEqual(2, query.RegexOperatorFlags.Count());
        }
    }
}
