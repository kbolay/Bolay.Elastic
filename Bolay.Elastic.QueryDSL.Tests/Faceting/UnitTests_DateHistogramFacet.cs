using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Faceting.DateHistogram;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Faceting
{
    [TestClass]
    public class UnitTests_DateHistogramFacet
    {
        [TestMethod]
        public void PASS_Create()
        {
            DateHistogramFacet facet = new DateHistogramFacet("histo1", "field", DateIntervalEnum.Hour);
            Assert.IsNotNull(facet);
            Assert.AreEqual("histo1", facet.FacetName);
            Assert.AreEqual("field", facet.Field);
            Assert.AreEqual(DateIntervalEnum.Hour, facet.ConstantInterval);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            DateHistogramFacet facet = new DateHistogramFacet("histo1", "field", DateIntervalEnum.Hour);
            string json = JsonConvert.SerializeObject(facet);
            Assert.IsNotNull(json);

            string expectedJson = "{\"histo1\":{\"date_histogram\":{\"field\":\"field\",\"interval\":\"hour\"}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"histo1\":{\"date_histogram\":{\"field\":\"field\",\"interval\":\"hour\"}}}";
            DateHistogramFacet facet = JsonConvert.DeserializeObject<DateHistogramFacet>(json);
            Assert.IsNotNull(facet);
            Assert.AreEqual("histo1", facet.FacetName);
            Assert.AreEqual("field", facet.Field);
            Assert.AreEqual(DateIntervalEnum.Hour, facet.ConstantInterval);
        }

        [TestMethod]
        public void PASS_Deserialize_Response()
        {
            string json = "{\"date-histo\":{\"_type\":\"date_histogram\",\"entries\":[{\"time\":1393027200000,\"count\":15}]}}";
            DateHistogramResponse response = JsonConvert.DeserializeObject<DateHistogramResponse>(json);
            Assert.IsNotNull(response);
            Assert.AreEqual("date-histo", response.Name);
            Assert.AreEqual((int)1, response.Entries.Count());
            Assert.AreEqual(new DateTime(2014, 2, 22), response.Entries.First().Value);
            Assert.AreEqual((Int64)15, response.Entries.First().Count);
        }
    }
}
