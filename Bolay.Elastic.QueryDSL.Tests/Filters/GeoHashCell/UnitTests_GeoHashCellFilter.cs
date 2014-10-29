using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Filters.GeoHashCell;
using Bolay.Elastic.Coordinates;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Filters.GeoHashCell
{
    [TestClass]
    public class UnitTests_GeoHashCellFilter
    {
        [TestMethod]
        public void PASS_CreateFilter()
        {
            GeoHashCellFilter filter = new GeoHashCellFilter("field",
                new CoordinatePoint(1.1, 2.2),
                3,
                true);

            Assert.IsNotNull(filter);
            Assert.AreEqual("field", filter.Field);
            Assert.AreEqual(1.1, filter.GeoHash.Latitude);
            Assert.AreEqual(2.2, filter.GeoHash.Longitude);
            Assert.AreEqual((int)3, (int)filter.GeoHashPrecision.Value);
            Assert.AreEqual(true, filter.AllowNeighbors);
        }

        [TestMethod]
        public void FAIL_CreateFilter_Field()
        {
            try
            {
                GeoHashCellFilter filter = new GeoHashCellFilter(null,
                        new CoordinatePoint(1.1, 2.2),
                        3,
                        true);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            { 
                Assert.AreEqual("field", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateFilter_GeoHash()
        {
            try
            {
                GeoHashCellFilter filter = new GeoHashCellFilter("field",
                        null,
                        3,
                        true);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("geoHash", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateFilter_GeoHashPrecision()
        {
            try
            {
                GeoHashCellFilter filter = new GeoHashCellFilter("field",
                        new CoordinatePoint(1.1, 2.2),
                        0,
                        true);
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.AreEqual("geoHashPrecision", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateFilter_DistancePrecision()
        {
            try
            {
                GeoHashCellFilter filter = new GeoHashCellFilter("field",
                        new CoordinatePoint(1.1, 2.2),
                        null,
                        true);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("distancePrecision", ex.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serializer()
        {
            GeoHashCellFilter filter = new GeoHashCellFilter("field",
                new CoordinatePoint(1.1, 2.2),
                3,
                true);

            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"geohash_cell\":{\"field\":{\"lat\":1.1,\"lon\":2.2},\"precision\":3,\"neighbors\":true}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"geohash_cell\":{\"field\":{\"lat\":1.1,\"lon\":2.2},\"precision\":3,\"neighbors\":true}}";
            GeoHashCellFilter filter = JsonConvert.DeserializeObject<GeoHashCellFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("field", filter.Field);
            Assert.AreEqual(1.1, filter.GeoHash.Latitude);
            Assert.AreEqual(2.2, filter.GeoHash.Longitude);
            Assert.AreEqual((int)3, (int)filter.GeoHashPrecision.Value);
            Assert.AreEqual(true, filter.AllowNeighbors);
        }
    }
}
