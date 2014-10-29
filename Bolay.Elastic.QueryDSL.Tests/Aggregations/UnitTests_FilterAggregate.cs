using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Aggregations.Filter;
using Bolay.Elastic.QueryDSL.Filters.Term;
using Newtonsoft.Json;
using System.Collections.Generic;
using Bolay.Elastic.QueryDSL.Aggregations;
using Bolay.Elastic.QueryDSL.Aggregations.Sum;

namespace Bolay.Elastic.QueryDSL.Tests.Aggregations
{
    [TestClass]
    public class UnitTests_FilterAggregate
    {
        [TestMethod]
        public void PASS_Create()
        {
            FilterAggregate agg = new FilterAggregate("filter-name", new TermFilter("field", "value"));
            Assert.IsNotNull(agg);
            Assert.AreEqual("filter-name", agg.Name);
            Assert.AreEqual("field", (agg.Filter as TermFilter).Field);
            Assert.AreEqual("value", (agg.Filter as TermFilter).Value);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            FilterAggregate agg = new FilterAggregate("filter-name", new TermFilter("field", "value"));
            string json = JsonConvert.SerializeObject(agg);
            Assert.IsNotNull(json);

            string expectedJson = "{\"filter-name\":{\"filter\":{\"term\":{\"field\":\"value\"}}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"filter-name\":{\"filter\":{\"term\":{\"field\":\"value\"}}}}";
            FilterAggregate agg = JsonConvert.DeserializeObject<FilterAggregate>(json);
            Assert.IsNotNull(agg);
            Assert.AreEqual("filter-name", agg.Name);
            Assert.AreEqual("field", (agg.Filter as TermFilter).Field);
            Assert.AreEqual("value", (agg.Filter as TermFilter).Value);
        }

        [TestMethod]
        public void PASS_Serialize_Subs()
        {
            FilterAggregate agg = new FilterAggregate("filter-name", new TermFilter("field", "value"))
            {
                SubAggregations = new List<IAggregation>() 
                { 
                    new SumAggregate("sum-name", "field")
                }
            };
            string json = JsonConvert.SerializeObject(agg);
            Assert.IsNotNull(json);

            string expectedJson = "{\"filter-name\":{\"filter\":{\"term\":{\"field\":\"value\"}},\"aggregations\":{\"sum-name\":{\"sum\":{\"field\":\"field\"}}}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_Subs()
        {
            string json = "{\"filter-name\":{\"filter\":{\"term\":{\"field\":\"value\"}},\"aggregations\":{\"sum-name\":{\"sum\":{\"field\":\"field\"}}}}}";
            FilterAggregate agg = JsonConvert.DeserializeObject<FilterAggregate>(json);
            Assert.IsNotNull(agg);
            Assert.AreEqual("filter-name", agg.Name);
            Assert.AreEqual("field", (agg.Filter as TermFilter).Field);
            Assert.AreEqual("value", (agg.Filter as TermFilter).Value);
            Assert.AreEqual("sum-name", agg.SubAggregations.First().Name);
            Assert.AreEqual("field", (agg.SubAggregations.First() as SumAggregate).Field);
        }
    }
}
