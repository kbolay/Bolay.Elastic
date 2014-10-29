using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Filters.Regex;
using Newtonsoft.Json;
using Bolay.Elastic.QueryDSL.Regex;
using System.Collections.Generic;

namespace Bolay.Elastic.QueryDSL.Tests.Filters.Regex
{
    [TestClass]
    public class UnitTests_RegexFilter
    {
        [TestMethod]
        public void PASS_CreateFilter()
        {
            RegexFilter filter = new RegexFilter("field", "pattern");
            Assert.IsNotNull(filter);
            Assert.AreEqual("field", filter.Field);
            Assert.AreEqual("pattern", filter.Pattern);
        }

        [TestMethod]
        public void FAIL_CreateFilter_Field()
        {
            try
            {
                RegexFilter filter = new RegexFilter(null, "pattern");
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("field", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateFilter_Pattern()
        {
            try
            {
                RegexFilter filter = new RegexFilter("field", null);
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
            RegexFilter filter = new RegexFilter("field", "pattern");
            string json = JsonConvert.SerializeObject(filter);

            Assert.IsNotNull(json);

            string expectedJson = "{\"regexp\":{\"field\":\"pattern\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"regexp\":{\"field\":\"pattern\"}}";
            RegexFilter filter = JsonConvert.DeserializeObject<RegexFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("field", filter.Field);
            Assert.AreEqual("pattern", filter.Pattern);
        }

        [TestMethod]
        public void PASS_Serialize_Flags()
        {
            RegexFilter filter = new RegexFilter("field", "pattern")
            {
                RegexOperatorFlags = new List<RegexOperatorEnum>()
                {
                    RegexOperatorEnum.AnyString,
                    RegexOperatorEnum.Automaton
                }
            };
            string json = JsonConvert.SerializeObject(filter);

            Assert.IsNotNull(json);

            string expectedJson = "{\"regexp\":{\"field\":{\"value\":\"pattern\",\"flags\":\"ANYSTRING|AUTOMATON\"}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_Flags()
        {
            string json = "{\"regexp\":{\"field\":{\"value\":\"pattern\",\"flags\":\"ANYSTRING|AUTOMATON\"}}}";
            RegexFilter filter = JsonConvert.DeserializeObject<RegexFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("field", filter.Field);
            Assert.AreEqual("pattern", filter.Pattern);
            Assert.AreEqual(2, filter.RegexOperatorFlags.Count());
        }

        [TestMethod]
        public void PASS_Serialize_CacheName()
        {
            RegexFilter filter = new RegexFilter("field", "pattern")
            {
                CacheName = "name",
                Cache = true,
                CacheKey = "key"
            };

            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"regexp\":{\"field\":\"pattern\",\"_name\":\"name\",\"_cache\":true,\"_cache_key\":\"key\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_CacheName()
        {
            string json = "{\"regexp\":{\"field\":\"pattern\",\"_name\":\"name\",\"_cache\":true,\"_cache_key\":\"key\"}}";
            RegexFilter filter = JsonConvert.DeserializeObject<RegexFilter>(json);
            Assert.AreEqual("field", filter.Field);
            Assert.AreEqual("pattern", filter.Pattern);
            Assert.AreEqual(true, filter.Cache);
            Assert.AreEqual("name", filter.CacheName);
            Assert.AreEqual("key", filter.CacheKey);
        }
    }
}
