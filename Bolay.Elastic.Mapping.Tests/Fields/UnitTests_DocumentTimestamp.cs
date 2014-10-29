using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Mapping.Fields._Timestamp;
using Newtonsoft.Json;
using Bolay.Elastic.Time;

namespace Bolay.Elastic.Mapping.Tests.Fields
{
    [TestClass]
    public class UnitTests_DocumentTimestamp
    {
        [TestMethod]
        public void PASS_Create()
        {
            DocumentTimestamp stamp = new DocumentTimestamp()
            {
                Format = new DateFormat(DateTimeFormatEnum.DateTime),
                Path = "timestamp"
            };

            Assert.IsNotNull(stamp);
            Assert.AreEqual(true, stamp.IsEnabled);
            Assert.AreEqual(IndexSettingEnum.NotAnalyzed, stamp.Index);
            Assert.AreEqual(false, stamp.Store);
            Assert.AreEqual("timestamp", stamp.Path);
            Assert.AreEqual("date_time", stamp.Format.Format);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            DocumentTimestamp stamp = new DocumentTimestamp()
            {
                Format = new DateFormat(DateTimeFormatEnum.DateTime),
                Path = "timestamp"
            };

            string json = JsonConvert.SerializeObject(stamp);
            Assert.IsNotNull(json);

            string expectedJson = "{\"enabled\":true,\"path\":\"timestamp\",\"format\":\"date_time\"}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"enabled\":true,\"path\":\"timestamp\",\"format\":\"date_time\"}";
            DocumentTimestamp stamp = JsonConvert.DeserializeObject<DocumentTimestamp>(json);
            Assert.IsNotNull(stamp);
            Assert.AreEqual(true, stamp.IsEnabled);
            Assert.AreEqual(IndexSettingEnum.NotAnalyzed, stamp.Index);
            Assert.AreEqual(false, stamp.Store);
            Assert.AreEqual("timestamp", stamp.Path);
            Assert.AreEqual("date_time", stamp.Format.Format);
        }
    }
}
