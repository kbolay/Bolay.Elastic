using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.SourceFiltering;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.SourceFiltering
{
    [TestClass]
    public class UnitTests_SourceFilter
    {
        [TestMethod]
        public void PASS_Create()
        {
            SourceFilter source = new SourceFilter();
            Assert.IsNotNull(source);
            Assert.AreEqual(true, source.DisableSourceRetrieval);
        }

        [TestMethod]
        public void FAIL_Create_IncludeField()
        {
            try
            {
                SourceFilter source = new SourceFilter((string)null);
                Assert.Fail();
            }
            catch(ArgumentNullException ex)
            {
                Assert.AreEqual("includeField", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_Create_IncludeFields()
        {
            try
            {
                SourceFilter source = new SourceFilter(new List<string>());
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("includeFields", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_Create_ExcludeFields()
        {
            try
            {
                SourceFilter source = new SourceFilter(new List<string>() { "field1" }, null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("excludeFields", ex.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            SourceFilter source = new SourceFilter();
            string json = JsonConvert.SerializeObject(source);
            Assert.IsNotNull(json);
            string expectedJson = "false";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "false";
            SourceFilter source = JsonConvert.DeserializeObject<SourceFilter>(json);
            Assert.IsNotNull(source);
            Assert.AreEqual(true, source.DisableSourceRetrieval);
        }

        [TestMethod]
        public void PASS_Serialize_IncludField()
        {
            SourceFilter source = new SourceFilter("field");
            string json = JsonConvert.SerializeObject(source);
            Assert.IsNotNull(json);
            string expectedJson = "\"field\"";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_IncludeField()
        {
            string json = "\"field\"";
            SourceFilter source = JsonConvert.DeserializeObject<SourceFilter>(json);
            Assert.IsNotNull(source);
            Assert.AreEqual("field", source.IncludeFields.First());
        }

        [TestMethod]
        public void PASS_Serialize_IncludeFields()
        {
            SourceFilter source = new SourceFilter(new List<string>() { "field1", "field2" });
            string json = JsonConvert.SerializeObject(source);
            Assert.IsNotNull(json);
            string expectedJson = "[\"field1\",\"field2\"]";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_IncludeFields()
        {
            string json = "[\"field1\",\"field2\"]";
            SourceFilter source = JsonConvert.DeserializeObject<SourceFilter>(json);
            Assert.IsNotNull(source);
            Assert.AreEqual("field1", source.IncludeFields.First());
            Assert.AreEqual("field2", source.IncludeFields.Last());
        }

        [TestMethod]
        public void PASS_Serialize_ExcludeFields()
        {
            SourceFilter source = new SourceFilter(
                new List<string>() { "field1", "field2" },
                new List<string>() { "field3", "field4" });
            string json = JsonConvert.SerializeObject(source);
            Assert.IsNotNull(json);
            string expectedJson = "{\"include\":[\"field1\",\"field2\"],\"exclude\":[\"field3\",\"field4\"]}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_ExcludeFields()
        {
            string json = "{\"include\":[\"field1\",\"field2\"],\"exclude\":[\"field3\",\"field4\"]}";
            SourceFilter source = JsonConvert.DeserializeObject<SourceFilter>(json);
            Assert.IsNotNull(source);
            Assert.AreEqual("field1", source.IncludeFields.First());
            Assert.AreEqual("field2", source.IncludeFields.Last());
            Assert.AreEqual("field3", source.ExcludeFields.First());
            Assert.AreEqual("field4", source.ExcludeFields.Last());
        }
    }
}
