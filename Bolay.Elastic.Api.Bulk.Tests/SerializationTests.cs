using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Api.Bulk.Request;
using Bolay.Elastic.Time;
using Bolay.Elastic.Scripts;

namespace Bolay.Elastic.Api.Bulk.Tests
{
    /// <summary>
    /// Summary description for SerializationTests
    /// </summary>
    [TestClass]
    public class SerializationTests
    {
        [TestMethod]
        public void IndexBulkActionSerialization()
        {
            TimeValue ttl = new TimeValue("1d");
            DateTime utcNow = DateTime.UtcNow;

            IndexBulkAction<TestClass> bulkAction = new IndexBulkAction<TestClass>("index", "type", "1", new TestClass() { Content = "content" })
            {
                Parent = "parent",
                Routing = "routing",
                TimeStamp = utcNow,
                TimeToLive = ttl,
                Version = 1
            };

            string json = bulkAction.ToString();

            string expectedJson = "{\"index\":{\"_index\":\"index\",\"_type\":\"type\",\"_id\":\"1\",\"_version\":1,\"_routing\":\"routing\",\"_parent\":\"parent\",\"_timestamp\":\"" + 
                utcNow.ToString(Bolay.Elastic.Time.DateTimeFormatEnum.Date.Format) + 
                "\",\"_ttl\":\"1d\"}}\r\n{\"Content\":\"content\"}\r\n";

            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void UpdateBulkActionSerialization_Basic()
        {
            TimeValue ttl = new TimeValue("1d");
            DateTime utcNow = DateTime.UtcNow;

            UpdateBulkAction<TestClass> bulkAction = new UpdateBulkAction<TestClass>("index", "type", "1", new TestClass() { Content = "content" })
            {
                Parent = "parent",
                Routing = "routing",
                TimeStamp = utcNow,
                TimeToLive = ttl,
                Version = 1,
                RetriesOnConflict = 12
            };

            string json = bulkAction.ToString();

            string expectedJson = "{\"update\":{\"_index\":\"index\",\"_type\":\"type\",\"_id\":\"1\",\"_version\":1,\"_routing\":\"routing\",\"_parent\":\"parent\",\"_timestamp\":\"" +
                utcNow.ToString(Bolay.Elastic.Time.DateTimeFormatEnum.Date.Format) +
                "\",\"_ttl\":\"1d\",\"_retry_on_conflict\":12}}\r\n{\"doc\":{\"Content\":\"content\"}}\r\n";

            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void UpdateBulkActionSerialization_Doc_UpsertDoc()
        {
            TimeValue ttl = new TimeValue("1d");
            DateTime utcNow = DateTime.UtcNow;

            UpdateBulkAction<TestClass> bulkAction = new UpdateBulkAction<TestClass>("index", "type", "1", new TestClass() { Content = "content" }, new TestClass() { Content = "upsert" })
            {
                Parent = "parent",
                Routing = "routing",
                TimeStamp = utcNow,
                TimeToLive = ttl,
                Version = 1,
                RetriesOnConflict = 12
            };

            string json = bulkAction.ToString();

            string expectedJson = "{\"update\":{\"_index\":\"index\",\"_type\":\"type\",\"_id\":\"1\",\"_version\":1,\"_routing\":\"routing\",\"_parent\":\"parent\",\"_timestamp\":\"" +
                utcNow.ToString(Bolay.Elastic.Time.DateTimeFormatEnum.Date.Format) +
                "\",\"_ttl\":\"1d\",\"_retry_on_conflict\":12}}\r\n{\"doc\":{\"Content\":\"content\"},\"upsert\":{\"Content\":\"upsert\"}}\r\n";

            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void UpdateBulkActionSerialization_Doc_DocAsUpsert()
        {
            TimeValue ttl = new TimeValue("1d");
            DateTime utcNow = DateTime.UtcNow;

            UpdateBulkAction<TestClass> bulkAction = new UpdateBulkAction<TestClass>("index", "type", "1", new TestClass() { Content = "content" }, true)
            {
                Parent = "parent",
                Routing = "routing",
                TimeStamp = utcNow,
                TimeToLive = ttl,
                Version = 1,
                RetriesOnConflict = 12,
                
            };

            string json = bulkAction.ToString();

            string expectedJson = "{\"update\":{\"_index\":\"index\",\"_type\":\"type\",\"_id\":\"1\",\"_version\":1,\"_routing\":\"routing\",\"_parent\":\"parent\",\"_timestamp\":\"" +
                utcNow.ToString(Bolay.Elastic.Time.DateTimeFormatEnum.Date.Format) +
                "\",\"_ttl\":\"1d\",\"_retry_on_conflict\":12}}\r\n{\"doc\":{\"Content\":\"content\"},\"doc_as_upsert\":true}\r\n";

            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void UpdateBulkActionSerialization_Script()
        {
            TimeValue ttl = new TimeValue("1d");
            DateTime utcNow = DateTime.UtcNow;

            Script script = new Script("scriptText") 
            { 
                Language = "js", 
                Parameters = new List<ScriptParameter>() 
                {  
                    new ScriptParameter("name", "value")
                }
            };

            UpdateBulkAction<TestClass> bulkAction = new UpdateBulkAction<TestClass>("index", "type", "1", script)
            {
                Parent = "parent",
                Routing = "routing",
                TimeStamp = utcNow,
                TimeToLive = ttl,
                Version = 1,
                RetriesOnConflict = 12,
            };

            string json = bulkAction.ToString();

            string expectedJson = "{\"update\":{\"_index\":\"index\",\"_type\":\"type\",\"_id\":\"1\",\"_version\":1,\"_routing\":\"routing\",\"_parent\":\"parent\",\"_timestamp\":\"" +
                utcNow.ToString(Bolay.Elastic.Time.DateTimeFormatEnum.Date.Format) +
                "\",\"_ttl\":\"1d\",\"_retry_on_conflict\":12}}\r\n{\"lang\":\"js\",\"script\":\"scriptText\",\"params\":{\"name\":\"value\"}}\r\n";

            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void UpdateBulkActionSerialization_Script_UpsertDoc()
        {
            TimeValue ttl = new TimeValue("1d");
            DateTime utcNow = DateTime.UtcNow;

            Script script = new Script("scriptText")
            {
                Language = "js",
                Parameters = new List<ScriptParameter>() 
                {  
                    new ScriptParameter("name", "value")
                }
            };

            UpdateBulkAction<TestClass> bulkAction = new UpdateBulkAction<TestClass>("index", "type", "1", script, new TestClass() { Content = "content" })
            {
                Parent = "parent",
                Routing = "routing",
                TimeStamp = utcNow,
                TimeToLive = ttl,
                Version = 1,
                RetriesOnConflict = 12,
            };

            string json = bulkAction.ToString();

            string expectedJson = "{\"update\":{\"_index\":\"index\",\"_type\":\"type\",\"_id\":\"1\",\"_version\":1,\"_routing\":\"routing\",\"_parent\":\"parent\",\"_timestamp\":\"" +
                utcNow.ToString(Bolay.Elastic.Time.DateTimeFormatEnum.Date.Format) +
                "\",\"_ttl\":\"1d\",\"_retry_on_conflict\":12}}\r\n{\"lang\":\"js\",\"script\":\"scriptText\",\"params\":{\"name\":\"value\"},\"upsert\":{\"Content\":\"content\"}}\r\n";

            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void CreateBulkActionSerialization()
        {
            TimeValue ttl = new TimeValue("1d");
            DateTime utcNow = DateTime.UtcNow;

            CreateBulkAction<TestClass> bulkAction = new CreateBulkAction<TestClass>("index", "type", "1", new TestClass() { Content = "content" })
            {
                Parent = "parent",
                Routing = "routing",
                TimeStamp = utcNow,
                TimeToLive = ttl,
                Version = 1
            };

            string json = bulkAction.ToString();

            string expectedJson = "{\"create\":{\"_index\":\"index\",\"_type\":\"type\",\"_id\":\"1\",\"_version\":1,\"_routing\":\"routing\",\"_parent\":\"parent\",\"_timestamp\":\"" +
                utcNow.ToString(Bolay.Elastic.Time.DateTimeFormatEnum.Date.Format) +
                "\",\"_ttl\":\"1d\"}}\r\n{\"Content\":\"content\"}\r\n";

            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void DeleteBulkActionSerialization()
        {
            DeleteBulkAction bulkAction = new DeleteBulkAction("index", "type", "1")
            {
                Parent = "parent",
                Routing = "routing",
                Version = 1
            };

            string json = bulkAction.ToString();

            string expectedJson = "{\"delete\":{\"_index\":\"index\",\"_type\":\"type\",\"_id\":\"1\",\"_version\":1,\"_routing\":\"routing\",\"_parent\":\"parent\"}}\r\n";

            Assert.AreEqual(expectedJson, json);
        }
    }

    public class TestClass
    {
        public string Content { get; set; }
    }
}
