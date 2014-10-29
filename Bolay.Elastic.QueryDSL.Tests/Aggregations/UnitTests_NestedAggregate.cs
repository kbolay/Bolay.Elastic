using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Aggregations.Nested;
using Newtonsoft.Json;
using Bolay.Elastic.QueryDSL.Aggregations;
using System.Collections.Generic;
using Bolay.Elastic.QueryDSL.Aggregations.Sum;

namespace Bolay.Elastic.QueryDSL.Tests.Aggregations
{
    [TestClass]
    public class UnitTests_NestedAggregate
    {
        [TestMethod]
        public void PASS_Create()
        {
            NestedAggregate agg = new NestedAggregate("nested-name", "field");
            Assert.IsNotNull(agg);
            Assert.AreEqual("nested-name", agg.Name);
            Assert.AreEqual("field", agg.Path);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            NestedAggregate agg = new NestedAggregate("nested-name", "field");
            string json = JsonConvert.SerializeObject(agg);
            Assert.IsNotNull(json);

            string expectedJson = "{\"nested-name\":{\"nested\":{\"path\":\"field\"}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"nested-name\":{\"nested\":{\"path\":\"field\"}}}";
            NestedAggregate agg = JsonConvert.DeserializeObject<NestedAggregate>(json);
            Assert.IsNotNull(agg);
            Assert.AreEqual("nested-name", agg.Name);
            Assert.AreEqual("field", agg.Path);
        }

        [TestMethod]
        public void PASS_Serialize_Aggs()
        {
            NestedAggregate agg = new NestedAggregate("nested-name", "field")
            {
                SubAggregations = new List<IAggregation>() 
                { 
                    new SumAggregate("sum-name", "field")
                }
            };
            string json = JsonConvert.SerializeObject(agg);
            Assert.IsNotNull(json);

            string expectedJson = "{\"nested-name\":{\"nested\":{\"path\":\"field\"},\"aggregations\":{\"sum-name\":{\"sum\":{\"field\":\"field\"}}}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_Aggs()
        {
            string json = "{\"nested-name\":{\"nested\":{\"path\":\"field\"},\"aggregations\":{\"sum-name\":{\"sum\":{\"field\":\"field\"}}}}}";
            NestedAggregate agg = JsonConvert.DeserializeObject<NestedAggregate>(json);
            Assert.IsNotNull(agg);
            Assert.AreEqual("nested-name", agg.Name);
            Assert.AreEqual("field", agg.Path);
            Assert.AreEqual("sum-name", agg.SubAggregations.First().Name);
            Assert.AreEqual("field", (agg.SubAggregations.First() as SumAggregate).Field);
        }
    }
}
