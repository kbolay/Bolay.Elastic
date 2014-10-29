using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Aggregations.Range.Date;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Aggregations
{
    [TestClass]
    public class UnitTests_DateRangeAggregate
    {
        [TestMethod]
        public void PASS_Create()
        {
            DateRangeAggregate agg = new DateRangeAggregate("name", "field",
                new List<DateRangeBucket>()
                {
                    new DateRangeBucket()
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
            DateRangeAggregate agg = new DateRangeAggregate("name", "field",
                new List<DateRangeBucket>()
                {
                    new DateRangeBucket()
                    {
                        To = "to",
                        From = "from"
                    }
                });

            string json = JsonConvert.SerializeObject(agg);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"date_range\":{\"field\":\"field\",\"ranges\":[{\"to\":\"to\",\"from\":\"from\"}]}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"date_range\":{\"field\":\"field\",\"ranges\":[{\"to\":\"to\",\"from\":\"from\"}]}}}";
            DateRangeAggregate agg = JsonConvert.DeserializeObject<DateRangeAggregate>(json);
            Assert.IsNotNull(agg);
            Assert.AreEqual("name", agg.Name);
            Assert.AreEqual("field", agg.Field);
            Assert.AreEqual("to", agg.Ranges.First().To.ToString());
            Assert.AreEqual("from", agg.Ranges.First().From.ToString());
        }
    }
}
