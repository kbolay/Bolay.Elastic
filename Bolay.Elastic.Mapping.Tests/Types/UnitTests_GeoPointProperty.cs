using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Mapping.Properties.GeoPoint;
using Bolay.Elastic.Distance;
using Newtonsoft.Json;

namespace Bolay.Elastic.Mapping.Tests.Types
{
    [TestClass]
    public class UnitTests_GeoPointProperty
    {
        [TestMethod]
        public void PASS_Create()
        {
            GeoPointProperty prop = new GeoPointProperty("geo-point-name")
            {
                IndexLatLon = true,
                IndexGeoHashPrefix = true,
                CompressionPrecision = new Distance.DistanceValue(1, DistanceUnitEnum.Millimeter)
            };

            Assert.IsNotNull(prop);
            Assert.AreEqual("geo-point-name", prop.Name);
            Assert.AreEqual(true, prop.IndexLatLon);
            Assert.AreEqual(true, prop.IndexGeoHashPrefix);
            Assert.AreEqual("1mm", prop.CompressionPrecision.ToString());
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            GeoPointProperty prop = new GeoPointProperty("geo-point-name")
            {
                IndexLatLon = true,
                IndexGeoHashPrefix = true,
                CompressionPrecision = new Distance.DistanceValue(1, DistanceUnitEnum.Millimeter)
            };
            string json = JsonConvert.SerializeObject(prop);
            Assert.IsNotNull(json);

            string expectedJson = "{\"geo-point-name\":{\"type\":\"geo_point\",\"lat_lon\":true,\"geohash_prefix\":true,\"fielddata\":{\"format\":\"compressed\",\"precision\":\"1mm\"}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"geo-point-name\":{\"type\":\"geo_point\",\"lat_lon\":true,\"geohash_prefix\":true,\"fielddata\":{\"format\":\"compressed\",\"precision\":\"1mm\"}}}";

            GeoPointProperty prop = JsonConvert.DeserializeObject<GeoPointProperty>(json);
            Assert.IsNotNull(prop);
            Assert.AreEqual("geo-point-name", prop.Name);
            Assert.AreEqual(true, prop.IndexLatLon);
            Assert.AreEqual(true, prop.IndexGeoHashPrefix);
            Assert.AreEqual("1mm", prop.CompressionPrecision.ToString());
        }
    }
}
