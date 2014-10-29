using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Mapping.Types.String;
using Newtonsoft.Json;
using Bolay.Elastic.Mapping.Types;
using System.Collections.Generic;
using Bolay.Elastic.Analysis.Analyzers.Standard;

namespace Bolay.Elastic.Mapping.Tests.Types.Core
{
    [TestClass]
    public class UnitTests_StringProperty
    {
        [TestMethod]
        public void PASS_Create()
        {
            StringProperty prop = new StringProperty("name")
            {
                Analyzer = new PropertyAnalyzer(new StandardAnalyzer("standard"), new StandardAnalyzer("standard"))
            };

            Assert.IsNotNull(prop);
            Assert.AreEqual(IndexSettingEnum.Analyzed, prop.Index);
            Assert.AreEqual("name", prop.Name);
            Assert.AreEqual("name", prop.IndexName);
            Assert.AreEqual("standard", prop.Analyzer.IndexAnalyzer.Name);
            Assert.AreEqual("standard", prop.Analyzer.SearchAnalyzer.Name);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            StringProperty prop = new StringProperty("name")
            {
                Analyzer = new PropertyAnalyzer(new StandardAnalyzer("standard"), new StandardAnalyzer("standard"))
            };

            string json = JsonConvert.SerializeObject(prop);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"string\",\"index_analyzer\":\"standard\",\"search_analyzer\":\"standard\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"string\",\"index_analyzer\":\"standard\",\"search_analyzer\":\"standard\"}}";
            StringProperty prop = JsonConvert.DeserializeObject<StringProperty>(json);
            Assert.IsNotNull(prop);
            Assert.AreEqual(IndexSettingEnum.Analyzed, prop.Index);
            Assert.AreEqual(PropertyTypeEnum.String, prop.PropertyType);
            Assert.AreEqual("name", prop.Name);
            Assert.AreEqual("standard", prop.Analyzer.IndexAnalyzer.Name);
            Assert.AreEqual("standard", prop.Analyzer.SearchAnalyzer.Name);
        }

        [TestMethod]
        public void PASS_Serialize_Fields()
        {
            StringProperty prop = new StringProperty("name")
            {
                Analyzer = new PropertyAnalyzer(new StandardAnalyzer("standard"), new StandardAnalyzer("standard")),
                Fields = new List<IDocumentProperty>() 
                { 
                    new StringProperty("subname")
                }
            };

            string json = JsonConvert.SerializeObject(prop);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"string\",\"fields\":{\"subname\":{\"type\":\"string\"}},\"index_analyzer\":\"standard\",\"search_analyzer\":\"standard\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_Fields()
        {
            string json = "{\"name\":{\"type\":\"string\",\"fields\":{\"subname\":{\"type\":\"string\"}},\"index_analyzer\":\"standard\",\"search_analyzer\":\"standard\"}}";
            StringProperty prop = JsonConvert.DeserializeObject<StringProperty>(json);
            Assert.IsNotNull(prop);
            Assert.AreEqual(IndexSettingEnum.Analyzed, prop.Index);
            Assert.AreEqual(PropertyTypeEnum.String, prop.PropertyType);
            Assert.AreEqual("name", prop.Name);
            Assert.AreEqual("standard", prop.Analyzer.IndexAnalyzer.Name);
            Assert.AreEqual("standard", prop.Analyzer.SearchAnalyzer.Name);
            Assert.AreEqual((int)1, prop.Fields.Count());
            StringProperty subProp = prop.Fields.First() as StringProperty;
            Assert.AreEqual("subname", subProp.Name);
        }
    }
}
