using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Aggregations.GeoHashGrid;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Aggregations
{
    [TestClass]
    public class UnitTests_GeoHashGridAggregate
    {
        [TestMethod]
        public void PASS_Create()
        {
            GeoHashGridAggregate agg = new GeoHashGridAggregate("name", "field", 7);
            Assert.IsNotNull(agg);
            Assert.AreEqual("name", agg.Name);
            Assert.AreEqual("field", agg.Field);
            Assert.AreEqual((int)7, agg.Precision);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            GeoHashGridAggregate agg = new GeoHashGridAggregate("name", "field", 7)
            {
                Size = 12,
                ShardSize = 13
            };

            string json = JsonConvert.SerializeObject(agg);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"geohash_grid\":{\"field\":\"field\",\"precision\":7,\"size\":12,\"shard_size\":13}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"geohash_grid\":{\"field\":\"field\",\"precision\":7,\"size\":12,\"shard_size\":13}}}";
            GeoHashGridAggregate agg = JsonConvert.DeserializeObject<GeoHashGridAggregate>(json);
            Assert.IsNotNull(agg);
            Assert.AreEqual("name", agg.Name);
            Assert.AreEqual("field", agg.Field);
            Assert.AreEqual((int)7, agg.Precision);
            Assert.AreEqual((int)12, agg.Size);
            Assert.AreEqual((int)13, agg.ShardSize);
        }
    }
}
