using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Sorting.GeoDistance;
using Bolay.Elastic.Coordinates;
using Bolay.Elastic.Distance;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Sorting.GeoDistance
{
    [TestClass]
    public class UnitTests_GeoDistanceSort
    {
        [TestMethod]
        public void PASS_CreateSort()
        {
            GeoDistanceSort sort = new GeoDistanceSort("field", new CoordinatePoint(1.1, 2.2), DistanceUnitEnum.Kilometer);
            Assert.IsNotNull(sort);
            Assert.AreEqual("field", sort.Field);
            Assert.AreEqual(1.1, sort.CenterPoint.Latitude);
            Assert.AreEqual(2.2, sort.CenterPoint.Longitude);
            Assert.AreEqual(DistanceUnitEnum.Kilometer, sort.Unit);
        }

        [TestMethod]
        public void FAIL_CreateSort_Field()
        {
            try
            {
                GeoDistanceSort sort = new GeoDistanceSort(null, new CoordinatePoint(1.1, 2.2), DistanceUnitEnum.Kilometer);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("field", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateSort_CenterPoint()
        {
            try
            {
                GeoDistanceSort sort = new GeoDistanceSort("field", null, DistanceUnitEnum.Kilometer);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("centerPoint", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateSort_Unit()
        {
            try
            {
                GeoDistanceSort sort = new GeoDistanceSort("field", new CoordinatePoint(1.1, 2.2), null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("unit", ex.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            GeoDistanceSort sort = new GeoDistanceSort("field", new CoordinatePoint(1.1, 2.2), DistanceUnitEnum.Kilometer);
            string json = JsonConvert.SerializeObject(sort);
            Assert.IsNotNull(json);
            string expectedJson = "{\"_geo_distance\":{\"field\":{\"lat\":1.1,\"lon\":2.2},\"unit\":\"km\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"_geo_distance\":{\"field\":{\"lat\":1.1,\"lon\":2.2},\"unit\":\"km\"}}";
            GeoDistanceSort sort = JsonConvert.DeserializeObject<GeoDistanceSort>(json);
            Assert.IsNotNull(sort);
            Assert.AreEqual("field", sort.Field);
            Assert.AreEqual(1.1, sort.CenterPoint.Latitude);
            Assert.AreEqual(2.2, sort.CenterPoint.Longitude);
            Assert.AreEqual(DistanceUnitEnum.Kilometer, sort.Unit);
        }
    }
}
