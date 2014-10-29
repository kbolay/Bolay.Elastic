using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Tokenizers.Standard;
using Bolay.Elastic.Analysis.Filters.Tokens;
using System.Collections.Generic;
using Bolay.Elastic.Analysis.Filters.Tokens.Standard;
using Bolay.Elastic.Analysis.Filters.Tokens.Length;
using Bolay.Elastic.Analysis.Filters.Characters;
using Bolay.Elastic.Analysis.Filters.Characters.Mapping;
using Bolay.Elastic.Analysis.Filters.Characters.HtmlStrip;
using Newtonsoft.Json;
using Bolay.Elastic.Analysis.Analyzers.Custom;
using Bolay.Elastic.Analysis.Tokenizers;
using System.IO;

namespace Bolay.Elastic.Analysis.Tests.Analyzers
{
    [TestClass]
    public class UnitTests_CustomAnalyzer
    {
        [TestMethod]
        public void PASS_Create()
        {
            CustomAnalyzer analyzer = new CustomAnalyzer("name",
                new StandardTokenizer("std-tokenizer-name"),
                new List<ITokenFilter>()
                {
                    new StandardTokenFilter("std-token-filter-name"),
                    new LengthTokenFilter("length-token-filter-name")
                },
                new List<ICharacterFilter>()
                {
                    new MappingCharacterFilter("mapping-char-filter-name", "path"),
                    new HtmlStripCharacterFilter("html-strip-char-filter-name")
                });

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
        public void PASS_Serialize()
        {
            CustomAnalyzer analyzer = new CustomAnalyzer("name",
                new StandardTokenizer("std-tokenizer-name"),
                new List<ITokenFilter>()
                {
                    new StandardTokenFilter("std-token-filter-name"),
                    new LengthTokenFilter("length-token-filter-name")
                },
                new List<ICharacterFilter>()
                {
                    new MappingCharacterFilter("mapping-char-filter-name", "path"),
                    new HtmlStripCharacterFilter("html-strip-char-filter-name")
                });

            string json = JsonConvert.SerializeObject(analyzer);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"custom\",\"tokenizer\":\"std-tokenizer-name\",\"filter\":[\"std-token-filter-name\",\"length-token-filter-name\"],\"char_filter\":[\"mapping-char-filter-name\",\"html-strip-char-filter-name\"]}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"analyzer\":{\"name\":{\"type\":\"custom\",\"tokenizer\":\"std-tokenizer-name\",\"filter\":[\"std-token-filter-name\",\"length-token-filter-name\"],\"char_filter\":[\"mapping-char-filter-name\",\"html-strip-char-filter-name\"]}},\"tokenizer\":{\"std-tokenizer-name\":{\"type\":\"standard\"}},\"filter\":{\"std-token-filter-name\":{\"type\":\"standard\"},\"length-token-filter-name\":{\"type\":\"length\"}},\"char_filter\":{\"mapping-char-filter-name\":{\"type\":\"mapping\",\"mappings_path\":\"path\"},\"html-strip-char-filter-name\":{\"type\":\"html_strip\"}}}";

            AnalysisSettings settings = JsonConvert.DeserializeObject<AnalysisSettings>(json);

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

        //[TestMethod]
        //public void PASS_Deserialize_ExistingObject()
        //{
        //    string json = "{\"analyzer\":{\"name\":{\"type\":\"custom\",\"tokenizer\":\"std-tokenizer-name\",\"filter\":[\"std-token-filter-name\",\"length-token-filter-name\"],\"char_filter\":[\"mapping-char-filter-name\",\"html-strip-char-filter-name\"]}},\"tokenizer\":{\"std-tokenizer-name\":{\"type\":\"standard\"}},\"filter\":{\"std-token-filter-name\":{\"type\":\"standard\"},\"length-token-filter-name\":{\"type\":\"length\"}},\"char_filter\":{\"mapping-char-filter-name\":{\"type\":\"mapping\",\"mappings_path\":\"path\"},\"html-strip-char-filter-name\":{\"type\":\"html_strip\"}}}";

        //    AnalysisSettings settings = new AnalysisSettings();
        //    settings.Tokenizers = new List<ITokenizer>() { new StandardTokenizer("std-tokenizer-name") };
        //    settings.TokenFilters = new List<ITokenFilter>()
        //    {
        //        new StandardTokenFilter("std-token-filter-name"),
        //        new LengthTokenFilter("length-token-filter-name")
        //    };
        //    settings.CharacterFilters = new List<ICharacterFilter>()
        //    {
        //        new MappingCharacterFilter("mapping-char-filter-name", "path"),
        //        new HtmlStripCharacterFilter("html-strip-char-filter-name")
        //    };

        //    JsonTextReader reader = new JsonTextReader(new StringReader(json));
        //    CustomAnalyzer analyzer = new CustomAnalyzerSerializer().ReadJson(reader, typeof(CustomAnalyzer), settings, new JsonSerializer()) as CustomAnalyzer;
        //    Assert.IsNotNull(analyzer);
        //    Assert.AreEqual("name", analyzer.Name);
        //    Assert.AreEqual("std-tokenizer-name", analyzer.Tokenizer.Name);
        //    Assert.IsInstanceOfType(analyzer.TokenFilters.First(), typeof(StandardTokenFilter));
        //    Assert.AreEqual("std-token-filter-name", analyzer.TokenFilters.First().Name);
        //    Assert.IsInstanceOfType(analyzer.TokenFilters.Last(), typeof(LengthTokenFilter));
        //    Assert.AreEqual("length-token-filter-name", analyzer.TokenFilters.Last().Name);
        //    Assert.IsInstanceOfType(analyzer.CharacterFilters.First(), typeof(MappingCharacterFilter));
        //    Assert.AreEqual("mapping-char-filter-name", analyzer.CharacterFilters.First().Name);
        //    Assert.IsInstanceOfType(analyzer.CharacterFilters.Last(), typeof(HtmlStripCharacterFilter));
        //    Assert.AreEqual("html-strip-char-filter-name", analyzer.CharacterFilters.Last().Name);
        //}
    }
}
