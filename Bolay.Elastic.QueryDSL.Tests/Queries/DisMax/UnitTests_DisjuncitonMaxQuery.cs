using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Queries.DisMax;
using Bolay.Elastic.QueryDSL.Queries.Term;
using System.Collections.Generic;
using Bolay.Elastic.QueryDSL.Queries;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Queries.DisMax
{
    [TestClass]
    public class UnitTests_DisjuncitonMaxQuery
    {
        [TestMethod]
        public void PASS_CreateQuery()
        {
            DisjunctionMaxQuery query = new DisjunctionMaxQuery(
                    new List<IQuery>()
                    {
                        new TermQuery("field", "value1"),
                        new TermQuery("field", "value2")
                    }
                );

            Assert.IsNotNull(query);
            Assert.AreEqual(2, query.Queries.Count());
        }

        [TestMethod]
        public void FAIL_CreateQuery_Queries()
        {
            try
            {
                DisjunctionMaxQuery query = new DisjunctionMaxQuery(
                    new List<IQuery>()
                    {
                    }
                );

                Assert.Fail();
            }
            catch (ArgumentNullException argEx)
            {
                Assert.AreEqual("queries", argEx.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            DisjunctionMaxQuery query = new DisjunctionMaxQuery(
                    new List<IQuery>()
                    {
                        new TermQuery("field", "value1"),
                        new TermQuery("field", "value2")
                    }
                );

            string json = JsonConvert.SerializeObject(query);
            Assert.IsNotNull(json);

            string expectedJson = "{\"dis_max\":{\"queries\":[{\"term\":{\"field\":\"value1\"}},{\"term\":{\"field\":\"value2\"}}]}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"dis_max\":{\"queries\":[{\"term\":{\"field\":\"value1\"}},{\"term\":{\"field\":\"value2\"}}]}}";

            DisjunctionMaxQuery query = JsonConvert.DeserializeObject<DisjunctionMaxQuery>(json);

            Assert.IsNotNull(query);
            Assert.AreEqual(2, query.Queries.Count());
            Assert.AreEqual(1.0, query.Boost);
            Assert.AreEqual(0.0, query.TieBreaker);
        }
    }
}
