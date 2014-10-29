using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Aggregations.Terms;
using Bolay.Elastic.Scripts;
using System.Collections.Generic;
using Newtonsoft.Json;
using Bolay.Elastic.Models;

namespace Bolay.Elastic.QueryDSL.Tests.Aggregations
{
    [TestClass]
    public class UnitTests_TermsAggregate
    {
        [TestMethod]
        public void PASS_Create()
        {
            TermsAggregate agg = new TermsAggregate("name", "field");
            Assert.IsNotNull(agg);
            Assert.AreEqual("name", agg.Name);
            Assert.AreEqual("field", agg.Field);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            TermsAggregate agg = new TermsAggregate(
                "name",
                "field",
                new Script("scripttext")
                {
                    Language = "js",
                    Parameters = new List<ScriptParameter>()
                    {
                        new ScriptParameter("name", "value")
                    }
                })
            {
                Exclude = new RegexPattern("excludepattern"),
                ExecutionHint = ExecutionTypeEnum.Map,
                Include = new RegexPattern("includepattern", new List<RegexFlagEnum>()
                    {
                        RegexFlagEnum.Literal,
                        RegexFlagEnum.Multiline
                    }),
                MinimumDocumentCount = 5,
                ShardSize = 10,
                Size = 7,
                SortValue = "_term"
            };

            string json = JsonConvert.SerializeObject(agg);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"terms\":{\"field\":\"field\",\"lang\":\"js\",\"script\":\"scripttext\",\"params\":{\"name\":\"value\"},\"size\":7,\"shard_size\":10,\"min_doc_count\":5,\"order\":{\"_term\":\"asc\"},\"include\":{\"pattern\":\"includepattern\",\"flags\":\"LITERAL|MULTILINE\"},\"exclude\":\"excludepattern\",\"execution_hint\":\"map\"}}}";

            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"terms\":{\"field\":\"field\",\"lang\":\"js\",\"script\":\"scripttext\",\"params\":{\"name\":\"value\"},\"size\":7,\"shard_size\":10,\"min_doc_count\":5,\"order\":{\"_term\":\"asc\"},\"include\":{\"pattern\":\"includepattern\",\"flags\":\"LITERAL|MULTILINE\"},\"exclude\":\"excludepattern\",\"execution_hint\":\"map\"}}}";

            TermsAggregate agg = JsonConvert.DeserializeObject<TermsAggregate>(json);
            Assert.IsNotNull(agg);
            Assert.AreEqual("name", agg.Name);
            Assert.AreEqual("field", agg.Field);
            Assert.AreEqual("scripttext", agg.Script.ScriptText);
            Assert.AreEqual("js", agg.Script.Language);
            Assert.AreEqual("name", agg.Script.Parameters.First().Name);
            Assert.AreEqual("value", agg.Script.Parameters.First().Value);
            Assert.AreEqual((int)7, agg.Size);
            Assert.AreEqual((int)10, agg.ShardSize);
            Assert.AreEqual((int)5, agg.MinimumDocumentCount);
            Assert.AreEqual("_term", agg.SortValue);
            Assert.AreEqual(SortOrderEnum.Ascending, agg.SortOrder);
            Assert.AreEqual("includepattern", agg.Include.Pattern);
            Assert.AreEqual(RegexFlagEnum.Literal, agg.Include.Flags.First());
            Assert.AreEqual(RegexFlagEnum.Multiline, agg.Include.Flags.Last());
            Assert.AreEqual("excludepattern", agg.Exclude.Pattern);
            Assert.AreEqual(ExecutionTypeEnum.Map, agg.ExecutionHint);
        }
    }
}
