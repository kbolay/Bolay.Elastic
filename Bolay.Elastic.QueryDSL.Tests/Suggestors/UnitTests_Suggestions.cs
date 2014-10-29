using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Bolay.Elastic.QueryDSL.Suggesters.Suggestions;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Suggestors
{
    [TestClass]
    public class UnitTests_Suggestions
    {
        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":[{\"text\":\"devloping\",\"offset\":0,\"length\":9,\"options\":[{\"text\":\"developing\",\"freq\":97,\"score\":0.891424}]}]}";
            IEnumerable<Suggestion> suggestions = JsonConvert.DeserializeObject<SuggestionCollection>(json);
            Assert.IsNotNull(suggestions);
            Assert.AreEqual(1, suggestions.Count());
            Assert.AreEqual("name", suggestions.First().Name);
            Assert.AreEqual("devloping", suggestions.First().Terms.First().Text);
            Assert.AreEqual((int)0, suggestions.First().Terms.First().Offset);
            Assert.AreEqual((int)9, suggestions.First().Terms.First().Length);
            Assert.AreEqual("developing", suggestions.First().Terms.First().Options.First().Text);
            Assert.AreEqual((int)97, suggestions.First().Terms.First().Options.First().Frequency);
            Assert.AreEqual(0.891424, suggestions.First().Terms.First().Options.First().Score);
        }
    }
}
