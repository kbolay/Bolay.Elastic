using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Bolay.Elastic.Api.Settings;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using Bolay.Elastic.Api.Mapping.Models;

namespace Bolay.Elastic.Api.Mapping.Tests
{
    [TestClass]
    public class UnitTests_MappingRepository
    {
        private Mock<IHttpLayer> _MockedhttpLayer { get; set; }
        private Mock<ISettingsRepository> _MockedSettingsRepository { get; set; }

        [TestInitialize]
        public void Init()
        {
            _MockedhttpLayer = new Mock<IHttpLayer>();
            _MockedSettingsRepository = new Mock<ISettingsRepository>();
        }

        [TestMethod]
        public void PASS_GetClusterMapping()
        {
            string clusterSettingsJson = EmbeddedResource.GetContentOfEmbeddedResourceFile(typeof(UnitTests_MappingRepository).Assembly, "Bolay.Elastic.Api.Mapping.Tests.MappingExamples.MultiIndexSettings.json");
            string clusterMappingJson = EmbeddedResource.GetContentOfEmbeddedResourceFile(typeof(UnitTests_MappingRepository).Assembly, "Bolay.Elastic.Api.Mapping.Tests.MappingExamples.ElasticMappingByCluster.txt");

            IEnumerable<Settings.Models.Settings> clusterSettings = JsonConvert.DeserializeObject<Settings.Models.SettingsCollection>(clusterSettingsJson);
            _MockedSettingsRepository.Setup(x => x.Get())
                                     .Returns(clusterSettings);

            _MockedhttpLayer.Setup(x => x.Get(It.IsAny<HttpRequest>()))
                                     .Returns(new HttpResponse(HttpStatusCode.OK, clusterMappingJson));

            MappingRepository mappingRepo = new MappingRepository(
                new ElasticUriProvider("http://es:9200/"),
                _MockedSettingsRepository.Object,
                _MockedhttpLayer.Object);

            IEnumerable<IndexMapping> clusterMappings = mappingRepo.GetClusterMapping();

            Assert.IsNotNull(clusterMappings);
            Assert.IsTrue(clusterMappings.Any());
        }
    }
}
