using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Filters.Terms;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Filters.Terms
{
    [TestClass]
    public class UnitTests_TermsFilter
    {
        [TestMethod]
        public void PASS_CreateFilter_Terms()
        {
            TermsFilter filter = new TermsFilter("field", new List<object>() { "1", "2" });
            Assert.IsNotNull(filter);
            Assert.AreEqual("field", filter.Field);
            Assert.AreEqual(2, filter.Terms.Count());
        }

        [TestMethod]
        public void FAIL_CreateFilter_Terms_Field()
        {
            try
            {
                TermsFilter filter = new TermsFilter(null, new List<object>() { "1", "2" });
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("field", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateFilter_Terms_Terms()
        {
            try
            {
                TermsFilter filter = new TermsFilter("field", null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("terms", ex.ParamName);
            }
        }

        [TestMethod]
        public void PASS_CreateFilter_IndexedTerms()
        {
            TermsFilter filter = new TermsFilter("field", "index", "type", "id", "path");
            Assert.IsNotNull(filter);
            Assert.AreEqual("field", filter.Field);
            Assert.AreEqual("index", filter.Index);
            Assert.AreEqual("type", filter.DocumentType);
            Assert.AreEqual("id", filter.DocumentId);
            Assert.AreEqual("path", filter.Path);
        }

        [TestMethod]
        public void FAIL_CreateFilter_IndexTerms_Index()
        {
            try
            {
                TermsFilter filter = new TermsFilter("field", "", "type", "id", "path");
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("index", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateFilter_IndexTerms_DocumentType()
        {
            try
            {
                TermsFilter filter = new TermsFilter("field", "index", "", "id", "path");
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("documentType", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateFilter_IndexTerms_DocumentId()
        {
            try
            {
                TermsFilter filter = new TermsFilter("field", "index", "type", null, "path");
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("documentId", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateFilter_IndexTerms_Path()
        {
            try
            {
                TermsFilter filter = new TermsFilter("field", "index", "type", "id", "");
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("path", ex.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize_Terms_BoolNoCache()
        {
            TermsFilter filter = new TermsFilter("field", new List<object>() { "1", "2" }) { ExecutionType = ExecutionTypeEnum.BoolNoCache };
            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);
            string expectedJson = "{\"terms\":{\"field\":[\"1\",\"2\"],\"execution\":\"bool_nocache\",\"_cache\":true}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_Terms_BoolNoCache()
        {
            string json = "{\"terms\":{\"field\":[\"1\",\"2\"],\"execution\":\"bool_nocache\",\"_cache\":true}}";
            TermsFilter filter = JsonConvert.DeserializeObject<TermsFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("field", filter.Field);
            Assert.AreEqual(2, filter.Terms.Count());
            Assert.AreEqual(ExecutionTypeEnum.BoolNoCache, filter.ExecutionType);
            Assert.AreEqual(true, filter.Cache);
        }

        [TestMethod]
        public void PASS_Serialize_IndexedTerms()
        {
            TermsFilter filter = new TermsFilter("field", "index", "type", "id", "path") { CacheKey = "cache_key"};
            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);
            string expectedJson = "{\"terms\":{\"field\":{\"index\":\"index\",\"type\":\"type\",\"id\":\"id\",\"path\":\"path\"},\"_cache_key\":\"cache_key\"}}";

            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_IndexedTerms()
        {
            string json = "{\"terms\":{\"field\":{\"index\":\"index\",\"type\":\"type\",\"id\":\"id\",\"path\":\"path\"},\"_cache_key\":\"cache_key\"}}";
            TermsFilter filter = JsonConvert.DeserializeObject<TermsFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("field", filter.Field);
            Assert.AreEqual("index", filter.Index);
            Assert.AreEqual("type", filter.DocumentType);
            Assert.AreEqual("id", filter.DocumentId);
            Assert.AreEqual("path", filter.Path);
            Assert.AreEqual("cache_key", filter.CacheKey);
        }
    }
}
