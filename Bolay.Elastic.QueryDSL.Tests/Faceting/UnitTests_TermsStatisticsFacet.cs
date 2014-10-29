using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Faceting.TermsStatistics;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Faceting
{
    [TestClass]
    public class UnitTests_TermsStatisticsFacet
    {
        [TestMethod]
        public void PASS_Create()
        {
            TermsStatisticsFacet facet = new TermsStatisticsFacet("name", "keyfield", "valuefield");
            Assert.IsNotNull(facet);
            Assert.AreEqual("name", facet.FacetName);
            Assert.AreEqual("keyfield", facet.KeyField);
            Assert.AreEqual("valuefield", facet.ValueField);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            TermsStatisticsFacet facet = new TermsStatisticsFacet("name", "keyfield", "valuefield");
            string json = JsonConvert.SerializeObject(facet);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"terms_stats\":{\"key_field\":\"keyfield\",\"value_field\":\"valuefield\"}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"terms_stats\":{\"key_field\":\"keyfield\",\"value_field\":\"valuefield\"}}}";
            TermsStatisticsFacet facet = JsonConvert.DeserializeObject<TermsStatisticsFacet>(json);
            Assert.IsNotNull(facet);
            Assert.AreEqual("name", facet.FacetName);
            Assert.AreEqual("keyfield", facet.KeyField);
            Assert.AreEqual("valuefield", facet.ValueField);
        }

        [TestMethod]
        public void PASS_Deserialize_Response()
        {
            string json = "{\"name\":{\"_type\":\"terms_stats\",\"terms\":[{\"term\":\"term1\",\"count\":14,\"min\":1.9556740077956032,\"max\":9.956190163777851,\"total_count\":14,\"total\":99.23173167835192,\"mean\":7.087980834167994},{\"term\":\"term2\",\"count\":39,\"min\":10.002835842271903,\"max\":19.92768557178301,\"total_count\":39,\"total\":606.3820935824962,\"mean\":15.548258809807594}]}}";
            TermsStatisticsResponse facet = JsonConvert.DeserializeObject<TermsStatisticsResponse>(json);
            Assert.IsNotNull(facet);
            Assert.AreEqual("name", facet.Name);
            Assert.AreEqual("term1", facet.Terms.First().Term);
            Assert.AreEqual((Double)1.9556740077956, facet.Terms.First().Minimum);
            Assert.AreEqual((Double)9.95619016377785, facet.Terms.First().Maximum);
            Assert.AreEqual((Double)99.2317316783519, facet.Terms.First().Sum);
            Assert.AreEqual((Double)7.08798083416799, facet.Terms.First().Average);
            Assert.AreEqual((Int64)14, facet.Terms.First().Count);
            Assert.AreEqual((Int64)14, facet.Terms.First().TotalCount);

            Assert.AreEqual("term2", facet.Terms.Last().Term);
            Assert.AreEqual((Double)10.0028358422719, facet.Terms.Last().Minimum);
            Assert.AreEqual((Double)19.927685571783, facet.Terms.Last().Maximum);
            Assert.AreEqual((Double)606.382093582496, facet.Terms.Last().Sum);
            Assert.AreEqual((Double)15.5482588098076, facet.Terms.Last().Average);
            Assert.AreEqual((Int64)39, facet.Terms.Last().Count);
            Assert.AreEqual((Int64)39, facet.Terms.Last().TotalCount);
        }
    }
}
