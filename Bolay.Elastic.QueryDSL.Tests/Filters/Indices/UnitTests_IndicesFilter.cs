using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Filters.Indices;
using System.Collections.Generic;
using Bolay.Elastic.QueryDSL.Filters.Term;
using Bolay.Elastic.QueryDSL.Filters;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Filters.Indices
{
    [TestClass]
    public class UnitTests_IndicesFilter
    {
        [TestMethod]
        public void PASS_CreateQuery()
        {
            IndicesFilter filter = new IndicesFilter(
                new List<string>() { "1", "2", "3" },
                new TermFilter("field", "value"),
                new TermFilter("field", "value"));
            Assert.IsNotNull(filter);
            Assert.AreEqual(3, filter.Indices.Count());
            Assert.IsTrue(filter.MatchingFilter is TermFilter);
            Assert.AreEqual("field", (filter.MatchingFilter as TermFilter).Field);
            Assert.AreEqual("value", (filter.MatchingFilter as TermFilter).Value);
            Assert.IsTrue(filter.NonMatchingFilter is TermFilter);
            Assert.AreEqual("field", (filter.NonMatchingFilter as TermFilter).Field);
            Assert.AreEqual("value", (filter.NonMatchingFilter as TermFilter).Value);
            Assert.AreEqual(null, filter.NonMatchingFilterType);
        }

        [TestMethod]
        public void PASS_CreateQuery_All()
        {
            IndicesFilter filter = new IndicesFilter(
                new List<string>() { "1", "2", "3" },
                new TermFilter("field", "value"),
                NonMatchingTypeEnum.All);
            Assert.IsNotNull(filter);
            Assert.AreEqual(3, filter.Indices.Count());
            Assert.IsTrue(filter.MatchingFilter is TermFilter);
            Assert.AreEqual("field", (filter.MatchingFilter as TermFilter).Field);
            Assert.AreEqual("value", (filter.MatchingFilter as TermFilter).Value);
            Assert.AreEqual(NonMatchingTypeEnum.All, filter.NonMatchingFilterType);
        }

        [TestMethod]
        public void PASS_CreateQuery_None()
        {
            IndicesFilter filter = new IndicesFilter(
                new List<string>() { "1", "2", "3" },
                new TermFilter("field", "value"),
                NonMatchingTypeEnum.None);
            Assert.IsNotNull(filter);
            Assert.AreEqual(3, filter.Indices.Count());
            Assert.IsTrue(filter.MatchingFilter is TermFilter);
            Assert.AreEqual("field", (filter.MatchingFilter as TermFilter).Field);
            Assert.AreEqual("value", (filter.MatchingFilter as TermFilter).Value);
            Assert.AreEqual(NonMatchingTypeEnum.None, filter.NonMatchingFilterType);
        }

        [TestMethod]
        public void FAIL_CreateQuery_Indices()
        {
            try
            {
                IndicesFilter filter = new IndicesFilter(null, null, NonMatchingTypeEnum.All);
                Assert.Fail();
            }
            catch (ArgumentNullException argEx)
            {
                Assert.AreEqual("indices", argEx.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateQuery_MatchingFilter()
        {
            try
            {
                IndicesFilter filter = new IndicesFilter(
                    new List<string>() { "1" },
                    null,
                    NonMatchingTypeEnum.All);
                Assert.Fail();
            }
            catch (ArgumentNullException argEx)
            {
                Assert.AreEqual("matchingFilter", argEx.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateQuery_NonMatchingFilter()
        {
            try
            {
                IndicesFilter filter = new IndicesFilter(
                    new List<string>() { "1" },
                    new TermFilter("field", "value"),
                    (IFilter)null);
                Assert.Fail();
            }
            catch (ArgumentNullException argEx)
            {
                Assert.AreEqual("nonMatchingFilter", argEx.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateQuery_NonMatchingFilterType()
        {
            try
            {
                IndicesFilter filter = new IndicesFilter(
                    new List<string>() { "1" },
                    new TermFilter("field", "value"),
                    (NonMatchingTypeEnum)null);
                Assert.Fail();
            }
            catch (ArgumentNullException argEx)
            {
                Assert.AreEqual("nonMatchingFilterType", argEx.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            IndicesFilter filter = new IndicesFilter(
                new List<string>() { "1", "2", "3" },
                new TermFilter("field", "value"),
                new TermFilter("field", "value"));

            string result = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(result);

            string expectedJson = "{\"indices\":{\"indices\":[\"1\",\"2\",\"3\"],\"filter\":{\"term\":{\"field\":\"value\"}},\"no_match_filter\":{\"term\":{\"field\":\"value\"}}}}";
            Assert.AreEqual(expectedJson, result);
        }

        [TestMethod]
        public void PASS_Serialize_Index()
        {
            IndicesFilter query = new IndicesFilter(
                new List<string>() { "1" },
                new TermFilter("field", "value"),
                new TermFilter("field", "value"));

            string result = JsonConvert.SerializeObject(query);
            Assert.IsNotNull(result);

            string expectedJson = "{\"indices\":{\"index\":\"1\",\"filter\":{\"term\":{\"field\":\"value\"}},\"no_match_filter\":{\"term\":{\"field\":\"value\"}}}}";
            Assert.AreEqual(expectedJson, result);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"indices\":{\"indices\":[\"1\",\"2\",\"3\"],\"filter\":{\"term\":{\"field\":\"value\"}},\"no_match_filter\":{\"term\":{\"field\":\"value\"}}}}";

            IndicesFilter query = JsonConvert.DeserializeObject<IndicesFilter>(json);

            Assert.IsNotNull(query);
            Assert.AreEqual(3, query.Indices.Count());
            Assert.IsTrue(query.MatchingFilter is TermFilter);
            Assert.AreEqual("field", (query.MatchingFilter as TermFilter).Field);
            Assert.AreEqual("value", (query.MatchingFilter as TermFilter).Value);
            Assert.IsTrue(query.NonMatchingFilter is TermFilter);
            Assert.AreEqual("field", (query.NonMatchingFilter as TermFilter).Field);
            Assert.AreEqual("value", (query.NonMatchingFilter as TermFilter).Value);
            Assert.AreEqual(null, query.NonMatchingFilterType);
        }

        [TestMethod]
        public void PASS_Deserialize_Index_All()
        {
            string json = "{\"indices\":{\"index\":\"1\",\"filter\":{\"term\":{\"field\":\"value\"}},\"no_match_filter\":\"all\"}}";

            IndicesFilter query = JsonConvert.DeserializeObject<IndicesFilter>(json);

            Assert.IsNotNull(query);
            Assert.AreEqual(1, query.Indices.Count());
            Assert.AreEqual("1", query.Indices.First());
            Assert.IsTrue(query.MatchingFilter is TermFilter);
            Assert.AreEqual("field", (query.MatchingFilter as TermFilter).Field);
            Assert.AreEqual("value", (query.MatchingFilter as TermFilter).Value);
            Assert.AreEqual(NonMatchingTypeEnum.All, query.NonMatchingFilterType);
        }
    }
}
