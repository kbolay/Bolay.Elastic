using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Mapping.Defaults;
using System.Collections.Generic;
using Bolay.Elastic.Mapping.Properties;
using Bolay.Elastic.Time;
using Bolay.Elastic.Mapping.Properties.String;
using Newtonsoft.Json;
using Bolay.Elastic.Analysis.Analyzers.Standard;

namespace Bolay.Elastic.Mapping.Tests
{
    [TestClass]
    public class UnitTests_DefaultMapping
    {
        [TestMethod]
        public void PASS_Create()
        {
            DefaultMapping prop = new DefaultMapping()
            {
                Analyzer = new PropertyAnalyzer(new StandardAnalyzer("standard")),
                CopyTo = new List<string>() { "field1" },
                DetectDates = false,
                DetectNumbers = true,
                Dynamic = DynamicSettingEnum.Strict,
                DateFormats = new List<DateFormat>() { new DateFormat("format1"), new DateFormat("format2") },
                DynamicTemplates = new List<DynamicTemplate>()
                {
                    new DynamicTemplate("template1", new StringProperty("string-prop"))
                }
            };

            Assert.IsNotNull(prop);
            Assert.AreEqual("standard", prop.Analyzer.Analyzer.Name);
            Assert.AreEqual("field1", prop.CopyTo.First());
            Assert.AreEqual(false, prop.DetectDates);
            Assert.AreEqual(true, prop.DetectNumbers);
            Assert.AreEqual(DynamicSettingEnum.Strict, prop.Dynamic);
            Assert.AreEqual("format1", prop.DateFormats.First().Format);
            Assert.AreEqual("format2", prop.DateFormats.Last().Format);
            DynamicTemplate template = prop.DynamicTemplates.First();
            Assert.AreEqual("template1", template.Name);
            Assert.AreEqual("string-prop", template.Mapping.Name);
            Assert.AreEqual(PropertyTypeEnum.String, template.Mapping.PropertyType);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            DefaultMapping prop = new DefaultMapping()
            {
                Analyzer = new PropertyAnalyzer(new StandardAnalyzer("standard")),
                CopyTo = new List<string>() { "field1" },
                DetectDates = false,
                DetectNumbers = true,
                Dynamic = DynamicSettingEnum.Strict,
                DateFormats = new List<DateFormat>() { new DateFormat("format1"), new DateFormat("format2") },
                DynamicTemplates = new List<DynamicTemplate>()
                {
                    new DynamicTemplate("template1", new StringProperty("string-prop"))
                }
            };

            string json = JsonConvert.SerializeObject(prop);
            Assert.IsNotNull(json);

            string expectedJson = "{\"_default_\":{\"dynamic\":\"strict\",\"copy_to\":\"field1\",\"analyzer\":\"standard\",\"date_detection\":false,\"numeric_detection\":true,\"date_formats\":[\"format1\",\"format2\"],\"dynamic_templates\":[{\"template1\":{\"match\":\"*\",\"mapping\":{\"type\":\"string\"}}}]}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"_default_\":{\"dynamic\":\"strict\",\"copy_to\":\"field1\",\"analyzer\":\"standard\",\"date_detection\":false,\"numeric_detection\":true,\"date_formats\":[\"format1\",\"format2\"],\"dynamic_templates\":[{\"template1\":{\"match\":\"*\",\"mapping\":{\"type\":\"string\"}}}]}}";

            DefaultMapping prop = JsonConvert.DeserializeObject<DefaultMapping>(json);
            Assert.IsNotNull(prop);
            Assert.AreEqual("standard", prop.Analyzer.Analyzer.Name);
            Assert.AreEqual("field1", prop.CopyTo.First());
            Assert.AreEqual(false, prop.DetectDates);
            Assert.AreEqual(true, prop.DetectNumbers);
            Assert.AreEqual(DynamicSettingEnum.Strict, prop.Dynamic);
            Assert.AreEqual("format1", prop.DateFormats.First().Format);
            Assert.AreEqual("format2", prop.DateFormats.Last().Format);
            DynamicTemplate template = prop.DynamicTemplates.First();
            Assert.AreEqual("template1", template.Name);
            Assert.AreEqual(PropertyTypeEnum.String, template.Mapping.PropertyType);
        }
    }
}
