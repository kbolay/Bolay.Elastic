using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Bolay.Elastic.Analysis.Analyzers.Custom;
using Bolay.Elastic.Analysis.Filters.Tokens.Standard;
using Bolay.Elastic.Analysis.Filters.Tokens.Length;
using Bolay.Elastic.Analysis.Filters.Characters.Mapping;
using Bolay.Elastic.Analysis.Filters.Characters.HtmlStrip;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Bolay.Elastic.Analysis.Tests
{
    [TestClass]
    public class UnitTests_AnalysisSettings
    {
        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"analyzer\":{\"name\":{\"type\":\"custom\",\"tokenizer\":\"std-tokenizer-name\",\"filter\":[\"std-token-filter-name\",\"length-token-filter-name\"],\"char_filter\":[\"mapping-char-filter-name\",\"html-strip-char-filter-name\"]}},\"tokenizer\":{\"std-tokenizer-name\":{\"type\":\"standard\"}},\"filter\":{\"std-token-filter-name\":{\"type\":\"standard\"},\"length-token-filter-name\":{\"type\":\"length\"}},\"char_filter\":{\"mapping-char-filter-name\":{\"type\":\"mapping\",\"mappings_path\":\"path\"},\"html-strip-char-filter-name\":{\"type\":\"html_strip\"}}}";

            AnalysisSettings settings = JsonConvert.DeserializeObject<AnalysisSettings>(json);
            Assert.IsNotNull(settings);
            Assert.AreEqual((int)1, settings.Analyzers.Count());
            Assert.AreEqual((int)1, settings.Tokenizers.Count());
            Assert.AreEqual((int)2, settings.TokenFilters.Count());
            Assert.AreEqual((int)2, settings.CharacterFilters.Count());
            CustomAnalyzer analyzer = settings.Analyzers.First() as CustomAnalyzer;
            Assert.IsNotNull(analyzer);
            Assert.AreEqual("name", analyzer.Name);
            Assert.AreEqual("std-tokenizer-name", analyzer.Tokenizer.Name);
            Assert.IsInstanceOfType(analyzer.TokenFilters.First(), typeof(StandardTokenFilter));
            Assert.AreEqual("std-token-filter-name", analyzer.TokenFilters.First().Name);
            Assert.IsInstanceOfType(analyzer.TokenFilters.Last(), typeof(LengthTokenFilter));
            Assert.AreEqual("length-token-filter-name", analyzer.TokenFilters.Last().Name);
            Assert.IsInstanceOfType(analyzer.CharacterFilters.First(), typeof(MappingCharacterFilter));
            Assert.AreEqual("mapping-char-filter-name", analyzer.CharacterFilters.First().Name);
            Assert.IsInstanceOfType(analyzer.CharacterFilters.Last(), typeof(HtmlStripCharacterFilter));
            Assert.AreEqual("html-strip-char-filter-name", analyzer.CharacterFilters.Last().Name);
        }

        [TestMethod]
        public void PASS_Deserialize_V1()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(UnitTests_AnalysisSettings));
            string json = null;
            using (StreamReader stream = new StreamReader(assembly.GetManifestResourceStream("Bolay.Elastic.Analysis.Tests.Resources.AnalysisV1.json")))
            {
                json = stream.ReadToEnd();
            }

            AnalysisSettings settings = JsonConvert.DeserializeObject<AnalysisSettings>(json);
            Assert.IsNotNull(settings);
            Assert.IsTrue(settings.Analyzers.Count() > 0);
            Assert.IsTrue(settings.Tokenizers.Count() > 0);
            Assert.IsTrue(settings.TokenFilters.Count() > 0);
            //Assert.IsTrue(settings.CharacterFilters.Count() > 0);
        }
    }
}
