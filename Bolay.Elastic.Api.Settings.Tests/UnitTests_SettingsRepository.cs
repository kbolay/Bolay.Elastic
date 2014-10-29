using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Bolay.Elastic.Api.Settings.Tests
{
    [TestClass]
    public class UnitTests_SettingsRepository
    {
        private Mock<IHttpRequestUtility> _MockedHttpRequestUtility { get; set; }

        [TestInitialize]
        public void Init()
        {
            _MockedHttpRequestUtility = new Mock<IHttpRequestUtility>();
        }

        [TestMethod]
        public void PASS_Get()
        {
            string responseBody = EmbeddedResource.GetContentOfEmbeddedResourceFile(typeof(UnitTest_Deserialize).Assembly, "Bolay.Elastic.Api.Settings.Tests.Resources.IndexSettings.json");

            // set up mock http request utiltiy
            _MockedHttpRequestUtility
                .Setup(x => x.Get(It.IsAny<HttpRequest>()))
                .Returns(new HttpResponse(HttpStatusCode.OK, responseBody));

            // set up the repository with the mocked http request utility
            SettingsRepository settingsRepo = new SettingsRepository(
                    new ElasticUriProvider("http://es:9200/"),
                    _MockedHttpRequestUtility.Object);

            IEnumerable<Settings.Models.Settings> indexSettingsCollection = settingsRepo.Get();

            Assert.IsNotNull(indexSettingsCollection);
            Assert.IsTrue(indexSettingsCollection.Any());
        }

        [TestMethod]
        public void PASS_GetByAlias()
        {
            string responseBody = EmbeddedResource.GetContentOfEmbeddedResourceFile(typeof(UnitTest_Deserialize).Assembly, "Bolay.Elastic.Api.Settings.Tests.Resources.IndexSettings.json");

            // set up mock http request utiltiy
            _MockedHttpRequestUtility
                .Setup(x => x.Get(It.IsAny<HttpRequest>()))
                .Returns(new HttpResponse(HttpStatusCode.OK, responseBody));

            // set up the repository with the mocked http request utility
            SettingsRepository settingsRepo = new SettingsRepository(
                    new ElasticUriProvider("http://es:9200/"),
                    _MockedHttpRequestUtility.Object);

            IEnumerable<Settings.Models.Settings> indexSettingsCollection = settingsRepo.GetByAlias("alias");

            Assert.IsNotNull(indexSettingsCollection);
            Assert.IsTrue(indexSettingsCollection.Any());
        }

        [TestMethod]
        public void FAIL_GetByAlias_NoAlias()
        {
            // set up the repository with the mocked http request utility
            SettingsRepository settingsRepo = new SettingsRepository(
                    new ElasticUriProvider("http://es:9200/"),
                    _MockedHttpRequestUtility.Object);

            try
            {
                settingsRepo.GetByAlias(null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("alias", ex.ParamName);
            }
        }

        [TestMethod]
        public void PASS_GetByIndex()
        {
            string responseBody = EmbeddedResource.GetContentOfEmbeddedResourceFile(typeof(UnitTest_Deserialize).Assembly, "Bolay.Elastic.Api.Settings.Tests.Resources.IndexSettings.json");

            // set up mock http request utiltiy
            _MockedHttpRequestUtility
                .Setup(x => x.Get(It.IsAny<HttpRequest>()))
                .Returns(new HttpResponse(HttpStatusCode.OK, responseBody));

            // set up the repository with the mocked http request utility
            SettingsRepository settingsRepo = new SettingsRepository(
                    new ElasticUriProvider("http://es:9200/"),
                    _MockedHttpRequestUtility.Object);

            Settings.Models.Settings indexSettings = settingsRepo.GetByIndex("indexName");

            Assert.IsNotNull(indexSettings);
            Assert.IsNotNull(indexSettings.IndexName);
            Assert.IsNotNull(indexSettings.Analysis);
        }

        [TestMethod]
        public void FAIL_GetByIndex_NoIndex()
        {
            // set up the repository with the mocked http request utility
            SettingsRepository settingsRepo = new SettingsRepository(
                    new ElasticUriProvider("http://es:9200/"),
                    _MockedHttpRequestUtility.Object);

            try
            {
                settingsRepo.GetByIndex(null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("indexName", ex.ParamName);
            }
        }
    }
}
