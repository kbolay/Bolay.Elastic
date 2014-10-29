using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Api.Document.Delete;
using Bolay.Elastic.Api.Document.Get;
using Bolay.Elastic.Api.Exceptions;
using System.Collections.Generic;
using Bolay.Elastic.Api.Document.Models;
using Bolay.Elastic.Api.Document.Index;
using Bolay.Elastic.Api.ShardPreference;

namespace Bolay.Elastic.Api.Document.Tests.Get
{
    [TestClass]
    public class IntegrationTests_GetDocuments
    {
        private ElasticUriProvider clusterUri = new ElasticUriProvider("http://10.137.8.224:9200");
        private DocumentRepository<Tweet> tweetRepo;

        private string _Index = "tweetindex";
        private string _DocumentType = "tweet";
        private string _Id = "1";

        [TestInitialize]
        public void Init()
        {
            tweetRepo = new DocumentRepository<Tweet>(clusterUri, new HttpRequestUtility());
            tweetRepo.Index(
                new IndexDocumentRequest<Tweet>(
                    _Index,
                    _DocumentType,
                    new Tweet() { Author = "tester", Text = "my test tweet" },
                    _Id));

        }

        [TestCleanup]
        public void CleanUp()
        {            
            DeleteResponse response =  tweetRepo.Delete(new DeleteDocumentRequest(_Index, _DocumentType, _Id));
        }

        [TestMethod]
        public void PASS_GetDocument()
        {
            GetDocumentRequest request = new GetDocumentRequest(_Index, _DocumentType, _Id);
            GetResponse<Tweet> response = tweetRepo.Get(request);
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Document);
        }

        [TestMethod]
        public void PASS_GetDocument_ExcludeMetaData()
        {
            GetDocumentRequest request = new GetDocumentRequest(_Index, _DocumentType, _Id)
            {
                ExcludeMetaData = true
            };
            GetResponse<Tweet> response = tweetRepo.Get(request);
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Document);
        }

        [TestMethod]
        public void PASS_GetDocument_ClusterNotFound()
        {
            tweetRepo = new DocumentRepository<Tweet>(new ElasticUriProvider("http://notelastic:9200/"), new HttpRequestUtility());
            GetDocumentRequest request = new GetDocumentRequest(_Index, _DocumentType, _Id);
            try
            {
                GetResponse<Tweet> response = tweetRepo.Get(request);
                Assert.Fail();
            }
            catch (ElasticRequestException ex)
            {
                Assert.AreEqual(500, (int)ex.Response.StatusCode);
            }
            catch (Exception)
            {
                Assert.Fail();
            }

            tweetRepo = new DocumentRepository<Tweet>(clusterUri, new HttpRequestUtility());
        }

        [TestMethod]
        public void FAIL_GetDocument_IndexNotFound()
        {
            GetDocumentRequest request = new GetDocumentRequest("notanindex", _DocumentType, _Id);
            try
            {
                GetResponse<Tweet> response = tweetRepo.Get(request);
            }
            catch (IndexMissingException ex)
            {
                Assert.AreEqual(404, (int)ex.Response.StatusCode);
            }
        }

        [TestMethod]
        public void FAIL_GetDocument_DocumentTypeNotFound()
        {
            GetDocumentRequest request = new GetDocumentRequest(_Index, "notatype", _Id);
            GetResponse<Tweet> response = tweetRepo.Get(request);
            Assert.IsNotNull(response);
            Assert.AreEqual(false, response.Found);
        }

        [TestMethod]
        public void FAIL_GetDocument_DocumentNotFound()
        {
            GetDocumentRequest request = new GetDocumentRequest(_Index, _DocumentType, "151515");
            GetResponse<Tweet> response = tweetRepo.Get(request);
            Assert.IsNotNull(response);
            Assert.AreEqual(false, response.Found);
        }

        [TestMethod]
        public void PASS_GetDocument_Routing()
        {
            GetDocumentRequest request = new GetDocumentRequest(_Index, _DocumentType, _Id)
            {
                Routing = "route"
            };
            GetResponse<Tweet> response = tweetRepo.Get(request);
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Document);
        }

        [TestMethod]
        public void PASS_GetDocument_DisableRealTime()
        {
            GetDocumentRequest request = new GetDocumentRequest(_Index, _DocumentType, _Id)
            {
                DisableRealTime = true
            };
            GetResponse<Tweet> response = tweetRepo.Get(request);
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Document);
        }

        [TestMethod]
        public void PASS_GetDocument_Fields()
        {
            GetDocumentRequest request = new GetDocumentRequest(_Index, _DocumentType, _Id)
            {
                Fields = new List<string> { "Text" }
            };
            GetResponse<Tweet> response = tweetRepo.Get(request);
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Document);
            Assert.IsNull(response.Document.Author);
            Assert.IsNotNull(response.Document.Text);
        }

        [TestMethod]
        public void PASS_GetDocument_Refresh()
        {
            GetDocumentRequest request = new GetDocumentRequest(_Index, _DocumentType, _Id)
            {
                RefreshBeforeSearch = true
            };
            GetResponse<Tweet> response = tweetRepo.Get(request);
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Document);
        }

        [TestMethod]
        public void PASS_GetDocument_LocalShards()
        {
            GetDocumentRequest request = new GetDocumentRequest(_Index, _DocumentType, _Id)
            {
                ShardPreference = new LocalShardPreference()
            };
            GetResponse<Tweet> response = tweetRepo.Get(request);
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Document);
        }

        [TestMethod]
        public void PASS_GetDocument_PrimaryShards()
        {
            GetDocumentRequest request = new GetDocumentRequest(_Index, _DocumentType, _Id)
            {
                ShardPreference = new PrimaryShardPreference()
            };
            GetResponse<Tweet> response = tweetRepo.Get(request);
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Document);
        }

        [TestMethod]
        public void PASS_GetDocument_CustomShards()
        {
            GetDocumentRequest request = new GetDocumentRequest(_Index, _DocumentType, _Id)
            {
                ShardPreference = new CustomShardPreference("custom")
            };
            GetResponse<Tweet> response = tweetRepo.Get(request);
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Document);
        }
    }
}
