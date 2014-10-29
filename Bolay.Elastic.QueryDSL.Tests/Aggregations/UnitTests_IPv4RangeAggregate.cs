using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Aggregations.Range.IPv4;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Aggregations
{
    [TestClass]
    public class UnitTests_IPv4RangeAggregate
    {
        [TestMethod]
        public void PASS_Create()
        {
            IPv4RangeAggregate agg = new IPv4RangeAggregate("name", "field",
                new List<IpRangeBucket>()
                {
                    new IpRangeBucket()
                    {
                        To = "to",
                        From = "from"
                    }
                });

            Assert.IsNotNull(agg);
            Assert.AreEqual("name", agg.Name);
            Assert.AreEqual("field", agg.Field);
            Assert.AreEqual("to", agg.Ranges.First().To.ToString());
            Assert.AreEqual("from", agg.Ranges.First().From.ToString());
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            IPv4RangeAggregate agg = new IPv4RangeAggregate("name", "field",
                new List<IpRangeBucket>()
                {
                    new IpRangeBucket()
                    {
                        To = "to",
                        From = "from"
                    }
                });

            string json = JsonConvert.SerializeObject(agg);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"ip_range\":{\"field\":\"field\",\"ranges\":[{\"to\":\"to\",\"from\":\"from\"}]}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"ip_range\":{\"field\":\"field\",\"ranges\":[{\"to\":\"to\",\"from\":\"from\"}]}}}";
            IPv4RangeAggregate agg = JsonConvert.DeserializeObject<IPv4RangeAggregate>(json);
            Assert.IsNotNull(agg);
            Assert.AreEqual("name", agg.Name);
            Assert.AreEqual("field", agg.Field);
            Assert.AreEqual("to", agg.Ranges.First().To.ToString());
            Assert.AreEqual("from", agg.Ranges.First().From.ToString());
        }
    }
}
