using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Filters.GeoDistanceRange;
using Bolay.Elastic.Coordinates;
using Bolay.Elastic.Distance;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Filters.GeoDistanceRange
{
    [TestClass]
    public class UnitTests_GeoDistanceRangeFilter
    {
        [TestMethod]
        public void PASS_CreateFilter()
        {
            GeoDistanceRangeFilter filter = new GeoDistanceRangeFilter("field", new CoordinatePoint(1.1, 2.2))
            {
                GreaterThan = new DistanceValue("1m"),
                LessThan = new DistanceValue("1000m")
            };

            Assert.IsNotNull(filter);
            Assert.AreEqual("field", filter.Field);
            Assert.AreEqual(1.1, filter.CenterPoint.Latitude);
            Assert.AreEqual(2.2, filter.CenterPoint.Longitude);
            Assert.AreEqual((double)1, filter.GreaterThan.Size);
            Assert.AreEqual(DistanceUnitEnum.Meter, filter.GreaterThan.Unit);
            Assert.AreEqual((double)1000, filter.LessThan.Size);
            Assert.AreEqual(DistanceUnitEnum.Meter, filter.LessThan.Unit);
        }

        [TestMethod]
        public void FAIL_CreateFilter_Field()
        {
            try
            {
                GeoDistanceRangeFilter filter = new GeoDistanceRangeFilter(null, new CoordinatePoint(1.1, 2.2))
                {
                    GreaterThan = new DistanceValue("1m"),
                    LessThan = new DistanceValue("1000m")
                };

                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("field", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateFilter_CenterPoint()
        {
            try
            {
                GeoDistanceRangeFilter filter = new GeoDistanceRangeFilter("field", null)
                {
                    GreaterThan = new DistanceValue("1m"),
                    LessThan = new DistanceValue("1000m")
                };
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
            GeoDistanceRangeFilter filter = new GeoDistanceRangeFilter("field", new CoordinatePoint(1.1, 2.2))
            {
                GreaterThan = new DistanceValue("1m"),
                LessThan = new DistanceValue("1000m")
            };

            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"geo_distance_range\":{\"gt\":\"1m\",\"lt\":\"1000m\",\"field\":{\"lat\":1.1,\"lon\":2.2}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"geo_distance_range\":{\"gt\":\"1m\",\"lt\":\"1000m\",\"field\":{\"lat\":1.1,\"lon\":2.2}}}";
            GeoDistanceRangeFilter filter = JsonConvert.DeserializeObject<GeoDistanceRangeFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("field", filter.Field);
            Assert.AreEqual(1.1, filter.CenterPoint.Latitude);
            Assert.AreEqual(2.2, filter.CenterPoint.Longitude);
            Assert.AreEqual((double)1, filter.GreaterThan.Size);
            Assert.AreEqual(DistanceUnitEnum.Meter, filter.GreaterThan.Unit);
            Assert.AreEqual((double)1000, filter.LessThan.Size);
            Assert.AreEqual(DistanceUnitEnum.Meter, filter.LessThan.Unit);
        }
    }
}
