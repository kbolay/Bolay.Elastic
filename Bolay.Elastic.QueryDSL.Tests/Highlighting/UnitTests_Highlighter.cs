using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Highlighting;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Highlighting
{
    [TestClass]
    public class UnitTests_Highlighter
    {
        [TestMethod]
        public void PASS_Create()
        {
            Highlighter highlighter = new Highlighter(new List<FieldHighlighter>()
                                                        {
                                                            new FieldHighlighter("field")
                                                        });

            Assert.IsNotNull(highlighter);
            Assert.AreEqual("field", highlighter.FieldHighlighters.First().Field);
        }

        [TestMethod]
        public void FAIL_Create_Fields()
        {
            try
            {
                Highlighter highlighter = new Highlighter(null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("fieldHighlighters", ex.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serializer()
        {
            Highlighter highlighter = new Highlighter(new List<FieldHighlighter>()
                                                        {
                                                            new FieldHighlighter("field")
                                                        });
            string json = JsonConvert.SerializeObject(highlighter);
            Assert.IsNotNull(json);
            string expectedJson = "{\"field\":{}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"field\":{}}";
            Highlighter highlighter = JsonConvert.DeserializeObject<Highlighter>(json);
            Assert.IsNotNull(highlighter);
            Assert.AreEqual("field", highlighter.FieldHighlighters.First().Field);
        }
    }
}
