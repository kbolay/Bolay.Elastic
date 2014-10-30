using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Bolay.Elastic.Interfaces;
using Bolay.Elastic.Api.Bulk.Response;
using Newtonsoft.Json;
using Bolay.Elastic.Api.Bulk.Request;
using System.Collections.Generic;

namespace Bolay.Elastic.Api.Bulk.Tests
{
    [TestClass]
    public class BulkRepositoryTests
    {
        private Mock<IHttpLayer> _mockedHttpLayer;
        private Mock<IUriProvider> _mockedUriProvider;

        [TestInitialize]
        public void Init()
        {
            _mockedHttpLayer = new Mock<IHttpLayer>();
            _mockedUriProvider = new Mock<IUriProvider>();
            _mockedUriProvider.Setup(x => x.Uri).Returns(new Uri("http://localhost:9200"));
        }

        [TestMethod]
        public void IndexBulkRequest()
        {
            BulkRequest bulkRequest = new BulkRequest(new List<BulkActionBase>()
                {
                    new IndexBulkAction<TestModel>("test", "testtype", new TestModel(){ Content = "first doc" })
                });

            Uri bulkUri = new Uri(_mockedUriProvider.Object.Uri, "_bulk");

            BulkResponse mockedBulkResponse = new BulkResponse()
            {
                HadErrors = false
            };

            _mockedHttpLayer.Setup(x => x.Post(It.Is<HttpRequest>(y => y.Uri.ToString() == bulkUri.ToString() && y.Content.ToString() == bulkRequest.ToString())))
                .Returns(new HttpResponse(System.Net.HttpStatusCode.OK, JsonConvert.SerializeObject(mockedBulkResponse)));

            BulkRepository bulkRepo = new BulkRepository(_mockedUriProvider.Object, _mockedHttpLayer.Object);

            BulkResponse bulkResponse = bulkRepo.DoBulkRequest(bulkRequest);

            Assert.IsNotNull(bulkResponse);
            Assert.IsFalse(bulkResponse.HadErrors);
        }
    }

    public class TestModel
    {
        public string Content { get; set; }
    }
}
