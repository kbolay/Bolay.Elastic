using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Mapping.Properties.Attachment;
using System.Collections.Generic;
using Bolay.Elastic.Mapping.Properties;
using Bolay.Elastic.Mapping.Properties.String;
using Newtonsoft.Json;
using Bolay.Elastic.Mapping.Properties.Date;
using Bolay.Elastic.Analysis.Analyzers.Standard;

namespace Bolay.Elastic.Mapping.Tests.Types
{
    [TestClass]
    public class UnitTests_AttachmentProperty
    {
        [TestMethod]
        public void PASS_Create()
        {
            AttachmentProperty prop = new AttachmentProperty("attach")
            {
                Fields = new List<IDocumentProperty>()
                {
                    new StringProperty("file")
                    {
                        Index = IndexSettingEnum.No
                    },
                    new DateProperty("date")
                    {
                        Store = true
                    },
                    new StringProperty("author")
                    {
                        Analyzer = new PropertyAnalyzer(new StandardAnalyzer("standard"))
                    }
                }
            };
            Assert.IsNotNull(prop);
            Assert.AreEqual("attach", prop.Name);
            Assert.AreEqual((int)3, prop.Fields.Count());
            Assert.IsInstanceOfType(prop.Fields.First(), typeof(StringProperty));
            StringProperty stringProp = prop.Fields.First() as StringProperty;
            Assert.AreEqual(IndexSettingEnum.No, stringProp.Index);
            Assert.IsInstanceOfType(prop.Fields.ElementAt(1), typeof(DateProperty));
            DateProperty dateProp = prop.Fields.ElementAt(1) as DateProperty;
            Assert.AreEqual(true, dateProp.Store);
            Assert.IsInstanceOfType(prop.Fields.Last(), typeof(StringProperty));
            stringProp = prop.Fields.Last() as StringProperty;
            Assert.AreEqual("standard", stringProp.Analyzer.Analyzer.Name);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            AttachmentProperty prop = new AttachmentProperty("attach")
            {
                Fields = new List<IDocumentProperty>()
                {
                    new StringProperty("file")
                    {
                        Index = IndexSettingEnum.No,
                        IndexOptions = IndexOptionEnum.DocumentId,
                        OmitNorms = true
                    },
                    new DateProperty("date")
                    {
                        Store = true
                    },
                    new StringProperty("author")
                    {
                        Analyzer = new PropertyAnalyzer(new StandardAnalyzer("standard"))
                    }
                }
            };
            string json = JsonConvert.SerializeObject(prop);
            Assert.IsNotNull(json);

            string expectedJson = "{\"attach\":{\"type\":\"attachment\",\"fields\":{\"file\":{\"type\":\"string\",\"index\":\"no\"},\"date\":{\"type\":\"date\",\"store\":true},\"author\":{\"type\":\"string\",\"analyzer\":\"standard\"}}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"attach\":{\"type\":\"attachment\",\"fields\":{\"file\":{\"type\":\"string\",\"index\":\"no\"},\"date\":{\"type\":\"date\",\"store\":true},\"author\":{\"type\":\"string\",\"analyzer\":\"standard\"}}}}";

            AttachmentProperty prop = JsonConvert.DeserializeObject<AttachmentProperty>(json);
            Assert.IsNotNull(prop);
            Assert.AreEqual("attach", prop.Name);
            Assert.AreEqual((int)3, prop.Fields.Count());
            Assert.IsInstanceOfType(prop.Fields.First(), typeof(StringProperty));
            StringProperty stringProp = prop.Fields.First() as StringProperty;
            Assert.AreEqual(IndexSettingEnum.No, stringProp.Index);
            Assert.IsInstanceOfType(prop.Fields.ElementAt(1), typeof(DateProperty));
            DateProperty dateProp = prop.Fields.ElementAt(1) as DateProperty;
            Assert.AreEqual(true, dateProp.Store);
            Assert.IsInstanceOfType(prop.Fields.Last(), typeof(StringProperty));
            stringProp = prop.Fields.Last() as StringProperty;
            Assert.AreEqual("standard", stringProp.Analyzer.Analyzer.Name);
        }
    }
}
