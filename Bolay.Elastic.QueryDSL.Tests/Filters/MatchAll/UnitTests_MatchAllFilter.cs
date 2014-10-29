using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Filters.MatchAll;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Filters.MatchAll
{
    [TestClass]
    public class UnitTests_MatchAllFilter
    {
        [TestMethod]
        public void PASS_CreateFilter()
        {
            MatchAllFilter filter = new MatchAllFilter();
            Assert.IsNotNull(filter);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            MatchAllFilter filter = new MatchAllFilter();
            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);
            string expectedJson = "{\"match_all\":{}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"match_all\":{}}";
            MatchAllFilter filter = JsonConvert.DeserializeObject<MatchAllFilter>(json);
            Assert.IsNotNull(filter);
        }
    }
}
