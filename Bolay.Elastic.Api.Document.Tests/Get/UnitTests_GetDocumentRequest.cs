using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Api.Document.Get;
using System.Collections.Generic;
using Bolay.Elastic.Api.Document.Models;
using Bolay.Elastic.Api.ShardPreference;

namespace Bolay.Elastic.Api.Document.Tests.Get
{
    [TestClass]
    public class UnitTests_GetDocumentRequest
    {
        private string _Index = "testindex";
        private string _Type = "tweet";
        private string _Id = "1234";

        [TestMethod]
        public void PASS_CreateRequest()
        {
            GetDocumentRequest request = new GetDocumentRequest(_Index, _Type, _Id);
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
                GetDocumentRequest request = new GetDocumentRequest(null, _Type, _Id);
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
                GetDocumentRequest request = new GetDocumentRequest(_Index, null, _Id);
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
                GetDocumentRequest request = new GetDocumentRequest(_Index, _Type, null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("documentId", ex.ParamName);
            }
        }
    }
}
