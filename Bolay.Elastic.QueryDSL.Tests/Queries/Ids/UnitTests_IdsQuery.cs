using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Queries.Ids;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Queries.Ids
{
    [TestClass]
    public class UnitTests_IdsQuery
    {
        [TestMethod]
        public void PASS_CreateQuery()
        {
            IdsQuery query = new IdsQuery(new List<string>(){ "1", "2", "3" });
            Assert.IsNotNull(query);
            Assert.AreEqual(3, query.DocumentIds.Count());
        }

        [TestMethod]
        public void FAIL_CreateQuery_Ids()
        {
            try
            {
                IdsQuery query = new IdsQuery(null);
                Assert.Fail();
            }
            catch (ArgumentNullException argEx)
            {
                Assert.AreEqual("documentIds", argEx.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            IdsQuery query = new IdsQuery(new List<string>() { "1", "2", "3" }) 
            { 
                DocumentTypes = new List<string>() 
                { 
                    "type" 
                } 
            };

            string result = JsonConvert.SerializeObject(query);
            Assert.IsNotNull(result);

            string expectedJson = "{\"ids\":{\"type\":\"type\",\"values\":[\"1\",\"2\",\"3\"]}}";
            Assert.AreEqual(expectedJson, result);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string termQuery = "{\"ids\":{\"type\":\"type\",\"values\":[\"1\",\"2\",\"3\"]}}";

            IdsQuery query = JsonConvert.DeserializeObject<IdsQuery>(termQuery);

            Assert.IsNotNull(query);
            Assert.AreEqual(3, query.DocumentIds.Count());
            Assert.AreEqual(1, query.DocumentTypes.Count());
            Assert.AreEqual("type", query.DocumentTypes.First());
        }
    }
}
