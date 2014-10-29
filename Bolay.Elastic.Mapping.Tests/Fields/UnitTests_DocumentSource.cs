using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Mapping.Fields._Source;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bolay.Elastic.Mapping.Tests.Fields
{
    [TestClass]
    public class UnitTests_DocumentSource
    {
        [TestMethod]
        public void PASS_Create()
        {
            DocumentSource source = new DocumentSource()
            {
                IsEnabled = false,
                IsCompressed = true,
                Excludes = new List<string>()
                {
                    "ex1", "ex2"
                },
                Includes = new List<string>()
                {
                    "in1", "in2"
                },
                CompressionThreshold = new CompressionThreshold(10, SizeUnitEnum.Byte)
            };

            Assert.IsNotNull(source);
            Assert.AreEqual(false, source.IsEnabled);
            Assert.AreEqual(true, source.IsCompressed);
            Assert.AreEqual("ex1", source.Excludes.First());
            Assert.AreEqual("ex2", source.Excludes.Last());
            Assert.AreEqual("in1", source.Includes.First());
            Assert.AreEqual("in2", source.Includes.Last());
            Assert.AreEqual((Int64)10, source.CompressionThreshold.Size);
            Assert.AreEqual(SizeUnitEnum.Byte, source.CompressionThreshold.Unit);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            DocumentSource source = new DocumentSource()
            {
                IsEnabled = false,
                IsCompressed = true,
                Excludes = new List<string>()
                {
                    "ex1", "ex2"
                },
                Includes = new List<string>()
                {
                    "in1", "in2"
                },
                CompressionThreshold = new CompressionThreshold(10, SizeUnitEnum.Byte)
            };

            string json = JsonConvert.SerializeObject(source);
            Assert.IsNotNull(json);

            string expectedJson = "{\"enabled\":false,\"compress\":true,\"compress_threshold\":\"10b\",\"includes\":[\"in1\",\"in2\"],\"excludes\":[\"ex1\",\"ex2\"]}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"enabled\":false,\"compress\":true,\"compress_threshold\":\"10b\",\"includes\":[\"in1\",\"in2\"],\"excludes\":[\"ex1\",\"ex2\"]}";

            DocumentSource source = JsonConvert.DeserializeObject<DocumentSource>(json);
            Assert.IsNotNull(source);
            Assert.AreEqual(false, source.IsEnabled);
            Assert.AreEqual(true, source.IsCompressed);
            Assert.AreEqual("ex1", source.Excludes.First());
            Assert.AreEqual("ex2", source.Excludes.Last());
            Assert.AreEqual("in1", source.Includes.First());
            Assert.AreEqual("in2", source.Includes.Last());
            Assert.AreEqual((Int64)10, source.CompressionThreshold.Size);
            Assert.AreEqual(SizeUnitEnum.Byte, source.CompressionThreshold.Unit);
        }
    }
}