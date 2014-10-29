using Bolay.Elastic.Api.Mapping.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bolay.Elastic.Api.Mapping.Tests
{
    [TestClass]
    public class UnitTest_SerializeMapping
    {
        [TestMethod]
        public void PASS_SerializeAlias()
        {
            //string jsonValue = EmbeddedResource.GetContentOfEmbeddedResourceFile("Bolay.Elastic.Api.Mapping.Tests.MappingExamples.ElasticMappingByAlias.txt");

            //Alias alias = JsonConvert.DeserializeObject<Alias>(jsonValue);

            //string newJson = JsonConvert.SerializeObject(alias);
            //Assert.IsNotNull(newJson);
        }

        [TestMethod]
        public void PASS_SerializeAliasType()
        {
            //string jsonValue = EmbeddedResource.GetContentOfEmbeddedResourceFile("Bolay.Elastic.Api.Mapping.Tests.MappingExamples.ElasticMappingByAliasType.txt");

            //Dictionary<string, object> indexTypeDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonValue);

            //IndexType indexType = JsonConvert.DeserializeObject<IndexType>(indexTypeDict.First().Value.ToString());
            //indexType.Name = indexTypeDict.First().Key;

            //string newJson = JsonConvert.SerializeObject(indexType);
            //Assert.IsNotNull(newJson);
        }
    }
}
