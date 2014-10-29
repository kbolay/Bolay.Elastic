using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Aggregations;
using System.Collections.Generic;
using Bolay.Elastic.QueryDSL.Aggregations.Average;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Aggregations
{
    [TestClass]
    public class UnitTests_Aggregations
    {
        [TestMethod]
        public void PASS_Create()
        {
            QueryDSL.Aggregations.Aggregations aggs = new QueryDSL.Aggregations.Aggregations(
                new List<IAggregation>()
                {
                    new AverageAggregate("name", "field")
                });

            Assert.IsNotNull(aggs);
            Assert.AreEqual("name", aggs.Aggregators.First().Name);
            Assert.AreEqual("field", (aggs.Aggregators.First() as AverageAggregate).Field);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            QueryDSL.Aggregations.Aggregations aggs = new QueryDSL.Aggregations.Aggregations(
                new List<IAggregation>()
                {
                    new AverageAggregate("name", "field")
                });

            string json = JsonConvert.SerializeObject(aggs);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"avg\":{\"field\":\"field\"}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"avg\":{\"field\":\"field\"}}}";
            QueryDSL.Aggregations.Aggregations aggs = JsonConvert.DeserializeObject<QueryDSL.Aggregations.Aggregations>(json);
            Assert.IsNotNull(aggs);
            Assert.AreEqual("name", aggs.Aggregators.First().Name);
            Assert.AreEqual("field", (aggs.Aggregators.First() as AverageAggregate).Field);
        }
    }
}
