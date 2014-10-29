using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Mapping.Types.RootObject;
using Bolay.Elastic.Analysis.Analyzers.Standard;
using System.Collections.Generic;
using Bolay.Elastic.Mapping.Types;
using Bolay.Elastic.Time;
using Bolay.Elastic.Api.Mapping.Models;
using Newtonsoft.Json;
using Bolay.Elastic.Mapping;
using Bolay.Elastic.Mapping.Types.String;

namespace Bolay.Elastic.Api.Mapping.Tests
{
    [TestClass]
    public class UnitTests_IndexMapping
    {
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

            IndexMapping indexMapping = new IndexMapping("1234index", new List<RootObjectProperty>() { prop });

            string json = JsonConvert.SerializeObject(indexMapping);
            Assert.IsNotNull(json);

            string expectedJson = "{\"1234index\":{\"entity\":{\"analyzer\":\"standard\",\"date_detection\":false,\"numeric_detection\":true,\"dynamic_date_formats\":[\"format1\",\"format2\"],\"dynamic_templates\":[{\"template1\":{\"match\":\"*\",\"mapping\":{\"type\":\"string\"}}}],\"dynamic\":\"strict\",\"copy_to\":\"field1\",\"properties\":{\"name\":{\"type\":\"string\"}}}}}";

            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"1234index\":{\"entity\":{\"analyzer\":\"standard\",\"date_detection\":false,\"numeric_detection\":true,\"dynamic_date_formats\":[\"format1\",\"format2\"],\"dynamic_templates\":[{\"template1\":{\"match\":\"*\",\"mapping\":{\"type\":\"string\"}}}],\"dynamic\":\"strict\",\"copy_to\":\"field1\",\"properties\":{\"name\":{\"type\":\"string\"}}}}}";

            IndexMapping indexMapping = JsonConvert.DeserializeObject<IndexMapping>(json);
            Assert.IsNotNull(indexMapping);
            Assert.AreEqual("1234index", indexMapping.IndexName);
            RootObjectProperty prop = indexMapping.Types.First();
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
