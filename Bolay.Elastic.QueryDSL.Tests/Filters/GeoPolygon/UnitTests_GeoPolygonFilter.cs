using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Filters.GeoPolygon;
using Bolay.Elastic.Coordinates;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Filters.GeoPolygon
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void PASS_CreateFilter()
        {
            GeoPolygonFilter filter = new GeoPolygonFilter("field",
                new List<CoordinatePoint>(){
                    new CoordinatePoint(1.1, 1.1),
                    new CoordinatePoint(2.2, 2.2),
                    new CoordinatePoint(3.3, 3.3),
                });

            Assert.IsNotNull(filter);
            Assert.AreEqual("field", filter.Field);
            Assert.AreEqual(1.1, filter.PolygonPoints.First().Latitude);
            Assert.AreEqual(1.1, filter.PolygonPoints.First().Longitude);
            Assert.AreEqual(2.2, filter.PolygonPoints.ElementAt(1).Latitude);
            Assert.AreEqual(2.2, filter.PolygonPoints.ElementAt(1).Longitude);
            Assert.AreEqual(3.3, filter.PolygonPoints.Last().Latitude);
            Assert.AreEqual(3.3, filter.PolygonPoints.Last().Longitude);
        }

        [TestMethod]
        public void FAIL_CreateFilter_Field()
        {
            try
            {
                GeoPolygonFilter filter = new GeoPolygonFilter(null,
                new List<CoordinatePoint>(){
                    new CoordinatePoint(1.1, 1.1),
                    new CoordinatePoint(2.2, 2.2),
                    new CoordinatePoint(3.3, 3.3),
                });
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("field", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateFilter_Points()
        {
            try
            {
                GeoPolygonFilter filter = new GeoPolygonFilter("field",
                new List<CoordinatePoint>(){
                    new CoordinatePoint(1.1, 1.1),
                    null,
                    new CoordinatePoint(3.3, 3.3),
                });
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("polygonPoints", ex.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize_AllLatLon()
        {
            GeoPolygonFilter filter = new GeoPolygonFilter("field",
                new List<CoordinatePoint>(){
                    new CoordinatePoint(1.1, 1.1),
                    new CoordinatePoint(2.2, 2.2),
                    new CoordinatePoint(3.3, 3.3),
                });

            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"geo_polygon\":{\"field\":[{\"lat\":1.1,\"lon\":1.1},{\"lat\":2.2,\"lon\":2.2},{\"lat\":3.3,\"lon\":3.3}]}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_AllLatLon()
        {
            string json = "{\"geo_polygon\":{\"field\":[{\"lat\":1.1,\"lon\":1.1},{\"lat\":2.2,\"lon\":2.2},{\"lat\":3.3,\"lon\":3.3}]}}";

            GeoPolygonFilter filter = JsonConvert.DeserializeObject<GeoPolygonFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("field", filter.Field);
            Assert.AreEqual(1.1, filter.PolygonPoints.First().Latitude);
            Assert.AreEqual(1.1, filter.PolygonPoints.First().Longitude);
            Assert.AreEqual(2.2, filter.PolygonPoints.ElementAt(1).Latitude);
            Assert.AreEqual(2.2, filter.PolygonPoints.ElementAt(1).Longitude);
            Assert.AreEqual(3.3, filter.PolygonPoints.Last().Latitude);
            Assert.AreEqual(3.3, filter.PolygonPoints.Last().Longitude);
        }

        [TestMethod]
        public void PASS_Serialize_Mixed()
        {
            GeoPolygonFilter filter = new GeoPolygonFilter("field",
                new List<CoordinatePoint>(){
                    new CoordinatePoint(1.1, 1.1),
                    new CoordinatePoint("geohash"),
                    new CoordinatePoint(3.3, 3.3),
                });

            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"geo_polygon\":{\"field\":[\"1.1,1.1\",\"geohash\",\"3.3,3.3\"]}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_Mixed()
        {
            string json = "{\"geo_polygon\":{\"field\":[\"1.1,1.1\",\"geohash\",\"3.3,3.3\"]}}";
            GeoPolygonFilter filter = JsonConvert.DeserializeObject<GeoPolygonFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("field", filter.Field);
            Assert.AreEqual(1.1, filter.PolygonPoints.First().Latitude);
            Assert.AreEqual(1.1, filter.PolygonPoints.First().Longitude);
            Assert.AreEqual("geohash", filter.PolygonPoints.ElementAt(1).GeoHash);
            Assert.AreEqual(3.3, filter.PolygonPoints.Last().Latitude);
            Assert.AreEqual(3.3, filter.PolygonPoints.Last().Longitude);
        }
    }
}
