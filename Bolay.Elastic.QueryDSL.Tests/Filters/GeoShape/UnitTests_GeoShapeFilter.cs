using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Filters.GeoShape;
using Bolay.Elastic.GeoShapes;
using Bolay.Elastic.Coordinates;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Filters.GeoShape
{
    [TestClass]
    public class UnitTests_GeoShapeFilter
    {
        [TestMethod]
        public void PASS_CreateQuery()
        {
            GeoShapeFilter filter = new GeoShapeFilter("field", new Point(new CoordinatePoint(1.1, 2.2)));
            Assert.IsNotNull(filter);
            Assert.AreEqual("field", filter.Field);
            Assert.IsTrue(filter.GeoShape is Point);
            Assert.AreEqual(1.1, (filter.GeoShape as Point).Coordinate.Latitude);
            Assert.AreEqual(2.2, (filter.GeoShape as Point).Coordinate.Longitude);
        }

        [TestMethod]
        public void FAIL_CreateQuery_Field()
        {
            try
            {
                GeoShapeFilter filter = new GeoShapeFilter(null, new Point(new CoordinatePoint(1.1, 2.2)));
                Assert.Fail();
            }
            catch (ArgumentNullException argEx)
            {
                Assert.AreEqual("field", argEx.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateQuery_GeoShape()
        {
            try
            {
                GeoShapeFilter query = new GeoShapeFilter("field", null);
                Assert.Fail();
            }
            catch (ArgumentNullException argEx)
            {
                Assert.AreEqual("geoShape", argEx.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            GeoShapeFilter filter = new GeoShapeFilter("field", new Point(new CoordinatePoint(1.1, 2.2)));
            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"geo_shape\":{\"field\":{\"shape\":{\"type\":\"point\",\"coordinates\":[2.2,1.1]}}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"geo_shape\":{\"field\":{\"shape\":{\"type\":\"point\",\"coordinates\":[2.2,1.1]}}}}";
            GeoShapeFilter filter = JsonConvert.DeserializeObject<GeoShapeFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("field", filter.Field);
            Assert.IsTrue(filter.GeoShape is Point);
            Assert.AreEqual(1.1, (filter.GeoShape as Point).Coordinate.Latitude);
            Assert.AreEqual(2.2, (filter.GeoShape as Point).Coordinate.Longitude);
        }
    }
}
