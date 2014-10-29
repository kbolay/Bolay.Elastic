using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Aggregations.Histogram;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Aggregations
{
    [TestClass]
    public class UnitTests_HistogramAggregate
    {
        [TestMethod]
        public void PASS_Create()
        {
            HistogramAggregate agg = new HistogramAggregate("name", "field", 50);
            Assert.IsNotNull(agg);
            Assert.AreEqual("name", agg.Name);
            Assert.AreEqual("field", agg.Field);
            Assert.AreEqual((int)50, Convert.ToInt32(agg.Interval));
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            HistogramAggregate agg = new HistogramAggregate("name", "field", 50)
            {
                MinimumDocumentCount = 2,
                AreBucketsKeyed = true
            };

            string json = JsonConvert.SerializeObject(agg);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"histogram\":{\"field\":\"field\",\"interval\":50.0,\"min_doc_count\":2,\"keyed\":true}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"histogram\":{\"field\":\"field\",\"interval\":50.0,\"min_doc_count\":2,\"keyed\":true}}}";
            HistogramAggregate agg = JsonConvert.DeserializeObject<HistogramAggregate>(json);
            Assert.IsNotNull(agg);
            Assert.AreEqual("name", agg.Name);
            Assert.AreEqual("field", agg.Field);
            Assert.AreEqual((int)50, Convert.ToInt32(agg.Interval));
            Assert.AreEqual(2, agg.MinimumDocumentCount);
            Assert.AreEqual(true, agg.AreBucketsKeyed);
        }
    }
}
