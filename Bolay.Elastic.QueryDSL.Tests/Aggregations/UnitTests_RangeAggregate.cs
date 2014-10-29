using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Aggregations.Range;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Aggregations
{
    [TestClass]
    public class UnitTests_RangeAggregate
    {
        [TestMethod]
        public void PASS_Create()
        {
            RangeAggregate agg = new RangeAggregate("name", "field",
                new List<RangeBucket>()
                {
                    new RangeBucket()
                    {
                        GreaterThan = 50,
                        LessThan = 100
                    }
                });

            Assert.IsNotNull(agg);
            Assert.AreEqual("name", agg.Name);
            Assert.AreEqual("field", agg.Field);
            Assert.AreEqual((int)50, Convert.ToInt32(agg.Ranges.First().GreaterThan));
            Assert.AreEqual((int)100, Convert.ToInt32(agg.Ranges.First().LessThan));
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            RangeAggregate agg = new RangeAggregate("name", "field",
                new List<RangeBucket>()
                {
                    new RangeBucket()
                    {
                        GreaterThan = 50,
                        LessThan = 100
                    }
                });
            string json = JsonConvert.SerializeObject(agg);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"range\":{\"field\":\"field\",\"ranges\":[{\"gt\":50,\"lt\":100}]}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"range\":{\"field\":\"field\",\"ranges\":[{\"gt\":50,\"lt\":100}]}}}";
            RangeAggregate agg = JsonConvert.DeserializeObject<RangeAggregate>(json);
            Assert.IsNotNull(agg);
            Assert.AreEqual("name", agg.Name);
            Assert.AreEqual("field", agg.Field);
            Assert.AreEqual((int)50, Convert.ToInt32(agg.Ranges.First().GreaterThan));
            Assert.AreEqual((int)100, Convert.ToInt32(agg.Ranges.First().LessThan));
        }
    }
}
