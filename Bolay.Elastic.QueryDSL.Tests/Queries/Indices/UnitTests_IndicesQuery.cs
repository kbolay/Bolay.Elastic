using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Queries.Indices;
using System.Collections.Generic;
using Bolay.Elastic.QueryDSL.Queries.Term;
using Bolay.Elastic.QueryDSL.Queries;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Queries.Indices
{
    [TestClass]
    public class UnitTests_IndicesQuery
    {
        [TestMethod]
        public void PASS_CreateQuery()
        {
            IndicesQuery query = new IndicesQuery(
                new List<string>() { "1", "2", "3" },
                new TermQuery("field", "value"),
                new TermQuery("field", "value"));
            Assert.IsNotNull(query);
            Assert.AreEqual(3, query.Indices.Count());
            Assert.IsTrue(query.MatchingQuery is TermQuery);
            Assert.AreEqual("field", (query.MatchingQuery as TermQuery).Field);
            Assert.AreEqual("value", (query.MatchingQuery as TermQuery).Value);
            Assert.IsTrue(query.NonMatchingQuery is TermQuery);
            Assert.AreEqual("field", (query.NonMatchingQuery as TermQuery).Field);
            Assert.AreEqual("value", (query.NonMatchingQuery as TermQuery).Value);
            Assert.AreEqual(null, query.NonMatchingQueryType);
        }

        [TestMethod]
        public void PASS_CreateQuery_All()
        {
            IndicesQuery query = new IndicesQuery(
                new List<string>() { "1", "2", "3" },
                new TermQuery("field", "value"),
                NonMatchingTypeEnum.All);
            Assert.IsNotNull(query);
            Assert.AreEqual(3, query.Indices.Count());
            Assert.IsTrue(query.MatchingQuery is TermQuery);
            Assert.AreEqual("field", (query.MatchingQuery as TermQuery).Field);
            Assert.AreEqual("value", (query.MatchingQuery as TermQuery).Value);
            Assert.AreEqual(NonMatchingTypeEnum.All, query.NonMatchingQueryType);
        }

        [TestMethod]
        public void PASS_CreateQuery_None()
        {
            IndicesQuery query = new IndicesQuery(
                new List<string>() { "1", "2", "3" },
                new TermQuery("field", "value"),
                NonMatchingTypeEnum.None);
            Assert.IsNotNull(query);
            Assert.AreEqual(3, query.Indices.Count());
            Assert.IsTrue(query.MatchingQuery is TermQuery);
            Assert.AreEqual("field", (query.MatchingQuery as TermQuery).Field);
            Assert.AreEqual("value", (query.MatchingQuery as TermQuery).Value);
            Assert.AreEqual(NonMatchingTypeEnum.None, query.NonMatchingQueryType);
        }

        [TestMethod]
        public void FAIL_CreateQuery_Indices()
        {
            try
            {
                IndicesQuery query = new IndicesQuery(null, null, NonMatchingTypeEnum.All);
                Assert.Fail();
            }
            catch (ArgumentNullException argEx)
            {
                Assert.AreEqual("indices", argEx.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateQuery_MatchingQuery()
        {
            try
            {
                IndicesQuery query = new IndicesQuery(
                    new List<string>() { "1" }, 
                    null, 
                    NonMatchingTypeEnum.All);
                Assert.Fail();
            }
            catch (ArgumentNullException argEx)
            {
                Assert.AreEqual("matchingQuery", argEx.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateQuery_NonMatchingQuery()
        {
            try
            {
                IndicesQuery query = new IndicesQuery(
                    new List<string>() { "1" },
                    new TermQuery("field", "value"),
                    (IQuery)null);
                Assert.Fail();
            }
            catch (ArgumentNullException argEx)
            {
                Assert.AreEqual("nonMatchingQuery", argEx.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateQuery_NonMatchingQueryType()
        {
            try
            {
                IndicesQuery query = new IndicesQuery(
                    new List<string>() { "1" },
                    new TermQuery("field", "value"),
                    (NonMatchingTypeEnum)null);
                Assert.Fail();
            }
            catch (ArgumentNullException argEx)
            {
                Assert.AreEqual("nonMatchingQueryType", argEx.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            IndicesQuery query = new IndicesQuery(
                new List<string>() { "1", "2", "3" },
                new TermQuery("field", "value"),
                new TermQuery("field", "value"));

            string result = JsonConvert.SerializeObject(query);
            Assert.IsNotNull(result);

            string expectedJson = "{\"indices\":{\"indices\":[\"1\",\"2\",\"3\"],\"query\":{\"term\":{\"field\":\"value\"}},\"no_match_query\":{\"term\":{\"field\":\"value\"}}}}";
            Assert.AreEqual(expectedJson, result);
        }

        [TestMethod]
        public void PASS_Serialize_Index()
        {
            IndicesQuery query = new IndicesQuery(
                new List<string>() { "1" },
                new TermQuery("field", "value"),
                new TermQuery("field", "value"));

            string result = JsonConvert.SerializeObject(query);
            Assert.IsNotNull(result);

            string expectedJson = "{\"indices\":{\"index\":\"1\",\"query\":{\"term\":{\"field\":\"value\"}},\"no_match_query\":{\"term\":{\"field\":\"value\"}}}}";
            Assert.AreEqual(expectedJson, result);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"indices\":{\"indices\":[\"1\",\"2\",\"3\"],\"query\":{\"term\":{\"field\":\"value\"}},\"no_match_query\":{\"term\":{\"field\":\"value\"}}}}";

            IndicesQuery query = JsonConvert.DeserializeObject<IndicesQuery>(json);

            Assert.IsNotNull(query);
            Assert.AreEqual(3, query.Indices.Count());
            Assert.IsTrue(query.MatchingQuery is TermQuery);
            Assert.AreEqual("field", (query.MatchingQuery as TermQuery).Field);
            Assert.AreEqual("value", (query.MatchingQuery as TermQuery).Value);
            Assert.IsTrue(query.NonMatchingQuery is TermQuery);
            Assert.AreEqual("field", (query.NonMatchingQuery as TermQuery).Field);
            Assert.AreEqual("value", (query.NonMatchingQuery as TermQuery).Value);
            Assert.AreEqual(null, query.NonMatchingQueryType);
        }

        [TestMethod]
        public void PASS_Deserialize_Index_All()
        {
            string json = "{\"indices\":{\"index\":\"1\",\"query\":{\"term\":{\"field\":\"value\"}},\"no_match_query\":\"all\"}}";

            IndicesQuery query = JsonConvert.DeserializeObject<IndicesQuery>(json);

            Assert.IsNotNull(query);
            Assert.AreEqual(1, query.Indices.Count());
            Assert.AreEqual("1", query.Indices.First());
            Assert.IsTrue(query.MatchingQuery is TermQuery);
            Assert.AreEqual("field", (query.MatchingQuery as TermQuery).Field);
            Assert.AreEqual("value", (query.MatchingQuery as TermQuery).Value);
            Assert.AreEqual(NonMatchingTypeEnum.All, query.NonMatchingQueryType);            
        }
    }
}
