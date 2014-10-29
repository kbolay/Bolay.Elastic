using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Filters.Script;
using System.Collections.Generic;
using Newtonsoft.Json;
using Bolay.Elastic.Scripts;

namespace Bolay.Elastic.QueryDSL.Tests.Filters.Script
{
    [TestClass]
    public class UnitTests_ScriptFilter
    {
        [TestMethod]
        public void PASS_CreateFilter()
        {
            ScriptFilter filter = new ScriptFilter(new Scripts.Script("script"));
            Assert.IsNotNull(filter);
            Assert.AreEqual("script", filter.Script.ScriptText);
        }

        [TestMethod]
        public void FAIL_CreateFilter_Script()
        {
            try
            {
                ScriptFilter filter = new ScriptFilter(null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("script", ex.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serializer()
        {
            ScriptFilter filter = new ScriptFilter(new Scripts.Script("script")
            {
                Parameters = new List<ScriptParameter>()
                {
                    new ScriptParameter("field", "value")
                }
            });

            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);
            string expectedJson = "{\"script\":{\"script\":\"script\",\"params\":{\"field\":\"value\"}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        { 
            string json = "{\"script\":{\"script\":\"script\",\"params\":{\"field\":\"value\"}}}";
            ScriptFilter filter = JsonConvert.DeserializeObject<ScriptFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("script", filter.Script.ScriptText);
            Assert.AreEqual(1, filter.Script.Parameters.Count());
            Assert.AreEqual("field", filter.Script.Parameters.First().Name);
            Assert.AreEqual("value", filter.Script.Parameters.First().Value);
        }
    }
}
