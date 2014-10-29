using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Sorting.Script;
using System.Collections.Generic;
using Newtonsoft.Json;
using Bolay.Elastic.Scripts;

namespace Bolay.Elastic.QueryDSL.Tests.Sorting.Script
{
    [TestClass]
    public class UnitTests_ScriptSort
    {
        [TestMethod]
        public void PASS_CreateSort()
        {
            ScriptSort sort = new ScriptSort(new Scripts.Script("script"), "number");
            Assert.IsNotNull(sort);
            Assert.AreEqual("script", sort.Script.ScriptText);
            Assert.AreEqual("number", sort.Type);
        }

        [TestMethod]
        public void FAIL_CreateSort_Script()
        {
            try
            {
                ScriptSort sort = new ScriptSort(null, "number");
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("script", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateSort_Type()
        {
            try
            {
                ScriptSort sort = new ScriptSort(new Scripts.Script("script"), "");
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("type", ex.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            ScriptSort sort = new ScriptSort(new Scripts.Script("script") 
                { 
                    Parameters =  new List<ScriptParameter>() { new ScriptParameter("field", "value") }
                }, "number");
            string json = JsonConvert.SerializeObject(sort);
            Assert.IsNotNull(json);
            string expectedJson = "{\"_script\":{\"type\":\"number\",\"script\":\"script\",\"params\":{\"field\":\"value\"}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"_script\":{\"script\":\"script\",\"type\":\"number\",\"params\":{\"field\":\"value\"}}}";
            ScriptSort sort = JsonConvert.DeserializeObject<ScriptSort>(json);
            Assert.IsNotNull(sort);
            Assert.AreEqual("script", sort.Script.ScriptText);
            Assert.AreEqual("number", sort.Type);
            Assert.AreEqual("field", sort.Script.Parameters.First().Name);
            Assert.AreEqual("value", sort.Script.Parameters.First().Value.ToString());
        }
    }
}
