using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Aggregations.Percentiles;
using Newtonsoft.Json;
using Bolay.Elastic.QueryDSL.Aggregations;
using System.Collections.Generic;
using Bolay.Elastic.QueryDSL.Aggregations.Sum;

namespace Bolay.Elastic.QueryDSL.Tests.Aggregations
{
    [TestClass]
    public class UnitTests_PercentilesAggregate
    {
        [TestMethod]
        public void PASS_Create()
        {
            PercentilesAggregate agg = new PercentilesAggregate("percent-name", "field");
            Assert.IsNotNull(agg);
            Assert.AreEqual("percent-name", agg.Name);
            Assert.AreEqual("field", agg.Field);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            PercentilesAggregate agg = new PercentilesAggregate("percent-name", "field")
            {
                PercentBuckets = new List<Double>() { 95, 99, 99.9 },
                Compression = 200
            };
            string json = JsonConvert.SerializeObject(agg);
            Assert.IsNotNull(json);

            string expectedJson = "{\"percent-name\":{\"percentiles\":{\"field\":\"field\",\"percents\":[95.0,99.0,99.9],\"compression\":200}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"percent-name\":{\"percentiles\":{\"field\":\"field\",\"percents\":[95.0,99.0,99.9],\"compression\":200}}}";
            PercentilesAggregate agg = JsonConvert.DeserializeObject<PercentilesAggregate>(json);
            Assert.IsNotNull(agg);
            Assert.AreEqual("percent-name", agg.Name);
            Assert.AreEqual("field", agg.Field);
            Assert.AreEqual((int)3, agg.PercentBuckets.Count());
            Assert.AreEqual((int)200, agg.Compression);
        }

        [TestMethod]
        public void PASS_Serialize_Aggs()
        {
            PercentilesAggregate agg = new PercentilesAggregate("percent-name", "field")
            {
                SubAggregations = new List<IAggregation>() 
                { 
                    new SumAggregate("sum-name", "field")
                }
            };
            string json = JsonConvert.SerializeObject(agg);
            Assert.IsNotNull(json);

            string expectedJson = "{\"percent-name\":{\"percentiles\":{\"field\":\"field\"},\"aggregations\":{\"sum-name\":{\"sum\":{\"field\":\"field\"}}}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_Aggs()
        {
            string json = "{\"percent-name\":{\"percentiles\":{\"field\":\"field\"},\"aggregations\":{\"sum-name\":{\"sum\":{\"field\":\"field\"}}}}}";
            PercentilesAggregate agg = JsonConvert.DeserializeObject<PercentilesAggregate>(json);
            Assert.IsNotNull(agg);
            Assert.AreEqual("percent-name", agg.Name);
            Assert.AreEqual("field", agg.Field);
            Assert.AreEqual("sum-name", agg.SubAggregations.First().Name);
            Assert.AreEqual("field", (agg.SubAggregations.First() as SumAggregate).Field);
        }
    }
}
