using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Api.Document.MultiGet;
using System.Collections.Generic;

namespace Bolay.Elastic.Api.Document.Tests.MultiGet
{
    [TestClass]
    public class UnitTests_MultiGetRequest
    {
        private string _Index = "testindex";
        private string _Type = "tweet";
        private string _Id = "1234";

        [TestMethod]
        public void PASS_CreateRequest_Documents_IndexSpecified()
        {
            MultiGetRequestContent content = new MultiGetRequestContent(
                new List<MultiGetRequestedDocument>()
                {
                    new MultiGetRequestedDocument(_Index, _Type, _Id)
                });
            MultiGetDocumentRequest request = new MultiGetDocumentRequest(content);
        }
    }
}
