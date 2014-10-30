using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Api.Document.Delete;
using Bolay.Elastic.Api.Document.Models;
using Bolay.Elastic.Models;

namespace Bolay.Elastic.Api.Document.Tests.Delete
{
    [TestClass]
    public class UnitTests_DeleteDocumentRequest
    {
        private string _Index = "testindex";
        private string _Type = "tweet";
        private string _Id = "1234";

        [TestMethod]
        public void PASS_CreateRequest()
        {
            DeleteDocumentRequest request = new DeleteDocumentRequest(_Index, _Type, _Id);
            Assert.IsNotNull(request);
            Assert.AreEqual(_Index, request.Index);
            Assert.AreEqual(_Type, request.DocumentType);
            Assert.AreEqual(_Id, request.DocumentId);
        }

        [TestMethod]
        public void FAIL_CreateRequest_MissingIndex()
        {
            try
            {
                DeleteDocumentRequest request = new DeleteDocumentRequest(null, _Type, _Id);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("index", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateRequest_MissingDocumentType()
        {
            try
            {
                DeleteDocumentRequest request = new DeleteDocumentRequest(_Index, null, _Id);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("documentType", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateRequest_MissingDocumentId()
        {
            try
            {
                DeleteDocumentRequest request = new DeleteDocumentRequest(_Index, _Type, null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("documentId", ex.ParamName);
            }
        }

        [TestMethod]
        public void PASS_BuildUriPath()
        {
            DeleteDocumentRequest request = new DeleteDocumentRequest(_Index, _Type, _Id);
            Uri uri = request.BuildUri(new ElasticUriProvider("http://localhost:9200/dumb/values"));
            Assert.IsNotNull(uri);
            Assert.AreEqual(new Uri("http://localhost:9200/" + _Index + "/" + _Type + "/" + _Id), uri);
        }

        [TestMethod]
        public void PASS_BuildQueryString()
        {
            DeleteDocumentRequest request = new DeleteDocumentRequest(_Index, _Type, _Id)
            {
                OperationTimeOut = new TimeSpan(0, 0, 1),
                ParentId = "1",
                Refresh = true,
                Routing = "route",
                UseAsynchronousReplication = true,
                Version = 1,
                WriteConsistency = WriteConsistencyEnum.OneShard
            };

            string queryString = request.BuildQueryString();
            Assert.IsNotNull(queryString);

            string expectedQS = "?version=1&parent=1&routing=route&consistency=one&replication=async&refresh=true&timeout=1000";
            Assert.AreEqual(expectedQS, queryString);
        }
    }
}
