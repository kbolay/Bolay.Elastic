using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Sorting.Field;
using Bolay.Elastic.QueryDSL.Filters.Term;
using Bolay.Elastic.QueryDSL.Filters;
using Newtonsoft.Json;
using Bolay.Elastic.QueryDSL.Sorting;

namespace Bolay.Elastic.QueryDSL.Tests.Sorting.Field
{
    [TestClass]
    public class UnitTests_FieldSort
    {
        [TestMethod]
        public void FAIL_CreateSort_Field()
        {
            try
            {
                FieldSort sort = new FieldSort(null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("field", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateSort_NestedFilter()
        {
            try
            {
                FieldSort sort = new FieldSort("field", (IFilter)null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("nestedFilter", ex.ParamName);
            }
        }

        [TestMethod]
        public void FAIL_CreateSort_NestedPath()
        {
            try
            {
                FieldSort sort = new FieldSort("field", (string)null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("nestedPath", ex.ParamName);
            }
        }

        [TestMethod]
        public void PASS_Serialize()
        {
            FieldSort sort = new FieldSort("field");
            string json = JsonConvert.SerializeObject(sort);
            Assert.IsNotNull(json);
            string expectedJson = "\"field\"";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize()
        {
            string json = "\"field\"";
            FieldSort sort = JsonConvert.DeserializeObject<FieldSort>(json);
            Assert.IsNotNull(sort);
            Assert.AreEqual("field", sort.Field);
        }

        [TestMethod]
        public void PASS_Serialize_Order()
        {
            FieldSort sort = new FieldSort("field") { SortOrder = SortOrderEnum.Descending };
            string json = JsonConvert.SerializeObject(sort);
            Assert.IsNotNull(json);
            string expectedJson = "{\"field\":\"desc\"}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_Order()
        {
            string json = "{\"field\":\"desc\"}";
            FieldSort sort = JsonConvert.DeserializeObject<FieldSort>(json);
            Assert.IsNotNull(sort);
            Assert.AreEqual("field", sort.Field);
            Assert.AreEqual("desc", sort.SortOrder.ToString());
        }

        [TestMethod]
        public void PASS_Serialize_NestedFilter()
        {
            FieldSort sort = new FieldSort("field", new TermFilter("field", "value")){ SortMode = SortModeEnum.Maximum };
            string json = JsonConvert.SerializeObject(sort);
            Assert.IsNotNull(json);
            string expectedJson = "{\"field\":{\"mode\":\"max\",\"nested_filter\":{\"term\":{\"field\":\"value\"}}}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_NestedFilter()
        {
            string json = "{\"field\":{\"mode\":\"max\",\"nested_filter\":{\"term\":{\"field\":\"value\"}}}}";
            FieldSort sort = JsonConvert.DeserializeObject<FieldSort>(json);
            Assert.IsNotNull(sort);
            Assert.AreEqual("field", sort.Field);
            Assert.AreEqual(SortModeEnum.Maximum, sort.SortMode);
            Assert.AreEqual("field", (sort.NestedFilter as TermFilter).Field);
            Assert.AreEqual("value", (sort.NestedFilter as TermFilter).Value);
        }

        [TestMethod]
        public void PASS_Serialize_NestedPath()
        {
            FieldSort sort = new FieldSort("field", "object") { IgnoreUnmappedField = true, Reverse = true };
            string json = JsonConvert.SerializeObject(sort);
            Assert.IsNotNull(json);
            string expectedJson = "{\"field\":{\"ignore_unmapped\":true,\"reverse\":true,\"nested_path\":\"object\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void PASS_Deserialize_NestedPath()
        {
            string json = "{\"field\":{\"ignore_unmapped\":true,\"reverse\":true,\"nested_path\":\"object\"}}";
            FieldSort sort = JsonConvert.DeserializeObject<FieldSort>(json);
            Assert.IsNotNull(sort);
            Assert.AreEqual("field", sort.Field);
            Assert.AreEqual(true, sort.Reverse);
            Assert.AreEqual(true, sort.IgnoreUnmappedField);
            Assert.AreEqual("object", sort.NestedPath);
        }
    }
}
