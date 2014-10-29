using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Filters.Tokens.Elision;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bolay.Elastic.Analysis.Tests.Filters.Tokens
{
    [TestClass]
    public class UnitTests_ElisionTokenFilter
    {
        [TestMethod]
        public void PASS_Create()
        {
            ElisionTokenFilter filter = new ElisionTokenFilter("name", new List<string>() { "d'", "l'" });

            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual("d'", filter.Articles.First());
            Assert.AreEqual("l'", filter.Articles.Last());
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            ElisionTokenFilter filter = new ElisionTokenFilter("name", new List<string>() { "d'", "l'" });

            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"elision\",\"articles\":[\"d'\",\"l'\"]}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"elision\",\"articles\":[\"d'\",\"l'\"]}}";
            ElisionTokenFilter filter = JsonConvert.DeserializeObject<ElisionTokenFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("name", filter.Name);
            Assert.AreEqual("d'", filter.Articles.First());
            Assert.AreEqual("l'", filter.Articles.Last());
        }
    }
}
