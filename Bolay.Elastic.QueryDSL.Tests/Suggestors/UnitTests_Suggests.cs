using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Suggesters;
using System.Collections.Generic;
using Bolay.Elastic.QueryDSL.Suggesters.Term;
using Newtonsoft.Json;
using Bolay.Elastic.QueryDSL.Suggesters.Phrase;
using Bolay.Elastic.QueryDSL.Suggesters.Completion;

namespace Bolay.Elastic.QueryDSL.Tests.Suggesters
{
    [TestClass]
    public class UnitTests_Suggests
    {
        [TestMethod]
        public void PASS_Create()
        {
            Suggest suggest = new Suggest(
                new List<ISuggester>() 
                { 
                    new TermSuggester("test", "field")
                });

            Assert.IsNotNull(suggest);
            Assert.AreEqual(1, suggest.Suggestors.Count());
            Assert.AreEqual("test", (suggest.Suggestors.First() as TermSuggester).SuggestName);
            Assert.AreEqual("field", (suggest.Suggestors.First() as TermSuggester).Field);
        }

        [TestMethod]
        public void FAIL_Create()
        {
            try
            {
                Suggest suggest = new Suggest(null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("suggestors", ex.ParamName);   
            }
        }

        [TestMethod]
        public void PASS_Serialize_Term()
        {
            Suggest suggest = new Suggest(new List<ISuggester>() 
                                    { 
                                        new TermSuggester("test", "field") { Text = "text" }
                                    });

            string json = JsonConvert.SerializeObject(suggest);
            Assert.IsNotNull(json);

            string expectedJson = "{\"test\":{\"text\":\"text\",\"term\":{\"field\":\"field\"}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_Term()
        {
            string json = "{\"test\":{\"text\":\"text\",\"term\":{\"field\":\"field\"}}}";
            Suggest suggest = JsonConvert.DeserializeObject<Suggest>(json);
            Assert.IsNotNull(suggest);
            Assert.AreEqual(1, suggest.Suggestors.Count());
            Assert.AreEqual("test", (suggest.Suggestors.First() as TermSuggester).SuggestName);
            Assert.AreEqual("field", (suggest.Suggestors.First() as TermSuggester).Field);
            Assert.AreEqual("text", (suggest.Suggestors.First() as TermSuggester).Text);
        }

        [TestMethod]
        public void PASS_Serialize_Phrase_Term()
        {
            Suggest suggest = new Suggest(new List<ISuggester>() 
                                    { 
                                        new TermSuggester("test", "field"),
                                        new PhraseSuggester("test-phrase", "field2")
                                        {
                                            Analyzer = "analyzer"
                                        }
                                    });
            suggest.Text = "this is the text";

            string json = JsonConvert.SerializeObject(suggest);
            Assert.IsNotNull(json);

            string expectedJson = "{\"text\":\"this is the text\",\"test\":{\"term\":{\"field\":\"field\"}},\"test-phrase\":{\"phrase\":{\"field\":\"field2\",\"analyzer\":\"analyzer\"}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_Phrase_Term()
        {
            string json = "{\"text\":\"this is the text\",\"test\":{\"term\":{\"field\":\"field\"}},\"test-phrase\":{\"phrase\":{\"field\":\"field2\",\"analyzer\":\"analyzer\"}}}";

            Suggest suggest = JsonConvert.DeserializeObject<Suggest>(json);
            Assert.IsNotNull(suggest);
            Assert.AreEqual("this is the text", suggest.Text);
            Assert.AreEqual(2, suggest.Suggestors.Count());
            Assert.AreEqual("test", (suggest.Suggestors.First() as TermSuggester).SuggestName);
            Assert.AreEqual("field", (suggest.Suggestors.First() as TermSuggester).Field);
            Assert.AreEqual("test-phrase", (suggest.Suggestors.Last() as PhraseSuggester).SuggestName);
            Assert.AreEqual("field2", (suggest.Suggestors.Last() as PhraseSuggester).Field);
            Assert.AreEqual("analyzer", (suggest.Suggestors.Last() as PhraseSuggester).Analyzer);
        }

        [TestMethod]
        public void PASS_Serialize_Completion()
        {
            Suggest suggest = new Suggest(new List<ISuggester>() 
                                    { 
                                        new CompletionSuggester("test-completion", "field")
                                        {
                                            Fuzzy = new FuzzyCompletion()
                                            {
                                                Fuzziness = 4,
                                                IsUnicodeAware = true,
                                                MinimumLength = 2,
                                                PrefixLength = 5,
                                                Transpositions = false
                                            },
                                            Size = 2,
                                            Text = "mispeld"
                                        }
                                    });

            string json = JsonConvert.SerializeObject(suggest);
            Assert.IsNotNull(json);

            string expectedJson = "{\"test-completion\":{\"text\":\"mispeld\",\"completion\":{\"field\":\"field\",\"size\":2,\"fuzzy\":{\"fuzziness\":4,\"unicode_aware\":true,\"min_length\":2,\"prefix_length\":5,\"transpositions\":false}}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_Completion()
        {
            string json = "{\"test-completion\":{\"text\":\"mispeld\",\"completion\":{\"field\":\"field\",\"size\":2,\"fuzzy\":{\"fuzziness\":4,\"unicode_aware\":true,\"min_length\":2,\"prefix_length\":5,\"transpositions\":false}}}}";

            Suggest suggest = JsonConvert.DeserializeObject<Suggest>(json);
            Assert.IsNotNull(suggest);            
            Assert.AreEqual(1, suggest.Suggestors.Count());
            Assert.AreEqual(null, suggest.Text);
            Assert.AreEqual("test-completion", suggest.Suggestors.First().SuggestName);
            Assert.AreEqual("mispeld", (suggest.Suggestors.First() as CompletionSuggester).Text);
            Assert.AreEqual(2, (suggest.Suggestors.First() as CompletionSuggester).Size);
            Assert.AreEqual((int)4, Convert.ToInt32((suggest.Suggestors.First() as CompletionSuggester).Fuzzy.Fuzziness));
            Assert.AreEqual(true, (suggest.Suggestors.First() as CompletionSuggester).Fuzzy.IsUnicodeAware);
            Assert.AreEqual(2, (suggest.Suggestors.First() as CompletionSuggester).Fuzzy.MinimumLength);
            Assert.AreEqual(5, (suggest.Suggestors.First() as CompletionSuggester).Fuzzy.PrefixLength);
            Assert.AreEqual(false, (suggest.Suggestors.First() as CompletionSuggester).Fuzzy.Transpositions);
        }
    }
}
