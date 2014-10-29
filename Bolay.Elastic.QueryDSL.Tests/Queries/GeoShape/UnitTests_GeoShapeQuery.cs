using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Queries.GeoShape;
using Bolay.Elastic.GeoShapes;
using Bolay.Elastic.Models;
using Newtonsoft.Json;
using Bolay.Elastic.Coordinates;

namespace Bolay.Elastic.QueryDSL.Tests.Queries.GeoShape
{
    [TestClass]
    public class UnitTests_GeoShapeQuery
    {
        [TestMethod]
        public void PASS_CreateQuery()
        {
            GeoShapeQuery query = new GeoShapeQuery("field", new Point(new CoordinatePoint(1.1, 2.2)));
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Field);
            Assert.IsTrue(query.GeoShape is Point);
            Assert.AreEqual(1.1, (query.GeoShape as Point).Coordinate.Latitude);
            Assert.AreEqual(2.2, (query.GeoShape as Point).Coordinate.Longitude);
        }

        [TestMethod]
        public void FAIL_CreateQuery_Field()
        {
            try
            {
                GeoShapeQuery query = new GeoShapeQuery(null, new Point(new CoordinatePoint(1.1, 2.2)));
                Assert.Fail();
            }
            catch(ArgumentNullException argEx)
            {
                Assert.AreEqual("field", argEx.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateQuery_GeoShape()
        {
            try
            {
                GeoShapeQuery query = new GeoShapeQuery("field", null);
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
            GeoShapeQuery query = new GeoShapeQuery("field", new Point(new CoordinatePoint(1.1, 2.2)));
            string json = JsonConvert.SerializeObject(query);
            Assert.IsNotNull(json);

            string expectedJson = "{\"geo_shape\":{\"field\":{\"shape\":{\"type\":\"point\",\"coordinates\":[2.2,1.1]}}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"geo_shape\":{\"field\":{\"shape\":{\"type\":\"point\",\"coordinates\":[2.2,1.1]}}}}";
            GeoShapeQuery query = JsonConvert.DeserializeObject<GeoShapeQuery>(json);
            Assert.IsNotNull(query);
            Assert.AreEqual("field", query.Field);
            Assert.IsTrue(query.GeoShape is Point);
            Assert.AreEqual(1.1, (query.GeoShape as Point).Coordinate.Latitude);
            Assert.AreEqual(2.2, (query.GeoShape as Point).Coordinate.Longitude);
        }
    }
}
