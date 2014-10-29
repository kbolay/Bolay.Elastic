using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.MinimumShouldMatch
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-minimum-should-match.html
    /// </summary>
    public abstract class MinimumShouldMatchBase
    {
        public abstract object GetValue();

        /// <summary>
        /// Parse the minimum_should_match value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static MinimumShouldMatchBase BuildMinimumShouldMatch(string value)
        {
            MinimumShouldMatchBase minimumShouldMatch = null;
            value = value.Trim();
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException("MinimumShouldMatch value must be populated.");
            else if (value.Contains(CombinationMatch._DELIMITER))
            {
                if (value.Count(x => x.ToString().Equals(CombinationMatch._DELIMITER, StringComparison.OrdinalIgnoreCase)) > 1)
                {
                    string[] combinationMatches = value.Split(
                        new string[] { MultipleCombinationMatch._DELIMITER },
                        StringSplitOptions.RemoveEmptyEntries);

                    List<CombinationMatch> matches = new List<CombinationMatch>();
                    foreach (string combinationMatch in combinationMatches)
                    {
                        matches.Add(BuildCombinationMatch(combinationMatch));
                    }

                    minimumShouldMatch = new MultipleCombinationMatch(matches);
                }
                else
                {
                    minimumShouldMatch = BuildCombinationMatch(value);
                }
            }
            else
            {
                minimumShouldMatch = BuildSingleValueMatch(value);
            }

            return minimumShouldMatch;
        }

        private static CombinationMatch BuildCombinationMatch(string value)
        {
            string[] values = value.Split(new string[] { CombinationMatch._DELIMITER }, StringSplitOptions.RemoveEmptyEntries);
            if (values.Length != 2)
                throw new ArgumentException("CombinationMatch", "CombinationMatch value malformed: " + value);

            CombinationMatch match = null;
            int minimumShouldMatch = 0;
            if (!Int32.TryParse(values[0], out minimumShouldMatch))
                throw new ArgumentException("minimumShouldMatch", "Integer value is required.");
            else if (minimumShouldMatch <= 0)
                throw new ArgumentException("minimumShouldMatch", "Integer value is must be positive.");

            SingleValueMatchBase singleMatch = BuildSingleValueMatch(values[1]);

            if (singleMatch is IntegerMatch)
                match = new CombinationMatch(minimumShouldMatch, singleMatch as IntegerMatch);
            else if (singleMatch is PercentageMatch)
                match = new CombinationMatch(minimumShouldMatch, singleMatch as PercentageMatch);
            else
                throw new Exception("SingleValueMatch is not a PercentageMatch or an IntegerMatch.");

            return match;
        }

        private static SingleValueMatchBase BuildSingleValueMatch(string value)
        {
            SingleValueMatchBase match = null;
            if (value.Contains(PercentageMatch._PERCENTAGE))
            {
                Double percent = 0;
                string percentStr = value.Trim().TrimEnd(new char[] { '%' });
                if (Double.TryParse(percentStr, out percent))
                    match = new PercentageMatch(percent / (double)100);
                else
                    throw new ArgumentException("percentMatch", "Invalid percentage value: " + percentStr + ", from: " + value + ".");
            }
            else
            {
                int intValue = 0;
                string intStr = value.Trim();
                if (Int32.TryParse(intStr, out intValue))
                    match = new IntegerMatch(intValue);
                else
                    throw new ArgumentException("IntegerMatch", "Invalid integervalue value: " + value + ".");
            }

            return match;
        }
    }
}
