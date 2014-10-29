using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Mapping.Fields._Ttl;
using Newtonsoft.Json;

namespace Bolay.Elastic.Mapping.Tests.Fields
{
    [TestClass]
    public class UnitTests_DocumentTimeToLive
    {
        [TestMethod]
        public void PASS_Create()
        {
            DocumentTimeToLive ttl = new DocumentTimeToLive(new Time.TimeValue("1d"));
            Assert.IsNotNull(ttl);
            Assert.AreEqual(new TimeSpan(1, 0, 0, 0), ttl.DefaultTimeToLive.TimeSpan);
            Assert.AreEqual(true, ttl.IsEnabled);
            Assert.AreEqual(IndexSettingEnum.NotAnalyzed, ttl.Index);
            Assert.AreEqual(true, ttl.Store);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            DocumentTimeToLive ttl = new DocumentTimeToLive(new Time.TimeValue("1d"));
            string json = JsonConvert.SerializeObject(ttl);
            Assert.IsNotNull(json);

            string expectedJson = "{\"enabled\":true,\"default\":\"1d\"}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"enabled\":true,\"default\":\"1d\"}";
            DocumentTimeToLive ttl = JsonConvert.DeserializeObject<DocumentTimeToLive>(json);
            Assert.IsNotNull(ttl);
            Assert.AreEqual(new TimeSpan(1, 0, 0, 0), ttl.DefaultTimeToLive.TimeSpan);
            Assert.AreEqual(true, ttl.IsEnabled);
            Assert.AreEqual(IndexSettingEnum.NotAnalyzed, ttl.Index);
            Assert.AreEqual(true, ttl.Store);
        }
    }
}
