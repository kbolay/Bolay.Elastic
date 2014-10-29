using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Bolay.Elastic.QueryDSL.Filters;
using Bolay.Elastic.QueryDSL.Filters.Term;
using Bolay.Elastic.QueryDSL.Filters.Bool;
using Bolay.Elastic.QueryDSL.MinimumShouldMatch;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Filters.Bool
{
    [TestClass]
    public class UnitTests_BoolFilter
    {
        [TestMethod]
        public void PASS_CreateFilter()
        {
            List<IFilter> mustFilters = new List<IFilter>
            {
                new TermFilter("field", "value1"),
                new TermFilter("field", "value2")
            };

            List<IFilter> mustNotFilters = new List<IFilter>()
            {
                new TermFilter("field", "value3")
            };

            List<IFilter> shouldFilters = new List<IFilter>()
            {
                new TermFilter("field", "value4"),
                new TermFilter("field", "value5")
            };

            BoolFilter query = new BoolFilter(mustFilters, mustNotFilters, shouldFilters);

            Assert.IsNotNull(query);
        }

        [TestMethod]
        public void FAIL_CreateQuery_NoFilters()
        {
            try
            {
                BoolFilter query = new BoolFilter(null, null, null);
                Assert.Fail();
            }
            catch (ArgumentNullException argEx)
            {
                Assert.AreEqual("filters", argEx.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateFilter_MinimumShouldMatch()
        {
            List<IFilter> mustFilters = new List<IFilter>
            {
                new TermFilter("field", "value1"),
                new TermFilter("field", "value2")
            };

            try
            {
                BoolFilter query = new BoolFilter(mustFilters, null, null)
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
            List<IFilter> mustFilters = new List<IFilter>
            {
                new TermFilter("field", "value1"),
                new TermFilter("field", "value2")
            };

            List<IFilter> mustNotFilters = new List<IFilter>()
            {
                new TermFilter("field", "value3")
            };

            List<IFilter> shouldFilters = new List<IFilter>()
            {
                new TermFilter("field", "value4"),
                new TermFilter("field", "value5")
            };

            BoolFilter query = new BoolFilter(mustFilters, mustNotFilters, shouldFilters);

            string result = JsonConvert.SerializeObject(query);
            Assert.IsNotNull(result);

            string expectedResult = "{\"bool\":{\"must\":[{\"term\":{\"field\":\"value1\"}},{\"term\":{\"field\":\"value2\"}}],\"must_not\":[{\"term\":{\"field\":\"value3\"}}],\"should\":[{\"term\":{\"field\":\"value4\"}},{\"term\":{\"field\":\"value5\"}}]}}";

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void PASS_Deserialize_Terms()
        {
            string jsonQuery = "{\"bool\":{\"must\":[{\"term\":{\"field\":\"value1\"}},{\"term\":{\"field\":\"value2\"}}],\"must_not\":[{\"term\":{\"field\":\"value3\"}}],\"should\":[{\"term\":{\"field\":\"value4\"}},{\"term\":{\"field\":\"value5\"}}]}}";

            BoolFilter filter = JsonConvert.DeserializeObject<BoolFilter>(jsonQuery);
            Assert.IsNotNull(filter);
            Assert.AreEqual(2, filter.MustFilters.Count());
            Assert.AreEqual(1, filter.MustNotFilters.Count());
            Assert.AreEqual(2, filter.ShouldFilters.Count());
            Assert.AreEqual(false, filter.DisableCoords);
            Assert.AreEqual(1, filter.MinimumShouldMatch.GetValue());
        }
    }
}
