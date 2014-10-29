using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Filters.Not;
using Bolay.Elastic.QueryDSL.Filters.Term;
using Bolay.Elastic.QueryDSL.Queries.Term;
using Bolay.Elastic.QueryDSL.Filters;
using Bolay.Elastic.QueryDSL.Queries;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Filters.Not
{
    [TestClass]
    public class UnitTests_NotFilter
    {
        [TestMethod]
        public void PASS_CreateFilter_Filter()
        {
            NotFilter filter = new NotFilter(new TermFilter("field", "value"));
            Assert.IsNotNull(filter);
            Assert.AreEqual("field", (filter.Filter as TermFilter).Field);
            Assert.AreEqual("value", (filter.Filter as TermFilter).Value);
        }

        [TestMethod]
        public void PASS_CreateFilter_Query()
        {
            NotFilter filter = new NotFilter(new TermQuery("field", "value"));
            Assert.IsNotNull(filter);
            Assert.AreEqual("field", (filter.Query as TermQuery).Field);
            Assert.AreEqual("value", (filter.Query as TermQuery).Value);   
        }

        [TestMethod]
        public void FAIL_CreateFilter_Filter()
        {
            try
            {
                NotFilter filter = new NotFilter((IFilter)null);
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
                NotFilter filter = new NotFilter((IQuery)null);
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
            NotFilter filter = new NotFilter(new TermFilter("field", "value"));
            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);
            string expectedJson = "{\"not\":{\"filter\":{\"term\":{\"field\":\"value\"}}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_Filter()
        {
            string json = "{\"not\":{\"filter\":{\"term\":{\"field\":\"value\"}}}}";
            NotFilter filter = JsonConvert.DeserializeObject<NotFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("field", (filter.Filter as TermFilter).Field);
            Assert.AreEqual("value", (filter.Filter as TermFilter).Value);
        }

        [TestMethod]
        public void PASS_Serialize_Query()
        {
            NotFilter filter = new NotFilter(new TermQuery("field", "value"));
            string json = JsonConvert.SerializeObject(filter);
            Assert.IsNotNull(json);
            string expectedJson = "{\"not\":{\"term\":{\"field\":\"value\"}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_Query()
        {
            string json = "{\"not\":{\"term\":{\"field\":\"value\"}}}";
            NotFilter filter = JsonConvert.DeserializeObject<NotFilter>(json);
            Assert.IsNotNull(filter);
            Assert.AreEqual("field", (filter.Query as TermQuery).Field);
            Assert.AreEqual("value", (filter.Query as TermQuery).Value); 
        }
    }
}
