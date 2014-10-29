using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Faceting.Statistics;
using Newtonsoft.Json;
using System.Collections.Generic;
using Bolay.Elastic.Scripts;

namespace Bolay.Elastic.QueryDSL.Tests.Faceting
{
    [TestClass]
    public class UnitTests_StatisticalFacet
    {
        [TestMethod]
        public void PASS_Create()
        {
            StatisticsFacet facet = new StatisticsFacet("name", "field");
            Assert.IsNotNull(facet);
            Assert.AreEqual("name", facet.FacetName);
            Assert.AreEqual("field", facet.Field);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            StatisticsFacet facet = new StatisticsFacet("name", "field");
            string json = JsonConvert.SerializeObject(facet);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"statistical\":{\"field\":\"field\"}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"statistical\":{\"field\":\"field\"}}}";
            StatisticsFacet facet = JsonConvert.DeserializeObject<StatisticsFacet>(json);
            Assert.IsNotNull(facet);
            Assert.AreEqual("name", facet.FacetName);
            Assert.AreEqual("field", facet.Field);
        }

        [TestMethod]
        public void PASS_Serialize_Script()
        { 
            StatisticsFacet facet = new StatisticsFacet(
                "name", 
                new Scripts.Script("script")
                {
                    Parameters = new List<ScriptParameter>()
                    { 
                        new ScriptParameter("name", "value")
                    }
                });

            string json = JsonConvert.SerializeObject(facet);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"statistical\":{\"script\":\"script\",\"params\":{\"name\":\"value\"}}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_Script()
        {
            string json = "{\"name\":{\"statistical\":{\"script\":\"script\",\"params\":{\"name\":\"value\"}}}}";
            StatisticsFacet facet = JsonConvert.DeserializeObject<StatisticsFacet>(json);
            Assert.IsNotNull(facet);
            Assert.AreEqual("name", facet.FacetName);
            Assert.AreEqual("script", facet.Script.ScriptText);
            Assert.AreEqual("name", facet.Script.Parameters.First().Name);
            Assert.AreEqual("value", facet.Script.Parameters.First().Value);
        }

        [TestMethod]
        public void PASS_Deserialize_Response()
        {
            string json = "{\"name\":{\"_type\":\"statistical\",\"count\":39145,\"total\":168383.4390208544,\"min\":2.2603649999999997,\"max\":793.919967,\"mean\":4.3015312050288514,\"sum_of_squares\":1742502.4727569432,\"variance\":26.010878921922732,\"std_deviation\":5.100086168088019}}";
            StatisticsResponse result = JsonConvert.DeserializeObject<StatisticsResponse>(json);
            Assert.IsNotNull(result);
            Assert.AreEqual("name", result.Name);
            Assert.AreEqual((Int64)39145, result.Count);
            Assert.AreEqual((Double)168383.439020854, result.Sum);
            Assert.AreEqual((Double)2.260365, result.Minimum);
            Assert.AreEqual((Double)793.919967, result.Maximum);
            Assert.AreEqual((Double)4.30153120502885, result.Average);
            Assert.AreEqual((Double)1742502.47275694, result.SumOfSquares);
            Assert.AreEqual((Double)26.0108789219227, result.Variance);
            Assert.AreEqual((Double)5.10008616808802, result.StandardDeviation);
        }
    }
}
