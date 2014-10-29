using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Explanations;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Explanations
{
    [TestClass]
    public class UnitTests_Explanation
    {
        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"value\":1.23,\"description\":\"a\",\"details\":[{\"value\":2.34,\"description\":\"b\"}]}";
            Explanation explanation = JsonConvert.DeserializeObject<Explanation>(json);
            Assert.IsNotNull(explanation);
            Assert.AreEqual(1.23, explanation.Value);
            Assert.AreEqual("a", explanation.Description);
            Assert.AreEqual(2.34, explanation.Details.First().Value);
            Assert.AreEqual("b", explanation.Details.First().Description);
        }
    }
}
