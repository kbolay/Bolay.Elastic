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
    }
}
