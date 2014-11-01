using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Api.Document.Index;
using Bolay.Elastic.Api.Document.Models;
using Bolay.Elastic.Models;

namespace Bolay.Elastic.Api.Document.Tests.Index
{
    [TestClass]
    public class UnitTests_IndexDocumentRequest
    {
        private ElasticUriProvider _ClusterUri = new ElasticUriProvider("http://localhost:9200");
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
    }
}
