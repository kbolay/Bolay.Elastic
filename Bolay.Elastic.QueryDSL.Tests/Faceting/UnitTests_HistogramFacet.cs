using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Faceting.Histogram;
using Newtonsoft.Json;
using Bolay.Elastic.Time;

namespace Bolay.Elastic.QueryDSL.Tests.Faceting
{
    [TestClass]
    public class UnitTests_HistogramFacet
    {
        [TestMethod]
        public void PASS_Create()
        {
            HistogramFacet facet = new HistogramFacet("histo1", "field", 500);
            Assert.IsNotNull(facet);
            Assert.AreEqual("histo1", facet.FacetName);
            Assert.AreEqual("field", facet.Field);
            Assert.AreEqual((int)500, Convert.ToInt32(facet.Interval));
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            HistogramFacet facet = new HistogramFacet("histo1", "field", 500);
            string json = JsonConvert.SerializeObject(facet);
            Assert.IsNotNull(json);

            string expectedJson = "{\"histo1\":{\"histogram\":{\"field\":\"field\",\"interval\":500}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"histo1\":{\"histogram\":{\"field\":\"field\",\"interval\":500}}}";
            HistogramFacet facet = JsonConvert.DeserializeObject<HistogramFacet>(json);
            Assert.IsNotNull(facet);
            Assert.AreEqual("histo1", facet.FacetName);
            Assert.AreEqual("field", facet.Field);
            Assert.AreEqual((int)500, Convert.ToInt32(facet.Interval));
        }

        [TestMethod]
        public void PASS_Serialize_Time()
        {
            HistogramFacet facet = new HistogramFacet("histo1", new TimeValue(new TimeSpan(0, 1, 0)))
            {
                KeyField = "keyfield",
                ValueScript = "valuescript"
            };

            string json = JsonConvert.SerializeObject(facet);
            Assert.IsNotNull(json);

            string expectedJson = "{\"histo1\":{\"histogram\":{\"key_field\":\"keyfield\",\"value_script\":\"valuescript\",\"time_interval\":\"60000ms\"}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_Time()
        {
            string json = "{\"histo1\":{\"histogram\":{\"key_field\":\"keyfield\",\"value_script\":\"valuescript\",\"time_interval\":\"1m\"}}}";
            HistogramFacet facet = JsonConvert.DeserializeObject<HistogramFacet>(json);
            Assert.IsNotNull(facet);
            Assert.AreEqual("histo1", facet.FacetName);
            Assert.AreEqual("keyfield", facet.KeyField);
            Assert.AreEqual("valuescript", facet.ValueScript);
            Assert.AreEqual("1m", facet.TimeInterval.ToString());
        }

        [TestMethod]
        public void PASS_Deserialize_Response()
        {
            string json = "{\"histo1\":{\"_type\":\"histogram\",\"entries\":[{\"key\":2,\"count\":12},{\"key\":5,\"count\":3}]}}";
            HistogramResponse facet = JsonConvert.DeserializeObject<HistogramResponse>(json);
            Assert.IsNotNull(facet);
            Assert.AreEqual("histo1", facet.Name);
            Assert.AreEqual((int)2, facet.Entries.Count());
            Assert.AreEqual((int)2, Convert.ToInt32(facet.Entries.First().Value));
            Assert.AreEqual((Int64)12, facet.Entries.First().Count);
            Assert.AreEqual((int)5, Convert.ToInt32(facet.Entries.Last().Value));
            Assert.AreEqual((Int64)3, facet.Entries.Last().Count);
        }
    }
}
