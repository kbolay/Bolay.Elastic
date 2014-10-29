using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Filters.Tokens.WordDelimiter;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bolay.Elastic.Analysis.Tests.Filters.Tokens
{
    [TestClass]
    public class UnitTests_WordDelimiterTokenFilter
    {
        [TestMethod]
        public void PASS_Create()
        {
            WordDelimiterTokenFilter filter = new WordDelimiterTokenFilter("name")
            {
                CatenateAll = true,
                CatenateNumbers = true,
                CatenateWords = true,
                GenerateNumberParts = false,
                GenerateWordParts = false,
                PreserveOriginal = true,
                ProtectedWords = new List<string>() { "prot1", "prot2" },
                ProtectedWordsPath = "prot/path",
                SplitOnCaseChange = false,
                SplitOnNumerics = false,
                StemEnglishPossessive = false,
                TypeTable = new List<string>() { "typetable" },
                TypeTablePath = "typetable/path"
            };

            Assert.IsNotNull(filter);
            Assert.AreEqual(true, filter.CatenateAll);
            Assert.AreEqual(true, filter.CatenateNumbers);
            Assert.AreEqual(true, filter.CatenateWords);
            Assert.AreEqual(false, filter.GenerateNumberParts);
            Assert.AreEqual(false, filter.GenerateWordParts);
            Assert.AreEqual(true, filter.PreserveOriginal);
            Assert.AreEqual("prot1", filter.ProtectedWords.First());
            Assert.AreEqual("prot2", filter.ProtectedWords.Last());
            Assert.AreEqual("prot/path", filter.ProtectedWordsPath);
            Assert.AreEqual(false, filter.SplitOnCaseChange);
            Assert.AreEqual(false, filter.SplitOnNumerics);
            Assert.AreEqual(false, filter.StemEnglishPossessive);
            Assert.AreEqual("typetable", filter.TypeTable.First());
            Assert.AreEqual("typetable/path", filter.TypeTablePath);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            WordDelimiterTokenFilter filter = new WordDelimiterTokenFilter("name")
            {
                CatenateAll = true,
                CatenateNumbers = true,
                CatenateWords = true,
                GenerateNumberParts = false,
                GenerateWordParts = false,
                PreserveOriginal = true,
                ProtectedWords = new List<string>() { "prot1", "prot2" },
                ProtectedWordsPath = "prot/path",
                SplitOnCaseChange = false,
                SplitOnNumerics = false,
                StemEnglishPossessive = false,
                TypeTable = new List<string>() { "typetable" },
                TypeTablePath = "typetable/path"
            };

            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"word_delimiter\",\"generate_word_parts\":false,\"generate_number_parts\":false,\"catenate_words\":true,\"catenate_numbers\":true,\"catenate_all\":true,\"split_on_case_change\":false,\"preserve_original\":true,\"split_on_numerics\":false,\"stem_english_possessive\":false,\"protected_words\":[\"prot1\",\"prot2\"],\"protected_words_path\":\"prot/path\",\"type_table\":[\"typetable\"],\"type_table_path\":\"typetable/path\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"word_delimeter\",\"generate_word_parts\":false,\"generate_number_parts\":false,\"catenate_words\":true,\"catenate_numbers\":true,\"catenate_all\":true,\"split_on_case_change\":false,\"preserve_original\":true,\"split_on_numerics\":false,\"stem_english_possessive\":false,\"protected_words\":[\"prot1\",\"prot2\"],\"protected_words_path\":\"prot/path\",\"type_table\":[\"typetable\"],\"type_table_path\":\"typetable/path\"}}";

            WordDelimiterTokenFilter filter = JsonConvert.DeserializeObject<WordDelimiterTokenFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual(true, filter.CatenateAll);
            Assert.AreEqual(true, filter.CatenateNumbers);
            Assert.AreEqual(true, filter.CatenateWords);
            Assert.AreEqual(false, filter.GenerateNumberParts);
            Assert.AreEqual(false, filter.GenerateWordParts);
            Assert.AreEqual(true, filter.PreserveOriginal);
            Assert.AreEqual("prot1", filter.ProtectedWords.First());
            Assert.AreEqual("prot2", filter.ProtectedWords.Last());
            Assert.AreEqual("prot/path", filter.ProtectedWordsPath);
            Assert.AreEqual(false, filter.SplitOnCaseChange);
            Assert.AreEqual(false, filter.SplitOnNumerics);
            Assert.AreEqual(false, filter.StemEnglishPossessive);
            Assert.AreEqual("typetable", filter.TypeTable.First());
            Assert.AreEqual("typetable/path", filter.TypeTablePath);
        }
    }
}
