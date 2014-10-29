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

        [TestMethod]
        public void PASS_BuildUriPath()
        {
            GetDocumentRequest request = new GetDocumentRequest(_Index, _Type, _Id);
            Uri uri = request.BuildUri(new ElasticUriProvider("http://10.137.8.224:9200/dumb/values"));
            Assert.IsNotNull(uri);
            Assert.AreEqual(new Uri("http://10.137.8.224:9200/" + _Index + "/" + _Type + "/" + _Id), uri);
        }

        [TestMethod]
        public void PASS_BuildUriPath_ExcludeMetadata()
        {
            GetDocumentRequest request = new GetDocumentRequest(_Index, _Type, _Id) { ExcludeMetaData = true };
            Uri uri = request.BuildUri(new ElasticUriProvider("http://10.137.8.224:9200/dumb/values"));
            Assert.IsNotNull(uri);
            Assert.AreEqual(new Uri("http://10.137.8.224:9200/" + _Index + "/" + _Type + "/" + _Id + "/" + "_source"), uri);
        }

        [TestMethod]
        public void PASS_BuildQueryString()
        {
            GetDocumentRequest request = new GetDocumentRequest(_Index, _Type, _Id)
            {
                DisableRealTime = true,
                Fields = new List<string>() { "id", "name" },
                RefreshBeforeSearch = true,
                Routing = "route",
                ShardPreference = new LocalShardPreference()
            };

            string queryString = request.BuildQueryString();
            Assert.IsNotNull(queryString);

            string expectedQS = "?realtime=false&fields=id,name&routing=route&preference=_local&refresh=true";
            Assert.AreEqual(expectedQS, queryString);          
        }

        [TestMethod]
        public void PASS_BuildQueryString_PrimaryShards()
        {
            GetDocumentRequest request = new GetDocumentRequest(_Index, _Type, _Id)
            {
                ShardPreference = new PrimaryShardPreference()
            };

            string queryString = request.BuildQueryString();
            Assert.IsNotNull(queryString);

            string expectedQS = "?preference=_primary";
            Assert.AreEqual(expectedQS, queryString);
        }

        [TestMethod]
        public void PASS_BuildQueryString_Routing_CustomShards()
        {
            GetDocumentRequest request = new GetDocumentRequest(_Index, _Type, _Id)
            {
                Routing = "route",
                ShardPreference = new CustomShardPreference("foo,bar")
            };

            string queryString = request.BuildQueryString();
            Assert.IsNotNull(queryString);

            string expectedQS = "?routing=route&preference=foo,bar";
            Assert.AreEqual(expectedQS, queryString);
        }

        [TestMethod]
        public void PASS_BuildUriWithQueryString()
        {
            GetDocumentRequest request = new GetDocumentRequest(_Index, _Type, _Id)
            {
                DisableRealTime = true,
                Fields = new List<string>() { "id", "name" },
                RefreshBeforeSearch = true,
                Routing = "route",
                ShardPreference = new LocalShardPreference(),
                ExcludeMetaData = true
            };

            ElasticUriProvider uri = new ElasticUriProvider("http://esdomain:9200/dumb/values");

            Uri fullUri = request.BuildUri(uri);
            Assert.IsNotNull(fullUri);

            string expectedQS = "http://esdomain:9200/testindex/tweet/1234/_source?realtime=false&fields=id,name&routing=route&preference=_local&refresh=true";
            Assert.AreEqual(expectedQS, fullUri.ToString()); 
        }
    }
}
