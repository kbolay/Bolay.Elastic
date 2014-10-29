using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Aggregations.Global;
using Bolay.Elastic.QueryDSL.Aggregations;
using System.Collections.Generic;
using Bolay.Elastic.QueryDSL.Aggregations.Average;
using Bolay.Elastic.Scripts;
using Newtonsoft.Json;
using Bolay.Elastic.QueryDSL.Aggregations.Sum;

namespace Bolay.Elastic.QueryDSL.Tests.Aggregations
{
    [TestClass]
    public class UnitTests_GlobalAggregate
    {
        [TestMethod]
        public void PASS_Create()
        {
            GlobalAggregate agg = new GlobalAggregate("name", new List<IAggregation>()
                {
                    new AverageAggregate("avg-name", "field")
                });

            Assert.IsNotNull(agg);
            Assert.AreEqual("name", agg.Name);
            Assert.AreEqual("avg-name", agg.SubAggregations.First().Name);
            Assert.AreEqual("field", (agg.SubAggregations.First() as AverageAggregate).Field);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            GlobalAggregate agg = new GlobalAggregate("name", new List<IAggregation>()
                {
                    new AverageAggregate("avg-name", "field")
                });

            string json = JsonConvert.SerializeObject(agg);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"global\":{},\"aggregations\":{\"avg-name\":{\"avg\":{\"field\":\"field\"}}}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"global\":{},\"aggregations\":{\"avg-name\":{\"avg\":{\"field\":\"field\"}}}}}";
            GlobalAggregate agg = JsonConvert.DeserializeObject<GlobalAggregate>(json);
            Assert.IsNotNull(agg);
            Assert.AreEqual("name", agg.Name);
            Assert.AreEqual("avg-name", agg.SubAggregations.First().Name);
            Assert.AreEqual("field", (agg.SubAggregations.First() as AverageAggregate).Field);
        }

        [TestMethod]
        public void PASS_Serialize_Avg_Sum()
        {
            GlobalAggregate agg = new GlobalAggregate("name", new List<IAggregation>()
                {
                    new AverageAggregate("avg-name", "field"),
                    new SumAggregate("sum-name", "field", new Script("scripttext")
                        {
                            Language = "python",
                            Parameters = new List<ScriptParameter>()
                            {
                                new ScriptParameter("name1", "value1"),
                                new ScriptParameter("name2", 2)
                            }
                        })
                });

            string json = JsonConvert.SerializeObject(agg);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"global\":{},\"aggregations\":{\"avg-name\":{\"avg\":{\"field\":\"field\"}},\"sum-name\":{\"sum\":{\"field\":\"field\",\"lang\":\"python\",\"script\":\"scripttext\",\"params\":{\"name1\":\"value1\",\"name2\":2}}}}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_Avg_Sum()
        {
            string json = "{\"name\":{\"global\":{},\"aggregations\":{\"avg-name\":{\"avg\":{\"field\":\"field\"}},\"sum-name\":{\"sum\":{\"field\":\"field\",\"lang\":\"python\",\"script\":\"scripttext\",\"params\":{\"name1\":\"value1\",\"name2\":2}}}}}}";
            GlobalAggregate agg = JsonConvert.DeserializeObject<GlobalAggregate>(json);
            Assert.IsNotNull(agg);
            Assert.AreEqual("name", agg.Name);
            Assert.AreEqual("avg-name", agg.SubAggregations.First().Name);
            Assert.AreEqual("field", (agg.SubAggregations.First() as AverageAggregate).Field);
            Assert.AreEqual("sum-name", agg.SubAggregations.Last().Name);
            Assert.AreEqual("field", (agg.SubAggregations.Last() as SumAggregate).Field);
            Assert.AreEqual("scripttext", (agg.SubAggregations.Last() as SumAggregate).Script.ScriptText);
            Assert.AreEqual("python", (agg.SubAggregations.Last() as SumAggregate).Script.Language);
            Assert.AreEqual("name1", (agg.SubAggregations.Last() as SumAggregate).Script.Parameters.First().Name);
            Assert.AreEqual("value1", (agg.SubAggregations.Last() as SumAggregate).Script.Parameters.First().Value);
            Assert.AreEqual("name2", (agg.SubAggregations.Last() as SumAggregate).Script.Parameters.Last().Name);
            Assert.AreEqual((int)2, Convert.ToInt32((agg.SubAggregations.Last() as SumAggregate).Script.Parameters.Last().Value));
        }
    }
}