using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL
{
    public class BoostedField
    {
        private const string _BOOST_DELIMITER_DEFAULT = "^";

        private string _Delimeter;

        public string Field { get; set; }
        public Double? Boost { get; set; }      
        
        /// <summary>
        /// Create a boosted field. Use ToString to create the value that would go in a query.
        /// </summary>
        /// <param name="field">The name of the field to search in.</param>
        /// <param name="boost">The boost value to apply to matches on this field.</param>
        /// <param name="delimeter">The value to place between the field and boost values.</param>
        public BoostedField(string field, Double? boost = null, string delimiter = _BOOST_DELIMITER_DEFAULT)
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "BoostedField must have a field.");

            Field = field;
            Boost = boost;
            _Delimeter = delimiter;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(Field);
            if(Boost.HasValue)
            {
                builder.Append(_Delimeter);
                builder.Append(Boost);
            }           

            return builder.ToString();
        }
    }
}
