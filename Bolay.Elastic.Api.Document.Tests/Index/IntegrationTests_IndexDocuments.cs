using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Api.Document.Index;
using Bolay.Elastic.Api.Document.Models;
using Bolay.Elastic.Models;

namespace Bolay.Elastic.Api.Document.Tests.Index
{
    [TestClass]
    public class IntegrationTests_IndexDocuments
    {
        private ElasticUriProvider _ClusterUri = new ElasticUriProvider("http://localhost:9200");
        private DocumentRepository<Tweet> tweetRepo;

        private string _Index = "tweetindex";
        private string _DocumentType = "tweet";
        private string _Id = "15";

        private Tweet _Document = new Tweet()
        {
            Author = "indextester",
            Text = "index test tweet"
        };

        [TestMethod]
        public void PASS_IndexDocument()
        {
            tweetRepo = new DocumentRepository<Tweet>(_ClusterUri, new HttpLayer());
            IndexDocumentRequest<Tweet> request = new IndexDocumentRequest<Tweet>(_Index, _DocumentType, _Document);
            IndexResponse response = tweetRepo.Index(request);
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.DocumentId);
            Assert.AreEqual(1, response.Version.Value);
        }

        [TestMethod]
        public void PASS_IndexDocument_Id()
        {
            tweetRepo = new DocumentRepository<Tweet>(_ClusterUri, new HttpLayer());
            IndexDocumentRequest<Tweet> request = new IndexDocumentRequest<Tweet>(_Index, _DocumentType, _Document, _Id);
            IndexResponse response = tweetRepo.Index(request);
            Assert.IsNotNull(response);
            Assert.AreEqual(_Id, response.DocumentId);
        }

        [TestMethod]
        public void PASS_IndexDocument_TimeOut_1m()
        {
            tweetRepo = new DocumentRepository<Tweet>(_ClusterUri, new HttpLayer());
            IndexDocumentRequest<Tweet> request = new IndexDocumentRequest<Tweet>(_Index, _DocumentType, _Document)
            {
                OperationTimeOut = new TimeSpan(0, 1, 0)
            };
            IndexResponse response = tweetRepo.Index(request);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void FAIL_IndexDocument_TimeOut_1ms()
        {
            tweetRepo = new DocumentRepository<Tweet>(_ClusterUri, new HttpLayer());
            IndexDocumentRequest<Tweet> request = new IndexDocumentRequest<Tweet>(_Index, _DocumentType, _Document)
            {
                OperationTimeOut = new TimeSpan(0, 0, 0, 0, 1)
            };

            try
            {
                IndexResponse response = tweetRepo.Index(request);
            }
            catch (Exception ex)
            {
                //TODO: learn more about this response.
                Assert.Fail();
            }
        }

        [TestMethod]
        public void PASS_IndexDocument_Parent()
        {
            tweetRepo = new DocumentRepository<Tweet>(_ClusterUri, new HttpLayer());
            IndexDocumentRequest<Tweet> request = new IndexDocumentRequest<Tweet>(_Index, _DocumentType, _Document, _Id)
            {
                ParentId = "2525"
            };
            IndexResponse response = tweetRepo.Index(request);
            Assert.IsNotNull(response);
            Assert.AreEqual(_Id, response.DocumentId);
        }

        [TestMethod]
        public void PASS_IndexDocument_Refresh()
        {
            tweetRepo = new DocumentRepository<Tweet>(_ClusterUri, new HttpLayer());
            IndexDocumentRequest<Tweet> request = new IndexDocumentRequest<Tweet>(_Index, _DocumentType, _Document, _Id)
            {
                Refresh = true
            };
            IndexResponse response = tweetRepo.Index(request);
            Assert.IsNotNull(response);
            Assert.AreEqual(_Id, response.DocumentId);
        }

        [TestMethod]
        public void PASS_IndexDocument_Routing()
        {
            tweetRepo = new DocumentRepository<Tweet>(_ClusterUri, new HttpLayer());
            IndexDocumentRequest<Tweet> request = new IndexDocumentRequest<Tweet>(_Index, _DocumentType, _Document, _Id)
            {
                Routing = "route"
            };
            IndexResponse response = tweetRepo.Index(request);
            Assert.IsNotNull(response);
            Assert.AreEqual(_Id, response.DocumentId);
        }

        [TestMethod]
        public void PASS_IndexDocument_TimeStamp()
        {
            tweetRepo = new DocumentRepository<Tweet>(_ClusterUri, new HttpLayer());
            IndexDocumentRequest<Tweet> request = new IndexDocumentRequest<Tweet>(_Index, _DocumentType, _Document, _Id)
            {
                TimeStamp = DateTime.UtcNow
            };
            IndexResponse response = tweetRepo.Index(request);
            Assert.IsNotNull(response);
            Assert.AreEqual(_Id, response.DocumentId);
        }

        [TestMethod]
        public void PASS_IndexDocument_TimeToLive()
        {
            tweetRepo = new DocumentRepository<Tweet>(_ClusterUri, new HttpLayer());
            IndexDocumentRequest<Tweet> request = new IndexDocumentRequest<Tweet>(_Index, _DocumentType, _Document, _Id)
            {
                TimeToLive = new TimeSpan(0, 10, 0)
            };
            IndexResponse response = tweetRepo.Index(request);
            Assert.IsNotNull(response);
            Assert.AreEqual(_Id, response.DocumentId);
        }

        [TestMethod]
        public void PASS_IndexDocument_AsyncReplication()
        {
            tweetRepo = new DocumentRepository<Tweet>(_ClusterUri, new HttpLayer());
            IndexDocumentRequest<Tweet> request = new IndexDocumentRequest<Tweet>(_Index, _DocumentType, _Document, _Id)
            {
                UseAsynchronousReplication = true
            };
            IndexResponse response = tweetRepo.Index(request);
            Assert.IsNotNull(response);
            Assert.AreEqual(_Id, response.DocumentId);
        }

        [TestMethod]
        public void PASS_IndexDocument_CreateOnly()
        {
            tweetRepo = new DocumentRepository<Tweet>(_ClusterUri, new HttpLayer());
            IndexDocumentRequest<Tweet> request = new IndexDocumentRequest<Tweet>(_Index, _DocumentType, _Document)
            {
                UseCreateOperationType = true
            };
            IndexResponse response = tweetRepo.Index(request);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void FAIL_IndexDocument_CreateOnly()
        {
            tweetRepo = new DocumentRepository<Tweet>(_ClusterUri, new HttpLayer());
            IndexDocumentRequest<Tweet> request = new IndexDocumentRequest<Tweet>(_Index, _DocumentType, _Document, _Id)
            {
                UseCreateOperationType = true
            };
            try
            {
                IndexResponse response = tweetRepo.Index(request);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void PASS_IndexDocument_Version()
        {
            tweetRepo = new DocumentRepository<Tweet>(_ClusterUri, new HttpLayer());
            IndexDocumentRequest<Tweet> request = new IndexDocumentRequest<Tweet>(_Index, _DocumentType, _Document, _Id)
            {
                Version = 115
            };
            IndexResponse response = tweetRepo.Index(request);
            Assert.IsNotNull(response);
            Assert.AreEqual(_Id, response.DocumentId);
        }

        [TestMethod]
        public void PASS_IndexDocument_Consistency_One()
        {
            tweetRepo = new DocumentRepository<Tweet>(_ClusterUri, new HttpLayer());
            IndexDocumentRequest<Tweet> request = new IndexDocumentRequest<Tweet>(_Index, _DocumentType, _Document, _Id)
            {
                WriteConsistency = WriteConsistencyEnum.OneShard
            };
            IndexResponse response = tweetRepo.Index(request);
            Assert.IsNotNull(response);
            Assert.AreEqual(_Id, response.DocumentId);
        }

        [TestMethod]
        public void PASS_IndexDocument_Consistency_All()
        {
            tweetRepo = new DocumentRepository<Tweet>(_ClusterUri, new HttpLayer());
            IndexDocumentRequest<Tweet> request = new IndexDocumentRequest<Tweet>(_Index, _DocumentType, _Document, _Id)
            {
                WriteConsistency = WriteConsistencyEnum.AllShards
            };
            IndexResponse response = tweetRepo.Index(request);
            Assert.IsNotNull(response);
            Assert.AreEqual(_Id, response.DocumentId);
        }

        [TestMethod]
        public void PASS_IndexDocument_Consistency_Quorum()
        {
            tweetRepo = new DocumentRepository<Tweet>(_ClusterUri, new HttpLayer());
            IndexDocumentRequest<Tweet> request = new IndexDocumentRequest<Tweet>(_Index, _DocumentType, _Document, _Id)
            {
                WriteConsistency = WriteConsistencyEnum.QuorumOfShards
            };
            IndexResponse response = tweetRepo.Index(request);
            Assert.IsNotNull(response);
            Assert.AreEqual(_Id, response.DocumentId);
        }
    }
}
