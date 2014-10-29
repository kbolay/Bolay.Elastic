using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.QueryDSL.Aggregations.Results;
using Newtonsoft.Json;

namespace Bolay.Elastic.QueryDSL.Tests.Aggregations
{
    [TestClass]
    public class UnitTests_AggregationResults
    {
        [TestMethod]
        public void PASS_Deserialize_Single()
        {
            string json = "{\"min-value\":{\"value\":5}}";
            AggregationsResult result = JsonConvert.DeserializeObject<AggregationsResult>(json);

            Assert.IsNotNull(result);
            Assert.AreEqual("min-value", result.Aggregations.First().Name);
            Assert.AreEqual((int)5, Convert.ToInt32((result.Aggregations.First() as SingleValueAggregation).Value));
        }

        [TestMethod]
        public void PASS_Deserialize_Stats()
        {
            string json = "{\"grades_stats\":{\"count\":6,\"min\":60,\"max\":98,\"avg\":78.5,\"sum\":471}}";
            AggregationsResult result = JsonConvert.DeserializeObject<AggregationsResult>(json);
            Assert.IsNotNull(result);
            Assert.AreEqual("grades_stats", result.Aggregations.First().Name);
            StatisticsAggregation stats = result.Aggregations.First() as StatisticsAggregation;
            Assert.AreEqual((Int64)6, stats.Count);
            Assert.AreEqual((double)60, stats.Minimum);
            Assert.AreEqual((double)98, stats.Maximum);
            Assert.AreEqual((double)78.5, stats.Average);
            Assert.AreEqual((double)471, stats.Sum);
        }

        [TestMethod]
        public void PASS_Deserialize_ExtendedStats()
        {
            string json = "{\"grades_stats\":{\"count\":6,\"min\":60,\"max\":98,\"avg\":78.5,\"sum\":471,\"sum_of_squares\":54551.51999999999,\"variance\":218.2799999999976,\"std_deviation\":14.774302013969987}}";
            AggregationsResult result = JsonConvert.DeserializeObject<AggregationsResult>(json);
            Assert.IsNotNull(result);
            Assert.AreEqual("grades_stats", result.Aggregations.First().Name);
            ExtendedStatisticsAggregation stats = result.Aggregations.First() as ExtendedStatisticsAggregation;
            Assert.AreEqual((Int64)6, stats.Count);
            Assert.AreEqual((double)60, stats.Minimum);
            Assert.AreEqual((double)98, stats.Maximum);
            Assert.AreEqual((double)78.5, stats.Average);
            Assert.AreEqual((double)471, stats.Sum);
            Assert.AreEqual((double)54551.52, stats.SumOfSquares);
            Assert.AreEqual((double)218.279999999998, stats.Variance);
            Assert.AreEqual((double)14.77430201397, stats.StandardDeviation);
        }

        [TestMethod]
        public void PASS_Deserialize_Bucket()
        {
            string json = "{\"all_products\":{\"doc_count\":100,\"avg_price\":{\"value\":56.3}}}";
            AggregationsResult aggResult = JsonConvert.DeserializeObject<AggregationsResult>(json);
            Assert.IsNotNull(aggResult);
            Assert.AreEqual("all_products", aggResult.Aggregations.First().Name);
            BucketAggregation bucketAgg = aggResult.Aggregations.First() as BucketAggregation;
            Assert.AreEqual(100, bucketAgg.DocumentCount);
            Assert.IsNotNull(bucketAgg.AggregationResults);
            Assert.IsNotNull("avg_price", bucketAgg.AggregationResults.First().Name);
            SingleValueAggregation singleAgg = bucketAgg.AggregationResults.First() as SingleValueAggregation;
            Assert.AreEqual((double)56.3, Convert.ToDouble(singleAgg.Value));
        }

        [TestMethod]
        public void PASS_Deserialize_MultiBucket()
        {
            string json = "{\"genders\":{\"buckets\":[{\"key\":\"male\",\"doc_count\":10},{\"key\":\"female\",\"doc_count\":7}]}}";
            AggregationsResult aggResult = JsonConvert.DeserializeObject<AggregationsResult>(json);
            Assert.IsNotNull(aggResult);
            Assert.AreEqual("genders", aggResult.Aggregations.First().Name);
            MultiBucketAggregation multiBucket = aggResult.Aggregations.First() as MultiBucketAggregation;
            Assert.AreEqual("male", multiBucket.Buckets.First().Key);
            Assert.AreEqual(10, multiBucket.Buckets.First().DocumentCount);
            Assert.AreEqual("female", multiBucket.Buckets.Last().Key);
            Assert.AreEqual(7, multiBucket.Buckets.Last().DocumentCount);
        }

        [TestMethod]
        public void PASS_Deserialize_MultiBucket_SubAgg()
        {
            string json = "{\"genders\":{\"buckets\":[{\"key\":\"male\",\"doc_count\":10},{\"key\":\"female\",\"doc_count\":7,\"avg_age\":{\"value\":25}}]}}";
            AggregationsResult aggResult = JsonConvert.DeserializeObject<AggregationsResult>(json);
            Assert.IsNotNull(aggResult);
            Assert.AreEqual("genders", aggResult.Aggregations.First().Name);
            MultiBucketAggregation multiBucket = aggResult.Aggregations.First() as MultiBucketAggregation;
            Assert.AreEqual("male", multiBucket.Buckets.First().Key);
            Assert.AreEqual(10, multiBucket.Buckets.First().DocumentCount);
            Assert.AreEqual("female", multiBucket.Buckets.Last().Key);
            Assert.AreEqual(7, multiBucket.Buckets.Last().DocumentCount);
            Assert.AreEqual("avg_age", multiBucket.Buckets.Last().AggregationResults.First().Name);
            Assert.AreEqual((int)25, Convert.ToInt32((multiBucket.Buckets.Last().AggregationResults.First() as SingleValueAggregation).Value));
        }
    }
}
