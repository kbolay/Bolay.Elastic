using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Mapping.Fields._All;
using System.Collections.Generic;
using Newtonsoft.Json;
using Bolay.Elastic.Analysis.Analyzers.Standard;

namespace Bolay.Elastic.Mapping.Tests.Fields
{
    [TestClass]
    public class UnitTests_All
    {
        [TestMethod]
        public void PASS_Create()
        {
            All all = new All()
            {
                Analyzer = new PropertyAnalyzer(new StandardAnalyzer("standard"), new StandardAnalyzer("standard")),
                Excludes = new List<string>() { "ex1", "ex2" },
                Includes = new List<string>() { "in1", "in2" },
                IsEnabled = false,
                Store = true,
                TermVector = TermVectorEnum.WithPositions
            };

            Assert.IsNotNull(all);
            Assert.AreEqual(false, all.IsEnabled);
            Assert.AreEqual(true, all.Store);
            Assert.AreEqual(TermVectorEnum.WithPositions, all.TermVector);
            Assert.AreEqual("standard", all.Analyzer.IndexAnalyzer.Name);
            Assert.AreEqual("standard", all.Analyzer.SearchAnalyzer.Name);
            Assert.AreEqual("ex1", all.Excludes.First());
            Assert.AreEqual("ex2", all.Excludes.Last());
            Assert.AreEqual("in1", all.Includes.First());
            Assert.AreEqual("in2", all.Includes.Last());
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            All all = new All()
            {
                Analyzer = new PropertyAnalyzer(new StandardAnalyzer("standard"), new StandardAnalyzer("standard")),
                Excludes = new List<string>() { "ex1", "ex2" },
                Includes = new List<string>() { "in1", "in2" },
                IsEnabled = false,
                Store = true,
                TermVector = TermVectorEnum.WithPositions
            };

            string json = JsonConvert.SerializeObject(all);
            Assert.IsNotNull(json);

            string expectedJson = "{\"enabled\":false,\"store\":true,\"term_vector\":\"with_positions\",\"includes\":[\"in1\",\"in2\"],\"excludes\":[\"ex1\",\"ex2\"],\"index_analyzer\":\"standard\",\"search_analyzer\":\"standard\"}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"enabled\":false,\"store\":true,\"term_vector\":\"with_positions\",\"includes\":[\"in1\",\"in2\"],\"excludes\":[\"ex1\",\"ex2\"],\"index_analyzer\":\"standard\",\"search_analyzer\":\"standard\"}";

            All all = JsonConvert.DeserializeObject<All>(json);
            Assert.IsNotNull(all);
            Assert.AreEqual(false, all.IsEnabled);
            Assert.AreEqual(true, all.Store);
            Assert.AreEqual(TermVectorEnum.WithPositions, all.TermVector);
            Assert.AreEqual("standard", all.Analyzer.IndexAnalyzer.Name);
            Assert.AreEqual("standard", all.Analyzer.SearchAnalyzer.Name);
            Assert.AreEqual("ex1", all.Excludes.First());
            Assert.AreEqual("ex2", all.Excludes.Last());
            Assert.AreEqual("in1", all.Includes.First());
            Assert.AreEqual("in2", all.Includes.Last());
        }
    }
}
