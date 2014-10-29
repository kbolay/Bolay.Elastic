using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Filters.GeoDistance;
using Bolay.Elastic.Distance;
using Bolay.Elastic.Coordinates;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Filters.GeoDistance
{
    [TestClass]
    public class UnitTests_GeoDistanceFilter
    {
        [TestMethod]
        public void PASS_CreateFilter()
        {
            GeoDistanceFilter filter = new GeoDistanceFilter("field", new DistanceValue("1m"), new CoordinatePoint(1.1, 2.2));
            Assert.IsNotNull(filter);
            Assert.AreEqual("field", filter.Field);
            Assert.AreEqual((double)1, filter.Distance.Size);
            Assert.AreEqual(DistanceUnitEnum.Meter, filter.Distance.Unit);
            Assert.AreEqual(1.1, filter.CenterPoint.Latitude);
            Assert.AreEqual(2.2, filter.CenterPoint.Longitude);
        }

        [TestMethod]
        public void FAIL_CreateFilter_Field()
        {
            try
            {
                GeoDistanceFilter filter = new GeoDistanceFilter(null, new DistanceValue("1m"), new CoordinatePoint(1.1, 2.2));
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("field", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateFilter_Distance()
        {
            try
            {
                GeoDistanceFilter filter = new GeoDistanceFilter("field", null, new CoordinatePoint(1.1, 2.2));
                Assert.Fail();
            }
            catch(ArgumentNullException ex)
            {
                Assert.AreEqual("distance", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateFilter_Point()
        {
            try
            {
                GeoDistanceFilter filter = new GeoDistanceFilter("field", new DistanceValue("1m"), null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("centerPoint", ex.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            GeoDistanceFilter filter = new GeoDistanceFilter("field", new DistanceValue("1m"), new CoordinatePoint(1.1, 2.2));
            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"geo_distance\":{\"distance\":\"1m\",\"field\":{\"lat\":1.1,\"lon\":2.2}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"geo_distance\":{\"distance\":\"1m\",\"field\":{\"lat\":1.1,\"lon\":2.2}}}";
            GeoDistanceFilter filter = JsonConvert.DeserializeObject<GeoDistanceFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("field", filter.Field);
            Assert.AreEqual((double)1, filter.Distance.Size);
            Assert.AreEqual(DistanceUnitEnum.Meter, filter.Distance.Unit);
            Assert.AreEqual(1.1, filter.CenterPoint.Latitude);
            Assert.AreEqual(2.2, filter.CenterPoint.Longitude);
        }
    }
}
