using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Mapping.Properties.GeoShape;
using Bolay.Elastic.Distance;
using Newtonsoft.Json;

namespace Bolay.Elastic.Mapping.Tests.Types
{
    [TestClass]
    public class UnitTests_GeoShapeProperty
    {
        [TestMethod]
        public void PASS_Create()
        {
            GeoShapeProperty prop = new GeoShapeProperty("geo-shape-name")
            {
                Tree = PrefixTreeEnum.QuadTree,
                Precision = new DistanceValue(10, DistanceUnitEnum.Meter),
                DistanceErrorPercentage = 0.25
            };

            Assert.IsNotNull(prop);
            Assert.AreEqual("geo-shape-name", prop.Name);
            Assert.AreEqual(PrefixTreeEnum.QuadTree, prop.Tree);
            Assert.AreEqual("10m", prop.Precision.ToString());
            Assert.AreEqual(0.25, prop.DistanceErrorPercentage);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            GeoShapeProperty prop = new GeoShapeProperty("geo-shape-name")
            {
                Tree = PrefixTreeEnum.QuadTree,
                Precision = new DistanceValue(10, DistanceUnitEnum.Meter),
                DistanceErrorPercentage = 0.25
            };

            string json = JsonConvert.SerializeObject(prop);
            Assert.IsNotNull(json);

            string expectedJson = "{\"geo-shape-name\":{\"type\":\"geo_shape\",\"tree\":\"quadtree\",\"precision\":\"10m\",\"distance_error_pct\":0.25}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"geo-shape-name\":{\"type\":\"geo_shape\",\"tree\":\"quadtree\",\"precision\":\"10m\",\"distance_error_pct\":0.25}}";
            GeoShapeProperty prop = JsonConvert.DeserializeObject<GeoShapeProperty>(json);
            Assert.IsNotNull(prop);
            Assert.AreEqual("geo-shape-name", prop.Name);
            Assert.AreEqual(PrefixTreeEnum.QuadTree, prop.Tree);
            Assert.AreEqual("10m", prop.Precision.ToString());
            Assert.AreEqual(0.25, prop.DistanceErrorPercentage);
        }
    }
}
