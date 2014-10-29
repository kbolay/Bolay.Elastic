using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Queries.Bool;
using Bolay.Elastic.QueryDSL.Queries.Term;
using Bolay.Elastic.QueryDSL.Queries;
using System.Collections.Generic;
using Newtonsoft.Json;
using Bolay.Elastic.QueryDSL.MinimumShouldMatch;

namespace Bolay.Elastic.QueryDSL.Tests.Queries.Bool
{
    [TestClass]
    public class UnitTests_BoolQuery
    {
        [TestMethod]
        public void PASS_CreateQuery()
        {
            List<IQuery> mustQueries = new List<IQuery>
            {
                new TermQuery("field", "value1"),
                new TermQuery("field", "value2")
            };

            List<IQuery> mustNotQueries = new List<IQuery>()
            {
                new TermQuery("field", "value3")
            };

            List<IQuery> shouldQueries = new List<IQuery>()
            {
                new TermQuery("field", "value4"),
                new TermQuery("field", "value5")
            };

            BoolQuery query = new BoolQuery(mustQueries, mustNotQueries, shouldQueries);

            Assert.IsNotNull(query);
        }

        [TestMethod]
        public void FAIL_CreateQuery_NoQueries()
        {
            try
            {
                BoolQuery query = new BoolQuery(null, null, null);
                Assert.Fail();
            }
            catch (ArgumentNullException argEx)
            {
                Assert.AreEqual("queries", argEx.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateQuery_MinimumShouldMatch()
        {
            List<IQuery> mustQueries = new List<IQuery>
            {
                new TermQuery("field", "value1"),
                new TermQuery("field", "value2")
            };

            try
            {
                BoolQuery query = new BoolQuery(mustQueries, null, null)
                {
                    MinimumShouldMatch = new IntegerMatch(0)
                };
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException argEx)
            {
                Assert.AreEqual("value", argEx.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize_Terms()
        {
            List<IQuery> mustQueries = new List<IQuery>
            {
                new TermQuery("field", "value1"),
                new TermQuery("field", "value2")
            };

            List<IQuery> mustNotQueries = new List<IQuery>()
            {
                new TermQuery("field", "value3")
            };

            List<IQuery> shouldQueries = new List<IQuery>()
            {
                new TermQuery("field", "value4"),
                new TermQuery("field", "value5")
            };

            BoolQuery query = new BoolQuery(mustQueries, mustNotQueries, shouldQueries);

            string result = JsonConvert.SerializeObject(query);
            Assert.IsNotNull(result);

            string expectedResult = "{\"bool\":{\"must\":[{\"term\":{\"field\":\"value1\"}},{\"term\":{\"field\":\"value2\"}}],\"must_not\":[{\"term\":{\"field\":\"value3\"}}],\"should\":[{\"term\":{\"field\":\"value4\"}},{\"term\":{\"field\":\"value5\"}}]}}";

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void PASS_Deserialize_Terms()
        {
            string jsonQuery = "{\"bool\":{\"must\":[{\"term\":{\"field\":\"value1\"}},{\"term\":{\"field\":\"value2\"}}],\"must_not\":[{\"term\":{\"field\":\"value3\"}}],\"should\":[{\"term\":{\"field\":\"value4\"}},{\"term\":{\"field\":\"value5\"}}]}}";

            BoolQuery query = JsonConvert.DeserializeObject<BoolQuery>(jsonQuery);
            Assert.IsNotNull(query);
            Assert.AreEqual(2, query.MustQueries.Count());
            Assert.AreEqual(1, query.MustNotQueries.Count());
            Assert.AreEqual(2, query.ShouldQueries.Count());
            Assert.AreEqual(1.0, query.Boost);
            Assert.AreEqual(false, query.DisableCoords);
            Assert.AreEqual(1, query.MinimumShouldMatch.GetValue());
        }
    }
}
