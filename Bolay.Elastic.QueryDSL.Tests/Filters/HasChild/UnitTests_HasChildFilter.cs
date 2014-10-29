using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Filters.HasChild;
using Bolay.Elastic.QueryDSL.Filters.Term;
using Bolay.Elastic.QueryDSL.Queries.Term;
using Bolay.Elastic.QueryDSL.Filters;
using Bolay.Elastic.QueryDSL.Queries;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Filters.HasChild
{
    [TestClass]
    public class UnitTests_HasChildFilter
    {
        [TestMethod]
        public void PASS_CreateFilter_Filter()
        {
            HasChildFilter filter = new HasChildFilter("child", new TermFilter("field", "value"));
            Assert.IsNotNull("child", filter.ChildType);
            Assert.IsTrue(filter.Filter is TermFilter);
            TermFilter termFilter = filter.Filter as TermFilter;
            Assert.AreEqual("field", termFilter.Field);
            Assert.AreEqual("value", termFilter.Value);
        }

        [TestMethod]
        public void PASS_CreateFilter_Query()
        {
            HasChildFilter filter = new HasChildFilter("child", new TermQuery("field", "value"));
            Assert.IsNotNull("child", filter.ChildType);
            Assert.IsTrue(filter.Query is TermQuery);
            TermQuery termQuery = filter.Query as TermQuery;
            Assert.AreEqual("field", termQuery.Field);
            Assert.AreEqual("value", termQuery.Value);
        }

        [TestMethod]
        public void FAIL_CreateFilter_ChildType()
        {
            try
            {
                HasChildFilter filter = new HasChildFilter(null, (IFilter)null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("childType", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateFilter_Filter()
        {
            try
            {
                HasChildFilter filter = new HasChildFilter("child", (IFilter)null);
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
                HasChildFilter filter = new HasChildFilter("child", (IQuery)null);
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
            HasChildFilter filter = new HasChildFilter("child", new TermFilter("field", "value"));
            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"has_child\":{\"type\":\"child\",\"filter\":{\"term\":{\"field\":\"value\"}}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_Filter()
        {
            string json = "{\"has_child\":{\"type\":\"child\",\"filter\":{\"term\":{\"field\":\"value\"}}}}";
            HasChildFilter filter = JsonConvert.DeserializeObject<HasChildFilter>(json);
            Assert.IsNotNull("child", filter.ChildType);
            Assert.IsTrue(filter.Filter is TermFilter);
            TermFilter termFilter = filter.Filter as TermFilter;
            Assert.AreEqual("field", termFilter.Field);
            Assert.AreEqual("value", termFilter.Value);
        }

        [TestMethod]
        public void PASS_Serialize_Query()
        {
            HasChildFilter filter = new HasChildFilter("child", new TermQuery("field", "value"));
            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);

            string expectedJson = "{\"has_child\":{\"type\":\"child\",\"query\":{\"term\":{\"field\":\"value\"}}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_Query()
        {
            string json = "{\"has_child\":{\"type\":\"child\",\"query\":{\"term\":{\"field\":\"value\"}}}}";
            HasChildFilter filter = JsonConvert.DeserializeObject<HasChildFilter>(json);
            Assert.IsNotNull("child", filter.ChildType);
            Assert.IsTrue(filter.Query is TermQuery);
            TermQuery termQuery = filter.Query as TermQuery;
            Assert.AreEqual("field", termQuery.Field);
            Assert.AreEqual("value", termQuery.Value);
        }
    }
}
