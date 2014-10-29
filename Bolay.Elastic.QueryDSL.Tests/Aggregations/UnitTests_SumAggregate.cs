using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Aggregations.Sum;
using Newtonsoft.Json;
using Bolay.Elastic.Scripts;
using System.Collections.Generic;

namespace Bolay.Elastic.QueryDSL.Tests.Aggregations
{
    [TestClass]
    public class UnitTests_SumAggregate
    {
        [TestMethod]
        public void PASS_Create()
        {
            SumAggregate agg = new SumAggregate("name", "field",
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
            SumAggregate agg = new SumAggregate("name", "field",
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

            string expectedJson = "{\"name\":{\"sum\":{\"field\":\"field\",\"lang\":\"lang\",\"script\":\"script\",\"params\":{\"name\":\"value\"}}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"sum\":{\"field\":\"field\",\"lang\":\"lang\",\"script\":\"script\",\"params\":{\"name\":\"value\"}}}}";
            SumAggregate agg = JsonConvert.DeserializeObject<SumAggregate>(json);
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
            SumAggregate agg = new SumAggregate("name", "field", new Script("script"));

            string json = JsonConvert.SerializeObject(agg);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"sum\":{\"field\":\"field\",\"script\":\"script\"}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_Field_Script()
        {
            string json = "{\"name\":{\"sum\":{\"field\":\"field\",\"script\":\"script\"}}}";
            SumAggregate agg = JsonConvert.DeserializeObject<SumAggregate>(json);
            Assert.IsNotNull(agg);
            Assert.AreEqual("name", agg.Name);
            Assert.AreEqual("field", agg.Field);
            Assert.AreEqual("script", agg.Script.ScriptText);
        }
    }
}
