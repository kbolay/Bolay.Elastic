using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.ScriptFields;
using System.Collections.Generic;
using Newtonsoft.Json;
using Bolay.Elastic.Scripts;

namespace Bolay.Elastic.QueryDSL.Tests.ScriptFields
{
    [TestClass]
    public class UnitTests_ScriptFields
    {
        [TestMethod]
        public void PASS_Create()
        {
            ScriptFieldRequest request = new ScriptFieldRequest(
                new List<ScriptField>()
                { 
                    new ScriptField(
                            "field", 
                            new Script("script"){
                                Parameters = new List<ScriptParameter>()
                                { 
                                    new ScriptParameter("name", "value")
                                }
                            })                            
                });

            Assert.IsNotNull(request);
            Assert.AreEqual("field", request.Fields.First().Field);
            Assert.AreEqual("script", request.Fields.First().Script.ScriptText);
            Assert.AreEqual("name", request.Fields.First().Script.Parameters.First().Name);
            Assert.AreEqual("value", request.Fields.First().Script.Parameters.First().Value);
        }

        [TestMethod]
        public void FAIL_Create_ScriptFields()
        {
            try
            {
                ScriptFieldRequest request = new ScriptFieldRequest(null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("fields", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_Create_ScriptFieldName()
        {
            try
            {
                ScriptFieldRequest request = new ScriptFieldRequest(
                    new List<ScriptField>() 
                    { 
                        new ScriptField(null, null)
                    });
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("field", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_Create_ScriptFieldScript()
        {
            try
            {
                ScriptFieldRequest request = new ScriptFieldRequest(
                    new List<ScriptField>() 
                    { 
                        new ScriptField("field", null)
                    });
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("script", ex.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            ScriptFieldRequest request = new ScriptFieldRequest(
                new List<ScriptField>()
                { 
                    new ScriptField(
                            "field", 
                            new Script("script")
                            {
                                Parameters = new List<ScriptParameter>()
                                { 
                                    new ScriptParameter("name", "value")
                                }
                            }),
                    new ScriptField("field1", 
                        new Script("script1")
                        {
                            Parameters = new List<ScriptParameter>()
                            {
                                new ScriptParameter("name1", "value1"),
                                new ScriptParameter("name2", "value2")
                            }
                        })
                });

            string json = JsonConvert.SerializeObject(request);
            Assert.IsNotNull(json);
            string expectedJson = "{\"script_field\":{\"field\":{\"script\":\"script\",\"params\":{\"name\":\"value\"}},\"field1\":{\"script\":\"script1\",\"params\":{\"name1\":\"value1\",\"name2\":\"value2\"}}}}";
            Assert.AreEqual(expectedJson.Trim(), json.Trim());
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"script_field\":{\"field\":{\"script\":\"script\",\"params\":{\"name\":\"value\"}},\"field1\":{\"script\":\"script1\",\"params\":{\"name1\":\"value1\",\"name2\":\"value2\"}}}}";

            ScriptFieldRequest request = JsonConvert.DeserializeObject<ScriptFieldRequest>(json);
            Assert.IsNotNull(request);
            Assert.AreEqual("field", request.Fields.First().Field);
            Assert.AreEqual("script", request.Fields.First().Script.ScriptText);
            Assert.AreEqual("name", request.Fields.First().Script.Parameters.First().Name);
            Assert.AreEqual("value", request.Fields.First().Script.Parameters.First().Value);
            Assert.AreEqual("field1", request.Fields.Last().Field);
            Assert.AreEqual("script1", request.Fields.Last().Script.ScriptText);
            Assert.AreEqual("name1", request.Fields.Last().Script.Parameters.First().Name);
            Assert.AreEqual("value1", request.Fields.Last().Script.Parameters.First().Value);
            Assert.AreEqual("name2", request.Fields.Last().Script.Parameters.Last().Name);
            Assert.AreEqual("value2", request.Fields.Last().Script.Parameters.Last().Value);
        }
    }
}
