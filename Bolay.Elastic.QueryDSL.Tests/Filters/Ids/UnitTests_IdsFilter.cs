using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Filters.Ids;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Filters.Ids
{
    [TestClass]
    public class UnitTests_IdsFilter
    {
        [TestMethod]
        public void PASS_CreateFilter()
        {
            IdsFilter filter = new IdsFilter(new List<string>() { "1", "2", "3" });
            Assert.IsNotNull(filter);
            Assert.AreEqual(3, filter.DocumentIds.Count());
        }

        [TestMethod]
        public void FAIL_CreateFilter()
        {
            try
            {
                IdsFilter filter = new IdsFilter(null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("documentIds", ex.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            IdsFilter filter = new IdsFilter(new List<string>() { "1", "2", "3" });
            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);
            string expectedJson = "{\"ids\":{\"values\":[\"1\",\"2\",\"3\"]}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"ids\":{\"values\":[\"1\",\"2\",\"3\"]}}";
            IdsFilter filter = JsonConvert.DeserializeObject<IdsFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual(3, filter.DocumentIds.Count());
        }
    }
}
