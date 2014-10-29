using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Filters.Nested;
using Bolay.Elastic.QueryDSL.Filters.Term;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Filters.Nested
{
    [TestClass]
    public class UnitTests_NestedFilter
    {
        [TestMethod]
        public void PASS_CreateQuery()
        {
            NestedFilter query = new NestedFilter("obj1", new TermFilter("field", "value"));
            Assert.IsNotNull(query);
            Assert.AreEqual("obj1", query.Path);
            Assert.IsTrue(query.Filter is TermFilter);
            TermFilter TermFilter = query.Filter as TermFilter;
            Assert.AreEqual("field", TermFilter.Field);
            Assert.AreEqual("value", TermFilter.Value);
        }

        [TestMethod]
        public void FAIL_CreateQuery_Path()
        {
            try
            {
                NestedFilter query = new NestedFilter(null, new TermFilter("field", "value"));
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("path", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateQuery_Query()
        {
            try
            {
                NestedFilter query = new NestedFilter("obj1", null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("filter", ex.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            NestedFilter query = new NestedFilter("obj1", new TermFilter("field", "value"));
            string json = JsonConvert.SerializeObject(query);

            Assert.IsNotNull(json);
            string expectedJson = "{\"nested\":{\"path\":\"obj1\",\"filter\":{\"term\":{\"field\":\"value\"}}}}";

            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "{\"nested\":{\"path\":\"obj1\",\"filter\":{\"term\":{\"field\":\"value\"}}}}";
            NestedFilter query = JsonConvert.DeserializeObject<NestedFilter>(json);
            Assert.IsNotNull(query);
            Assert.AreEqual("obj1", query.Path);
            Assert.IsTrue(query.Filter is TermFilter);
            TermFilter TermFilter = query.Filter as TermFilter;
            Assert.AreEqual("field", TermFilter.Field);
            Assert.AreEqual("value", TermFilter.Value);
        }
    }
}
