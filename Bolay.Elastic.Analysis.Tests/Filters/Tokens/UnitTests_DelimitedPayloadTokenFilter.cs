using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Filters.Tokens.DelimitedPayload;
using Newtonsoft.Json;

namespace Bolay.Elastic.Analysis.Tests.Filters.Tokens
{
    [TestClass]
    public class UnitTests_DelimitedPayloadTokenFilter
    {
        [TestMethod]
        public void PASS_Create()
        {
            DelimitedPayloadTokenFilter filter = new DelimitedPayloadTokenFilter("name")
            {
                Delimiter = ",",
                Encoding = EncodingTypeEnum.Identity
            };

            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual(",", filter.Delimiter);
            Assert.AreEqual(EncodingTypeEnum.Identity, filter.Encoding);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            DelimitedPayloadTokenFilter filter = new DelimitedPayloadTokenFilter("name")
            {
                Delimiter = ",",
                Encoding = EncodingTypeEnum.Identity
            };

            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"delimited_payload_filter\",\"delimiter\":\",\",\"encoding\":\"identity\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"delimited_payload_filter\",\"delimiter\":\",\",\"encoding\":\"identity\"}}";
            DelimitedPayloadTokenFilter filter = JsonConvert.DeserializeObject<DelimitedPayloadTokenFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual(",", filter.Delimiter);
            Assert.AreEqual(EncodingTypeEnum.Identity, filter.Encoding);
        }
    }
}
