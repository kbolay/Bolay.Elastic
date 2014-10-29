using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Bolay.Elastic.QueryDSL.Filters.HasParent;
using Bolay.Elastic.QueryDSL.Queries.Term;
using Bolay.Elastic.QueryDSL.Filters;
using Bolay.Elastic.QueryDSL.Queries;
using Bolay.Elastic.QueryDSL.Filters.Term;

namespace Bolay.Elastic.QueryDSL.Tests.Filters.HasParent
{
    [TestClass]
    public class UnitTests_HasParentFilter
    {
        [TestMethod]
        public void PASS_CreateFilter_Filter()
        {
            HasParentFilter filter = new HasParentFilter("child", new TermFilter("field", "value"));
            Assert.IsNotNull("child", filter.ParentType);
            Assert.IsTrue(filter.Filter is TermFilter);
            TermFilter termFilter = filter.Filter as TermFilter;
            Assert.AreEqual("field", termFilter.Field);
            Assert.AreEqual("value", termFilter.Value);
        }

        [TestMethod]
        public void PASS_CreateFilter_Query()
        {
            HasParentFilter filter = new HasParentFilter("child", new TermQuery("field", "value"));
            Assert.IsNotNull("child", filter.ParentType);
            Assert.IsTrue(filter.Query is TermQuery);
            TermQuery termQuery = filter.Query as TermQuery;
            Assert.AreEqual("field", termQuery.Field);
            Assert.AreEqual("value", termQuery.Value);
        }

        [TestMethod]
        public void FAIL_CreateFilter_ParentType()
        {
            try
            {
                HasParentFilter filter = new HasParentFilter(null, (IFilter)null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("parentType", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateFilter_Filter()
        {
            try
            {
                HasParentFilter filter = new HasParentFilter("child", (IFilter)null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("filter", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateFilter_Query()
        {
            try
            {
                HasParentFilter filter = new HasParentFilter("child", (IQuery)null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("query", ex.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize_Filter()
        {
            HasParentFilter filter = new HasParentFilter("child", new TermFilter("field", "value"));
            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"has_parent\":{\"parent_type\":\"child\",\"filter\":{\"term\":{\"field\":\"value\"}}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_Filter()
        {
            string json = "{\"has_parent\":{\"parent_type\":\"child\",\"filter\":{\"term\":{\"field\":\"value\"}}}}";
            HasParentFilter filter = JsonConvert.DeserializeObject<HasParentFilter>(json);
            Assert.IsNotNull("child", filter.ParentType);
            Assert.IsTrue(filter.Filter is TermFilter);
            TermFilter termFilter = filter.Filter as TermFilter;
            Assert.AreEqual("field", termFilter.Field);
            Assert.AreEqual("value", termFilter.Value);
        }

        [TestMethod]
        public void PASS_Serialize_Query()
        {
            HasParentFilter filter = new HasParentFilter("child", new TermQuery("field", "value"));
            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"has_parent\":{\"parent_type\":\"child\",\"query\":{\"term\":{\"field\":\"value\"}}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_Query()
        {
            string json = "{\"has_parent\":{\"parent_type\":\"child\",\"query\":{\"term\":{\"field\":\"value\"}}}}";
            HasParentFilter filter = JsonConvert.DeserializeObject<HasParentFilter>(json);
            Assert.IsNotNull("child", filter.ParentType);
            Assert.IsTrue(filter.Query is TermQuery);
            TermQuery termQuery = filter.Query as TermQuery;
            Assert.AreEqual("field", termQuery.Field);
            Assert.AreEqual("value", termQuery.Value);
        }
    }
}
