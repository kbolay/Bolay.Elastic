using Bolay.Elastic.Analysis.Analyzers.Standard;
using Bolay.Elastic.Api.Mapping.Models;
using Bolay.Elastic.Mapping;
using Bolay.Elastic.Mapping.Properties;
using Bolay.Elastic.Mapping.Properties.String;
using Bolay.Elastic.Time;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Bolay.Elastic.Api.Mapping.Tests
{
    [TestClass]
    public class UnitTests_IndexMapping
    {
        [TestMethod]
        public void PASS_Serialize()
        {
            TypeMapping prop = new TypeMapping("entity")
            {
                Analyzer = new PropertyAnalyzer(new StandardAnalyzer("standard")),
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

            IndexMapping indexMapping = new IndexMapping("1234index", new List<TypeMapping>() { prop });

            string json = JsonConvert.SerializeObject(indexMapping);
            Assert.IsNotNull(json);

            string expectedJson = "{\"1234index\":{\"entity\":{\"analyzer\":\"standard\",\"date_detection\":false,\"numeric_detection\":true,\"dynamic_date_formats\":[\"format1\",\"format2\"],\"dynamic_templates\":[{\"template1\":{\"match\":\"*\",\"mapping\":{\"type\":\"string\"}}}],\"dynamic\":\"strict\",\"properties\":{\"name\":{\"type\":\"string\"}}}}}";

            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"1234index\":{\"entity\":{\"analyzer\":\"standard\",\"date_detection\":false,\"numeric_detection\":true,\"dynamic_date_formats\":[\"format1\",\"format2\"],\"dynamic_templates\":[{\"template1\":{\"match\":\"*\",\"mapping\":{\"type\":\"string\"}}}],\"dynamic\":\"strict\",\"properties\":{\"name\":{\"type\":\"string\"}}}}}";

            IndexMapping indexMapping = JsonConvert.DeserializeObject<IndexMapping>(json);
            Assert.IsNotNull(indexMapping);
            Assert.AreEqual("1234index", indexMapping.IndexName);
            TypeMapping prop = indexMapping.Types.First();
            Assert.IsNotNull(prop);
            Assert.AreEqual("standard", prop.Analyzer.Analyzer.Name);
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
