using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Aggregations.Missing;
using Newtonsoft.Json;
using System.Collections.Generic;
using Bolay.Elastic.QueryDSL.Aggregations;
using Bolay.Elastic.QueryDSL.Aggregations.Sum;

namespace Bolay.Elastic.QueryDSL.Tests.Aggregations
{
    [TestClass]
    public class UnitTests_MissingAggregate
    {
        [TestMethod]
        public void PASS_Create()
        {
            MissingAggregate agg = new MissingAggregate("missing-name", "field");
            Assert.IsNotNull(agg);
            Assert.AreEqual("missing-name", agg.Name);
            Assert.AreEqual("field", agg.Field);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            MissingAggregate agg = new MissingAggregate("missing-name", "field");
            string json = JsonConvert.SerializeObject(agg);
            Assert.IsNotNull(json);

            string expectedJson = "{\"missing-name\":{\"missing\":{\"field\":\"field\"}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"missing-name\":{\"missing\":{\"field\":\"field\"}}}";
            MissingAggregate agg = JsonConvert.DeserializeObject<MissingAggregate>(json);
            Assert.IsNotNull(agg);
            Assert.AreEqual("missing-name", agg.Name);
            Assert.AreEqual("field", agg.Field);
        }

        [TestMethod]
        public void PASS_Serialize_Aggs()
        {
            MissingAggregate agg = new MissingAggregate("missing-name", "field")
            {
                SubAggregations = new List<IAggregation>() 
                { 
                    new SumAggregate("sum-name", "field")
                }
            };
            string json = JsonConvert.SerializeObject(agg);
            Assert.IsNotNull(json);

            string expectedJson = "{\"missing-name\":{\"missing\":{\"field\":\"field\"},\"aggregations\":{\"sum-name\":{\"sum\":{\"field\":\"field\"}}}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_Aggs()
        {
            string json = "{\"missing-name\":{\"missing\":{\"field\":\"field\"},\"aggregations\":{\"sum-name\":{\"sum\":{\"field\":\"field\"}}}}}";
            MissingAggregate agg = JsonConvert.DeserializeObject<MissingAggregate>(json);
            Assert.IsNotNull(agg);
            Assert.AreEqual("missing-name", agg.Name);
            Assert.AreEqual("field", agg.Field);
            Assert.AreEqual("sum-name", agg.SubAggregations.First().Name);
            Assert.AreEqual("field", (agg.SubAggregations.First() as SumAggregate).Field);
        }
    }
}
