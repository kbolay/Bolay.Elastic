using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Filters.GeoBoundingBox;
using Newtonsoft.Json;
using Bolay.Elastic.Coordinates;

namespace Bolay.Elastic.QueryDSL.Tests.Filters.GeoBoundingBox
{
    [TestClass]
    public class UnitTests_GeoBoundingBoxFilter
    {
        [TestMethod]
        public void PASS_CreateFilter()
        {
            GeoBoundingBoxFilter filter = new GeoBoundingBoxFilter(
                "field",
                new CoordinatePoint(1.1, 2.2),
                new CoordinatePoint(3.3, 4.4));

            Assert.IsNotNull(filter);
            Assert.AreEqual("field", filter.Field);
            Assert.AreEqual(1.1, filter.TopLeft.Latitude);
            Assert.AreEqual(2.2, filter.TopLeft.Longitude);
            Assert.AreEqual(3.3, filter.BottomRight.Latitude);
            Assert.AreEqual(4.4, filter.BottomRight.Longitude);
        }

        [TestMethod]
        public void FAIL_CreateFilter_Field()
        {
            try
            {
                GeoBoundingBoxFilter filter = new GeoBoundingBoxFilter(null,
                    new CoordinatePoint(1.1, 2.2),
                    new CoordinatePoint(3.3, 4.4));

                Assert.Fail();
            }
            catch(ArgumentNullException ex)
            {
                Assert.AreEqual("field", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateFilter_TopLeft()
        { 
            try
            {
                GeoBoundingBoxFilter filter = new GeoBoundingBoxFilter("field",
                    null,
                    new CoordinatePoint(3.3, 4.4));

                Assert.Fail();
            }
            catch(ArgumentNullException ex)
            {
                Assert.AreEqual("topLeft", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateFilter_BottomRight()
        {
            try
            {
                GeoBoundingBoxFilter filter = new GeoBoundingBoxFilter("field",
                    new CoordinatePoint(3.3, 4.4),
                    null);

                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("bottomRight", ex.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            GeoBoundingBoxFilter filter = new GeoBoundingBoxFilter(
                "field",
                new CoordinatePoint(1.1, 2.2),
                new CoordinatePoint(3.3, 4.4));

            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);
            string expectedJson = "{\"geo_bounding_box\":{\"field\":{\"top_left\":{\"lat\":1.1,\"lon\":2.2},\"bottom_right\":{\"lat\":3.3,\"lon\":4.4}}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"geo_bounding_box\":{\"field\":{\"top_left\":{\"lat\":1.1,\"lon\":2.2},\"bottom_right\":{\"lat\":3.3,\"lon\":4.4}}}}";
            GeoBoundingBoxFilter filter = JsonConvert.DeserializeObject<GeoBoundingBoxFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("field", filter.Field);
            Assert.AreEqual(1.1, filter.TopLeft.Latitude);
            Assert.AreEqual(2.2, filter.TopLeft.Longitude);
            Assert.AreEqual(3.3, filter.BottomRight.Latitude);
            Assert.AreEqual(4.4, filter.BottomRight.Longitude);
        }

        // TODO: build more unit tests around geohash, and the various deserializing tasks
    }
}
