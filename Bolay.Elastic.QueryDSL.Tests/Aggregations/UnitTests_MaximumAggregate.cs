using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Scripts;
using System.Collections.Generic;
using Bolay.Elastic.QueryDSL.Aggregations.Maximum;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Aggregations
{
    [TestClass]
    public class UnitTests_MaximumAggregate
    {
        [TestMethod]
        public void PASS_Create()
        {
            MaximumAggregate agg = new MaximumAggregate("name", "field",
                new Script("script")
                {
                    Language = "lang",
                    Parameters = new List<ScriptParameter>() 
                    { 
                        new ScriptParameter("name", "value")
                    }
                });

            Assert.IsNotNull(agg);
            Assert.AreEqual("name", agg.Name);
            Assert.AreEqual("field", agg.Field);
            Assert.AreEqual("script", agg.Script.ScriptText);
            Assert.AreEqual("name", agg.Script.Parameters.First().Name);
            Assert.AreEqual("value", agg.Script.Parameters.First().Value);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            MaximumAggregate agg = new MaximumAggregate("name", "field",
                new Script("script")
                {
                    Language = "lang",
                    Parameters = new List<ScriptParameter>() 
                    { 
                        new ScriptParameter("name", "value")
                    }
                });

            string json = JsonConvert.SerializeObject(agg);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"max\":{\"field\":\"field\",\"lang\":\"lang\",\"script\":\"script\",\"params\":{\"name\":\"value\"}}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"max\":{\"field\":\"field\",\"lang\":\"lang\",\"script\":\"script\",\"params\":{\"name\":\"value\"}}}}";
            MaximumAggregate agg = JsonConvert.DeserializeObject<MaximumAggregate>(json);
            Assert.IsNotNull(agg);
            Assert.AreEqual("name", agg.Name);
            Assert.AreEqual("field", agg.Field);
            Assert.AreEqual("script", agg.Script.ScriptText);
            Assert.AreEqual("name", agg.Script.Parameters.First().Name);
            Assert.AreEqual("value", agg.Script.Parameters.First().Value);
        }

        [TestMethod]
        public void PASS_Serialize_Field_Script()
        {
            MaximumAggregate agg = new MaximumAggregate("name", "field", new Script("script"));

            string json = JsonConvert.SerializeObject(agg);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"max\":{\"field\":\"field\",\"script\":\"script\"}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_Field_Script()
        {
            string json = "{\"name\":{\"max\":{\"field\":\"field\",\"script\":\"script\"}}}";
            MaximumAggregate agg = JsonConvert.DeserializeObject<MaximumAggregate>(json);
            Assert.IsNotNull(agg);
            Assert.AreEqual("name", agg.Name);
            Assert.AreEqual("field", agg.Field);
            Assert.AreEqual("script", agg.Script.ScriptText);
        }
    }
}
