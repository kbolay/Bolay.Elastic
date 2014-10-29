using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using Bolay.Elastic.Api.Mapping;
using Bolay.Elastic.Api.Mapping.Models;
using Bolay.Elastic.Distance;
using Bolay.Elastic.Time;

namespace Kgb.Elastic.Api.Mapping.Tests
{
    [TestClass]
    public class UnitTest_DeserializeMapping
    {
        [TestInitialize]
        public void Init()
        {
            InitializeTypeSafeEnums();
            JsonConvert.DefaultSettings = () =>
            {
                return new JsonSerializerSettings()
                {
                    DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
                    NullValueHandling = NullValueHandling.Ignore
                };
            };
        }

        //[TestMethod]
        //public void PASS_DeserializeCluster()
        //{
        //    string jsonValue = EmbeddedResource.GetContentOfEmbeddedResourceFile("Bolay.Elastic.Api.Mapping.Tests.MappingExamples.ElasticMappingByCluster.txt");

        //    Cluster cluster = JsonConvert.DeserializeObject<Cluster>(jsonValue);

        //    Assert.IsNotNull(cluster);
        //    Assert.IsNotNull(cluster.Indexes);
        //    Assert.IsTrue(cluster.Indexes.Any());
        //}

        //[TestMethod]
        //public void PASS_DeserializeAlias()
        //{
        //    string jsonValue = EmbeddedResource.GetContentOfEmbeddedResourceFile("Bolay.Elastic.Api.Mapping.Tests.MappingExamples.ElasticMappingByAlias.txt");

        //    Alias alias = JsonConvert.DeserializeObject<Alias>(jsonValue);

        //    Assert.IsNotNull(alias);
        //    Assert.IsNotNull(alias.Indexes);
        //    Assert.IsTrue(alias.Indexes.Any());
        //}

        //[TestMethod]
        //public void PASS_DeserializeIndex()
        //{
        //    string jsonValue = EmbeddedResource.GetContentOfEmbeddedResourceFile("Bolay.Elastic.Api.Mapping.Tests.MappingExamples.ElasticMappingByIndex.txt");

        //    Index index = JsonConvert.DeserializeObject<Index>(jsonValue);

        //    Assert.IsNotNull(index);
        //    Assert.IsNotNull(index.Types);
        //    Assert.IsTrue(index.Types.Any());
        //}

        //[TestMethod]
        //public void PASS_DeserializeAliasType()
        //{
        //    string jsonValue = EmbeddedResource.GetContentOfEmbeddedResourceFile("Bolay.Elastic.Api.Mapping.Tests.MappingExamples.ElasticMappingByAliasType.txt");

        //    Dictionary<string, object> indexTypeDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonValue);

        //    IndexType indexType = JsonConvert.DeserializeObject<IndexType>(indexTypeDict.First().Value.ToString());
        //    indexType.Name = indexTypeDict.First().Key;

        //    Assert.IsNotNull(indexType);
        //    Assert.IsNotNull(indexType.Properties);
        //    Assert.IsTrue(indexType.Properties.Any());
        //}

        //[TestMethod]
        //public void PASS_DeserializeIndexType()
        //{
        //    string jsonValue = EmbeddedResource.GetContentOfEmbeddedResourceFile("Bolay.Elastic.Api.Mapping.Tests.MappingExamples.ElasticMappingByIndexType.txt");

        //    Dictionary<string, object> indexTypeDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonValue);

        //    IndexType indexType = JsonConvert.DeserializeObject<IndexType>(indexTypeDict.First().Value.ToString());
        //    indexType.Name = indexTypeDict.First().Key;
        //    Assert.IsNotNull(indexType);
        //    Assert.IsNotNull(indexType.Properties);
        //    Assert.IsTrue(indexType.Properties.Any());
        //}

        //[TestMethod]
        //public void PASS_SerializeAlias()
        //{
        //    string jsonValue = EmbeddedResource.GetContentOfEmbeddedResourceFile("Bolay.Elastic.Api.Mapping.Tests.MappingExamples.ElasticMappingByAlias.txt");  

        //    Alias alias = JsonConvert.DeserializeObject<Alias>(jsonValue);

        //    string newJson = JsonConvert.SerializeObject(alias);
        //    Assert.IsNotNull(newJson);
        //}

        //[TestMethod]
        //public void PASS_SerializeIndex()
        //{
        //    string jsonValue = EmbeddedResource.GetContentOfEmbeddedResourceFile("Bolay.Elastic.Api.Mapping.Tests.MappingExamples.ElasticMappingByIndex.txt");

        //    Index index = JsonConvert.DeserializeObject<Index>(jsonValue);

        //    string newJson = JsonConvert.SerializeObject(index);
        //    Assert.IsNotNull(newJson);
        //}

        private void InitializeTypeSafeEnums()
        {
            //var distance = DistanceUnitEnum.Centimeter;
            //var dynamic = DynamicSetting.Strict;
            //var indexoption = IndexOptionSetting.DocumentId;
            //var index = IndexSettingEnum.Analyzed;
            //var path = PathSetting.Full;
            //var posting = PostingSetting.BloomDefault;
            //var prefix = PrefixTree.GeoHash;
            //var property = PropertyType.Object;
            //var similarity = SimilarityAlgorithm.BM25;
            //var size = SizeUnit.Byte;
            //var store = StoreSettingEnum.No;
            //var term = TermVectorSetting.No;
            //var time = TimeUnit.Days;
        }
    }
}
