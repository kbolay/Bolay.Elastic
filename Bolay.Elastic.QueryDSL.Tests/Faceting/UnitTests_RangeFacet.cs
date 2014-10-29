using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Faceting.Range;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Faceting
{
    [TestClass]
    public class UnitTests_RangeFacet
    {
        [TestMethod]
        public void PASS_Create()
        {
            RangeFacet facet = new RangeFacet("range-name", "field", new List<RangeBucket>() { new RangeBucket() { GreaterThan = 5 } });
            Assert.IsNotNull(facet);
            Assert.AreEqual("range-name", facet.FacetName);
            Assert.AreEqual("field", facet.Field);
            Assert.AreEqual((int)5, Convert.ToInt32(facet.RangeBuckets.First().GreaterThan));
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            RangeFacet facet = new RangeFacet("range-name", "field", new List<RangeBucket>() { new RangeBucket() { GreaterThan = 5 } });
            string json = JsonConvert.SerializeObject(facet);
            Assert.IsNotNull(json);

            string expectedJson = "{\"range-name\":{\"range\":{\"field\":\"field\",\"ranges\":[{\"gt\":5}]}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"range-name\":{\"range\":{\"field\":\"field\",\"ranges\":[{\"gt\":5}]}}}";
            RangeFacet facet = JsonConvert.DeserializeObject<RangeFacet>(json);
            Assert.IsNotNull(facet);
            Assert.AreEqual("range-name", facet.FacetName);
            Assert.AreEqual("field", facet.Field);
            Assert.AreEqual((int)5, Convert.ToInt32(facet.RangeBuckets.First().GreaterThan));
        }

        [TestMethod]
        public void PASS_Deserialize_Response()
        {

            string json = "{\"name\":{\"_type\":\"range\",\"ranges\":[{\"to\":10.0,\"count\":14,\"min\":1.9556740077956032,\"max\":9.956190163777851,\"total_count\":14,\"total\":99.23173167835192,\"mean\":7.087980834167994},{\"from\":10.0,\"to\":20.0,\"count\":39,\"min\":10.002835842271903,\"max\":19.92768557178301,\"total_count\":39,\"total\":606.3820935824962,\"mean\":15.548258809807594}]}}";
            RangeResponse facet = JsonConvert.DeserializeObject<RangeResponse>(json);
            Assert.IsNotNull(facet);
            Assert.AreEqual("name", facet.Name);
            Assert.AreEqual((Double)0.0, facet.Ranges.First().From);
            Assert.AreEqual((Double)10.0, facet.Ranges.First().To);
            Assert.AreEqual((Double)1.9556740077956, facet.Ranges.First().Minimum);
            Assert.AreEqual((Double)9.95619016377785, facet.Ranges.First().Maximum);
            Assert.AreEqual((Double)99.2317316783519, facet.Ranges.First().Sum);
            Assert.AreEqual((Double)7.08798083416799, facet.Ranges.First().Average);
            Assert.AreEqual((Int64)14, facet.Ranges.First().Count);
            Assert.AreEqual((Int64)14, facet.Ranges.First().TotalCount);

            Assert.AreEqual((Double)10.0, facet.Ranges.Last().From);
            Assert.AreEqual((Double)20.0, facet.Ranges.Last().To);
            Assert.AreEqual((Double)10.0028358422719, facet.Ranges.Last().Minimum);
            Assert.AreEqual((Double)19.927685571783, facet.Ranges.Last().Maximum);
            Assert.AreEqual((Double)606.382093582496, facet.Ranges.Last().Sum);
            Assert.AreEqual((Double)15.5482588098076, facet.Ranges.Last().Average);
            Assert.AreEqual((Int64)39, facet.Ranges.Last().Count);
            Assert.AreEqual((Int64)39, facet.Ranges.Last().TotalCount);
        }
    }
}
