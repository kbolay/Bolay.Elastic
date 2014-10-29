using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Mapping.Types.RootObject;
using System.Collections.Generic;
using Bolay.Elastic.Mapping.Types;
using Bolay.Elastic.Mapping.Types.String;
using Newtonsoft.Json;
using Bolay.Elastic.Time;
using Bolay.Elastic.Analysis.Analyzers.Standard;

namespace Bolay.Elastic.Mapping.Tests.Types
{
    [TestClass]
    public class UnitTests_RootObjectProperty
    {
        [TestMethod]
        public void PASS_Create()
        {
            RootObjectProperty prop = new RootObjectProperty("entity")
            {
                Analyzer = new PropertyAnalyzer(new StandardAnalyzer("standard")),
                CopyTo = new List<string>() { "field1" },
                DetectDates = false,
                DetectNumbers = true,
                Dynamic = DynamicSettingEnum.Strict,
                DynamicDateFormats = new List<DateFormat>() { new DateFormat("format1"), new DateFormat("format2") },
                DynamicTemplates = new List<DynamicTemplate>()
                {
                    new DynamicTemplate("template1", new StringProperty("string-prop"))
                },
                Properties = new List<IDocumentProperty>()
                {
                    new StringProperty("name")
                }
            };

            Assert.IsNotNull(prop);
            Assert.AreEqual("standard", prop.Analyzer.Analyzer.Name);
            Assert.AreEqual("field1", prop.CopyTo.First());
            Assert.AreEqual(false, prop.DetectDates);
            Assert.AreEqual(true, prop.DetectNumbers);
            Assert.AreEqual(DynamicSettingEnum.Strict, prop.Dynamic);
            Assert.AreEqual("format1", prop.DynamicDateFormats.First().Format);
            Assert.AreEqual("format2", prop.DynamicDateFormats.Last().Format);
            DynamicTemplate template = prop.DynamicTemplates.First();
            Assert.AreEqual("template1", template.Name);
            Assert.AreEqual("string-prop", template.Mapping.Name);
            Assert.AreEqual(PropertyTypeEnum.String, template.Mapping.PropertyType);
            Assert.AreEqual("name", prop.Properties.First().Name);
            Assert.AreEqual(PropertyTypeEnum.String, prop.Properties.First().PropertyType);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            RootObjectProperty prop = new RootObjectProperty("entity")
            {
                Analyzer = new PropertyAnalyzer(new StandardAnalyzer("standard")),
                CopyTo = new List<string>() { "field1" },
                DetectDates = false,
                DetectNumbers = true,
                Dynamic = DynamicSettingEnum.Strict,
                DynamicDateFormats = new List<DateFormat>() { new DateFormat("format1"), new DateFormat("format2") },
                DynamicTemplates = new List<DynamicTemplate>()
                {
                    new DynamicTemplate("template1", new StringProperty("string-prop"))
                },
                Properties = new List<IDocumentProperty>()
                {
                    new StringProperty("name")
                }
            };

            string json = JsonConvert.SerializeObject(prop);
            Assert.IsNotNull(json);

            string expectedJson = "{\"entity\":{\"analyzer\":\"standard\",\"date_detection\":false,\"numeric_detection\":true,\"dynamic_date_formats\":[\"format1\",\"format2\"],\"dynamic_templates\":[{\"template1\":{\"match\":\"*\",\"mapping\":{\"type\":\"string\"}}}],\"dynamic\":\"strict\",\"copy_to\":\"field1\",\"properties\":{\"name\":{\"type\":\"string\"}}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"entity\":{\"analyzer\":\"standard\",\"date_detection\":false,\"numeric_detection\":true,\"dynamic_date_formats\":[\"format1\",\"format2\"],\"dynamic_templates\":[{\"template1\":{\"match\":\"*\",\"mapping\":{\"type\":\"string\"}}}],\"dynamic\":\"strict\",\"copy_to\":\"field1\",\"properties\":{\"name\":{\"type\":\"string\"}}}}";

            RootObjectProperty prop = JsonConvert.DeserializeObject<RootObjectProperty>(json);
            Assert.IsNotNull(prop);
            Assert.AreEqual("standard", prop.Analyzer.Analyzer.Name);
            Assert.AreEqual("field1", prop.CopyTo.First());
            Assert.AreEqual(false, prop.DetectDates);
            Assert.AreEqual(true, prop.DetectNumbers);
            Assert.AreEqual(DynamicSettingEnum.Strict, prop.Dynamic);
            Assert.AreEqual("format1", prop.DynamicDateFormats.First().Format);
            Assert.AreEqual("format2", prop.DynamicDateFormats.Last().Format);
            DynamicTemplate template = prop.DynamicTemplates.First();
            Assert.AreEqual("template1", template.Name);
            Assert.AreEqual(PropertyTypeEnum.String, template.Mapping.PropertyType);
            Assert.AreEqual("name", prop.Properties.First().Name);
            Assert.AreEqual(PropertyTypeEnum.String, prop.Properties.First().PropertyType);
        }
    }
}
