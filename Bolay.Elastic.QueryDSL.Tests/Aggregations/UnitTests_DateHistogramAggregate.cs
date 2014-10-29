using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Aggregations.Histogram.Date;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Aggregations
{
    [TestClass]
    public class UnitTests_DateHistogramAggregate
    {
        [TestMethod]
        public void PASS_Create()
        {
            DateHistogramAggregate agg = new DateHistogramAggregate("name", "field", DateIntervalEnum.Day);
            Assert.IsNotNull(agg);
            Assert.AreEqual("name", agg.Name);
            Assert.AreEqual("field", agg.Field);
            Assert.AreEqual(DateIntervalEnum.Day, agg.ConstantInterval);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            DateHistogramAggregate agg = new DateHistogramAggregate("name", "field", DateIntervalEnum.Day);

            string json = JsonConvert.SerializeObject(agg);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"date_histogram\":{\"field\":\"field\",\"interval\":\"day\"}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"date_histogram\":{\"field\":\"field\",\"interval\":\"day\"}}}";
            DateHistogramAggregate agg = JsonConvert.DeserializeObject<DateHistogramAggregate>(json);
            Assert.IsNotNull(agg);
            Assert.AreEqual("name", agg.Name);
            Assert.AreEqual("field", agg.Field);
            Assert.AreEqual(DateIntervalEnum.Day, agg.ConstantInterval);
        }
    }
}
