using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Aggregations.GeoDistance;
using Bolay.Elastic.Coordinates;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Aggregations
{
    [TestClass]
    public class UnitTests_GeoDistanceAggregate
    {
        [TestMethod]
        public void PASS_Create()
        {
            GeoDistanceAggregate agg = new GeoDistanceAggregate("name", "field",
                new CoordinatePoint(1.1, 2.2),
                new List<DistanceRangeBucket>()
                {
                    new DistanceRangeBucket()
                    {
                        To = 50
                    }
                });

            Assert.IsNotNull(agg);
            Assert.AreEqual("name", agg.Name);
            Assert.AreEqual("field", agg.Field);
            Assert.AreEqual(1.1, agg.OriginPoint.Latitude);
            Assert.AreEqual(2.2, agg.OriginPoint.Longitude);
            Assert.AreEqual((int)50, Convert.ToInt32(agg.Ranges.First().To));
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            GeoDistanceAggregate agg = new GeoDistanceAggregate("name", "field",
                new CoordinatePoint(1.1, 2.2),
                new List<DistanceRangeBucket>()
                {
                    new DistanceRangeBucket()
                    {
                        To = 50
                    }
                });

            string json = JsonConvert.SerializeObject(agg);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"geo_distance\":{\"field\":\"field\",\"origin\":{\"lat\":1.1,\"lon\":2.2},\"ranges\":[{\"to\":50.0}]}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"geo_distance\":{\"field\":\"field\",\"origin\":{\"lat\":1.1,\"lon\":2.2},\"ranges\":[{\"to\":50.0}]}}}";
            GeoDistanceAggregate agg = JsonConvert.DeserializeObject<GeoDistanceAggregate>(json);
            Assert.IsNotNull(agg);
            Assert.AreEqual("name", agg.Name);
            Assert.AreEqual("field", agg.Field);
            Assert.AreEqual(1.1, agg.OriginPoint.Latitude);
            Assert.AreEqual(2.2, agg.OriginPoint.Longitude);
            Assert.AreEqual((int)50, Convert.ToInt32(agg.Ranges.First().To));
        }
    }
}
