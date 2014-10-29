using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Aggregations.Statistics;
using Newtonsoft.Json;
using Bolay.Elastic.Scripts;
using System.Collections.Generic;

namespace Bolay.Elastic.QueryDSL.Tests.Aggregations
{
    [TestClass]
    public class UnitTests_StatisticsAggregate
    {
        [TestMethod]
        public void PASS_Create()
        {
            StatisticsAggregate agg = new StatisticsAggregate("name", "field",
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
            StatisticsAggregate agg = new StatisticsAggregate("name", "field",
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

            string expectedJson = "{\"name\":{\"stats\":{\"field\":\"field\",\"lang\":\"lang\",\"script\":\"script\",\"params\":{\"name\":\"value\"}}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"stats\":{\"field\":\"field\",\"lang\":\"lang\",\"script\":\"script\",\"params\":{\"name\":\"value\"}}}}";
            StatisticsAggregate agg = JsonConvert.DeserializeObject<StatisticsAggregate>(json);
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
            StatisticsAggregate agg = new StatisticsAggregate("name", "field", new Script("script"));

            string json = JsonConvert.SerializeObject(agg);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"stats\":{\"field\":\"field\",\"script\":\"script\"}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_Field_Script()
        {
            string json = "{\"name\":{\"stats\":{\"field\":\"field\",\"script\":\"script\"}}}";
            StatisticsAggregate agg = JsonConvert.DeserializeObject<StatisticsAggregate>(json);
            Assert.IsNotNull(agg);
            Assert.AreEqual("name", agg.Name);
            Assert.AreEqual("field", agg.Field);
            Assert.AreEqual("script", agg.Script.ScriptText);
        }
    }
}
