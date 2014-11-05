using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Bolay.Elastic.Mapping.Properties.Date;
using Bolay.Elastic.Time;

namespace Bolay.Elastic.Mapping.Tests.Types.Core
{
    [TestClass]
    public class UnitTests_DateProperty
    {
        [TestMethod]
        public void PASS_Create()
        {
            DateProperty prop = new DateProperty("dateme");

            Assert.IsNotNull(prop);
            Assert.AreEqual("dateme", prop.Name);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            DateProperty prop = new DateProperty("dateme");
            string json = JsonConvert.SerializeObject(prop);
            Assert.IsNotNull(json);

            string expectedJson = "{\"dateme\":{\"type\":\"date\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"dateme\":{\"type\":\"date\"}}";
            DateProperty prop = JsonConvert.DeserializeObject<DateProperty>(json);
            Assert.IsNotNull(prop);
            Assert.AreEqual("dateme", prop.Name);
        }

        [TestMethod]
        public void PASS_Serialize_Format()
        {
            DateProperty prop = new DateProperty("dateme")
                {
                    Format = new DateFormat(DateTimeFormatEnum.DateHourMinute)
                };
            string json = JsonConvert.SerializeObject(prop);
            Assert.IsNotNull(json);

            string expectedJson = "{\"dateme\":{\"type\":\"date\",\"format\":\"date_hour_minute\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_Format()
        {
            string json = "{\"dateme\":{\"type\":\"date\",\"format\":\"date_hour_minute\"}}";
            DateProperty prop = JsonConvert.DeserializeObject<DateProperty>(json);
            Assert.IsNotNull(prop);
            Assert.AreEqual("dateme", prop.Name);
            Assert.AreEqual("date_hour_minute", prop.Format.Format);
        }
    }
}
