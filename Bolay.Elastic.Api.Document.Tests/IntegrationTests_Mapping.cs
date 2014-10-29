using Bolay.Elastic.Api.Document;
using Bolay.Elastic.Api.Mapping;
using Bolay.Elastic.Api.Mapping.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bolay.Elastic.Repository.Tests
{
    [TestClass]
    public class IntegrationTests_Mapping
    {
        /// <summary>
        /// Emergency test made to expose what types are under a specific alias.
        /// </summary>
        [TestMethod]
        public void PASS_GetTypesFromIndex()
        {
            //DocumentRepository<object> repo = new DocumentRepository<object>(new UriProvider("http://internal-lb-esapi-369524352.eu-west-1.elb.amazonaws.com:9200/globalapi_else/"));
            //string mappingJson = repo.GetMapping();

            //Alias alias = JsonConvert.DeserializeObject<Alias>(mappingJson);
            //Assert.IsNotNull(alias);
            //Assert.IsNotNull(alias.Indexes);
            //List<IndexType> types = alias.Indexes.SelectMany(x => x.Types).ToList();
            //Assert.IsNotNull(types);
            //string typeString = string.Join(", ", types.Select(x => x.Name));
        }
    }
}