using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Filters.And;
using Bolay.Elastic.QueryDSL.Filters.Term;
using Bolay.Elastic.QueryDSL.Filters;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Filters.And
{
    [TestClass]
    public class UnitTests_AndFilter
    {
        [TestMethod]
        public void PASS_CreateFilter()
        {
            AndFilter filter = new AndFilter(new List<IFilter>() { new TermFilter("field", "value") });
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
                AndFilter filter = new AndFilter(null);
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
            AndFilter filter = new AndFilter(new List<IFilter>() { new TermFilter("field", "value") });
            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);
            string expectedJson = "{\"and\":[{\"term\":{\"field\":\"value\"}}]}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"and\":[{\"term\":{\"field\":\"value\"}}]}";
            AndFilter filter = JsonConvert.DeserializeObject<AndFilter>(json);
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
            AndFilter filter = new AndFilter(new List<IFilter>() { new TermFilter("field", "value") }) { Cache = true };
            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);
            string expectedJson = "{\"and\":{\"filters\":[{\"term\":{\"field\":\"value\"}}],\"_cache\":true}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_Cache()
        {
            string json = "{\"and\":{\"filters\":[{\"term\":{\"field\":\"value\"}}],\"_cache\":true}}";
            AndFilter filter = JsonConvert.DeserializeObject<AndFilter>(json);

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
