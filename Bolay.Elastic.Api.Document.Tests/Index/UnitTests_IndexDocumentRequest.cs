using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Api.Document.Index;
using Bolay.Elastic.Api.Document.Models;

namespace Bolay.Elastic.Api.Document.Tests.Index
{
    [TestClass]
    public class UnitTests_IndexDocumentRequest
    {
        private ElasticUriProvider _ClusterUri = new ElasticUriProvider("http://10.137.8.224:9200");
        private string _Index = "testindex";
        private string _Type = "tweet";
        private string _Id = "1234";
        private Tweet _Document = new Tweet()
        {
            Author = "indextester",
            Text = "index test tweet"
        };

        [TestMethod]
        public void PASS_CreateRequest()
        {
            IndexDocumentRequest<Tweet> request = new IndexDocumentRequest<Tweet>(_Index, _Type, _Document);
            Assert.IsNotNull(request);
        }

        [TestMethod]
        public void PASS_CreateRequest_Id()
        {
            IndexDocumentRequest<Tweet> request = new IndexDocumentRequest<Tweet>(_Index, _Type, _Document, _Id);
            Assert.IsNotNull(request);
        }

        [TestMethod]
        public void FAIL_CreateRequest_Index()
        {
            try
            {
                IndexDocumentRequest<Tweet> request = new IndexDocumentRequest<Tweet>(null, _Type, _Document);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("index", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateRequest_Type()
        {
            try
            {
                IndexDocumentRequest<Tweet> request = new IndexDocumentRequest<Tweet>(_Index, null, _Document);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("documentType", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateRequest_Document()
        {
            try
            {
                IndexDocumentRequest<Tweet> request = new IndexDocumentRequest<Tweet>(_Index, _Type, null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("document", ex.ParamName);
            }
        }

        [TestMethod]
        public void PASS_BuildUri()
        {
            IndexDocumentRequest<Tweet> request = new IndexDocumentRequest<Tweet>(_Index, _Type, _Document);
            Uri uri = request.BuildUri(_ClusterUri);
            Assert.IsNotNull(uri);

            string expectedUri = string.Format("{0}{1}/{2}/", _ClusterUri.ClusterUri, _Index, _Type);
            Assert.AreEqual(expectedUri, uri.ToString());
        }

        [TestMethod]
        public void PASS_BuildUri_Id()
        {
            IndexDocumentRequest<Tweet> request = new IndexDocumentRequest<Tweet>(_Index, _Type, _Document, _Id);
            Uri uri = request.BuildUri(_ClusterUri);
            Assert.IsNotNull(uri);

            string expectedUri = string.Format("{0}{1}/{2}/{3}", _ClusterUri.ClusterUri, _Index, _Type, _Id);
            Assert.AreEqual(expectedUri, uri.ToString());
        }

        [TestMethod]
        public void PASS_BuildQueryString()
        {
            DateTime utcNow = DateTime.UtcNow;
            TimeSpan ttl = new TimeSpan(24, 0, 0);
            TimeSpan timeOut = new TimeSpan(0, 0, 10);

            IndexDocumentRequest<Tweet> request = new IndexDocumentRequest<Tweet>(_Index, _Type, _Document)
            {
                OperationTimeOut = timeOut,
                ParentId = "9999",
                Refresh = true,
                Routing = "route",
                TimeStamp = utcNow,
                TimeToLive = ttl,
                UseAsynchronousReplication = true,
                Version = 66,
                WriteConsistency = WriteConsistencyEnum.OneShard,
                UseCreateOperationType = true
            };

            string queryString = request.BuildQueryString();
            Assert.IsNotNull(queryString);

            string expectedQS = "?version=66&op_type=_create&parent=9999&timestamp=" + utcNow.ToString("yyyy-MM-ddTHH:mm:ss") +
                "&ttl=" + ttl.TotalMilliseconds.ToString() + "&routing=route&consistency=one&replication=async&refresh=true&timeout=" +
                timeOut.TotalMilliseconds.ToString();

            Assert.AreEqual(expectedQS, queryString);
        }
    }
}
