using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Filters.Or;
using Bolay.Elastic.QueryDSL.Filters;
using System.Collections.Generic;
using Bolay.Elastic.QueryDSL.Filters.Term;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Filters.Or
{
    [TestClass]
    public class UnitTests_OrFilter
    {
        [TestMethod]
        public void PASS_CreateFilter()
        {
            OrFilter filter = new OrFilter(new List<IFilter>() { new TermFilter("field", "value") });
            Assert.IsNotNull(filter);
            Assert.AreEqual(false, filter.Cache);
            Assert.AreEqual(null, filter.CacheKey);
            Assert.IsTrue(filter.Filters.First() is TermFilter);
            TermFilter termFilter = filter.Filters.First() as TermFilter;
            Assert.AreEqual("field", termFilter.Field);
            Assert.AreEqual("value", termFilter.Value);
        }

        [TestMethod]
        public void FAIL_CreateFilter()
        {
            try
            {
                OrFilter filter = new OrFilter(null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("filters", ex.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            OrFilter filter = new OrFilter(new List<IFilter>() { new TermFilter("field", "value") });
            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);
            string expectedJson = "{\"or\":[{\"term\":{\"field\":\"value\"}}]}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"or\":[{\"term\":{\"field\":\"value\"}}]}";
            OrFilter filter = JsonConvert.DeserializeObject<OrFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual(false, filter.Cache);
            Assert.AreEqual(null, filter.CacheKey);
            Assert.IsTrue(filter.Filters.First() is TermFilter);
            TermFilter termFilter = filter.Filters.First() as TermFilter;
            Assert.AreEqual("field", termFilter.Field);
            Assert.AreEqual("value", termFilter.Value);
        }

        [TestMethod]
        public void PASS_Serialize_Cache()
        {
            OrFilter filter = new OrFilter(new List<IFilter>() { new TermFilter("field", "value") }) { Cache = true };
            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);
            string expectedJson = "{\"or\":{\"filters\":[{\"term\":{\"field\":\"value\"}}],\"_cache\":true}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_Cache()
        {
            string json = "{\"or\":{\"filters\":[{\"term\":{\"field\":\"value\"}}],\"_cache\":true}}";
            OrFilter filter = JsonConvert.DeserializeObject<OrFilter>(json);

            Assert.IsNotNull(filter);
            Assert.AreEqual(true, filter.Cache);
            Assert.AreEqual(null, filter.CacheKey);
            Assert.IsTrue(filter.Filters.First() is TermFilter);
            TermFilter termFilter = filter.Filters.First() as TermFilter;
            Assert.AreEqual("field", termFilter.Field);
            Assert.AreEqual("value", termFilter.Value);
        }
    }
}
