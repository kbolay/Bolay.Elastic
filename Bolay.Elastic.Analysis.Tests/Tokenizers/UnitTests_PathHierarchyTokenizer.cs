using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Analysis.Tokenizers.PathHierarchy;
using Newtonsoft.Json;

namespace Bolay.Elastic.Analysis.Tests.Tokenizers
{
    [TestClass]
    public class UnitTests_PathHierarchyTokenizer
    {
        [TestMethod]
        public void PASS_Create()
        {
            PathHierarchyTokenizer token = new PathHierarchyTokenizer("name")
            {
                BufferSize = 512,
                Delimeter = ",",
                Replacement = ";",
                Reverse = true,
                Skip = 2,
                Version = 2.1
            };

            Assert.IsNotNull(token);
            Assert.AreEqual("name", token.Name);
            Assert.AreEqual(2.1, token.Version);
            Assert.AreEqual((Int64)512, token.BufferSize);
            Assert.AreEqual(",", token.Delimeter);
            Assert.AreEqual(";", token.Replacement);
            Assert.AreEqual(true, token.Reverse);
            Assert.AreEqual((Int64)2, token.Skip);
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            PathHierarchyTokenizer token = new PathHierarchyTokenizer("name")
            {
                BufferSize = 512,
                Delimeter = ",",
                Replacement = ";",
                Reverse = true,
                Skip = 2,
                Version = 2.1
            };

            string json = JsonConvert.SerializeObject(token);
            Assert.IsNotNull(json);

            string expectedJson = "{\"name\":{\"type\":\"path_hierarchy\",\"version\":2.1,\"delimiter\":\",\",\"replacement\":\";\",\"buffer_size\":512,\"reverse\":true,\"skip\":2}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"name\":{\"type\":\"path_hierarchy\",\"version\":2.1,\"delimiter\":\",\",\"replacement\":\";\",\"buffer_size\":512,\"reverse\":true,\"skip\":2}}";
            PathHierarchyTokenizer token = JsonConvert.DeserializeObject<PathHierarchyTokenizer>(json);

            Assert.IsNotNull(token);
            Assert.AreEqual("name", token.Name);
            Assert.AreEqual(2.1, token.Version);
            Assert.AreEqual((Int64)512, token.BufferSize);
            Assert.AreEqual(",", token.Delimeter);
            Assert.AreEqual(";", token.Replacement);
            Assert.AreEqual(true, token.Reverse);
            Assert.AreEqual((Int64)2, token.Skip);
        }
    }
}
