using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Bolay.Elastic.Api.Settings.Tests
{
    [TestClass]
    public class UnitTest_Deserialize
    {
        [TestMethod]
        public void PASS_Deserialize()
        {
            string jsonValue = EmbeddedResource.GetContentOfEmbeddedResourceFile(typeof(UnitTest_Deserialize).Assembly, "Bolay.Elastic.Api.Settings.Tests.Resources.IndexSettings.json");

            Settings.Models.Settings indexSettings = JsonConvert.DeserializeObject<Settings.Models.Settings>(jsonValue);

            Assert.IsNotNull(indexSettings);
        }
    }
}
